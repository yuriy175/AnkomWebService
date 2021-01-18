using Ankor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.Interfaces
{
    /// <summary>
    /// IAuthorizingService
    /// </summary>
    public interface IAuthorizingService
    {
        /// <summary>
        /// logins a user
        /// </summary>
        /// <param name = "userName" > user name</param>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <returns>token info</returns>
        Task<(string Token, string UserName, int UserId, string AccountNum)?> LoginAsync(string userName, string email, string password);

        /// <summary>
        /// logins an anonymous user
        /// </summary>
        /// <returns>token info</returns>
        Task<(string Token, string UserName, int UserId, string AccountNum)?> LoginAnonymousAsync();

        /// <summary>
        /// sends temp password
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="clientInfoId">client info id</param>
        /// <returns>result</returns>
        Task<bool> SendTempPasword(string email, int clientInfoId);

        /// <summary>
        /// sends registration check code
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="clientInfoId">client info id</param>
        /// <returns>check code</returns>
        Task<string> SendCheckCodeAsync(string email, int clientInfoId);

        /// <summary>
        /// registers a user
        /// </summary>
        /// <param name="regViewModel">register view model</param>
        /// <returns>result</returns>
        Task<bool> RegisterAsync(RegisterViewModel regViewModel);

        /// <summary>
        /// Changes a user password
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="oldPassword">old password</param>
        /// <param name="newPassword">new password</param>
        /// <returns>result</returns>
        Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
    }
}
