using Ankor.Model;
using Ankor.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.Interfaces
{
    /// <summary>
    /// IProfileService
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// gets a user profile
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>account</returns>
        Task<Account> GetProfileAsync(int userId);

        /// <summary>
        /// gets a client info
        /// </summary>
        /// <returns>client info</returns>
        Task<ClientInfo> GetClientInfoAsync();

        /// <summary>
        /// Changes a user profile
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="profile">profile</param>
        /// <returns>result</returns>
        Task<bool> ChangeProfileAsync(int userId, (string Phone, string Email) profile);

        /// <summary>
        /// Allows email notifications
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="allowed">notifications allowed</param>
        /// <returns>result</returns>
        Task<bool> AllowEmailNotificationsAsync(int userId, bool allowed);
    }
}
