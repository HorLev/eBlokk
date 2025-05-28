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

        private async void OnFilterButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string dateInput = await DisplayPromptAsync("Szûrés", "Írj be egy dátumot (ÉÉÉÉ-HH-NN) vagy hagyd üresen:", "Tovább", "Mégse");
                DateTime? dateFilter = null;

                if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out DateTime parsedDate))
                {
                    dateFilter = parsedDate.Date;
                }

                string storeFilter = await DisplayPromptAsync("Szûrés", "Írj be egy üzletnevet (vagy hagyd üresen):", "Tovább", "Mégse");

                string locationFilter = await DisplayPromptAsync("Szûrés", "Írj be egy helyszínt (vagy hagyd üresen):", "Szûrés", "Mégse");

                await ApplyFilterAsync(dateFilter, storeFilter, locationFilter);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Szûrési hiba: {ex.Message}");
            }
        }

        private async Task ApplyFilterAsync(DateTime? date, string? store, string? location)
        {
            Receipts.Clear();

            try
            {
                var db = new DatabaseService();
                var allBlokkok = await db.GetBlokkokAsync();

                var filtered = allBlokkok.Where(b =>
                    (date == null || b.VasarDatum.Date == date.Value) &&
                    (string.IsNullOrWhiteSpace(store) || b.Uzlet.Contains(store, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(location) || b.VasarHely.Contains(location, StringComparison.OrdinalIgnoreCase))
                );

                foreach (var blokk in filtered)
                {
                    Receipts.Add(blokk);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hiba szûrés közben: {ex.Message}");
            }
        }

        private async void MainPageClicked(object sender, EventArgs e) => await Navigation.PushAsync(new MainPage());
        private async void ProfilePageClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ProfilePage());
    }
}