using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.Interfaces
{
    /// <summary>
    /// email sender interface
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// sends temp password
        /// </summary>
        /// <param name="clientInfoId">client info id</param>
        /// <param name="email">email</param>
        /// <param name="password">temp password</param>
        /// <returns>result</returns>
        Task<bool> SendTempPaswordAsync(int clientInfoId, string email, string password);

        /// <summary>
        /// sends registration check code
        /// </summary>
        /// <param name="clientInfoId">client info id</param>
        /// <param name="email">email</param>
        /// <param name="checkCode">check code</param>
        /// <returns>result</returns>
        Task<bool> SendCheckCodeAsync(int clientInfoId, string email, string checkCode);

        /// <summary>
        /// sends user question
        /// </summary>
        /// <param name="clientInfoId">client info id</param>
        /// <param name="userId">user id</param>
        /// <param name="title">question title</param>
        /// <param name="body">question body</param>
        /// <returns>result</returns>
        Task<bool> SendUserQuestionAsync(int clientInfoId, int userId, string title, string body);
    }
}
