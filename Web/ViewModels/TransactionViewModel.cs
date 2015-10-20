using System;
using Core.Domains;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.ViewModels
{
	public class TransactionViewModel
	{
		public Transaction Transaction { get; set; }

		public IEnumerable<SelectListItem> CategorySelectList { get; set; }

		public TransactionViewModel()
		{
			Transaction = new Transaction(){ Date = DateTime.Now };
			CategorySelectList = new List<SelectListItem>();
		}
	}

	public class TransactionListViewModel
	{
		public IEnumerable<Transaction> Transactions { get; set; }

		public double TotalIncome { get; set; }

		public double TotalExpenses { get; set; }
	}
}