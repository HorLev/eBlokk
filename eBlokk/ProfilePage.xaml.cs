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
        // Navig�l�s az �j oldalra
        await Navigation.PushAsync(new MyReceiptsPage());
    }
    private async void MainPageClicked(object sender, EventArgs e)
    {
        // Navig�l�s az �j oldalra
        await Navigation.PushAsync(new MainPage());
    }
    private async void OnLogoutClicked(object sender, EventArgs e)
    {

        Device.BeginInvokeOnMainThread(async () =>
        {
            bool logOut = await Application.Current.MainPage.DisplayAlert(
                "Kijelentkez�s",
                "Biztosan ki akarsz jelentkezni?",
                "Igen", "Nem");

            if (logOut)
            {
                // Kijelentkeztet�s 
                UserSession.Username = string.Empty;
                UserSession.QRCode = string.Empty;

                await Application.Current.MainPage.DisplayAlert("Kijelentkez�s", "Sikeresen kijelentkezt�l", "Ok");

                // �j f�oldal be�ll�t�sa 
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        });



    }



}
