using System.Linq;
using Core;

namespace Data
{
	/// <summary>
	/// Repository
	/// </summary>
	public partial interface IRepository<T> where T : BaseEntity
	{
		/// <summary>
		/// Gets the by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		T GetById(object id);

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Insert(T entity);

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Update(T entity);

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Delete(T entity);

		/// <summary>
		/// Gets the table.
		/// </summary>
		/// <value>
		/// The table.
		/// </value>
		IQueryable<T> Table { get; }

	} // interface
} // namespace