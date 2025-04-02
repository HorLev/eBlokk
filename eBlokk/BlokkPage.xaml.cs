using System.Collections.ObjectModel;

namespace eBlokk
{
    public partial class MyReceiptsPage : ContentPage
    {

        private async void MainPageClicked(object sender, EventArgs e)
        {
            // Navig�l�s az �j oldalra
            await Navigation.PushAsync(new MainPage());
        }
        private async void ProfilePageClicked(object sender, EventArgs e)
        {
            // Navig�l�s az �j oldalra
            await Navigation.PushAsync(new ProfilePage());
        }

        private async void AddBlokk_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddBlokk());
        }
        public ObservableCollection<YourModelType> Receipts { get; set; }

        public MyReceiptsPage()
        {
            InitializeComponent();
            Receipts = new ObservableCollection<YourModelType>
            {
                new YourModelType { Date = "2023-09-20", Chain = "SPAR Magyarorsz�g", City = "Budapest", Amount = 1000 },
                new YourModelType { Date = "2023-09-21", Chain = "Sinsay Magyarorsz�g", City = "B�k�scsaba", Amount = 2000 },
                new YourModelType { Date = "2023-09-22", Chain = "TESCO Magyarorsz�g", City = "Gy�r", Amount = 3000 }
            };
            BindingContext = this;
        }

        public class YourModelType
        {
            public required string Date { get; set; }
            public required string Chain { get; set; }
            public required string City { get; set; }
            public int Amount { get; set; }
        }


    }
}
