using AndroidProjekat.Business.DTO;
using AndroidProjekat.Common;
using Microsoft.Maui;
using RestSharp;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AndroidProjekat.Components;

public partial class PlaylistCard : ContentView
{

    public ObservableCollection<TrackDto>? TracksFromServer { get; set; } = new ObservableCollection<TrackDto>();

    public static BindableProperty NameProperty =
    BindableProperty.Create(nameof(Name), typeof(string), typeof(PlaylistCard), "/", BindingMode.TwoWay);

    public static BindableProperty AccessLevelProperty =
BindableProperty.Create(nameof(AccessLevel), typeof(int), typeof(PlaylistCard), 0, BindingMode.TwoWay);


    public static BindableProperty PlaylistIdProperty =
BindableProperty.Create(nameof(PlaylistId), typeof(int), typeof(PlaylistCard), 0, BindingMode.TwoWay);

    public static BindableProperty TracksProperty =
BindableProperty.Create(nameof(Tracks), typeof(List<int>), typeof(PlaylistCard), null, BindingMode.TwoWay, propertyChanged: OnTracksPropertyChanged);

    private static void OnTracksPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var playlistCard= (PlaylistCard)bindable;
        playlistCard.LoadTracks(newValue as List<int>);
    }

    public string Name
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    public int AccessLevel
    {
        get => (int)GetValue(AccessLevelProperty);
        set => SetValue(AccessLevelProperty, value);
    }


    public int PlaylistId
    {
        get => (int)GetValue(PlaylistIdProperty);
        set => SetValue(PlaylistIdProperty, value);
    }

    public List<int> Tracks
    {
        get => (List<int>)GetValue(TracksProperty);
        set
        {
             SetValue(TracksProperty, value);
            //LoadTracks();
        }
    }

    public string AccessLevelText
    {
        get
        {
            if(AccessLevel == 0)
            {
                return "Public";
            }
            return "Private";
        }
    }

    public void LoadTracks(List<int> tracks)
    {
        //if(tracks == null || tracks.Count == 0)
        //{
        //    return;
        //}


        if(tracks == null || tracks.Count == 0)
        {
            TracksFromServer = [];
        }else
        {
            RestRequest request = new RestRequest("tracks");

            request.AddParameter("Ids", $"[{string.Join(",", tracks)}]");

            var response = Api.Client.Execute<TrackPagedResponse>(request);

            if (response.IsSuccessful)
            {
                var tracksS = response.Data.Data;
                this.TracksFromServer.Clear();
                foreach (var track in tracksS)
                {
                    track.RemoveTrackCommand = new Command<int>(RemoveTrackFN);
                    TracksFromServer.Add(track);
                }
            }
        }

        OnPropertyChanged(nameof(TracksFromServer));
    }

    public PlaylistCard()
	{
		InitializeComponent();
	}

    private  void Button_Clicked(object sender, EventArgs e)
    {
        RestRequest request = new RestRequest("playlists/" + PlaylistId, Method.Delete);

        Api.Client.Execute(request);

        //DeletePlaylistEvent?.Invoke(this, PlaylistId);

    }
    //public delegate void HandleEvent(object sender, int id);
    //public event HandleEvent DeletePlaylistEvent;



    public void RemoveTrackFN(int id)
    {
        RestRequest request = new RestRequest("playlists/" + PlaylistId + "/deleteTracks", Method.Post);

        request.AddBody(new { tracks = new List<int> { id } });

        var response = Api.Client.Execute(request);

        if (response.IsSuccessful)
        {
            
        var track = Tracks.Where(x=> x== id).FirstOrDefault();

        if(track == 0)
        {
            return;
        }
        Tracks.Remove(track);

        LoadTracks(Tracks);
        }
    }
}