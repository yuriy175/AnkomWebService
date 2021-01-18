using Ankor.Model;
using Ankor.Model.DAL;
using Ankor.Personal.WebService.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ankor.Personal.WebService.Constants;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Ankor.ViewModels;

namespace Ankor.Personal.WebService.Services
{
    /// <summary>
    /// IAuthorizingService implementation
    /// </summary>
    public class AuthorizingService : IAuthorizingService
    {
        private const string TempPassword = "Ankor+!";
        private const string TempCheckCode = "5791";

        private readonly ILogger _logger;
        private readonly IDBEntityService _entityService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger">logger </param>
        /// <param name="entityService">db service</param>
        /// <param name="passwordHasher">password hash helper</param>
        /// <param name="emailSender">email sender service</param>
        public AuthorizingService(
            ILogger logger,
            IDBEntityService entityService,
            IPasswordHasher passwordHasher,
            IEmailSender emailSender
            )
        {
            _entityService = entityService;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _emailSender = emailSender;
        }

        /// <summary>
        /// logins a user
        /// </summary>
        /// <param name = "userName" > user name</param>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <returns>token info</returns>
        public async Task<(string Token, string UserName, int UserId, string AccountNum)?> LoginAsync(string userName, string email, string password)
        {
            var account = await _entityService.GetAccountAsync(userName, email, password);
            if (account == null)
            {
                _logger.Error($"No account for {userName} {password}");
                return null;
            }

            var hash = account.hashCode;
            if (!string.IsNullOrEmpty(hash))
            {
                var result = await _passwordHasher.VerifyPassword(password, hash);
                if (!result)
                {
                    _logger.Error($"Password doesn't match {userName}");
                    return null;
                }
            }

            var token = GetJwt((await GetIdentity(account))?.Claims);
            if (token == null)
            {
                _logger.Error($"No token for {userName}");
                return null;
            }

            return (token, userName, account.id, account.account_num);
        }

        /// <summary>
        /// registers a user
        /// </summary>
        /// <param name = "userName" > user name</param>
        /// <param name="password">password</param>
        /// <returns>token info</returns>
        //public async Task<(string Token, string UserName, int UserId)?> RegisterAsync(string userName, string password)
        //{
        //    return null;
        //}

        /// <summary>
        /// registers a user
        /// </summary>
        /// <param name="regViewModel">register view model</param>
        /// <returns>result</returns>
        public async Task<bool> RegisterAsync(RegisterViewModel regViewModel)
        {
            int.TryParse(regViewModel.SerialNum, out int factoryId);
            var passwordHash = await _passwordHasher.HashPassword(regViewModel.Password);

            return await _entityService.ActivateAndUpdateAccountAsync(regViewModel.Account, factoryId, 
                (regViewModel.Login, regViewModel.Email, regViewModel.Phone, passwordHash));
        }

        /// <summary>
        /// logins an anonymous user
        /// </summary>
        /// <returns>token info</returns>
        public async Task<(string Token, string UserName, int UserId, string AccountNum)?> LoginAnonymousAsync()
        {
            var account = new Account { id = 1, loginName = Constants.Constants.AnonymousLogin, account_num = "123456788" };

            var token = GetJwt((await GetIdentity(account, true))?.Claims);
            if (token == null)
            {
                _logger.Error($"No token for anonymous");
                return null;
            }

            return (token, Constants.Constants.AnonymousLogin, account.id, account.account_num);
        }

        /// <summary>
        /// sends registration check code
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="clientInfoId">client info id</param>
        /// <returns>check code</returns>
        public async Task<string> SendCheckCodeAsync(string email, int clientInfoId)
        {
            return await _emailSender.SendCheckCodeAsync(clientInfoId, email, TempCheckCode) ? TempCheckCode : null;
        }

        /// <summary>
        /// sends temp password
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="clientInfoId">client info id</param>
        /// <returns>result</returns>
        public async Task<bool> SendTempPasword(string email, int clientInfoId)
        {
            var result = await _emailSender.SendTempPaswordAsync(clientInfoId, email, TempPassword);
            if (!result)
            {
                _logger.Error($"Couldn't send temp password to {email}");
                return false;
            }

            var hash = await _passwordHasher.HashPassword(TempPassword);
            if (!result)
            {
                _logger.Error($"Couldn't get hash password {email}");
                return false;
            }

            result = await _entityService.SetPasswordByEmailAsync(email, hash);
            if (!result)
            {
                _logger.Error($"Couldn't update hash {hash}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Changes a user password
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="oldPassword">old password</param>
        /// <param name="newPassword">new password</param>
        /// <returns>result</returns>
        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var account = await _entityService.GetAccountAsync(userId);
            if (account == null)
            {
                _logger.Error($"No account for {userId}");
                return false;
            }

            var result = false;

            var hash = account.hashCode;
            if (!string.IsNullOrEmpty(hash))
            {
                result = await _passwordHasher.VerifyPassword(oldPassword, hash);
                if (!result)
                {
                    _logger.Error($"Password doesn't match {userId}");
                    return false;
                }
            }

            var newHash = await _passwordHasher.HashPassword(newPassword);
            result = await _entityService.SetPasswordByEmailAsync(account.Email, newHash);
            if (!result)
            {
                _logger.Error($"Couldn't update hash {hash}");
                return false;
            }

            return true;
        }

        private async Task<(string Name, IEnumerable<Claim> Claims)?> GetIdentity(Account account , bool isAnonym = false)
        {
            if (account != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, account.loginName),
                    new Claim(Constants.Constants.AnonymousClaimType, isAnonym.ToString()),
                };
                return (account.loginName, claims);
            }

            return null;
        }

        private string GetJwt(IEnumerable<Claim> claims)
        {
            if (claims == null)
                return null;

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
