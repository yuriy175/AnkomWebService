using Ankor.Model;
using Ankor.Model.DAL;
using Ankor.Personal.WebService.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.Services
{
    /// <summary>
    /// IProfileService implementation
    /// </summary>
    public class ProfileService : IProfileService
    {
        private readonly ILogger _logger;
        private readonly IDBEntityService _entityService;

        public ProfileService(
            ILogger logger,
            IDBEntityService entityService)
        {
            _entityService = entityService;
            _logger = logger;
        }

        /// <summary>
        /// gets a user profile
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>account</returns>
        public async Task<Account> GetProfileAsync(int userId)
        {
            return await _entityService.GetAccountAsync(userId);
        }

        /// <summary>
        /// gets a client info
        /// </summary>
        /// <returns>client info</returns>
        public async Task<ClientInfo> GetClientInfoAsync()
        {
            return await _entityService.GetClientInfoAsync();
        }

        /// <summary>
        /// Changes a user profile
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="profile">profile</param>
        /// <returns>result</returns>
        public async Task<bool> ChangeProfileAsync(int userId, (string Phone, string Email) profile)
        {
            return await _entityService.ChangeProfileAsync(userId, profile);
        }

        /// <summary>
        /// Allows email notifications
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="allowed">notifications allowed</param>
        /// <returns>result</returns>
        public async Task<bool> AllowEmailNotificationsAsync(int userId, bool allowed)
        {
            return await _entityService.AllowEmailNotificationsAsync(userId, allowed);
        }
    }
}
