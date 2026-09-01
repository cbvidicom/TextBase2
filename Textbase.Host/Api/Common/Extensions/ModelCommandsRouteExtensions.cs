using System.Linq.Expressions;
using Microsoft.AspNetCore.Routing;
using Uwn.EntityFrameworkCore.Infrastructure;

namespace Textbase.Host.Api.Common.Extensions;

public static class ModelCommandsRouteExtensions
{
	public static RouteValueDictionary ExtractRouteValues<TDTO>(
		this IModelCommandsKeyInfo<TDTO> commands,
		TDTO dto)
		where TDTO : class
	{
		ArgumentNullException.ThrowIfNull(commands);
		ArgumentNullException.ThrowIfNull(dto);

		string[] routeParameterNames = ExtractRouteParameterNames(commands.KeySelector);
		object[] keyValues = commands.ExtractKeyValues(dto);

		if (routeParameterNames.Length != keyValues.Length)
			throw new InvalidOperationException($"Key selector returned {routeParameterNames.Length} route parameter names, but ExtractKeyValues returned {keyValues.Length} values.");

		RouteValueDictionary result = [];

		for (int i = 0; i < routeParameterNames.Length; i++)
			result[routeParameterNames[i]] = keyValues[i];

		return result;
	}

	//

	private static string[] ExtractRouteParameterNames<TDTO>(
		Expression<Func<TDTO, object>> keySelector)
	{
		ArgumentNullException.ThrowIfNull(keySelector);

		return [.. ExtractMemberNames(keySelector.Body).Select(static name => name.ToLowerInvariant())];
	}

	private static IEnumerable<string> ExtractMemberNames(
		Expression expression)
	{
		expression = RemoveConvert(expression);

		if (expression is MemberExpression memberExpression)
		{
			yield return memberExpression.Member.Name;
			yield break;
		}

		if (expression is NewExpression newExpression)
		{
			foreach (Expression argument in newExpression.Arguments)
			{
				Expression unwrappedArgument = RemoveConvert(argument);

				if (unwrappedArgument is not MemberExpression memberArgument)
					throw new NotSupportedException($"Unsupported key selector argument '{argument}'.");

				yield return memberArgument.Member.Name;
			}

			yield break;
		}

		throw new NotSupportedException($"Unsupported key selector expression '{expression}'.");
	}

	private static Expression RemoveConvert(
		Expression expression)
	{
		while (expression.NodeType is ExpressionType.Convert or ExpressionType.ConvertChecked)
			expression = ((UnaryExpression)expression).Operand;

		return expression;
	}
}