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

                Debug.WriteLine($"Bet�lt�tt blokkok sz�ma: {blokkok.Count}");

                foreach (var blokk in blokkok)
                {
                    Receipts.Add(blokk);
                    Debug.WriteLine($"Blokk hozz�adva: {blokk.Id} - {blokk.QR} - {blokk.Adatok}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hiba a blokkok bet�lt�sekor: {ex.Message}");
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
                string dateInput = await DisplayPromptAsync("Sz�r�s", "�rj be egy d�tumot (����-HH-NN) vagy hagyd �resen:", "Tov�bb", "M�gse");
                DateTime? dateFilter = null;

                if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out DateTime parsedDate))
                {
                    dateFilter = parsedDate.Date;
                }

                string storeFilter = await DisplayPromptAsync("Sz�r�s", "�rj be egy �zletnevet (vagy hagyd �resen):", "Tov�bb", "M�gse");

                string locationFilter = await DisplayPromptAsync("Sz�r�s", "�rj be egy helysz�nt (vagy hagyd �resen):", "Sz�r�s", "M�gse");

                await ApplyFilterAsync(dateFilter, storeFilter, locationFilter);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Sz�r�si hiba: {ex.Message}");
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
                Debug.WriteLine($"Hiba sz�r�s k�zben: {ex.Message}");
            }
        }

        private async void MainPageClicked(object sender, EventArgs e) => await Navigation.PushAsync(new MainPage());
        private async void ProfilePageClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ProfilePage());
    }
}