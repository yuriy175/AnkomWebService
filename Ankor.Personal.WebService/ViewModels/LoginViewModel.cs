using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// login via user name or account num
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// login via email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }
    }

    public class ChangePasswordViewModel
    {
        /// <summary>
        /// user id
        /// </summary>
        public uint UserId { get; set; }

        /// <summary>
        /// old password
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// new password
        /// </summary>
        public string NewPassword { get; set; }
    }
}
