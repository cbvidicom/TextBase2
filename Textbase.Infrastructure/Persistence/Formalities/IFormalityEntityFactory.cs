/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.Formalities;

public partial interface IFormalityEntityFactory
	: IEntityFactory<CM.FormalityDto, DM.Formality, FormalityEntity>
{
	FormalityEntity Create(string formalityKey);
}