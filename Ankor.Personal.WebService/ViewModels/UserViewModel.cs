using Ankor.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.ViewModels
{
    public struct UserViewModel
    {
        public string Login { get; set; }

        public string LocalityName { get; set; }
        public string Street { get; set; }
        public string Location { get; set; }
        public string Apartment { get; set; }
        public string Zip { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AccountNum { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool AllowEmailNotifications { get; set; }

        public UserViewModel(Account account)
        {
            Login = account.loginName;

            LocalityName = account.Address?.locality_Name;
            Street = account.Address?.street;
            Location = account.Address?.location;
            Apartment = account.Address?.apartment;
            Zip = account.Address?.zipcode;

            FirstName = account.Person?.firstName;
            MiddleName = account.Person?.middleName;
            LastName = account.Person?.lastName;
            AccountNum = account.account_num;
            Email = account.Email;
            Phone = account.Phone;
            AllowEmailNotifications = account.AllowEmailNotifications;
        }
    }
}
