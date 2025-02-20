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

            // Ellen�rizz�k, hogy mindk�t mez� kit�lt�tt-e
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Hiba", "Felhaszn�l�n�v �s jelsz� megad�sa k�telez�", "OK");
                return;
            }

            // Jelsz� titkos�t�sa (SHA256)
            var hashedPassword = HashPassword(password);

            // Adatok ellen�rz�se az adatb�zisban
            var dbService = new DatabaseService();
            bool success = await dbService.ValidateUser(username, hashedPassword);
            bool success2 = await dbService.ValidateUser(username, password);

            if (success || success2)
            {
                UserSession.Username = username;
                // Lek�rj�k a bejelentkezett felhaszn�l� QR-k�dj�t
                var qrCode = await dbService.GetUserQRCode(username);
                if (!string.IsNullOrEmpty(qrCode))
                {
                    UserSession.QRCode = qrCode; // QR-k�d ment�se a session-be
                }

                await DisplayAlert("Siker", $"�dv�z�llek, {username}!", "OK");
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Hiba", "Helytelen felhaszn�l�n�v vagy jelsz�", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        // Jelsz� titkos�t�sa SHA-256 seg�ts�g�vel
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
