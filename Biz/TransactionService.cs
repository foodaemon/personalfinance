using System;
using System.Collections.Generic;
using System.Linq;

using Data;
using Service.Interfaces;
using Core.Domains;
using Core.Helpers;
using Core;

namespace Service
{
	public class TransactionService : ITransactionService
	{
		private readonly IRepository<Transaction> _transactionRepo;

		public TransactionService ()
		{
			_transactionRepo = new Repository<Transaction>();
		}

		#region implementation

		public void DeleteTransaction(int id)
		{
			var transaction = GetTransactionById (id);
			if (transaction != null)
				_transactionRepo.Delete (transaction);
		}

		public Transaction GetTransactionById(int id)
		{
			var transaction = _transactionRepo.Table.SingleOrDefault (t => t.Id == id);
			return transaction;
		}

		public IEnumerable<Transaction> GetTransactionByMonthAndYear(int month, int year)
		{
			var startDate = DateTimeHelper.StartOfMonth (year: year, month: month);
			var endDate = DateTimeHelper.EndOfMonth (year: year, month: month);
			var transactions = _transactionRepo.Table
				.Where (x => x.Date >= startDate && x.Date <= endDate)
				.OrderByDescending(x => x.Date);
			return transactions.ToList();
		}

		public IEnumerable<Transaction> GetTransactionByYear(int year)
		{
			var startDate = DateTimeHelper.StartOfMonth (year: year, month: 1);
			var endDate = DateTimeHelper.EndOfMonth (year: year, month: 12);
			var transactions = _transactionRepo.Table
				.Where (x => x.Date >= startDate && x.Date <= endDate)
				.OrderByDescending(x => x.Date);
			return transactions.ToList();
		}

		public void InsertTransaction(Transaction transaction)
		{
			transaction.Created_At = DateTime.Now;
			transaction.Updated_At = DateTime.Now;
			_transactionRepo.Insert(transaction);
		}

		public double GetTotalAmount(bool isIncome, IEnumerable<Transaction> transactions)
		{
			double totalAmount = 0.0;
			if(transactions.Any())
				totalAmount = transactions
					.Where (x => x.Is_Income == isIncome)
					.Select(x => x.Amount)
					.Sum();
			return totalAmount;
		}

		public Dictionary<string, double> GetTotalExpensesByCategory(IEnumerable<Transaction> transactions)
		{
			var expensesByCategory = transactions.Where(x => x.Is_Income == false)
				.GroupBy(x => new{ x.Category_Id, x.Category.Name })
				.Select(g => new { Category = g.Key.Name, Amount = g.Sum(t => t.Amount) })
				.ToDictionary(x => x.Category, x => x.Amount);

			return expensesByCategory;
		}

		public void UpdateTransaction(Transaction transaction)
		{
			var data = GetTransactionById (transaction.Id);
			if (data == null)
				return;

			transaction.Updated_At = DateTime.Now;
			transaction.Created_At = data.Created_At;

			_transactionRepo.Update(transaction);
		}

		public Dictionary<int, double> GetYearlyTransactionsByMonth(bool isIncome = false)
		{
			var monthData = new Dictionary<int, double> ();

			// initialize dictionary
			for (var i = 1; i <= 12; i++) 
				monthData.Add(i, 0.0);

			var transactions = GetTransactionByYear (DateTime.Now.Year);
			var data = transactions.Where(x => x.Is_Income == isIncome)
				.GroupBy (x => new { x.Date.Year, x.Date.Month })
				.ToDictionary (d => d.Key.Month, v => v.Sum (x => x.Amount));

			foreach (var kv in data) 
			{
				monthData [kv.Key] = kv.Value;
			}

			return monthData;
		}

		#endregion

	} // class
} // namespace