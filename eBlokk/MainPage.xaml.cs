using eBlokk;
using System.Diagnostics;
using eBlokk.Model;

namespace eBlokk
{
    public partial class MainPage : ContentPage
    {
        public string Username => UserSession.Username ?? "Vendég";
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public string Greeting
        {
            get
            {
                var hour = DateTime.Now.Hour;
                var random = new Random();

                string[] morningGreetings =
                {
                    "Helló, {0}!",
                    "Jó reggelt, {0}!",
                    "Készen állsz a mai napra, {0}?",
                    "Friss nap, friss blokkok, {0}!"
                };

                string[] afternoonGreetings =
                {
                    "Mi a helyzet, {0}?",
                    "Remélem jól telik a napod, {0}!",
                    "Blokkokra fel, {0}!",
                    "Pörög még a nap, {0}?"
                };

                string[] eveningGreetings =
                {
                    "Lazulj, {0}, megérdemled!",
                    "Pihenj egyet, {0}!",
                    "Esti blokk-nézés? Jöhet, {0}!",
                    "Relax mód ON, {0}!"
                };

                string selectedGreeting;

                if (hour < 12)
                    selectedGreeting = morningGreetings[random.Next(morningGreetings.Length)];
                else if (hour < 18)
                    selectedGreeting = afternoonGreetings[random.Next(afternoonGreetings.Length)];
                else
                    selectedGreeting = eveningGreetings[random.Next(eveningGreetings.Length)];

                return string.Format(selectedGreeting, Username);
            }
        }


        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool LogOut = await Application.Current.MainPage.DisplayAlert(
                    "Kijelentkezés",
                    "Biztosan ki akarsz jelentkezni?",
                    "Igen", "Nem");

                if (LogOut)
                {
                    Navigation.PushAsync(new LoginPage());
                }
            });
            return true;
        }

        private async void OnMyReceiptsPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyReceiptsPage());
        }
        private async void ProfilePageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new QRPage1());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hiba: {ex.Message}");
            }
        }
    }
}

