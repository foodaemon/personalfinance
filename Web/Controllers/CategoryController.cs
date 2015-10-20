using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Interfaces;
using Service;
using Core.Domains;

namespace Web.Controllers
{
    public class CategoryController : Controller
    {
		private readonly ICategoryService _categoryService;

		public CategoryController()
		{
			_categoryService = new CategoryService();
		}

        public ActionResult Index()
        {
			var categories = _categoryService.GetAllCategories();

			if (Request.IsAjaxRequest ()) 
				return Json (categories, JsonRequestBehavior.AllowGet);

			return View ("Index", categories);
        }

        public ActionResult Details(int id)
        {
            return View ();
        }

        public ActionResult Create()
        {
			return View ("Create");
        } 

        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Category category)
        {
            try 
			{
				_categoryService.InsertCategory(category);
                return RedirectToAction ("Index");
            } 
			catch 
			{
				return View ("Index", category);
            }
        }
        
        public ActionResult Edit(int id)
        {
            return View ();
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, Category category)
        {
            try 
			{
				_categoryService.UpdateCategory(category);
				return RedirectToAction ("Index");
            } 
			catch 
			{
				return View ("Update", category);
            }
        }

        public ActionResult Delete(int id)
        {
			_categoryService.DeleteCategory (id: id);
			return RedirectToAction("Index");
        }
	} // class
} // namespace