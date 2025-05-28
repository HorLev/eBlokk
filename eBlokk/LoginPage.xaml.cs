using eBlokk.Model;
using System;
using System.Security.Cryptography;
using System.Text;

namespace eBlokk
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        public async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Hiba", "Felhasználónév és jelszó megadása kötelezõ", "OK");
                return;
            }

            var hashedPassword = HashPassword(password);

            var dbService = new DatabaseService();
            bool success = await dbService.ValidateUser(username, hashedPassword);
            bool success2 = await dbService.ValidateUser(username, password);

            if (success || success2)
            {
                UserSession.Username = username;
                var qrCode = await dbService.GetUserQRCode(username);
                if (!string.IsNullOrEmpty(qrCode))
                {
                    UserSession.QRCode = qrCode;
                }

                await DisplayAlert("Sikeres Bejelentkezés", $"Üdvözöllek, {username}!", "OK");
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Hiba", "Helytelen felhasználónév vagy jelszó", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
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
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool Exit = await Application.Current.MainPage.DisplayAlert(
                    "Kilépés",
                    "Biztosan ki akarsz lépni?",
                    "Igen", "Nem");

                if (Exit)
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            });
            return true;
        }
    }
}
