/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.Presentations;

public partial interface IPresentationEntityFactory
	: IEntityFactory<CM.PresentationDto, DM.Presentation, PresentationEntity>
{
	PresentationEntity Create(string presentationKey);
}