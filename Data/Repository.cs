using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Core;

namespace Data
{
	/// <summary>
	/// Entity Framework repository
	/// </summary>
	public partial class Repository<T> : IRepository<T> where T : BaseEntity
	{
		//private readonly IDbContext _context;
		private readonly AppContext _context;
		private IDbSet<T> _entities;

		// <summary>
		// Ctor
		// </summary>
		// <param name="context">Object context</param>
		//public Repository(IDbContext context)
		//{
		//    this._context = context;
		//}

		/// <summary>
		/// Initializes a new instance of the <see cref="Repository{T}"/> class.
		/// </summary>
		public Repository()
		{
			_context = new AppContext();
		}

		/// <summary>
		/// Gets entity by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public T GetById(object id)
		{
			return this.Entities.Find(id);
		}

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <exception cref="System.ArgumentNullException">entity</exception>
		public void Insert(T entity)
		{
			try
			{
				if (entity == null)
					throw new ArgumentNullException("entity");

				this.Entities.Add(entity);

				this._context.SaveChanges();
			}
			catch (DbEntityValidationException dbEx)
			{
				var msg = string.Empty;

				foreach (var validationErrors in dbEx.EntityValidationErrors)
					foreach (var validationError in validationErrors.ValidationErrors)
						msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

				var fail = new Exception(msg, dbEx);
				//Debug.WriteLine(fail.Message, fail);
				throw fail;
			}
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <exception cref="System.ArgumentNullException">entity</exception>
		public void Update(T entity)
		{
			try
			{
				if (entity == null)
					throw new ArgumentNullException("entity");
				if (_context.Entry(entity).State == EntityState.Detached)
				{
					var alreadyAttached = Entities.Local.Where(x => x.Id == entity.Id).FirstOrDefault();
					if (alreadyAttached != null)
					{
						_context.Entry(alreadyAttached).CurrentValues.SetValues(entity);
					}
					else
					{
						_context.Entry(entity).State = EntityState.Modified;
					}
				}

				this._context.SaveChanges();
			}
			catch (DbEntityValidationException dbEx)
			{
				var msg = string.Empty;

				foreach (var validationErrors in dbEx.EntityValidationErrors)
					foreach (var validationError in validationErrors.ValidationErrors)
						msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

				var fail = new Exception(msg, dbEx);
				//Debug.WriteLine(fail.Message, fail);
				throw fail;
			}
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <exception cref="System.ArgumentNullException">entity</exception>
		public void Delete(T entity)
		{
			try
			{
				if (entity == null)
					throw new ArgumentNullException("entity");

				this.Entities.Remove(entity);
				this._context.SaveChanges();
			}
			catch (DbEntityValidationException dbEx)
			{
				var msg = string.Empty;

				foreach (var validationErrors in dbEx.EntityValidationErrors)
					foreach (var validationError in validationErrors.ValidationErrors)
						msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

				var fail = new Exception(msg, dbEx);
				//Debug.WriteLine(fail.Message, fail);
				throw fail;
			}
		}

		/// <summary>
		/// Gets the table.
		/// </summary>
		/// <value>
		/// The table.
		/// </value>
		public virtual IQueryable<T> Table
		{
			get
			{
				return this.Entities;
			}
		}

		/// <summary>
		/// Gets the entities.
		/// </summary>
		/// <value>
		/// The entities.
		/// </value>
		private IDbSet<T> Entities
		{
			get
			{
				if (_entities == null)
					_entities = _context.Set<T>();
				return _entities;
			}
		}
	} // class
} // namespace