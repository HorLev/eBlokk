using System.Collections.ObjectModel;

namespace eBlokk
{
    public partial class MyReceiptsPage : ContentPage
    {

        private async void MainPageClicked(object sender, EventArgs e)
        {
            // Navigálás az új oldalra
            await Navigation.PushAsync(new MainPage());
        }
        private async void ProfilePageClicked(object sender, EventArgs e)
        {
            // Navigálás az új oldalra
            await Navigation.PushAsync(new ProfilePage());
        }

        public ObservableCollection<YourModelType> Receipts { get; set; }

        public MyReceiptsPage()
        {
            InitializeComponent();
            Receipts = new ObservableCollection<YourModelType>
            {
                new YourModelType { Date = "2023-09-20", Chain = "SPAR Magyarország", City = "Budapest", Amount = 1000 },
                new YourModelType { Date = "2023-09-21", Chain = "Sinsay Magyarország", City = "Békéscsaba", Amount = 2000 },
                new YourModelType { Date = "2023-09-22", Chain = "TESCO Magyarország", City = "Gyõr", Amount = 3000 }
            };
            BindingContext = this;
        }

        public class YourModelType
        {
            public string Date { get; set; }
            public string Chain { get; set; }
            public string City { get; set; }
            public int Amount { get; set; }
        }
    }
}
