using System;
using System.Linq;
#if LINQ_SUPPORTED
using System.Linq.Expressions;
#endif
#if EFCORE_SUPPORTED
using Microsoft.EntityFrameworkCore;
#elif EF6_SUPPORTED
using System.Data.Entity;
#endif

namespace EntityFrameworkCore.SqlServer.Extra;

public static class QueryableExtensions
{
#if LINQ_SUPPORTED
    public static IQueryable<T> ContainsPersian<T>(
        this IQueryable<T> query,
        Expression<Func<T, string>> selector,
        string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText)) return query;

        var searchParts = BuildSearchParts(searchText);
        if (searchParts.Count == 0) return query;

#if EFCORE_SUPPORTED
        return ContainsPersian_EFCore(query, selector, searchParts);
#elif EF6_SUPPORTED
        return ContainsPersian_EF6(query, selector, searchParts);
#else
        return ContainsPersian_Default(query, selector, searchParts);
#endif
    }

    private static System.Collections.Generic.List<string> BuildSearchParts(string searchText)
    {
        return searchText
            // Split on spaces; this may include Arabic/Persian non-breaking space (U+200C, 200D)
            .Split(new[] { ' ', '‌' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(i => i.ExpandPersianCharsForSearch())
            .Select(i => EscapeLikePattern(i))
            .Where(i => !string.IsNullOrWhiteSpace(i))
            .Distinct()
            .ToList();
    }

#if EFCORE_SUPPORTED
    private static IQueryable<T> ContainsPersian_EFCore<T>(IQueryable<T> query, Expression<Func<T, string>> selector, System.Collections.Generic.List<string> searchParts)
    {
        Expression body = Expression.Constant(false, typeof(bool));
        var parameter = selector.Parameters[0];
        Expression member = selector.Body;
        if (member is UnaryExpression ue && ue.NodeType == ExpressionType.Convert) member = ue.Operand;

        foreach (var searchPart in searchParts)
        {
            var patternExpr = Expression.Constant($"%{searchPart}%");
            var likeCall = Expression.Call(
                typeof(DbFunctionsExtensions),
                nameof(DbFunctionsExtensions.Like),
                Type.EmptyTypes,
                Expression.Constant(EF.Functions),
                member,
                patternExpr
            );

            var notNull = Expression.NotEqual(member, Expression.Constant(null, typeof(string)));
            var clause = Expression.AndAlso(notNull, likeCall);

            body = Expression.OrElse(body, clause);
        }

        return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
    }
#endif

#if EF6_SUPPORTED
    private static IQueryable<T> ContainsPersian_EF6<T>(IQueryable<T> query, Expression<Func<T, string>> selector, System.Collections.Generic.List<string> searchParts)
    {
        Expression body = Expression.Constant(false, typeof(bool));
        var parameter = selector.Parameters[0];
        Expression member = selector.Body;
        if (member is UnaryExpression ue && ue.NodeType == ExpressionType.Convert) member = ue.Operand;

        foreach (var searchPart in searchParts)
        {
            var patternExpr = Expression.Constant($"%{searchPart}%");
            var likeCall = Expression.Call(
                typeof(DbFunctions),
                nameof(DbFunctions.Like),
                Type.EmptyTypes,
                member,
                patternExpr
            );

            var notNull = Expression.NotEqual(member, Expression.Constant(null, typeof(string)));
            var clause = Expression.AndAlso(notNull, likeCall);
            body = Expression.OrElse(body, clause);
        }

        return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
    }
#endif

#if !EFCORE_SUPPORTED && !EF6_SUPPORTED
    private static IQueryable<T> ContainsPersian_Default<T>(IQueryable<T> query, Expression<Func<T, string>> selector, System.Collections.Generic.List<string> searchParts)
    {
        Expression body = Expression.Constant(false, typeof(bool));
        var parameter = selector.Parameters[0];
        Expression member = selector.Body;
        if (member is UnaryExpression ue && ue.NodeType == ExpressionType.Convert) member = ue.Operand;

        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

        foreach (var searchPart in searchParts)
        {
            var call = Expression.Call(member, containsMethod!, Expression.Constant(searchPart));
            var notNull = Expression.NotEqual(member, Expression.Constant(null, typeof(string)));
            var clause = Expression.AndAlso(notNull, call);
            body = Expression.OrElse(body, clause);
        }

        return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
    }
#endif

    // Escape SQL LIKE special characters from user input to treat them as literals in patterns.
    // We intentionally do not escape '[' and ']' because ExpandPersianCharsForSearch uses character classes.
    private static string EscapeLikePattern(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;

        // Escape backslash first
        input = input.Replace("\\", "\\\\");
        input = input.Replace("%", "[%]");
        input = input.Replace("_", "[_]");
        return input;
    }
#endif
}