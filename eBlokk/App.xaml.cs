namespace eBlokk
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Application.Current.UserAppTheme = AppTheme.Dark;
            //Application.Current.UserAppTheme = AppTheme.Unspecified;

            MainPage = new NavigationPage(new RegisterPage());
        }
    }
}