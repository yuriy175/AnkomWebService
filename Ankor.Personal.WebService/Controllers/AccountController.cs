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

namespace Ankor.Personal.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthorizingService _authorizingService;
        private readonly ILogger _logger;

        /// <summary>
        /// public constructor.
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="authorizingService">authorizing service</param>
        public AccountController(ILogger logger, IAuthorizingService authorizingService)
        {
            _authorizingService = authorizingService;
            _logger = logger;
        }

        /// <summary>
        /// logins a user
        /// </summary>
        /// <param name="userViewModel">user view model</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> LoginAsync(LoginViewModel userViewModel)
        {
            if (string.IsNullOrEmpty(userViewModel.Login) && string.IsNullOrEmpty(userViewModel.Email))// || string.IsNullOrEmpty(userViewModel.Password))
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var result = await _authorizingService.LoginAsync(userViewModel.Login, userViewModel.Email, userViewModel.Password);

            return result == null ? new UnauthorizedResult() as ActionResult :
                new JsonResult(new
                {
                    access_token = result.Value.Token,
                    username = result.Value.UserName,
                    userid = result.Value.UserId,
                    account_num = result.Value.AccountNum,
                });
        }

        /// <summary>
        /// registers a user
        /// </summary>
        /// <param name="regViewModel">register view model</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> RegisterAsync(RegisterViewModel regViewModel)
        {
            if (string.IsNullOrEmpty(regViewModel.Login) && string.IsNullOrEmpty(regViewModel.Email))// || string.IsNullOrEmpty(userViewModel.Password))
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var result = await _authorizingService.RegisterAsync(regViewModel);

            return result == null ? new UnauthorizedResult() as ActionResult : new JsonResult(true);
        }

        /// <summary>
        /// logins an anonymous user
        /// </summary>
        /// <returns>result</returns>
        [HttpPost]
        [Route("LoginAnonymous")]
        public async Task<ActionResult> LoginAnonymousAsync()
        {
            var result = await _authorizingService.LoginAnonymousAsync();

            return result == null ? new UnauthorizedResult() as ActionResult :
                new JsonResult(new
                {
                    access_token = result.Value.Token,
                    username = result.Value.UserName,
                    userid = result.Value.UserId,
                    account_num = result.Value.AccountNum,
                });
        }

        /// <summary>
        /// sends temp password
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="clientInfoId">client info id</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("SendTempPasword")]
        public async Task<ActionResult> SendTempPaswordAsync(string email, uint clientInfoId)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { errorText = "Invalid email." });
            }
            var result = await _authorizingService.SendTempPasword(email, (int)clientInfoId);

            return new JsonResult(result);
        }

        /// <summary>
        /// sends registration check code
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="clientInfoId">client info id</param>
        /// <returns>result</returns>
        [HttpGet]
        [Route("GetCheckCode")]
        public async Task<ActionResult> GetCheckCodeAsync(string email, uint clientInfoId)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { errorText = "Invalid email." });
            }
            return new JsonResult(await _authorizingService.SendCheckCodeAsync(email, (int)clientInfoId));
        }

        /// <summary>
        /// Changes a user password
        /// </summary>
        /// <param name="changePasswordViewModel">change зassword мiewModel</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("ChangePassword")]
        [Authorize]
        public async Task<ActionResult> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel)
        {
            if (string.IsNullOrEmpty(changePasswordViewModel.OldPassword) && string.IsNullOrEmpty(changePasswordViewModel.NewPassword))
            {
                return BadRequest(new { errorText = "Invalid password." });
            }

            var result = (await _authorizingService.ChangePasswordAsync(
                (int)changePasswordViewModel.UserId, changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword));
            return result ? new OkResult() : StatusCode(500);
        }
    }
}
