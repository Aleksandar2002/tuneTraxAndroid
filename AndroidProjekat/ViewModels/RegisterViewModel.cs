using AndroidProjekat.Business.DTO;
using AndroidProjekat.Common;
using AndroidProjekat.Validators;
using FluentValidation.Results;
using Microsoft.Maui.ApplicationModel.Communication;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AndroidProjekat.ViewModels
{
    public class RegisterViewModel: INotifyPropertyChanged
    {
        public MProp<string> Email { get; set; } = new MProp<string>();
        public MProp<string> Username{ get; set; } = new MProp<string>();
        public MProp<SelectDto> Gender { get; set; } = new MProp<SelectDto>();
        public MProp<SelectDto> Country { get; set; } = new MProp<SelectDto>();
        public MProp<DateOnly> DateOfBirth { get; set; } = new MProp<DateOnly>();
        public MProp<string> Password { get; set; } = new MProp<string>();
        public MProp<bool> ButtonEnabled { get; set; } = new MProp<bool>();
        public MProp<bool> ServerError { get; set; } = new MProp<bool>();

        public ObservableCollection<SelectDto> Genders { get; set; } =  new ObservableCollection<SelectDto>
        {
            new SelectDto
            {
                Id = 1 ,
                Name = "Male"
            },
            new SelectDto
            {
                Id = 2,
                Name = "Female"
            }
        };

        public ObservableCollection<SelectDto> Countries { get; set; } = new ObservableCollection<SelectDto>();

        public RegisterViewModel()
        {
            //Email.Value = "mail@gmail.com";
            RegisterCommand = new Command(Register);

            Email.OnChange = Validate;
            Password.OnChange = Validate;
            Username.OnChange = Validate;
            DateOfBirth.OnChange = Validate;
            Gender.OnChange = Validate;
            Country.OnChange = Validate;
            ButtonEnabled.Value = true;

            Email.Value = "test123@gmail.com";
            Password.Value = "Lozinka123!";
            DateOfBirth.Value = new DateOnly(2002,10,10);
            Username.Value = "Pera123";
            Gender.Value = Genders.First();

            this.GetCountries();
        }

        public void GetCountries()
        {

            RestRequest request = new RestRequest("countries", Method.Get);

            var response = Api.Client.Execute<CountryPagedResponse>(request);
                //var response = Api.Client.Execute<List<PostDto>>(request);


            if (response.IsSuccessful)
            {

                    var countries = response.Data.Data;
                    this.Countries.Clear();
                    this.Countries = new ObservableCollection<SelectDto>(countries);
                    ServerError.Value = false;
                    Country.Value = Countries.First();
            }
            else
            {
                ServerError.Value = true;
            }
        }

        private void Validate()
        {
            RegisterViewModelValidator validator = new RegisterViewModelValidator();

            ValidationResult result = validator.Validate(this);

            if (!result.IsValid)
            {
                //GetError - nas extension metod
                Email.Error = result.GetError("Email");
                Password.Error = result.GetError("Password");
                Username.Error = result.GetError("Username");
                Country.Error = result.GetError("Country");
                Gender.Error = result.GetError("Gender");
                DateOfBirth.Error = result.GetError("DateOfBirth");

                ButtonEnabled.Value = false;
            }
            else
            {
                Email.Error = null;
                Password.Error = null;
                Username.Error = null ;
                Country.Error = null;
                Gender.Error = null;
                DateOfBirth.Error = null;

                ButtonEnabled.Value = true;
            }
        }

        public ICommand RegisterCommand { get; }

        private  void Register()
        {
            //var registerUserData = new RegisterUserDto
            //{
            //    Username = Username.Value,
            //    Password = Password.Value,
            //    Email = Email.Value,
            //    DateOfBirth = DateOfBirth.Value,
            //    Gender = Gender.Value.Id - 1,
            //    CountryId = Country.Value.Id
            //};

            RestRequest request = new RestRequest("users", Method.Post);
            request.AddBody(new
            {
                Username = Username.Value,
                Password = Password.Value,
                Email = Email.Value,
                DateOfBirth = DateOfBirth.Value,
                Gender = Gender.Value.Id - 1,
                CountryId = Country.Value.Id
            });

            var response = Api.Client.Execute(request);

            if (response.IsSuccessful)
            {
                ServerError.Value = false;

                App.Current.MainPage = new Login();
            }
            else
            {
                ServerError.Value = true;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
