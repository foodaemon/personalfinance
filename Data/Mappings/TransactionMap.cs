using System;
using System.Data.Entity.ModelConfiguration;
using Core.Domains;

namespace Data
{
	public class TransactionMap : EntityTypeConfiguration<Transaction>
	{
		public TransactionMap ()
		{
			ToTable ("transactions", "public");

			// primary key
			HasKey (c => c.Id);

			// validations
			Property (c => c.Amount).IsRequired ();
			Property (c => c.Description).HasMaxLength(255);

			// relationships
			HasRequired (c => c.Category)
				.WithMany(x => x.Transactions)
				.HasForeignKey(x => x.Category_Id);

		}
	} // class
} // namespace