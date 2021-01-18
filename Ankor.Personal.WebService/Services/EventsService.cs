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
    /// IEventsService implementation
    /// </summary>
    public class EventsService : IEventsService
    {
        private readonly ILogger _logger;
        private readonly IDBEntityService _entityService;

        public EventsService(
            ILogger logger,
            IDBEntityService entityService)
        {
            _entityService = entityService;
            _logger = logger;
        }

        /// <summary>
        /// gets events
        /// </summary>
        /// <param name="factoryIds">factory ids</param>
        /// <param name="imeis">modem EUIs</param>
        /// <param name="startDate">initial date</param>
        /// <param name="endDate">end date</param>
        /// <returns>events</returns>
        public async Task<IEnumerable<EventsViewModel>> GetEventsAsync(int[] factoryIds, long[] imeis, DateTime? startDate, DateTime? endDate)
        {
            var events = await _entityService.GetEventsAsync(factoryIds, imeis, startDate, endDate);
            return events?.Select(p => new EventsViewModel(p));
        }

        /// <summary>
        /// sets a user events read
        /// </summary>
        /// <param name="readIds">events ids</param>
        /// <returns>result</returns>
        public async Task<bool> SetEventsReadAsync(int[] readIds)
        {
            return await _entityService.SetEventsReadAsync(readIds);
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
            return await _entityService.GetNewEventsCountAsync(userId, startDate, endDate);
        }
    }
}
