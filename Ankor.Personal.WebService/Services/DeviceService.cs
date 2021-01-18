using Ankor.Model;
using Ankor.Model.DAL;
using Ankor.Personal.WebService.Interfaces;
using Ankor.Personal.WebService.ViewModels;
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
    /// IDeviceService implementation
    /// </summary>
    public class DeviceService : IDeviceService
    {
        private readonly ILogger _logger;
        private readonly IDBEntityService _entityService;

        public DeviceService(
            ILogger logger,
            IDBEntityService entityService)
        {
            _entityService = entityService;
            _logger = logger;
        }

        /// <summary>
        /// gets a user devices
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>devices</returns>
        public async Task<IEnumerable<DeviceViewModel>> GetDevicesAsync(int userId)
        {
            return (await _entityService.GetDevicesForUserAsync(userId))?
                .Select(d => new DeviceViewModel(d)).ToList();
        }

        /// <summary>
        /// checks device for account
        /// </summary>
        /// <param name="account">new account</param>
        /// <param name="serialNum">device serial number</param>
        /// <returns>result</returns>
        public async Task<bool> CheckDeviceForAccountAsync(string account, string serialNum)
        {
            int.TryParse(serialNum, out int factoryId);
            return (await _entityService.CheckDeviceForAccountAsync(account, factoryId));
        }
    }
}
