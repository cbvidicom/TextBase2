using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Textbase.Infrastructure.Persistence;
using Textbase.Infrastructure.Persistence.Locales;
using Uwn.Common.IO;

namespace Textbase.Cli;

internal sealed class LocaleSynchronizer(
	IDbContextFactory<TextbaseDbContext> dbContextFactory)
{
	public async Task<LocaleSynchronizationResult> SynchronizeAsync(CancellationToken cancellationToken = default)
	{
		CultureInfo[] cultures = GetCultures();

		await using TextbaseDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

		Dictionary<string, LocaleEntity> existingLocales = await dbContext.Locales
			.ToDictionaryAsync(e => e.LocaleKey, StringComparer.OrdinalIgnoreCase, cancellationToken);

		int added = 0;
		int updated = 0;
		int unchanged = 0;

		for (int index = 0; index < cultures.Length; index++)
		{
			CultureInfo culture = cultures[index];
			CultureData data = CreateCultureData(culture);

			if (!existingLocales.TryGetValue(data.LocaleKey, out LocaleEntity? locale))
			{
				locale = new LocaleEntity
				{
					LocaleKey = data.LocaleKey,
					ParentLocaleKey = data.ParentLocaleKey,
					LanguageIso2 = data.LanguageIso2,
					LanguageIso3 = data.LanguageIso3,
					LanguageIsoN = null,
					LanguageLCID = data.LanguageLCID,
					LanguageWinApi = data.LanguageWinApi,
					CountryIso2 = data.CountryIso2,
					CountryIso3 = data.CountryIso3,
					NativeName = data.NativeName,
					EnglishName = data.EnglishName
				};

				dbContext.Locales.Add(locale);
				existingLocales.Add(locale.LocaleKey, locale);
				added++;
			}
			else if (UpdateMissingData(locale, data))
			{
				updated++;
			}
			else
			{
				unchanged++;
			}

			Terminal.Progress(index + 1, cultures.Length, "Culture");
		}

		Terminal.Empty();

		await dbContext.SaveChangesAsync(cancellationToken);

		return new LocaleSynchronizationResult(added, updated, unchanged);
	}

	private static CultureInfo[] GetCultures()
	{
		Dictionary<string, CultureInfo> cultures = new(StringComparer.OrdinalIgnoreCase);

		foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
		{
			AddCultureAndParents(cultures, culture);
		}

		return [.. cultures.Values.OrderBy(GetHierarchyDepth).ThenBy(c => c.Name, StringComparer.OrdinalIgnoreCase)];
	}

	private static void AddCultureAndParents(Dictionary<string, CultureInfo> cultures, CultureInfo culture)
	{
		CultureInfo current = culture;

		while (!String.IsNullOrWhiteSpace(current.Name))
		{
			cultures.TryAdd(current.Name, current);

			if (current.Parent is null || String.IsNullOrWhiteSpace(current.Parent.Name) || String.Equals(current.Parent.Name, current.Name, StringComparison.OrdinalIgnoreCase))
				break;

			current = current.Parent;
		}
	}

	private static int GetHierarchyDepth(CultureInfo culture)
	{
		int depth = 0;
		CultureInfo current = culture;

		while (current.Parent is not null && !String.IsNullOrWhiteSpace(current.Parent.Name) && !String.Equals(current.Parent.Name, current.Name, StringComparison.OrdinalIgnoreCase))
		{
			depth++;
			current = current.Parent;
		}

		return depth;
	}

	private static CultureData CreateCultureData(CultureInfo culture)
	{
		string? countryIso2 = null;
		string? countryIso3 = null;

		if (!culture.IsNeutralCulture)
		{
			try
			{
				RegionInfo region = new(culture.Name);
				countryIso2 = GetString(region.TwoLetterISORegionName, 2);
				countryIso3 = GetString(region.ThreeLetterISORegionName, 3);
			}
			catch (ArgumentException)
			{
				// Some platform-specific cultures do not expose RegionInfo data.
			}
		}

		return new CultureData(
			culture.Name,
			String.IsNullOrWhiteSpace(culture.Parent?.Name) ? null : culture.Parent.Name,
			GetString(culture.TwoLetterISOLanguageName, 2),
			GetString(culture.ThreeLetterISOLanguageName, 3),
			culture.LCID,
			GetString(culture.ThreeLetterWindowsLanguageName, 3),
			countryIso2,
			countryIso3,
			culture.NativeName,
			culture.EnglishName);
	}

	private static bool UpdateMissingData(LocaleEntity locale, CultureData data)
	{
		bool changed = false;

		if (IsMissing(locale.ParentLocaleKey, data.ParentLocaleKey))
		{
			locale.ParentLocaleKey = data.ParentLocaleKey;
			changed = true;
		}

		if (IsMissing(locale.LanguageIso2, data.LanguageIso2))
		{
			locale.LanguageIso2 = data.LanguageIso2;
			changed = true;
		}

		if (IsMissing(locale.LanguageIso3, data.LanguageIso3))
		{
			locale.LanguageIso3 = data.LanguageIso3;
			changed = true;
		}

		if (locale.LanguageLCID is null)
		{
			locale.LanguageLCID = data.LanguageLCID;
			changed = true;
		}

		if (IsMissing(locale.LanguageWinApi, data.LanguageWinApi))
		{
			locale.LanguageWinApi = data.LanguageWinApi;
			changed = true;
		}

		if (IsMissing(locale.CountryIso2, data.CountryIso2))
		{
			locale.CountryIso2 = data.CountryIso2;
			changed = true;
		}

		if (IsMissing(locale.CountryIso3, data.CountryIso3))
		{
			locale.CountryIso3 = data.CountryIso3;
			changed = true;
		}

		if (IsMissing(locale.NativeName, data.NativeName))
		{
			locale.NativeName = data.NativeName;
			changed = true;
		}

		if (IsMissing(locale.EnglishName, data.EnglishName))
		{
			locale.EnglishName = data.EnglishName;
			changed = true;
		}

		return changed;
	}

	private static bool IsMissing(string? target, string? value)
		=> String.IsNullOrWhiteSpace(target) && !String.IsNullOrWhiteSpace(value);

	private static string? GetString(string? value, int requiredLength)
		=> value?.Length == requiredLength ? value : null;

	private sealed record CultureData(
		string LocaleKey,
		string? ParentLocaleKey,
		string? LanguageIso2,
		string? LanguageIso3,
		int LanguageLCID,
		string? LanguageWinApi,
		string? CountryIso2,
		string? CountryIso3,
		string NativeName,
		string EnglishName);
}
