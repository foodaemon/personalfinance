using System;
using Core.Domains;
using System.Collections.Generic;

namespace Service
{
	public interface IAccountService
	{
		/// <summary>
		/// Creates the account.
		/// </summary>
		/// <param name="email">Email.</param>
		/// <param name="password">Password.</param>
		void CreateAccount(string email, string password);

		/// <summary>
		/// Gets all users.
		/// </summary>
		/// <returns>The all users.</returns>
		IEnumerable<Account> GetAllUsers();

		/// <summary>
		/// Gets the account by email.
		/// </summary>
		/// <returns>The account by email.</returns>
		/// <param name="email">Email.</param>
		Account GetAccountByEmail(string email);

		/// <summary>
		/// Validates the account.
		/// </summary>
		/// <returns><c>true</c>, if account was validated, <c>false</c> otherwise.</returns>
		/// <param name="email">Email.</param>
		/// <param name="password">Password.</param>
		bool ValidateAccount(string email, string password);
	}
}