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
    public class AddToPlaylistViewModel
    {
        public int TrackId { get; set; }
        public ObservableCollection<SelectDto> Playlists { get; set; } = new ObservableCollection<SelectDto>();

        public MProp<SelectDto> SelectedPlaylist{ get; set; } = new MProp<SelectDto>();

        public AddToPlaylistViewModel(int id )
        {
            TrackId = id;
            LoadPlaylists();
            SaveTrackToPlaylist = new Command(SaveTrackToPlaylistFN);
        }

        public ICommand SaveTrackToPlaylist { get; }

        public void SaveTrackToPlaylistFN()
        {
            if(SelectedPlaylist.Value == null)
            {
                return;
            }

            RestRequest request = new RestRequest("playlists/" + SelectedPlaylist.Value.Id + "/addTracks" , Method.Post);

            request.AddBody(new { Tracks  = new List<int> { TrackId } });

            var response = Api.Client.Execute<PlaylistPagedResponseDto>(request);

            if (response.IsSuccessful)
            {
                App.Current.MainPage = new UserPage();
            }
        }

        public AddToPlaylistViewModel() {
            SaveTrackToPlaylist = new Command(SaveTrackToPlaylistFN);
        }

        public void LoadPlaylists()
        {
            RestRequest request = new RestRequest("playlists");


            var response = Api.Client.Execute<PlaylistPagedResponseDto>(request);

            if (response.IsSuccessful)
            {
                var playlists = response.Data.Data ;
                this.Playlists.Clear();
                foreach (var playlist in playlists)
                {
                    Playlists.Add(new SelectDto
                    {
                        Id = playlist.Id,
                        Name = playlist.Name
                    });
                }
                GetTrackPlaylist();
            }

            OnPropertyChanged(nameof(Playlists));
        }

        public void GetTrackPlaylist()
        {
            RestRequest request = new RestRequest("tracks/"+ TrackId + "/playlists");


            var response = Api.Client.Execute<List<SelectDto>>(request);

            if (response.IsSuccessful)
            {
                SelectedPlaylist.Value= response.Data.FirstOrDefault();
            }
            OnPropertyChanged(nameof(SelectedPlaylist));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
