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
        public string Username => UserSession.Username ?? "Vendég";
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
