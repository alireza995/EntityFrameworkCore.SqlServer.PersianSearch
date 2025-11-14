using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.SqlServer.PersianSearch;

public static class QueryableExtensions
{
    public static IQueryable<T> ContainsPersian<T>(
        this IQueryable<T> query,
        Expression<Func<T, string>> selector,
        string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText)) return query;

        var searchParts = searchText
            .Split(' ', '‌')
            .Select(i => i.ExpandPersianCharsForSearch());

        Expression body = Expression.Constant(false);

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
    }
}