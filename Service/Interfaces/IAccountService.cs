using System;
using Core.Domains;
using System.Collections.Generic;

namespace Service.Interfaces
{
	public interface IAccountService
	{
		/// <summary>
		/// Creates the account.
		/// </summary>
		/// <param name="email">Email.</param>
		/// <param name="password">Password.</param>
		void Create(string email, string password);

		/// <summary>
		/// Gets all users.
		/// </summary>
		/// <returns>The all users.</returns>
		IEnumerable<Account> GetAll();

		/// <summary>
		/// Gets the account by email.
		/// </summary>
		/// <returns>The account by email.</returns>
		/// <param name="email">Email.</param>
		Account GetByEmail(string email);

		/// <summary>
		/// Gets the account by token.
		/// </summary>
		/// <returns>The account by token.</returns>
		/// <param name="token">Token.</param>
		Account GetByToken (string token);

		/// <summary>
		/// Validates the account.
		/// </summary>
		/// <returns><c>true</c>, if account was validated, <c>false</c> otherwise.</returns>
		/// <param name="email">Email.</param>
		/// <param name="password">Password.</param>
		bool Validate(string email, string password);
	}
}