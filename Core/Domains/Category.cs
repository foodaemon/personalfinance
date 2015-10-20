using System;
using System.Collections.Generic;

namespace Core.Domains
{
	/// <summary>
	/// Represents Category. e.g. Home, Shopping, Food & Dining
	/// </summary>
	public class Category : BaseEntity
	{
		public Category()
		{
			Transactions = new HashSet<Transaction> ();
		}

		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual DateTime Created_At { get; set; }
		public virtual DateTime Updated_At { get; set; }

		public virtual HashSet<Transaction> Transactions { get; set; }
	} // class
} // namespace