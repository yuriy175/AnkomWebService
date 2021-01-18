using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Ankor.ViewModels;
using Ankor.Model;
using Ankor.Personal.WebService.Interfaces;
using Serilog;
using Ankor.Personal.WebService.Constants;
using Ankor.Personal.WebService.Model.Demos;
using Microsoft.AspNetCore.Authorization;
using Ankor.Personal.WebService.ViewModels;

namespace Ankor.Personal.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;
        private readonly ILogger _logger;
        private readonly DemoEventsRepository _demoEventsRepository;

        /// <summary>
        /// public constructor.
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="eventsService">events service</param>
        public EventsController(
            ILogger logger, 
            IEventsService eventsService,
            DemoEventsRepository demoEventsRepository)
        {
            _eventsService = eventsService;
            _logger = logger;
            _demoEventsRepository = demoEventsRepository;
        }

        /// <summary>
        /// gets user new events count
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="startDate">initial date</param>
        /// <param name="endDate">end date</param>
        /// <returns>new events count</returns>
        [HttpGet]
        [Route("GetNewEventsCount")]
        [Authorize]
        public async Task<ActionResult> GetNewEventsCountAsync(int userId, DateTime? startDate, DateTime? endDate)
        {
            int? count = null;
            var isDemo = HttpContext.User?.Claims?.Any(c => c.Type == Constants.Constants.AnonymousClaimType && c.Value == (true).ToString());
            //if (isDemo.HasValue && isDemo.Value)
            //{
            //    count = await _demoEventsRepository.GetNewEventsCountAsync(userId);
            //}
            //else
            {
                count = (await _eventsService.GetNewEventsCountAsync((int)userId, startDate, endDate));
            }

            return !count.HasValue? StatusCode(500) as ActionResult : new JsonResult(count.Value);
        }

        /// <summary>
        /// gets a user events
        /// </summary>
        /// <param name="factoryIds">factory ids</param>
        /// <param name="imeis">modem EUIs</param>
        /// <param name="startDate">initial date</param>
        /// <param name="endDate">end date</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("GetEvents")]
        [Authorize]
        public async Task<ActionResult> GetEventsAsync(EventsRequestViewModel request)        
        {
            List<EventsViewModel> events = new List<EventsViewModel> { };
            var isDemo = HttpContext.User?.Claims?.Any(c => c.Type == Constants.Constants.AnonymousClaimType && c.Value == (true).ToString());
            //if (isDemo.HasValue && isDemo.Value)
            //{
            //    events = _demoEventsRepository.Events.ToList();
            //}
            //else
            {
                events = (await _eventsService.GetEventsAsync(request.FactoryIDs, 
                    request.Imeis.Select(i => Convert.ToInt64(i)).ToArray(), 
                    request.StartDate, request.EndDate)).ToList();
                if (events == null)
                    return StatusCode(500);
            }

            return new JsonResult(events);
        }

        /// <summary>
        /// sets a user events read
        /// </summary>
        /// <param name="readIds">events ids</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("SetEventsRead")]
        [Authorize]
        public async Task<ActionResult> SetEventsReadAsync(int[] readIds)
        {
            var result = false;
            var isDemo = HttpContext.User?.Claims?.Any(c => c.Type == Constants.Constants.AnonymousClaimType && c.Value == (true).ToString());
            //if (isDemo.HasValue && isDemo.Value)
            //{
            //    result = await _demoEventsRepository.SetEventsReadAsync(readIds);
            //}
            //else
            {
                result = (await _eventsService.SetEventsReadAsync(readIds));
            }

            return result ? new OkResult(): StatusCode(500);
        }
    }
}
