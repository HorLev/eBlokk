using eBlokk;
using System.Diagnostics;
using eBlokk.Model;

namespace eBlokk
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
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
            return true; // Ezzel letiltod a visszalépést
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
