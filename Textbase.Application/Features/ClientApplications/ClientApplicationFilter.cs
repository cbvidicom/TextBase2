/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Uwn.Common.Querying;
using Uwn.EntityFrameworkCore.Querying;

namespace Textbase.Application.Features.ClientApplications;

public sealed partial class ClientApplicationFilter
	: QueryFilterBase
{
	public GuidFilter? ClientApplicationGuid { get; set; }
	public StringFilter? Name { get; set; }
	public StringFilter? Description { get; set; }
	public StringFilter? DefaultLanguageTag { get; set; }
	public StringFilter? DefaultFormat { get; set; }
	public StringFilter? DefaultFileName { get; set; }
	
	public override bool IsEmpty =>
		!GuidFilter.IsSet(ClientApplicationGuid) &&
		!StringFilter.IsSet(Name) &&
		!StringFilter.IsSet(Description) &&
		!StringFilter.IsSet(DefaultLanguageTag) &&
		!StringFilter.IsSet(DefaultFormat) &&
		!StringFilter.IsSet(DefaultFileName);

	public static ClientApplicationFilter All()
		=> All<ClientApplicationFilter>();
}