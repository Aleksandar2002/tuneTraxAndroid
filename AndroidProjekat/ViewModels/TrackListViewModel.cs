using AndroidProjekat.Business.DTO;
using AndroidProjekat.Common;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AndroidProjekat.ViewModels
{
    public class TrackListViewModel
    {
        public MProp<string> Keyword { get; set; } = new MProp<string>();
        public ICommand RefreshPageCommand { get; }
        public ObservableCollection<TrackDto> Tracks { get; set; } = new ObservableCollection<TrackDto>();

        public TrackListViewModel()
        {
            Keyword.OnChange = LoadTracks;



            //AddNewPostCommand = new Command(AddNewItem);
            RefreshPageCommand = new Command(LoadTracks);

            LoadTracks();
        }

        private void LoadTracks()
        {

            string token = SecureStorage.Default.GetUser().Token;

            RestRequest request = new RestRequest("tracks");

            if (!string.IsNullOrEmpty(Keyword.Value))
            {
                request.AddParameter("Title"  ,Keyword.Value );
            }

            var response = Api.Client.Execute<TrackPagedResponse>(request);

            if (response.IsSuccessful)
            {
                var tracks= response.Data.Data;
                this.Tracks.Clear();
                foreach(var track in tracks)
                {
                    Tracks.Add(track);
                }
            }

            OnPropertyChanged(nameof(Tracks));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
