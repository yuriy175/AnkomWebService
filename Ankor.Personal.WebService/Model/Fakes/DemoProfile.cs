using Ankor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.Model.Demos
{
    public static class DemoProfile
    {
        private readonly static UserViewModel _profile = new UserViewModel
        {
            Login = Constants.Constants.AnonymousLogin,

            LocalityName = "Санкт-Петербург",
            Street = "Ленинский пр.",
            Location = "д. 32, корп. 1",
            Apartment = "кв. 123",
            Zip = "192356",

            FirstName = "Иван",
            MiddleName = "Иванович",
            LastName = "Иванов",
            AccountNum = "1234567890",
            Email = "ivanov@mail.ru",
            Phone = "+7 (921) 777-77-77",
            AllowEmailNotifications = true,
        };

        public static UserViewModel Profile { get { return _profile; } }
        public static bool ChangeProfile((string Email, string Phone) profile)
        {
            return true;
        }
    }
}
  
