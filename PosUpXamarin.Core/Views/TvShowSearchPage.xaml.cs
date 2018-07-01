using Xamarin.Forms;

namespace PosUpXamarin.Core.Views
{
    public partial class TvShowSearchPage
    {
        public TvShowSearchPage()
        {
            InitializeComponent();

            ItemsListView.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };

            if (Device.RuntimePlatform == Device.Android)
            {
                SearchBar.HeightRequest = 40;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (string.IsNullOrEmpty(SearchBar.Text))
            {
                SearchBar.Focus();
            }
        }
    }
}