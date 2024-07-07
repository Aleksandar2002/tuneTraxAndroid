using AndroidProjekat.Common;

namespace AndroidProjekat
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            var user = SecureStorage.Default.GetUser();

            if (user != null)
            {
                MainPage = new UserPage();
            }
            else
            {
                MainPage = new Login();
            }

            Application.Current.UserAppTheme = AppTheme.Dark;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Width = 500;

            return window;
        }

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //}
    }


}
