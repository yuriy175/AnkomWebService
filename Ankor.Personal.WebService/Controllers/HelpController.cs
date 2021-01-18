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
    public class HelpController : ControllerBase
    {
        private readonly IHelpService _helpService;
        private readonly ILogger _logger;

        /// <summary>
        /// public constructor.
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="profileService">profile service</param>
        public HelpController(ILogger logger, IHelpService helpService)
        {
            _helpService = helpService;
            _logger = logger;
        }

        /// <summary>
        /// ask question 
        /// </summary>
        /// <param name="clientInfoId">client info id</param>
        /// <param name="userId">user id</param>
        /// <param name="topic">question topic</param>
        /// <param name="body">question body</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("AskQuestion")]
        [Authorize]
        public async Task<ActionResult> AskQuestionAsync(uint clientInfoId, uint userId, string topic, string body)        
        {
            return new JsonResult(await _helpService.AskQuestionAsync((int)clientInfoId, (int)userId, topic, body));
        }

        /// <summary>
        /// gets FAQ list
        /// </summary>
        /// <returns>result</returns>
        [HttpGet]
        [Route("GetFAQs")]
        [Authorize]
        public async Task<ActionResult> GetFAQsAsync()
        {
            return new JsonResult(await _helpService.GetFAQsAsync());
        }

        /// <summary>
        /// gets FAQ answer
        /// </summary>
        /// <param name="faq">FAQ</param>
        /// <returns>result</returns>
        [HttpGet]
        [Route("GetFAQ")]
        [Authorize]
        public async Task<ActionResult> GetFAQAsync(string faq)
        {
            return new JsonResult(await _helpService.GetFAQAsync(faq));
        }
    }
}
