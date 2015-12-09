using System;
using System.Collections.Generic;
using Core.Domains;
using Data;
using System.Linq;
using Service.Interfaces;

namespace Service
{
	public class AccountService : IAccountService
	{
		private readonly IRepository<Account> _repo;
		private readonly IEncryptionService _encryptionService;

		public AccountService(IRepository<Account> repo,
			IEncryptionService encryptionService)
		{
			_repo = repo;
			_encryptionService = encryptionService;
		}

		public void Create(string email, string password)
		{
			email = email.ToLower ();
			var userAccount = GetByEmail(email);
			if (userAccount != null)
				throw new Exception ("Email address already exits.");

			var salt = _encryptionService.CreateSaltKey (12);
			var passwordHash = _encryptionService.CreatePasswordHash (password, salt);

			var account = new Account ()
			{
				Email = email,
				Password_Salt = salt,
				Password_Hash = passwordHash,
				Created_At = DateTime.Now,
				Updated_At = DateTime.Now
			};

			_repo.Insert (account);

		}

		public IEnumerable<Account> GetAll()
		{
			var accounts = _repo.Table.ToList();
			return accounts;
		}

		public Account GetByEmail(string email)
		{
			email = email.ToLower ();
			var account = _repo.Table
				.SingleOrDefault(x => x.Email == email);
			return account;
		}

		public Account GetByToken(string token)
		{
			var account = _repo.Table
				.FirstOrDefault(x => x.Password_Hash == token);
			return account;
			
		}

		public bool Validate(string email, string password)
		{
			var account = GetByEmail(email: email);
			var passwordHash = _encryptionService.CreatePasswordHash(password, account.Password_Salt);

			if (passwordHash == account.Password_Hash)
				return true;

			return false;
		}
	} // class
} // namespace