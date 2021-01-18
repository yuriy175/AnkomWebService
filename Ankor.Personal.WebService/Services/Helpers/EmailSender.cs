using Ankor.Personal.WebService.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.Services
{
    /// <summary>
    /// email sender
    /// </summary>
    public class EmailSender :IEmailSender
    {
        //private const string SmtpAddress = "smtp.mail.ru";
        //private const string FromAddress = "yuriy_nv@mail.ru";
        //private const string FromPassword = "qweqwe";

        private readonly ILogger _logger;
        private readonly IDBEntityService _entityService;

        public EmailSender(
            ILogger logger,
            IDBEntityService entityService
            )
        {
            _entityService = entityService;
            _logger = logger;
        }

        /// <summary>
        /// sends temp password
        /// </summary>
        /// <param name="clientInfoId">client info id</param>
        /// <param name="email">email</param>
        /// <param name="password">temp password</param>
        /// <returns>result</returns>
        public async Task<bool> SendTempPaswordAsync(int clientInfoId, string email, string password)
        {
            var clientInfo = await _entityService.GetClientInfoAsync(clientInfoId);
            if (clientInfo == null)
            {
                _logger.Error($"No client info for {clientInfoId}");
                return false;
            }

            var emailMessage = CreateMessage(clientInfo.Email, email, "Новый пароль", $"Войдите с паролем {password}");
            await SendEmail(clientInfo.Smtp, clientInfo.Email, clientInfo.Password, emailMessage);

            return true;
        }

        /// <summary>
        /// sends registration check code
        /// </summary>
        /// <param name="clientInfoId">client info id</param>
        /// <param name="email">email</param>
        /// <param name="checkCode">check code</param>
        /// <returns>result</returns>
        public async Task<bool> SendCheckCodeAsync(int clientInfoId, string email, string checkCode)
        {
            var clientInfo = await _entityService.GetClientInfoAsync(clientInfoId);
            if (clientInfo == null)
            {
                _logger.Error($"No client info for {clientInfoId}");
                return false;
            }

            var emailMessage = CreateMessage(clientInfo.Email, email, "Код регистрации", $"Код регистрации {checkCode}");
            await SendEmail(clientInfo.Smtp, clientInfo.Email, clientInfo.Password, emailMessage);

            return true;
        }

        /// <summary>
        /// sends user question
        /// </summary>
        /// <param name="clientInfoId">client info id</param>
        /// <param name="userId">user id</param>
        /// <param name="title">question title</param>
        /// <param name="body">question body</param>
        /// <returns>result</returns>
        public async Task<bool> SendUserQuestionAsync(int clientInfoId, int userId, string title, string body)
        {
            var clientInfo = await _entityService.GetClientInfoAsync(clientInfoId);
            if (clientInfo == null)
            {
                _logger.Error($"No client info for {clientInfoId}");
                return false;
            }

            var email = string.IsNullOrEmpty(clientInfo.QuestionEmail) ? clientInfo.Email : clientInfo.QuestionEmail;

            var emailMessage = CreateMessage(clientInfo.Email, email, $"Вопрос от абонента {userId}: {title}", $"{body}");
            await SendEmail(clientInfo.Smtp, clientInfo.Email, clientInfo.Password, emailMessage);

            return true;
        }

        private MimeMessage CreateMessage(string fromAddress, string email, string subject, string body)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", fromAddress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body,
            };

            return emailMessage;
        }

        private async Task SendEmail(string smtpAddress, string fromAddress, string fromPassword, MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpAddress, 25, false);
                await client.AuthenticateAsync(fromAddress, fromPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
