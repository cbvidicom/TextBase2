/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using CM = Textbase.Contracts.Models;
using DM = Textbase.Domain.Models;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Infrastructure.Persistence.TextResources;

public partial interface ITextResourceEntityFactory
	: IEntityFactory<CM.TextResourceDto, DM.TextResource, TextResourceEntity>
{
	TextResourceEntity Create(string textKey);
}