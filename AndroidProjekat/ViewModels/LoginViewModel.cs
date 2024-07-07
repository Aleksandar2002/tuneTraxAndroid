using AndroidProjekat.Business.DTO;
using AndroidProjekat.Common;
using AndroidProjekat.Validators;
using FluentValidation.Results;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AndroidProjekat.ViewModels
{
    public class LoginViewModel
    {
        public MProp<string> Email { get; set; } = new MProp<string>();
        public MProp<string> Password { get; set; } = new MProp<string>();
        public MProp<bool> ButtonEnabled { get; set; } = new MProp<bool>();
        public MProp<bool> InvalidCredentials { get; set; } = new MProp<bool>();

        public LoginViewModel()
        {
            //Email.Value = "mail@gmail.com";
            LoginCommand = new Command(Login);

            Email.OnChange = Validate;
            Password.OnChange = Validate;
            ButtonEnabled.Value = true;

            Email.Value = "admin@gmail.com";
            Password.Value = "admin123A";
        }

        private void Validate()
        {
            LoginViewModelValidator validator = new LoginViewModelValidator();

            ValidationResult result = validator.Validate(this);

            if (!result.IsValid)
            {
                Email.Error = result.GetError("Email");
                Password.Error = result.GetError("Password");
            }
            else
            {
                Email.Error = null;
                Password.Error = null;

                ButtonEnabled.Value = true;
            }
        }

        public ICommand LoginCommand { get; }

        private async void Login()
        {
            string email = Email.Value;
            string password = Password.Value;

            RestRequest request = new RestRequest("auth", Method.Post);
            request.AddJsonBody(new { email, password });

             RestResponse<TokenDto> response = Api.Client.Execute<TokenDto>(request);

            if (response.IsSuccessful)
            {


                Task t = SecureStorage.Default.SetAsync("token", response.Data.Token);


                await t;

                UserDto u = SecureStorage.Default.GetUser();

                InvalidCredentials.Value = false;


                App.Current.MainPage = new UserPage();
            }
            else
            {
                InvalidCredentials.Value = true;
            }
        }
    }
}
