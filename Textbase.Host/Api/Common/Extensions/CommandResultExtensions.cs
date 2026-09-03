/// This file was created by a generator. Do not modify the content, as any changes will be lost if the file is regenerated.

using Microsoft.AspNetCore.Mvc;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Common.Extensions;

public static class CommandResultExtensions
{
	public static ActionResult<TResponse> ToActionResult<TModel, TResponse>(
		this CreateResult<TModel> result,
		Func<TModel, ActionResult<TResponse>> success)
		where TModel : class
	{
		ArgumentNullException.ThrowIfNull(result);
		ArgumentNullException.ThrowIfNull(success);

		if (!result.Succeeded)
			return result.ToFailureActionResult<TResponse>();

		return success(result.Model);
	}

	public static IActionResult ToActionResult(
		this UpdateResult result,
		Func<UpdateResult, IActionResult> Success)
	{
		ArgumentNullException.ThrowIfNull(result);
		ArgumentNullException.ThrowIfNull(Success);

		return result.Succeeded
			? Success(result)
			: result.ToFailureActionResult();
	}

	public static IActionResult ToActionResult(
		this DeleteResult result,
		Func<DeleteResult, IActionResult> Success)
	{
		ArgumentNullException.ThrowIfNull(result);
		ArgumentNullException.ThrowIfNull(Success);

		return result.Succeeded
			? Success(result)
			: result.ToFailureActionResult();
	}

	private static ActionResult<TResponse> ToFailureActionResult<TResponse>(
		this CommandResult result)
	{
		return result.ToFailureActionResult();
	}

	private static ActionResult ToFailureActionResult(
		this CommandResult result)
	{
		if (result.Succeeded)
			throw new InvalidOperationException("A successful command result cannot be converted to a failure response.");

		return result.CancelReason switch
		{
			CancelReason.ObjectNotFound => new NotFoundResult(),

			CancelReason.BeforeActionHook => new ConflictResult(),
			CancelReason.BeforeSaveChangesHook => new ConflictResult(),

			_ => throw new NotSupportedException($"CancelReason '{result.CancelReason}' is not supported.")
		};
	}
}