using System.Collections.Generic;
using System.Linq;
using Core.Extensions;

namespace Core
{
	/// <summary>
	/// Paged list
	/// </summary>
	/// <typeparam name="T">T</typeparam>
	public class PagedList<T> : List<T>, IPagedList<T>
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="PagedList{T}"/> class.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="pageSize">Size of the page.</param>
		public PagedList(IEnumerable<T> source, int pageIndex, int pageSize) :
		this(source.GetPage(pageIndex, pageSize), pageIndex, pageSize, source.Count()) { }

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="source">source</param>
		/// <param name="pageIndex">Page index</param>
		/// <param name="pageSize">Page size</param>
		public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
		{
			int total = source.Count();
			this.TotalCount = total;
			this.TotalPages = total / pageSize;

			if (total % pageSize > 0)
				TotalPages++;

			this.PageSize = pageSize;
			this.PageIndex = pageIndex;
			this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="source">source</param>
		/// <param name="pageIndex">Page index</param>
		/// <param name="pageSize">Page size</param>
		public PagedList(IList<T> source, int pageIndex, int pageSize)
		{
			TotalCount = source.Count();
			TotalPages = TotalCount / pageSize;

			if (TotalCount % pageSize > 0)
				TotalPages++;

			this.PageSize = pageSize;
			this.PageIndex = pageIndex;
			this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="source">source</param>
		/// <param name="pageIndex">Page index</param>
		/// <param name="pageSize">Page size</param>
		/// <param name="totalCount">Total count</param>
		public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
		{
			TotalCount = totalCount;
			TotalPages = TotalCount / pageSize;

			if (TotalCount % pageSize > 0)
				TotalPages++;

			this.PageSize = pageSize;
			this.PageIndex = pageIndex;
			this.AddRange(source);
		}

		/// <summary>
		/// Gets the index of the page.
		/// </summary>
		public int PageIndex { get; private set; }

		/// <summary>
		/// Gets the size of the page.
		/// </summary>
		public int PageSize { get; private set; }

		/// <summary>
		/// Gets the total count.
		/// </summary>
		public int TotalCount { get; private set; }

		/// <summary>
		/// Gets the total pages.
		/// </summary>
		public int TotalPages { get; private set; }

		/// <summary>
		/// Gets a value indicating whether this instance has previous page.
		/// </summary>
		public bool HasPreviousPage
		{
			get { return (PageIndex > 0); }
		}

		/// <summary>
		/// Gets a value indicating whether this instance has next page.
		/// </summary>
		public bool HasNextPage
		{
			get { return (PageIndex + 1 < TotalPages); }
		}

	} // class
} // namespace