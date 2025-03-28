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
        UserSession.Username = string.Empty;
        UserSession.QRCode = string.Empty;

        await Navigation.PushAsync(new LoginPage());
        await DisplayAlert("Kijelentkezés", "Sikeresen Kijelentkeztél", "Ok");
        Navigation.RemovePage(this);
    }

    private void OnLanguageSelectionClicked(object sender, EventArgs e)
    {

    }
}