using EventManager.Util.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Util.ExtensionMethods
{
    public static class EntityFrameworkExtensionMethods
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, PagerSettings settings) where T : class
        {
            var result = new PagedList<T>();
            result.CurrentPage = settings.PageNumber;
            result.PageSize = settings.PageSize;


            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / settings.PageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (settings.PageNumber - 1) * settings.PageSize;
            result.Data = await query.Skip(skip).Take(settings.PageSize).ToListAsync();

            return result;
        }
    }
}
