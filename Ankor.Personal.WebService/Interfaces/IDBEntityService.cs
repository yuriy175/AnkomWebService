using Ankor.Model;
using Ankor.Model.DAL;
using Ankor.Personal.WebService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.Interfaces
{
    /// <summary>
    /// IDBEntityService
    /// </summary>
    public interface IDBEntityService
    {
        /// <summary>
        /// gets a user account
        /// </summary>
        /// <param name="username">user name</param>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <returns>account</returns>
        Task<Account> GetAccountAsync(string username, string email, string password);

        /// <summary>
        /// sets a user password hash
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="hash">password hash</param>
        /// <returns>result</returns>
        Task<bool> SetPasswordByEmailAsync(string email, string hash);

        /// <summary>
        /// gets a user account
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>account</returns>
        Task<Account> GetAccountAsync(int userId);

        /// <summary>
        /// gets events
        /// </summary>
        /// <param name="factoryIds">factory ids</param>
        /// <param name="imeis">modem EUIs</param>
        /// <param name="startDate">initial date</param>
        /// <param name="endDate">end date</param>
        /// <returns>events</returns>
        Task<IEnumerable<Event>> GetEventsAsync(int[] factoryIds, long[] imeis, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// gets a client info
        /// </summary>
        /// <param name="clientInfoId">client info id</param>
        /// <returns>client info</returns>
        Task<ClientInfo> GetClientInfoAsync(int? clientInfoId = null);

        /// <summary>
        /// gets devices for a user account
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>devices</returns>
        Task<IEnumerable<DeviceRegHistory>> GetDevicesForUserAsync(int userId);

        /// <summary>
        /// sets a user events read
        /// </summary>
        /// <param name="readIds">events ids</param>
        /// <returns>result</returns>
        Task<bool> SetEventsReadAsync(int[] readIds);

        /// <summary>
        /// Changes a user profile
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="profile">profile</param>
        /// <returns>result</returns>
        Task<bool> ChangeProfileAsync(int userId, (string Phone, string Email) profile);

        /// <summary>
        /// gets user new events count
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name = "startDate" > initial date</param>
        /// <param name="endDate">end date</param>
        /// <returns>new events count</returns>
        Task<int?> GetNewEventsCountAsync(int userId, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Allows email notifications
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="allowed">notifications allowed</param>
        /// <returns>result</returns>
        Task<bool> AllowEmailNotificationsAsync(int userId, bool allowed);

        /// <summary>
        /// gets measures
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="factoryId">factory id</param>
        /// <param name="imei">modem EUI</param>
        /// <param name="measureTypeID">measure type id</param>
        /// <param name="startDate">initial date</param>
        /// <param name="endDate">end date</param>
        /// <returns>measures</returns>
        Task<IEnumerable<Measure>> GetMeasuresForDeviceAsync(int deviceId, int factoryId, long? imei, int measureTypeID,
            DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// gets last measures
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="factoryId">factory id</param>
        /// <param name="imei">modem EUI</param>
        /// <returns>measures</returns>
        Task<IEnumerable<usp_GetLastDeviceMeasures_Result>> GetLastMeasuresForDeviceAsync(int deviceId, int factoryId, long? imei);

        /// <summary>
        /// gets spodes
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="factoryId">factory id</param>
        /// <returns>spodes</returns>
        Task<IEnumerable<udf_GetMeterSpodes_Result>> GetSpodesForDeviceAsync(int deviceId, int factoryId);

        /// <summary>
        /// registers a user
        /// </summary>
        /// <param name="account">new account</param>
        /// <param name="serialNum">device serial number</param>
        /// <returns>result</returns>
        Task<bool> AddAccountForDeviceAsync(Account account, int serialNum);

        /// <summary>
        /// checks device for account
        /// </summary>
        /// <param name="account">new account</param>
        /// <param name="serialNum">device serial number</param>
        /// <returns>result</returns>
        Task<bool> CheckDeviceForAccountAsync(string account, int serialNum);

        /// <summary>
        /// activates and updates account for a device
        /// </summary>
        /// <param name="account">new account</param>
        /// <param name="serialNum">device serial number</param>
        /// <param name="accountProps">account properties</param>
        /// <returns>result</returns>
        Task<bool> ActivateAndUpdateAccountAsync(string account, int serialNum, (string Login, string Email, string Phone, string PasswordHash) accountProps);
    }
}
