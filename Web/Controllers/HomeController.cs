using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Service;
using Service.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ITransactionService _transactionService;

		public HomeController()
		{
		}

		public HomeController(ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		public ActionResult Index ()
		{
//			var mvcName = typeof(Controller).Assembly.GetName ();
//			var isMono = Type.GetType ("Mono.Runtime") != null;
//
//			ViewData ["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
//			ViewData ["Runtime"] = isMono ? "Mono" : ".NET";

			var	year = DateTime.Now.Year;
			var	month = DateTime.Now.Month;
			var viewModel = new DashboardViewModel ();

			var transactions = _transactionService.GetTransactionByMonthAndYear(month, year);

			// monthly total
			viewModel.TotalMonthlyIncome = _transactionService.GetTotalAmount (isIncome: true, transactions: transactions);
			viewModel.TotalMonthlyExpense = _transactionService.GetTotalAmount (isIncome: false, transactions: transactions);

			var yearlyTransactions = _transactionService.GetTransactionByYear (year);

			// yearly total
			viewModel.TotalYearlyIncome = _transactionService.GetTotalAmount(true, yearlyTransactions);
			viewModel.TotalYearlyExpenses = _transactionService.GetTotalAmount(false, yearlyTransactions);

			return View(viewModel);
		}
	}
} // namespace