using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.ViewModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Password { get; set; }

        public string Account  { get; set; }
        public string SerialNum { get; set; }
        //public string Country  { get; set; }
        //public string Region  { get; set; }
        //public string City  { get; set; }
        //public string Street { get; set; }
        //public string House { get; set; }
    }
}
