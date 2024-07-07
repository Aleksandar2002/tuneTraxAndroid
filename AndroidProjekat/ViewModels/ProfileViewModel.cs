using AndroidProjekat.Business.DTO;
using AndroidProjekat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AndroidProjekat.ViewModels
{
    public class ProfileViewModel
    {
        public UserDto User { get; set; }
        public ProfileViewModel()
        {
            var user = SecureStorage.Default.GetUser();
            User = user;

            LogoutCommand = new Command(Logout);
        }

        public ICommand LogoutCommand { get; }

        public void Logout()
        {
            SecureStorage.Default.Remove("token");
            Api.ClearAuthorizationHeader();
            App.Current.MainPage = new Login();
        }
    }
}
