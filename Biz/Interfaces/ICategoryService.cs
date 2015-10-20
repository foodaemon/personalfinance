using System;
using System.Collections.Generic;
using Core.Domains;

namespace Service.Interfaces
{
	public interface ICategoryService
	{
		void DeleteCategory (int id);
		Category GetCategoryById(int id);
		IEnumerable<Category> GetAllCategories();
		void InsertCategory(Category category);
		void UpdateCategory(Category category);
	} // interface
} // namespace