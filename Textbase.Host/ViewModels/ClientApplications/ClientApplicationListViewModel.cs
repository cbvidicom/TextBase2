using Radzen;
using Textbase.Application.Features.ClientApplications;
using Textbase.Domain.Models;
using Uwn.Blazor.Enumerations.Radzen;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.ClientApplications;

public class ClientApplicationListViewModel(
	IClientApplicationQueries clientApplicationQueries,
	IClientApplicationServerQueries _clientApplicationServerQueries)
	: DataGridViewModel<ClientApplication, ClientApplicationFilter>(
		clientApplicationQueries)
{
	private IReadOnlyDictionary<Guid, ClientApplicationReferenceCounts>? _referenceCounts;


	protected override async Task AfterLoadDataAsync(
		LoadDataArgs args)
	{
		_referenceCounts = Data is null
			? null
			: await _clientApplicationServerQueries.GetReferenceCountsAsync([.. Data.Select(clientApplication => clientApplication.ClientApplicationGuid)]);
	}

	public override DataGridRowStyle GetItemStyle(
		ClientApplication item)
		=> item.IsActive ? DataGridRowStyle.Base : DataGridRowStyle.Danger;

	public int GetLocaleCount(
		Guid clientApplicationGuid)
	{
		if (_referenceCounts is null)
			return 0;

		return _referenceCounts.TryGetValue(clientApplicationGuid, out ClientApplicationReferenceCounts? value)
			? value.LocaleCount
			: 0;
	}

	public int GetTextResourceCount(
		Guid clientApplicationGuid)
	{
		if (_referenceCounts is null)
			return 0;

		return _referenceCounts.TryGetValue(clientApplicationGuid, out ClientApplicationReferenceCounts? value)
			? value.TextResourceCount
			: 0;
	}
}
