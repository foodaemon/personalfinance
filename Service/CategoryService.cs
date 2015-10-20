using System;
using System.Collections.Generic;
using System.Linq;

using Data;
using Service.Interfaces;
using Core.Domains;

namespace Service
{
	public class CategoryService : ICategoryService
	{
		private readonly IRepository<Category> _categoryRepo;

		public CategoryService ()
		{
			_categoryRepo = new Repository<Category>();
		}

		#region implementation

		public void DeleteCategory(int id)
		{
			var category = GetCategoryById (id: id);
			if (category != null)
				_categoryRepo.Delete (category);
		}

		public Category GetCategoryById (int id)
		{
			var category = _categoryRepo.Table.SingleOrDefault(c => c.Id == id);
			return category;
		}

		public IEnumerable<Category> GetAllCategories()
		{
			var categories = _categoryRepo.Table;
			return categories;
		}

		public void InsertCategory (Category category)
		{
			_categoryRepo.Insert (category);
		}

		public void UpdateCategory (Category category)
		{
			_categoryRepo.Update (category);
		}

		#endregion
	} // class
} // namespace