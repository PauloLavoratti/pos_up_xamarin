using Xamarin.Forms;

namespace PosUpXamarin.Core.Views
{
    public partial class TvShowMainPage
    {
        public TvShowMainPage()
        {
            InitializeComponent();

            ItemsListView.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }
    }
}
