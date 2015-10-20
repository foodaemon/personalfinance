using System;

namespace Core.Domains
{
	public class Budget : BaseEntity
	{
		/// <summary>
		/// Budget for Month and Year
		/// </summary>
		public DateTime Month { get; set; }

		/// <summary>
		/// Budget Amount
		/// </summary>
		public decimal Amount { get; set; }

		/// <summary>
		/// Category
		/// </summary>
		public int Category_Id { get; set; }
	} // class
} // namespace