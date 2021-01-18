using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.Interfaces
{
    /// <summary>
    /// password hashing interface
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hash password
        /// </summary>
        /// <param name="password">password</param>
        /// <returns>hash</returns>
        Task<string> HashPassword(string password);

        /// <summary>
        /// Verify password
        /// </summary>
        /// <param name="password">password</param>
        /// <param name="hash">hash</param>
        /// <returns>result</returns>
        Task<bool> VerifyPassword(string password, string hash);
    }
}
