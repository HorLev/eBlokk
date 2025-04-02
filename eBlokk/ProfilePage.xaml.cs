using eBlokk.Model;

namespace eBlokk;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }
    private async void OnMyReceiptsPageClicked(object sender, EventArgs e)
    {
        // Navigálás az új oldalra
        await Navigation.PushAsync(new MyReceiptsPage());
    }
    private async void MainPageClicked(object sender, EventArgs e)
    {
        // Navigálás az új oldalra
        await Navigation.PushAsync(new MainPage());
    }
    private async void OnLogoutClicked(object sender, EventArgs e)
    {

        Device.BeginInvokeOnMainThread(async () =>
        {
            bool logOut = await Application.Current.MainPage.DisplayAlert(
                "Kijelentkezés",
                "Biztosan ki akarsz jelentkezni?",
                "Igen", "Nem");

            if (logOut)
            {
                // Kijelentkeztetés 
                UserSession.Username = string.Empty;
                UserSession.QRCode = string.Empty;

                await Application.Current.MainPage.DisplayAlert("Kijelentkezés", "Sikeresen kijelentkeztél", "Ok");

                // Új fõoldal beállítása 
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        });



    }



}
