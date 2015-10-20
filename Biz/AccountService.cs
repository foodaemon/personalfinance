using System;
using System.Collections.Generic;
using Core.Domains;
using Data;
using System.Linq;

namespace Service
{
	public class AccountService : IAccountService
	{
		private readonly IRepository<Account> _accountRepo;
		private readonly IEncryptionService _encryptionService;

		public AccountService()
		{
			_accountRepo = new Repository<Account>();
			_encryptionService = new EncryptionService ();
		}

		/// <summary>
		/// Creates the account.
		/// </summary>
		/// <param name="email">Email.</param>
		/// <param name="password">Password.</param>
		public void CreateAccount(string email, string password)
		{
			email = email.ToLower ();
			var userAccount = GetAccountByEmail(email);
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

			_accountRepo.Insert (account);

		}

		/// <summary>
		/// Gets all users.
		/// </summary>
		/// <returns>The all users.</returns>
		public IEnumerable<Account> GetAllUsers()
		{
			var accounts = _accountRepo.Table.ToList();
			return accounts;
		}

		/// <summary>
		/// Gets the account by email.
		/// </summary>
		/// <returns>The account by email.</returns>
		/// <param name="email">Email.</param>
		public Account GetAccountByEmail(string email)
		{
			email = email.ToLower ();
			var account = _accountRepo.Table.SingleOrDefault(x => x.Email == email);
			return account;
		}

		/// <summary>
		/// Validates the account.
		/// </summary>
		/// <returns><c>true</c>, if account was validated, <c>false</c> otherwise.</returns>
		/// <param name="email">Email.</param>
		/// <param name="password">Password.</param>
		public bool ValidateAccount(string email, string password)
		{
			var account = GetAccountByEmail(email: email);
			var passwordHash = _encryptionService.CreatePasswordHash(password, account.Password_Salt);

			if (passwordHash == account.Password_Hash)
				return true;

			return false;
		}
	} // class
} // namespace