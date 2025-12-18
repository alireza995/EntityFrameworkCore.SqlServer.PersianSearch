using System;
using System.Linq;
#if LINQ_SUPPORTED
using System.Linq.Expressions;
#endif
#if EFCORE_SUPPORTED
using Microsoft.EntityFrameworkCore;
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

        var searchParts = searchText
            .Split(' ', '‌')
            .Select(i => i.ExpandPersianCharsForSearch());

#if EFCORE_SUPPORTED
        Expression body = Expression.Constant(false, typeof(bool));
        
        foreach (var searchPart in searchParts)
        {
            var containsCall = Expression.Call(
                typeof(DbFunctionsExtensions),
                nameof(DbFunctionsExtensions.Like),
                Type.EmptyTypes,
                Expression.Constant(EF.Functions),
                selector.Body,
                Expression.Constant($"%{searchPart}%")
            );
        
            body = Expression.OrElse(body, containsCall);
        }

        return query.Where(Expression.Lambda<Func<T, bool>>(body, selector.Parameters[0]));
#else
        // EF Core not available for this TFM. Fall back to expression-based Contains filtering so IQueryable is preserved.
        Expression body = Expression.Constant(false, typeof(bool));

        foreach (var searchPart in searchParts)
        {
            var call = Expression.Call(selector.Body, "Contains", null, Expression.Constant(searchPart));
            body = Expression.OrElse(body, call);
        }

        return query.Where(Expression.Lambda<Func<T, bool>>(body, selector.Parameters[0]));
#endif
    }
#endif
}