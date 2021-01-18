using Ankor.Model.DAL;
using Ankor.Personal.WebService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Data.SqlClient;

namespace Ankor.Personal.WebService.Services
{
    /// <summary>
    /// IDBEntityService implementation
    /// </summary>
    public class DBEntityService : IDBEntityService
    {
        private readonly ILogger _logger;
        private readonly DbContextOptions<ApplicationDbContext> _options;

        /// <summary>
        /// public constructor
        /// </summary>
        /// <param name="logger">logger.</param>
        public DBEntityService(ILogger logger)
        {
            _options = CreateOptions(@"Server=DESKTOP-92V8047;Database=UZTest;Trusted_Connection=True;MultipleActiveResultSets=true");
            _logger = logger;
        }

        /// <summary>
        /// gets a user account
        /// </summary>
        /// <param name="username">user name</param>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <returns>account</returns>
        public async Task<Account> GetAccountAsync(string username, string email, string password)
        {
            return await DoAsync(
                        context =>
                        {
                            var query = context.Accounts
                                            .Include(a => a.Address)
                                            .Include(a => a.Person);
                        return string.IsNullOrEmpty(username) ?
                            query.FirstOrDefaultAsync(e => e.Email == email) :// && e.hashCode == password) :
                            query.FirstOrDefaultAsync(e => e.loginName == username);// && e.hashCode == password);
                        });
        }

        /// <summary>
        /// gets a user account
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>account</returns>
        public async Task<Account> GetAccountAsync(int userId)
        {
            return await DoAsync(
                        context => context.Accounts
                            .Include(a => a.Address)
                            .Include(a => a.Person)
                            .FirstOrDefaultAsync(e => e.id == userId)
                            );
        }

        /// <summary>
        /// Changes a user profile
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="profile">profile</param>
        /// <returns>result</returns>
        public async Task<bool> ChangeProfileAsync(int userId, (string Phone, string Email) profile)
        {
            return await UpdateAsync(
                    context => context.Accounts.FirstOrDefaultAsync(a => a.id == userId),
                    (context, val) =>
                    {
                        if(!string.IsNullOrEmpty(profile.Phone))
                            val.Phone = profile.Phone;

                        if (!string.IsNullOrEmpty(profile.Email))
                            val.Email = profile.Email;
                    }
                );
        }

        /// <summary>
        /// Allows email notifications
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="allowed">notifications allowed</param>
        /// <returns>result</returns>
        public async Task<bool> AllowEmailNotificationsAsync(int userId, bool allowed)
        {
            return await UpdateAsync(
                    context => context.Accounts.FirstOrDefaultAsync(a => a.id == userId),
                    (context, val) =>
                    {
                        val.AllowEmailNotifications = allowed;
                    }
                );
        }

        /// <summary>
        /// gets a client info
        /// </summary>
        /// <param name="clientInfoId">client info id</param>
        /// <returns>client info</returns>
        public async Task<ClientInfo> GetClientInfoAsync(int? clientInfoId = null)
        {
            return await DoAsync(
                        context =>
                        {
                            var query = context.ClientInfos as IQueryable<ClientInfo>;
                            if (clientInfoId.HasValue)
                            {
                                var id = clientInfoId.Value;
                                query = query.Where(c => c.Id == id);
                            }
                            return query.FirstOrDefaultAsync();
                        });
        }

        /// <summary>
        /// sets a user password hash
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="hash">password hash</param>
        /// <returns>result</returns>
        public async Task<bool> SetPasswordByEmailAsync(string email, string hash)
        {
            return await UpdateAsync(
                    context => context.Accounts.FirstOrDefaultAsync(a => a.Email == email),
                    (context, val) =>
                    {
                        val.hashCode = hash;
                    }
                );
        }

        /// <summary>
        /// registers a user
        /// </summary>
        /// <param name="account">new account</param>
        /// <param name="serialNum">device serial number</param>
        /// <returns>result</returns>
        public async Task<bool> AddAccountForDeviceAsync(Account account, int serialNum)
        {
            return await AddAsync(
                    async context =>
                    {
                        var device = await context.Devices.FirstOrDefaultAsync(d => d.factoryID == serialNum);
                        if (device == null)
                            return null;

                        var deviceRegHistory = new DeviceRegHistory
                        {
                            Account = account,
                            Device = device,
                            tarifTypeID = 1,
                            registerDT = DateTime.Now,
                        };
                        context.DeviceRegHistories.Add(deviceRegHistory);

                        return deviceRegHistory;
                    }
                );
        }

        /// <summary>
        /// checks device for account
        /// </summary>
        /// <param name="account">new account</param>
        /// <param name="serialNum">device serial number</param>
        /// <returns>result</returns>
        public async Task<bool> CheckDeviceForAccountAsync(string account, int serialNum)
        {
            return await DoAsync(
                    context => context.DeviceRegHistories
                                .FirstOrDefaultAsync(a => a.Account.account_num == account && a.Device.factoryID == serialNum)) != null;
        }

        /// <summary>
        /// activates and updates account for a device
        /// </summary>
        /// <param name="account">new account</param>
        /// <param name="serialNum">device serial number</param>
        /// <param name="accountProps">account properties</param>
        /// <returns>result</returns>
        public async Task<bool> ActivateAndUpdateAccountAsync(string account, int serialNum, (string Login, string Email, string Phone, string PasswordHash) accountProps)
        {
            return await UpdateAsync(
                    context => context.DeviceRegHistories
                                .Include(e => e.Account)
                                .FirstOrDefaultAsync(a => a.Account.account_num == account && a.Device.factoryID == serialNum),
                    (context, val) =>
                    {
                        val.Account.Confirmed = true;
                        val.Account.loginName = accountProps.Login;
                        val.Account.Email = accountProps.Email;
                        val.Account.Phone = accountProps.Phone;
                        val.Account.hashCode = accountProps.PasswordHash;
                    }
                );
        }

        /// <summary>
        /// gets devices for a user account
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>account</returns>
        public async Task<IEnumerable<DeviceRegHistory>> GetDevicesForUserAsync(int userId)
        {
            return  await DoAsync(
                        context => context.DeviceRegHistories
                            .Include(e => e.Device)
                                .ThenInclude(e => e.DeviceSetting)
                                    .ThenInclude(e => e.DeviceModification)
                            .Where(e => e.id_account == userId)
                            .ToListAsync()
                            );
        }

        /// <summary>
        /// gets measures
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="measureTypeID">measure type id</param>
        /// <returns>measures</returns>
        //public async Task<IEnumerable<Measure>> GetMeasuresForDeviceAsync(int deviceId, int measureTypeID)
        //{
        //    var tt= await DoAsync(
        //                context => context.Measures
        //                    //.Include(e => e.Device)
        //                    .Include(e => e.SPODE)
        //                    .Where(e => e.deviceID == deviceId && e.SPODE.ParentID == measureTypeID)
        //                    .OrderBy(e => e.measureDT)
        //                    .ToListAsync()
        //                    );
        //    return tt;
        //}

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
        public async Task<IEnumerable<Measure>> GetMeasuresForDeviceAsync(int deviceId, int factoryId, long? imei, int measureTypeID,
            DateTime? startDate, DateTime? endDate)
        {
            //var tt = await DoAsync(
            //           context => context.GetDeviceMeasure_Results
            //                             .FromSqlRaw("usp_GetDeviceMeasure @factoryID, @IMEI, @MeasureType, @StartDT, @StopDT",
            //                                  new SqlParameter("@factoryID", factoryId),
            //                                  new SqlParameter("@IMEI", imei),
            //                                  new SqlParameter("@MeasureType", measureTypeID),
            //                                  new SqlParameter("@StartDT", startDate),
            //                                  new SqlParameter("@StopDT", endDate))                                          
            //                              .ToListAsync()
            //               );
            //return tt;
            var tt = await DoAsync(
                        context => context.Measures
                            .Include(e => e.SPODE)
                            .Where(e => e.deviceID == deviceId && e.SPODE.ParentID == measureTypeID)
                            .Where(e => e.measureDT >= startDate && e.measureDT <= endDate)
                            .OrderBy(e => e.measureDT)
                            .ToListAsync()
                            );
            return tt;
        }

        /// <summary>
        /// gets last measures
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="factoryId">factory id</param>
        /// <param name="imei">modem EUI</param>
        /// <returns>measures</returns>
        public async Task<IEnumerable<usp_GetLastDeviceMeasures_Result>> GetLastMeasuresForDeviceAsync(int deviceId, int factoryId, long? imei)
        {
            var tt = await DoAsync(
                       context => context.GetLastDeviceMeasures_Results
                                          //.FromSqlRaw("SELECT * FROM usp_GetLastDeviceMeasures (@factoryID, @IMEI)", 
                                          .FromSqlRaw("usp_GetLastDeviceMeasures @factoryID, @IMEI",
                                              new SqlParameter("@factoryID", factoryId),
                                              new SqlParameter("@IMEI", imei))
                                          .ToListAsync()
                           );
            return tt;
        }

        /// <summary>
        /// gets spodes
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="factoryId">factory id</param>
        /// <returns>spodes</returns>
        public async Task<IEnumerable<udf_GetMeterSpodes_Result>> GetSpodesForDeviceAsync(int deviceId, int factoryId)
        {
            var meterSpodes = await DoAsync(
                        context => context.GetMeterSpodes_Results
                                           .FromSqlRaw("SELECT * FROM udf_GetMeterSpodes (@SerialNum)", new SqlParameter("@SerialNum", factoryId))
                                           .ToListAsync()
                            );
            return meterSpodes;
        }

        /// <summary>
        /// gets user new events count
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="startDate">initial date</param>
        /// <param name="endDate">end date</param>
        /// <returns>new events count</returns>
        public async Task<int?> GetNewEventsCountAsync(int userId, DateTime? startDate, DateTime? endDate)
        {
            using (var context = new ApplicationDbContext(_options))
            {
                try
                {
                    return await context.DeviceRegHistories
                                    .Where(e => e.id_account == userId)
                                    .Join(
                                        context.Events.Where(e => e.startDT >= startDate && e.startDT <= endDate),
                                            d => d.id_device,
                                            d => d.deviceID,
                                            (reg, ev) => new { ev.id })
                                    .CountAsync();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "GetNewEventsCountAsync error");
                    return null;
                }
            }
        }

        /// <summary>
        /// sets a user events read
        /// </summary>
        /// <param name="readIds">events ids</param>
        /// <returns>result</returns>
        public async Task<bool> SetEventsReadAsync(int[] readIds)
        {
            return await UpdateAsync(
                    context => context.Events
                        .Where(e => readIds.Contains((int)e.id))
                        .ToListAsync(),
                    (context, vals) =>
                    {
                        vals.ForEach(e => e.IsNotified = true);
                    }
                );
        }

        /// <summary>
        /// gets events
        /// </summary>
        /// <param name="factoryIds">factory ids</param>
        /// <param name="imeis">modem EUIs</param>
        /// <param name="startDate">initial date</param>
        /// <param name="endDate">end date</param>
        /// <returns>events</returns>
        public async Task<IEnumerable<Event>> GetEventsAsync(int[] factoryIds, long[] imeis, DateTime? startDate, DateTime? endDate)
        {
            return await DoAsync(
                        context => context.Events
                            .Include(e => e.Device)
                            .Where(e => factoryIds.Contains(e.Device.factoryID))
                            .Where(e => e.startDT >= startDate && e.startDT <= endDate)
                            .OrderByDescending(e => e.startDT)
                            .ToListAsync()
                            //.FirstOrDefaultAsync()
                            );
        }


        private async Task<T> DoAsync<T>(Func<ApplicationDbContext, Task<T>> func) where T : class
        {
            using (var context = new ApplicationDbContext(_options))
            {
                try
                {
                    return await func(context);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "DoAsync error");
                    return null;
                }
            }
        }

        private async Task<bool> UpdateAsync<T>(
            Func<ApplicationDbContext, Task<T>> getAction,
            Action<ApplicationDbContext, T> updateAction)
            where T : class
        {
            using (var context = new ApplicationDbContext(_options))
            {
                try
                {
                    T val = await getAction(context);
                    if (val == default(T))
                        return false;
                    updateAction(context, val);
                    var result = await context.SaveChangesAsync();
                    return result > 0;
                }
                catch (DbUpdateException ex)
                {
                    _logger.Error(ex, "UpdateAsync error");
                    return false;
                }
            }
        }

        private async Task<bool> AddAsync<T>(Func<ApplicationDbContext, Task<T>> action)
           where T : class
        {
            using (var context = new ApplicationDbContext(_options))
            {
                try
                {
                    T value = await action(context);
                    var result = await context.SaveChangesAsync();
                    return value != null;
                }
                catch (DbUpdateException ex)
                {
                    _logger.Error(ex.Message);
                    return false;
                }
            }
        }

        private static DbContextOptions<ApplicationDbContext> CreateOptions(string connectionString)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseSqlServer(connectionString)
                       .Options;
        }

    }
}
