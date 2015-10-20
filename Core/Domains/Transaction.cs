using System;

namespace Core.Domains
{
	public class Transaction : BaseEntity
	{
		public virtual DateTime Date { get; set; }
		public virtual string Description { get; set; }
		public virtual int Category_Id { get; set; }
		public virtual double Amount { get; set; }
		//public virtual int Card_Id { get; set; }
		public virtual bool Is_Income { get; set; }
		public virtual string Comments { get; set; }
		public virtual DateTime Created_At { get; set; }
		public virtual DateTime Updated_At { get; set; }

		#region "navigation properties"
		public virtual Category Category { get; set; }
		#endregion "navigation properties"
	} // class
} // namespace