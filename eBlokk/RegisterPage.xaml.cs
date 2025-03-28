using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;
using System.Security.Cryptography;
using System.Text;
using eBlokk.Model;

namespace eBlokk
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
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
            return true; // Ezzel letiltod a visszalépést
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string fullName = fullNameEntry.Text;
            string email = emailEntry.Text;
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;
            string confirmPassword = confirmPasswordEntry.Text;

            if (!IsValidEmail(email))
            {
                await DisplayAlert("Hoppá", "Érvénytelen email cím", "OK");
                return;
            }

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Hoppá", "Minden mezõ kitöltése kötelezõ", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Hoppá", "A jelszavak nem egyeznek", "OK");
                return;
            }

            var hashedPassword = HashPassword(password);

            var dbService = new DatabaseService();
            var success = await dbService.AddUserToDatabase(fullName, email, username, hashedPassword);

            if (!success)
            {
                await DisplayAlert("Hoppá", "Hiba történt a regisztráció során  ", "OK");
                return;
            }

            await DisplayAlert("Siker", "Regisztráció sikeres", "OK");

            await Navigation.PushAsync(new LoginPage());
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            return emailRegex.IsMatch(email);
        }

        private void OnEmailEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string email = e.NewTextValue;

            if (!IsValidEmail(email))
            {
                emailEntry.Placeholder = "Érvénytelen email cím";
            }
            else
            {
                emailEntry.Placeholder = "Email cím";
            }
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
    }
}
