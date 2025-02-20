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
    private async void Button_Clicked(object sender, EventArgs e)
    {
        UserSession.Username = null;
        UserSession.QRCode = null;

        await Navigation.PushAsync(new LoginPage());

        Navigation.RemovePage(this);
    }
}