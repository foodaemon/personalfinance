using System;
using System.Security.Cryptography;
using System.Text;

namespace Service
{
	public class EncryptionService : IEncryptionService
	{
		#region IEncryptionService implementation

		/// <summary>
		/// Create salt key
		/// </summary>
		/// <param name="size">Key size</param>
		/// <returns>Salt key</returns>
		public virtual string CreateSaltKey(int size = 16)
		{
			// Generate a cryptographic random number
			var rng = new RNGCryptoServiceProvider();
			var buff = new byte[size];
			rng.GetBytes(buff);

			// Return a Base64 string representation of the random number
			return Convert.ToBase64String(buff);
		}

		/// <summary>
		/// Create a password hash
		/// </summary>
		/// <param name="password">{assword</param>
		/// <param name="saltkey">Salk key</param>
		/// <param name="passwordFormat">Password format (hash algorithm)</param>
		/// <returns>Password hash</returns>
		public virtual string CreatePasswordHash(string password, string saltkey)
		{

			string saltAndPassword = String.Concat(password, saltkey);

			//return FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, passwordFormat);

			var hashByteArray = SHA512.Create ().ComputeHash (Encoding.UTF8.GetBytes (saltAndPassword));
			var hashedPassword = Convert.ToBase64String (hashByteArray);
			
			return hashedPassword;
		}

		public string EncryptText (string plainText, string encryptionPrivateKey = "")
		{
			throw new NotImplementedException ();
		}

		public string DecryptText (string cipherText, string encryptionPrivateKey = "")
		{
			throw new NotImplementedException ();
		}

		#endregion

	} // class
} // namespace