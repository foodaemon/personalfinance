using System;

namespace Core.Domains
{
	public class Account : BaseEntity
	{
		public string Email { get; set; }
		public string Password_Hash { get; set; }
		public string Password_Salt { get; set; }
		public bool Is_Locked { get; set; }
		public DateTime? Last_Login_At { get; set; }
		public DateTime Created_At { get; set; }
		public DateTime Updated_At { get; set; }
	} // class
} // namespace