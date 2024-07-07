using AndroidProjekat.Business.DTO;
using System.Windows.Input;

namespace AndroidProjekat.Components;

public partial class TrackCard : ContentView
{


    public static BindableProperty TitleProperty=
        BindableProperty.Create(nameof(Title), typeof(string), typeof(TrackCard), "/", BindingMode.TwoWay);    
    
    public static BindableProperty CoverImageProperty =
        BindableProperty.Create(nameof(CoverImage), typeof(string), typeof(TrackCard), "/", BindingMode.TwoWay);    
    
    public static BindableProperty TrackIdProperty =
        BindableProperty.Create(nameof(TrackId), typeof(int), typeof(TrackCard), 0, BindingMode.TwoWay);    

    public static BindableProperty ReleaseDateProperty =
        BindableProperty.Create(nameof(ReleaseDate), typeof(DateTime), typeof(TrackCard), DateTime.UtcNow, BindingMode.TwoWay);

    public static BindableProperty ArtistsProperty=
    BindableProperty.Create(nameof(Artists), typeof(List<ArtistDto>), typeof(TrackCard), null, BindingMode.TwoWay);    
    
    public static BindableProperty GenresProperty =
    BindableProperty.Create(nameof(Genres), typeof(List<GenreDto>), typeof(TrackCard), null, BindingMode.TwoWay);    
    
    public static BindableProperty AlbumProperty =
    BindableProperty.Create(nameof(Album), typeof(AlbumDto), typeof(TrackCard), null, BindingMode.TwoWay);

    public TrackCard()
    {
        InitializeComponent();

        //NavigateToDetailsPage = new Command(NavigateToDetailedTrackPage);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string CoverImage
    { 
        get => (string)GetValue(CoverImageProperty);
        set => SetValue(CoverImageProperty, value);
    }

    public int TrackId
    {
        get => (int)GetValue(TrackIdProperty);
        set => SetValue(TrackIdProperty, value);
    }

    public DateTime ReleaseDate
    {
        get => (DateTime)GetValue(ReleaseDateProperty);
        set => SetValue(ReleaseDateProperty, value);
    }
    
    public List<ArtistDto> Artists
    {
        get => (List<ArtistDto>)GetValue(ArtistsProperty);
        set => SetValue(ArtistsProperty, value);
    }
    
    public List<GenreDto> Genres
    {
        get => (List<GenreDto>)GetValue(GenresProperty);
        set => SetValue(GenresProperty, value);
    }

    public AlbumDto Album
    {
        get => (AlbumDto)GetValue(AlbumProperty);
        set => SetValue(AlbumProperty, value);
    }

    //public ICommand NavigateToDetailsPage { get; }

    public async void NavigateToDetailedTrackPage()
    {
        //await Navigation.PushAsync(new TrackDetails(TrackId));
        App.Current.MainPage = new TrackDetails(TrackId);
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        NavigateToDetailedTrackPage();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PushModalAsync(new AddToPlaylist(TrackId));
    }

    //public string? GetGenres => string.Join(", ", Genres);

    //public string? GetArtists => string.Join(", ", Artists);
}