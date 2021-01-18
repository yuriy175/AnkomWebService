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
using Ankor.Model.DAL;
using Ankor.Personal.WebService.ViewModels;

namespace Ankor.Personal.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeasuresController : ControllerBase
    {
        private readonly IMeasuresService _measuresService;
        private readonly ILogger _logger;

        /// <summary>
        /// public constructor.
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="measuresService">measures service</param>
        public MeasuresController(ILogger logger, IMeasuresService measuresService)
        {
            _measuresService = measuresService;
            _logger = logger;
        }

        /// <summary>
        /// gets last measures
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="factoryId">factory id</param>
        /// <param name="imei">modem EUI</param>
        /// <returns>result</returns>
        [HttpGet]
        [Route("GetLastMeasures")]
        [Authorize]
        public async Task<ActionResult> GetLastMeasuresAsync(uint deviceId, uint factoryId, ulong? imei)
        {
            List<MeasureViewModel> measures = new List<MeasureViewModel> { };
            //var isDemo = HttpContext.User?.Claims?.Any(c => c.Type == Constants.AnonymousClaimType && c.Value == (true).ToString());
            //if (isDemo.HasValue && isDemo.Value)
            //{
            //    //profile = DemoProfile.Profile;
            //}
            //else
            //{
            measures = (await _measuresService.GetLastMeasuresAsync((int)deviceId, (int)factoryId, (long?)imei))?.ToList();
            if (measures == null)
                return StatusCode(500);
            //}

            return new JsonResult(measures);
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
        /// <returns>result</returns>
        [HttpGet]
        [Route("GetMeasures")]
        [Authorize]
        public async Task<ActionResult> GetMeasuresAsync(uint deviceId, uint factoryId, ulong? imei, uint measureTypeID, 
            DateTime? startDate, DateTime? endDate)        
        {
            List<MeasureViewModel> measures = new List<MeasureViewModel> { };
            //var isDemo = HttpContext.User?.Claims?.Any(c => c.Type == Constants.AnonymousClaimType && c.Value == (true).ToString());
            //if (isDemo.HasValue && isDemo.Value)
            //{
            //    //profile = DemoProfile.Profile;
            //}
            //else
            //{
            measures = (await _measuresService.GetMeasuresAsync((int) deviceId, (int)factoryId, (long?)imei, (int) measureTypeID,
                startDate, endDate))?.ToList();
            if (measures == null)
                    return StatusCode(500);
            //}

            return new JsonResult(measures);
        }

        /// <summary>
        /// gets spodes
        /// </summary>
        /// <param name="deviceId">device id</param>
        /// <param name="factoryId">factory id</param>
        /// <returns>result</returns>
        [HttpGet]
        [Route("GetSpodes")]
        [Authorize]
        public async Task<ActionResult> GetSpodesAsync(uint deviceId, uint factoryId)
        {
            var spodes = new List<SpodeViewModel> { };
            //var isDemo = HttpContext.User?.Claims?.Any(c => c.Type == Constants.AnonymousClaimType && c.Value == (true).ToString());
            //if (isDemo.HasValue && isDemo.Value)
            //{
            //    devices = _demoDevicesRepository.Devices.ToList();
            //}
            //else
            //{
                spodes = (await _measuresService.GetSpodesAsync((int)deviceId, (int)factoryId))?.ToList();
                if (spodes == null)
                    return StatusCode(500);
            //}


            return new JsonResult(spodes);
        }
    }
}
