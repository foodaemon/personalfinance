using System;
using Core.Domains;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mappings
{
	public class CategoryMap : EntityTypeConfiguration<Category>
	{
		public CategoryMap ()
		{
			ToTable ("categories", "public");

			HasKey (c => c.Id);

			Property (c => c.Name).IsRequired ().HasMaxLength (50);
			Property (c => c.Description).HasMaxLength (255);
		}
	} // class
} // namespace