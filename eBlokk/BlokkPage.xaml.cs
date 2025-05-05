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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadReceiptsAsync();
        }

        private async Task LoadReceiptsAsync()
        {
            Receipts.Clear();

            try
            {
                var db = new DatabaseService();
                var blokkok = await db.GetBlokkokAsync();

                Debug.WriteLine($"Betöltött blokkok száma: {blokkok.Count}");

                foreach (var blokk in blokkok)
                {
                    Receipts.Add(blokk);
                    Debug.WriteLine($"Blokk hozzáadva: {blokk.Id} - {blokk.QR} - {blokk.Adatok}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hiba a blokkok betöltésekor: {ex.Message}");
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