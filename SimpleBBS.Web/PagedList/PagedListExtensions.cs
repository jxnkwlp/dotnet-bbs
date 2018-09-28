using System.Collections.Generic;
using System.Linq;

namespace SimpleBBS
{
    public static class PagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> list, int pageNumber, int pageSize, int totalCount)
        {
            var result = new PagedList<T>();
            result.LoadSource(list, pageNumber, pageSize, totalCount);
            return result;
        }

        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> list, int pageNumber, int pageSize)
        {
            var result = new PagedList<T>();
            result.LoadSource(list, pageNumber, pageSize);
            return result;
        }

        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> list, int pageNumber, int pageSize)
        {
            var result = new PagedList<T>();
            result.LoadSource(list, pageNumber, pageSize);
            return result;
        }
    }
}
