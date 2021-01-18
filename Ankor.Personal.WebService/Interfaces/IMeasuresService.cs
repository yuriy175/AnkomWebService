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
    /// IMeasuresService
    /// </summary>
    public interface IMeasuresService
    {
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
        Task<IEnumerable<MeasureViewModel>> GetMeasuresAsync(int deviceId, int factoryId, long? imei, int measureTypeID,
            DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// gets last measures
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="factoryId">factory id</param>
        /// <param name="imei">modem EUI</param>
        /// <returns>measures</returns>
        Task<IEnumerable<MeasureViewModel>> GetLastMeasuresAsync(int deviceId, int factoryId, long? imei);

        /// <summary>
        /// gets spodes
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="factoryId">factory id</param>
        /// <returns>spodes</returns>
        Task<IEnumerable<SpodeViewModel>> GetSpodesAsync(int deviceId, int factoryId);
    }
}
