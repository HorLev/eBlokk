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

            Receipts = new ObservableCollection<Blokk>();

            BindingContext = this;

            LoadBlokkok();
        }


        private async void LoadBlokkok()
        {
            // Ez csak teszt
            Receipts.Add(new Blokk
            {
                Id = 1,
                QR = "QR001",
                VasarDatum = DateTime.Today,
                VasarIdo = "12:00",
                VasarHely = "Tesztváros",
                Uzlet = "Teszt Bolt",
                Adatok = "Termék 1 - 1000 Ft"
            });

            try
            {
                var db = new DatabaseService();
                var blokkok = await db.GetBlokkokAsync();

                Debug.WriteLine($"Betöltött blokkok száma: {blokkok.Count}");

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