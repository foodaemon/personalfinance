using System;
using System.Data.Entity.ModelConfiguration;
using Core.Domains;

namespace Data.Mappings
{
	public class AccountMap : EntityTypeConfiguration<Account>
	{
		public AccountMap ()
		{
			// Table
			ToTable ("users", "public");

			// Key
			HasKey (u => u.Id);
		}
	} // class
} // namespace