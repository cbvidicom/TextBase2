using Textbase.Application.Features.ClientApplications;
using Textbase.Contracts.Models;
using Textbase.Domain.Models;
using Textbase.Infrastructure.Persistence.ClientApplications;
using Uwn.Blazor.Models.ViewModels.Abstractions.Querying;

namespace Textbase.Host.ViewModels.ClientApplications;

public class ClientApplicationEditorViewModel(
	IClientApplicationQueries clientApplicationQueries,
	IClientApplicationCommands clientApplicationCommands,
	IClientApplicationEntityFactory _clientApplicationEntityFactory)
	: GuidEditorViewModel<ClientApplicationDto, ClientApplication>(
		clientApplicationQueries,
		clientApplicationCommands,
		clientApplicationCommands)
{
	protected override Task<ClientApplication> CreateNewItemAsync(
		CancellationToken cancellationToken = default)
	{
		ClientApplication clientApplication = _clientApplicationEntityFactory.Create(Guid.CreateVersion7());
		clientApplication.IsActive = true;

		return Task.FromResult(clientApplication);
	}
}
