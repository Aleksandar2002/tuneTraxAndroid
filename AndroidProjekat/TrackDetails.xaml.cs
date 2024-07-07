using AndroidProjekat.ViewModels;

namespace AndroidProjekat;

public partial class TrackDetails : ContentPage
{

	

	public TrackDetails(int id)
	{
		InitializeComponent();
		BindingContext = new TrackDetailsViewModel(id);
	}

}