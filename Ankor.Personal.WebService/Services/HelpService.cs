using Ankor.Model;
using Ankor.Model.DAL;
using Ankor.Personal.WebService.Interfaces;
using Ankor.Personal.WebService.Model.Demos;
using Microsoft.AspNetCore.Builder;
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
    /// IHelpService implementation
    /// </summary>
    public class HelpService : IHelpService
    {
        private readonly ILogger _logger;
        private readonly IDBEntityService _entityService;
        private readonly DemoFAQsRepository _fakeService;
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger">logger </param>
        /// <param name="emailSender">email sender service</param>
        /// <param name="entityService">db service</param>
        /// <param name="fakeService">fake service</param>
        public HelpService(
            ILogger logger,
            IEmailSender emailSender,
            IDBEntityService entityService,
            DemoFAQsRepository fakeService)
        {
            _entityService = entityService;
            _logger = logger;
            _fakeService = fakeService;
            _emailSender = emailSender;
        }

        /// <summary>
        /// ask question 
        /// </summary>
        /// <param name="clientInfoId">client info id</param>
        /// <param name="userId">user id</param>
        /// <param name="topic">question topic</param>
        /// <param name="body">question body</param>
        /// <returns>result</returns>
        public async Task<bool> AskQuestionAsync(int clientInfoId, int userId, string topic, string body)
        {
            return await _emailSender.SendUserQuestionAsync(clientInfoId, userId, topic, body);
        }

        /// <summary>
        /// gets FAQ list
        /// </summary>
        /// <returns>FAQ list</returns>
        public async Task<IEnumerable<string>> GetFAQsAsync()
        {
            return _fakeService.FAQs;
        }

        /// <summary>
        /// gets FAQ answer
        /// </summary>
        /// <param name="faq">FAQ</param>
        /// <returns>answer</returns>
        public async Task<string> GetFAQAsync(string faq)
        {
            return _fakeService.GetAnswer(faq);
        }
    }
}
