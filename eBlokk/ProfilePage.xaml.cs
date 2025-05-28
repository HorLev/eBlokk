using eBlokk.Model;
using System.Text;
using System.Security.Cryptography;

namespace eBlokk;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }
    private async void OnMyReceiptsPageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MyReceiptsPage());
    }
    private async void MainPageClicked(object sender, EventArgs e)
    {
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
                UserSession.Username = string.Empty;
                UserSession.QRCode = string.Empty;

                await Application.Current.MainPage.DisplayAlert("Kijelentkezés", "Sikeresen kijelentkeztél", "Ok");

                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        });



    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte byteData in bytes)
            {
                builder.Append(byteData.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    private async void OnDeleteAccountClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Fiók törlése", "Biztosan törölni szeretnéd a fiókodat?", "Igen", "Nem");

        if (confirm)
        {
            var dbService = new DatabaseService();
            bool success = await dbService.DeleteUserAsync(UserSession.Username);

            if (success)
            {
                await DisplayAlert("Fiók törölve", "A fiókodat sikeresen töröltük.", "OK");

                UserSession.Username = string.Empty;
                UserSession.QRCode = string.Empty;

                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                await DisplayAlert("Hiba", "Nem sikerült törölni a fiókodat.", "OK");
            }
        }
    }

    private async void OnGyikClicked(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("http://eblokk.nhely.hu/pages/GYIK.html");
    }

    private async void OnRateClicked(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("http://eblokk.nhely.hu/index.html");
    }

    private async void OnAboutClicked(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("http://eblokk.nhely.hu/pages/about.html");
    }

    private async void OnNotificationClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Értesítések", "Nincs új értesítés", "OK");
    }
}
