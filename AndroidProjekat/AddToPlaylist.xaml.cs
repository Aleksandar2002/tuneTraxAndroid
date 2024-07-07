using AndroidProjekat.ViewModels;

namespace AndroidProjekat;

public partial class AddToPlaylist : ContentPage
{
	public AddToPlaylist()
	{
		InitializeComponent();
	}

	public AddToPlaylist(int id)
	{
		InitializeComponent();
        BindingContext = new AddToPlaylistViewModel(id);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

        App.Current.MainPage = new UserPage();
    }
}