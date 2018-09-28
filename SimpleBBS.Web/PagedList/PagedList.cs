using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBBS
{
    public class PagedList<T> : List<T>, IList<T>, IPagedList<T>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPage { get; private set; }
        public int TotalCount { get; private set; }


        //public static IPagedList Create(IEnumerable<T> list, int pageNumber, int pageSize, int totalCount)
        //{
        //    var result = new PagedList<T>();
        //    result.LoadSource(list, pageNumber, pageSize, totalCount);
        //    return result;
        //}

        public void LoadSource(IEnumerable<T> list, int pageNumber, int pageSize, int totalCount)
        {
            pageNumber = pageNumber <= 1 ? 1 : pageNumber;

            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.TotalCount = totalCount;

            this.TotalPage = this.TotalCount % pageSize == 0 ? this.TotalCount / pageSize : this.TotalCount / pageSize + 1;

            this.AddRange(list);
        }

        public void LoadSource(IEnumerable<T> list, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 1 ? 1 : pageNumber;

            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.TotalCount = list.Count();

            this.TotalPage = this.TotalCount % pageSize == 0 ? this.TotalCount / pageSize : this.TotalCount / pageSize + 1;

            var result = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            this.AddRange(result);
        }

        public void LoadSource(IQueryable<T> list, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 1 ? 1 : pageNumber;

            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.TotalCount = list.Count();

            this.TotalPage = this.TotalCount % pageSize == 0 ? this.TotalCount / pageSize : this.TotalCount / pageSize + 1;

            var result = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            this.AddRange(result);
        }

        public IPagedList<T2> ReplaceTo<T2>(Func<T, T2> func)
        {
            var result = new PagedList<T2>
            {
                PageNumber = PageNumber,
                PageSize = PageSize,
                TotalCount = TotalCount,
                TotalPage = TotalPage,
            };

            foreach (var item in this)
            {
                result.Add(func(item));
            }

            return result;
        }
    }
}
