using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using eBlokk.Model;
using eBlokk;

namespace eBlokk
{
    public partial class MyReceiptsPage : ContentPage
    {
        public ObservableCollection<Blokk> Receipts { get; set; } = new();

        public MyReceiptsPage()
        {
            InitializeComponent();
            BindingContext = this;
            LoadBlokkok();
        }

        private async void LoadBlokkok()
        {
            try
            {
                var db = new DatabaseService();
                var blokkok = await db.GetBlokkokAsync();
                foreach (var blokk in blokkok)
                {
                    Receipts.Add(blokk);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hiba: {ex.Message}");
            }
        }

        private async void Blokk_Tapped(object sender, EventArgs e)
        {
            if (sender is Frame frame && frame.BindingContext is Blokk blokk)
            {
                await Navigation.PushAsync(new PDFPreviewPage(blokk));
            }
        }

        private async void AddBlokk_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddBlokk());
        }

        private async void MainPageClicked(object sender, EventArgs e) => await Navigation.PushAsync(new MainPage());
        private async void ProfilePageClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ProfilePage());
    }
}