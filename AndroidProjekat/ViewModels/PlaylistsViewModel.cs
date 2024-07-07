using AndroidProjekat.Business.DTO;
using AndroidProjekat.Common;
using Microsoft.Maui;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AndroidProjekat.ViewModels
{
    public class PlaylistsViewModel
    {
        public ObservableCollection<PlaylistDto> Playlists { get; set; } = new ObservableCollection<PlaylistDto>();

        public PlaylistsViewModel()
        { 

            LoadPlaylists();
            Refresh = new Command(RefreshPage);
            AddNewPlaylist = new Command(AddPlaylist);
        }

        public void LoadPlaylists()
        {
            string token = SecureStorage.Default.GetUser().Token;

            RestRequest request = new RestRequest("playlists");


            var response = Api.Client.Execute<PlaylistPagedResponseDto>(request);

            if (response.IsSuccessful)
            {
                var playlists = response.Data.Data;
                this.Playlists.Clear();
                foreach (var track in playlists)
                {
                    Playlists.Add(track);
                }
            }

            OnPropertyChanged(nameof(Playlists));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshPage()
        {
            LoadPlaylists();
        }

        public ICommand Refresh { get; }

        public MProp<string> PlaylistName { get; set; } = new MProp<string>();

        public ICommand AddNewPlaylist { get; }

        public void AddPlaylist()
        {
            if (string.IsNullOrEmpty(PlaylistName.Value))
            {
                return;
            }

            RestRequest request = new RestRequest("playlists", Method.Post);

            request.AddBody(new
            {
                Name = PlaylistName.Value,
                AccessLevel = 0
            });

            var response = Api.Client.Execute(request);
            if (response.IsSuccessful)
            {
                LoadPlaylists();
            }
        }
    }
}
