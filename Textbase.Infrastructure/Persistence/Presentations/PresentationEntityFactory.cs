/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.Presentations;

public sealed partial class PresentationEntityFactory
	: EntityFactory<CM.PresentationDto, DM.Presentation, PresentationEntity>
	, IPresentationEntityFactory
{
	public override PresentationEntity Create() => Create(default!);

	public PresentationEntity Create(string presentationKey)
	=> new() { PresentationKey = presentationKey };
}
