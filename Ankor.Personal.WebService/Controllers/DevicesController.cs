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
using Microsoft.AspNetCore.Authorization;
using Ankor.Personal.WebService.ViewModels;
using Ankor.Personal.WebService.Model.Demos;
using Ankor.Model.DAL;
using Ankor.Personal.WebService.Constants;

namespace Ankor.Personal.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly ILogger _logger;
        private readonly DemoDevicesRepository _demoDevicesRepository;

        /// <summary>
        /// public constructor.
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="deviceService">device service</param>
        /// <param name="demoDevicesRepository">devices repository for demo mode</param>
        public DevicesController(
            ILogger logger, 
            IDeviceService deviceService, 
            DemoDevicesRepository demoDevicesRepository)
        {
            _deviceService = deviceService;
            _logger = logger;
            _demoDevicesRepository = demoDevicesRepository;
        }

        /// <summary>
        /// gets user devices
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>result</returns>
        [HttpGet]
        [Route("GetDevices")]
        [Authorize]
        public async Task<ActionResult> GetDevicesAsync(int userId)
        {
            List<DeviceViewModel> devices = new List<DeviceViewModel> { };
            var isDemo = HttpContext.User?.Claims?.Any(c => c.Type == Constants.Constants.AnonymousClaimType && c.Value == (true).ToString());
            if (isDemo.HasValue && isDemo.Value)
            {
                devices = _demoDevicesRepository.Devices.ToList();
            }
            else
            {
                devices = (await _deviceService.GetDevicesAsync((int)userId)).ToList();
                if (devices == null)
                    return StatusCode(500);
            }

            return new JsonResult(devices);
        }

        /// <summary>
        /// checks device for account
        /// </summary>
        /// <param name="account">new account</param>
        /// <param name="serialNum">device serial number</param>
        /// <returns>result</returns>
        [HttpGet]
        [Route("CheckDeviceForAccount")]
        [AllowAnonymous]
        public async Task<ActionResult> CheckDeviceForAccountAsync(string account, string serialNum)
        {
            var result = (await _deviceService.CheckDeviceForAccountAsync(account, serialNum));
            return new JsonResult(result);
        }
    }
}
