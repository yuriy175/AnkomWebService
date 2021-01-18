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
    /// IMeasuresService implementation
    /// </summary>
    public class MeasuresService : IMeasuresService
    {
        private readonly ILogger _logger;
        private readonly IDBEntityService _entityService;

        public MeasuresService(
            ILogger logger,
            IDBEntityService entityService)
        {
            _entityService = entityService;
            _logger = logger;
        }

        /// <summary>
        /// gets spodes
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="factoryId">factory id</param>
        /// <returns>spodes</returns>
        public async Task<IEnumerable<SpodeViewModel>> GetSpodesAsync(int deviceId, int factoryId)
        {
            var spodes = await _entityService.GetSpodesForDeviceAsync(deviceId, factoryId);
            return spodes?.Select(p => new SpodeViewModel(p));        
        }

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
        public async Task<IEnumerable<MeasureViewModel>> GetMeasuresAsync(int deviceId, int factoryId, long? imei, int measureTypeID,
            DateTime? startDate, DateTime? endDate)
        {
            var measures = await _entityService.GetMeasuresForDeviceAsync(deviceId, factoryId, imei, measureTypeID, startDate, endDate);
            return measures?.Select(p => new MeasureViewModel(p));
        }

        /// <summary>
        /// gets last measures
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="factoryId">factory id</param>
        /// <param name="imei">modem EUI</param>
        /// <returns>measures</returns>
        public async Task<IEnumerable<MeasureViewModel>> GetLastMeasuresAsync(int deviceId, int factoryId, long? imei)
        {
            var measures = await _entityService.GetLastMeasuresForDeviceAsync(deviceId, factoryId, imei);
            return measures?.Select(p => new MeasureViewModel(deviceId, p));
        }
    }
}
