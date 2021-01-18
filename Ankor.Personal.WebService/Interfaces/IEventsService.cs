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
    /// IEventsService
    /// </summary>
    public interface IEventsService
    {
        /// <summary>
        /// gets events
        /// </summary>
        /// <param name="factoryIds">factory ids</param>
        /// <param name="imeis">modem EUIs</param>
        /// <param name="startDate">initial date</param>
        /// <param name="endDate">end date</param>
        /// <returns>events</returns>
        Task<IEnumerable<EventsViewModel>> GetEventsAsync(int[] factoryIds, long[] imeis, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// sets a user events read
        /// </summary>
        /// <param name="readIds">events ids</param>
        /// <returns>result</returns>
        Task<bool> SetEventsReadAsync(int[] readIds);

        /// <summary>
        /// gets user new events count
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="startDate">initial date</param>
        /// <param name="endDate">end date</param>
        /// <returns>new events count</returns>
        Task<int?> GetNewEventsCountAsync(int userId, DateTime? startDate, DateTime? endDate);
    }
}
