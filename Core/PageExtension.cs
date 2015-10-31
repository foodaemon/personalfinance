using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
	/// <summary>
	/// Paging Extension
	/// </summary>
	public static class PageExtension
	{
		/// <summary>
		/// To the paged list.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query">The query.</param>
		/// <param name="page">The page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns></returns>
		public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> query, int page, int pageSize)
		{
			return new PagedList<T>(query, page - 1, pageSize);
		}

		/// <summary>
		/// Gets the page.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns></returns>
		public static IEnumerable<T> GetPage<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
		{
			return source.Skip(pageIndex * pageSize).Take(pageSize);
		}

		// You can create your own paging extension that delegates to your
		// persistence layer such as NHibernate or Entity Framework.
		// This is an example how an `IPagedList<T>` can be created from 
		// an `IRavenQueryable<T>`:        
		/*
        public static IPagedList<T> ToPagedList<T>(this IRavenQueryable<T> query, int page, int pageSize)
        {
            RavenQueryStatistics stats;
            var q2 = query.Statistics(out stats)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            var list = new PagedList<T>(
                            q2,
                            page - 1,
                            pageSize,
                            stats.TotalResults
                        );
            return list;
        }
        */
	} // class   
} // namespace