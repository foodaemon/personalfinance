using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Domains;
using Service.Interfaces;
using Service;
using Web.ViewModels;
using Web.Infrastructure;

namespace Web.Controllers
{
    public class TransactionController : Controller
    {
		private readonly ICategoryService _categoryService;
		private readonly ITransactionService _transactionService;

		public TransactionController()
		{
			_categoryService = new CategoryService();
			_transactionService = new TransactionService();
		}

		[HttpGet]
		public ActionResult Create()
		{
			var categories = _categoryService.GetAllCategories().ToList();

			var categorySelectList = categories.Select(c => new SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString()
				});

			var viewModel = new TransactionViewModel ();
			viewModel.CategorySelectList = categorySelectList.ToList ();

			return View ("Create", viewModel);
		} 

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Transaction transaction)
		{
			_transactionService.InsertTransaction(transaction);
			return RedirectToAction ("Index", new { year = transaction.Date.Year, month = transaction.Date.Month });
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			_transactionService.DeleteTransaction (id);
			return RedirectToAction ("Index");
		}

		[HttpGet]
		public ActionResult Index(int year = 0, int month = 0)
        {
			if(year == 0)
				year = DateTime.Now.Year;
			if(month == 0)
				month = DateTime.Now.Month;
			var transactions = _transactionService.GetTransactionByMonthAndYear(month, year);

			var viewModel = new TransactionListViewModel ();
			viewModel.Transactions = transactions;
			viewModel.TotalIncome = _transactionService.GetTotalAmount (true, transactions);
			viewModel.TotalExpenses = _transactionService.GetTotalAmount (false, transactions);

			ViewBag.Date = month + "/" + year;
			return View ("Index", viewModel);
        }
			
		[HttpGet]
        public ActionResult Edit(int id)
        {
			var transaction = _transactionService.GetTransactionById (id: id);
			var categories = _categoryService.GetAllCategories().ToList();

			var categorySelectList = categories.Select(c => new SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString(),
					Selected = (c.Id == transaction.Category_Id)? true: false,
				});

			var viewModel = new TransactionViewModel () 
			{
				Transaction = transaction,
				CategorySelectList = categorySelectList.ToList()
			};
            return View ("Edit", viewModel);
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, Transaction transaction)
        {
			_transactionService.UpdateTransaction(transaction);
			return RedirectToAction("Index", new {year = transaction.Date.Year, month = transaction.Date.Month});
        }

		[HttpGet]
		public JsonNetResult ExpensesByCategory()
		{
			var month = DateTime.Now.Month;
			var year = DateTime.Now.Year;
			var transactions = _transactionService.GetTransactionByMonthAndYear (year: year, month: month);
			var expenses = _transactionService.GetTotalExpensesByCategory (transactions);

			var jsonResult = new JsonNetResult () { Data = expenses } ;
			return jsonResult;
		}

		[HttpGet]
		public JsonNetResult YearlyByMonth(bool isIncome = false)
		{
			var yearlyData = _transactionService.GetYearlyTransactionsByMonth (isIncome: isIncome);
			var jsonResult = new JsonNetResult (){ Data = yearlyData };
			return jsonResult;
		}
	} // class
} // namespace