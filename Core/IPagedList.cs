using System.Collections;
using System.Collections.Generic;

namespace Core
{
	/// <summary>
	/// Paged list interface
	/// </summary>
	public interface IPagedList : IEnumerable
	{
		/// <summary>
		/// Gets the index of the page.
		/// </summary>
		int PageIndex { get; }

		/// <summary>
		/// Gets the size of the page.
		/// </summary>
		int PageSize { get; }

		/// <summary>
		/// Gets the total count.
		/// </summary>
		int TotalCount { get; }

		/// <summary>
		/// Gets the total pages.
		/// </summary>
		int TotalPages { get; }

		/// <summary>
		/// Gets a value indicating whether this instance has previous page.
		/// </summary>
		bool HasPreviousPage { get; }

		/// <summary>
		/// Gets a value indicating whether this instance has next page.
		/// </summary>
		bool HasNextPage { get; }
	} // interface

	/// <summary>
	/// Interface for IPagedList<typeparam name="T"></typeparam>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IPagedList<T> : IPagedList, IList<T>
	{
	}

} // namespace