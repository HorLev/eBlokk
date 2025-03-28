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
        UserSession.Username = string.Empty;
        UserSession.QRCode = string.Empty;

        await Navigation.PushAsync(new LoginPage());
        await DisplayAlert("Kijelentkez�s", "Sikeresen Kijelentkezt�l", "Ok");
        Navigation.RemovePage(this);
    }

    private void OnLanguageSelectionClicked(object sender, EventArgs e)
    {

    }
}