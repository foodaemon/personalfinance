using System;
using System.Collections.Generic;

namespace Web.ViewModels
{
	public class DashboardViewModel
	{
		public double TotalMonthlyIncome { get; set; }
		public double TotalMonthlyExpense { get; set; }
		public double TotalYearlyIncome { get; set; }
		public double TotalYearlyExpenses { get; set; }

		public Dictionary<string, double> ExpensesByCategory { get; set; }
		public DashboardViewModel ()
		{
		}
	}
}