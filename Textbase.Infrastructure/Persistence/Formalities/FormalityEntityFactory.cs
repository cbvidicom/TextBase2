/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.Formalities;

public sealed partial class FormalityEntityFactory
	: EntityFactory<CM.FormalityDto, DM.Formality, FormalityEntity>
	, IFormalityEntityFactory
{
	public override FormalityEntity Create() => Create(default!);

	public FormalityEntity Create(string formalityKey)
	=> new() { FormalityKey = formalityKey };
}
