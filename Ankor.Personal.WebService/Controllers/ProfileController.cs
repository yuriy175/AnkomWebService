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

namespace Ankor.Personal.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly ILogger _logger;

        /// <summary>
        /// public constructor.
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="profileService">profile service</param>
        public ProfileController(ILogger logger, IProfileService profileService)
        {
            _profileService = profileService;
            _logger = logger;
        }

        /// <summary>
        /// gets a client info
        /// </summary>
        /// <returns>result</returns>
        [HttpGet]
        [Route("GetClientInfo")]
        [AllowAnonymous]
        public async Task<ActionResult> GetClientInfoAsync()
        {
            var info = (await _profileService.GetClientInfoAsync());
            if (info == null)
                return StatusCode(500);

            return new JsonResult(info);
        }

        /// <summary>
        /// gets a user profile
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>result</returns>
        [HttpGet]
        [Route("GetProfile")]
        [Authorize]
        public async Task<ActionResult> GetProfileAsync(uint userId)        
        {
            UserViewModel profile;
            var isDemo = HttpContext.User?.Claims?.Any(c => c.Type == Constants.Constants.AnonymousClaimType && c.Value == (true).ToString());
            if (isDemo.HasValue && isDemo.Value)
            {
                profile = DemoProfile.Profile;
            }
            else
            {
                var account = (await _profileService.GetProfileAsync((int)userId));
                if(account == null)
                    return StatusCode(500);

                profile = new UserViewModel(account);
            }

            return new JsonResult(profile);
        }

        /// <summary>
        /// Changes a user profile
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="email">email</param>
        /// <param name="phone">phone</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("ChangeProfile")]
        [Authorize]
        public async Task<ActionResult> ChangeProfileAsync(uint userId, string phone, string email)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(phone))
            {
                return BadRequest(new { errorText = "Invalid email or phone." });
            }

            var result = (await _profileService.ChangeProfileAsync((int)userId, (phone, email)));
            return result ? new OkResult() : StatusCode(500);
        }

        /// <summary>
        /// Allows email notifications
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="allowed">notifications allowed</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("AllowEmailNotifications")]
        [Authorize]
        public async Task<ActionResult> AllowEmailNotificationsAsync(uint userId, bool allowed)
        {
            var result = (await _profileService.AllowEmailNotificationsAsync((int)userId, allowed));
            return result ? new OkResult() : StatusCode(500);
        }        
    }
}
