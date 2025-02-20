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

            // Ellenõrizzük, hogy mindkét mezõ kitöltött-e
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Hiba", "Felhasználónév és jelszó megadása kötelezõ", "OK");
                return;
            }

            // Jelszó titkosítása (SHA256)
            var hashedPassword = HashPassword(password);

            // Adatok ellenõrzése az adatbázisban
            var dbService = new DatabaseService();
            bool success = await dbService.ValidateUser(username, hashedPassword);
            bool success2 = await dbService.ValidateUser(username, password);

            if (success || success2)
            {
                UserSession.Username = username;
                // Lekérjük a bejelentkezett felhasználó QR-kódját
                var qrCode = await dbService.GetUserQRCode(username);
                if (!string.IsNullOrEmpty(qrCode))
                {
                    UserSession.QRCode = qrCode; // QR-kód mentése a session-be
                }

                await DisplayAlert("Siker", $"Üdvözöllek, {username}!", "OK");
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

        // Jelszó titkosítása SHA-256 segítségével
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
    }
}
