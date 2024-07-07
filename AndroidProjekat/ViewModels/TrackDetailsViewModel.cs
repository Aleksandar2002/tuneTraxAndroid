using AndroidProjekat.Business.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AndroidProjekat.ViewModels
{
    internal class TrackDetailsViewModel
    {
        public TrackDto? Track { get; set; }

        public TrackDetailsViewModel() { }

        public ICommand GoBackCommand { get; }
        
        public void GoBackFN()
        {
            App.Current.MainPage = new UserPage();
        }

        public TrackDetailsViewModel(int id)
        {
            FindTrack(id);

            GoBackCommand = new Command(GoBackFN);
        }


        public void FindTrack(int id)
        {
            RestRequest request = new RestRequest("tracks/" + id);


            var response = Api.Client.Execute<TrackDto>(request);

            if (response.IsSuccessful)
            {
                var trackFromServer = response.Data;
                this.Track = trackFromServer;
            }

            OnPropertyChanged(nameof(Track));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
