using Ankor.Model;
using Ankor.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.Interfaces
{
    /// <summary>
    /// IHelpService
    /// </summary>
    public interface IHelpService
    {
        /// <summary>
        /// ask question 
        /// </summary>
        /// <param name="clientInfoId">client info id</param>
        /// <param name="userId">user id</param>
        /// <param name="topic">question topic</param>
        /// <param name="body">question body</param>
        /// <returns>result</returns>
        Task<bool> AskQuestionAsync(int clientInfoId, int userId, string topic, string body);

        /// <summary>
        /// gets FAQ list
        /// </summary>
        /// <returns>FAQ list</returns>
        Task<IEnumerable<string>> GetFAQsAsync();

        /// <summary>
        /// gets FAQ answer
        /// </summary>
        /// <param name="faq">FAQ</param>
        /// <returns>answer</returns>
        Task<string> GetFAQAsync(string faq);
    }
}
