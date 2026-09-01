/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.ClientApplicationLocales;

public sealed partial class ClientApplicationLocaleFilter
	: QueryFilterBase
{
	public GuidFilter? ClientApplicationGuid { get; set; }
	public StringFilter? LocaleKey { get; set; }
	
	public override bool IsEmpty =>
		!GuidFilter.IsSet(ClientApplicationGuid) &&
		!StringFilter.IsSet(LocaleKey);

	public static ClientApplicationLocaleFilter All()
		=> All<ClientApplicationLocaleFilter>();
}