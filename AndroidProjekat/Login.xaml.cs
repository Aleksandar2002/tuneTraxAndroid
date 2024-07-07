namespace AndroidProjekat;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new Register();
    }
}