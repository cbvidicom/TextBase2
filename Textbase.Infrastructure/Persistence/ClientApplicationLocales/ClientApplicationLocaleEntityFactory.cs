/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.ClientApplicationLocales;

public sealed partial class ClientApplicationLocaleEntityFactory
	: EntityFactory<CM.ClientApplicationLocaleDto, DM.ClientApplicationLocale, ClientApplicationLocaleEntity>
	, IClientApplicationLocaleEntityFactory
{
	public override ClientApplicationLocaleEntity Create() => Create(default!, default!, default!);

	public ClientApplicationLocaleEntity Create(Guid clientApplicationGuid) => Create(clientApplicationGuid, default!, default!);
	public ClientApplicationLocaleEntity Create(Guid clientApplicationGuid, string localeKey) => Create(clientApplicationGuid, localeKey, default!);
	public ClientApplicationLocaleEntity Create(Guid clientApplicationGuid, string localeKey, bool isDefault)
	=> new() { ClientApplicationGuid = clientApplicationGuid, LocaleKey = localeKey, IsDefault = isDefault };
}
