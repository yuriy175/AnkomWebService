using Ankor.Model.DAL;
using Ankor.Personal.WebService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.Interfaces
{
    /// <summary>
    /// IDeviceService
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        /// gets a user devices
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>devices</returns>
        Task<IEnumerable<DeviceViewModel>> GetDevicesAsync(int userId);

        /// <summary>
        /// checks device for account
        /// </summary>
        /// <param name="account">new account</param>
        /// <param name="serialNum">device serial number</param>
        /// <returns>result</returns>
        Task<bool> CheckDeviceForAccountAsync(string account, string serialNum);
    }
}
