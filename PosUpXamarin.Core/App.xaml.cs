using System.Globalization;
using System.Reflection;
using I18NPortable;
using PosUpXamarin.Core.Views;
using Prism.Unity;
using Xamarin.Forms;

namespace PosUpXamarin.Core
{
    public partial class App
    {
        public App() : base(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            I18N.Current
                .SetFallbackLocale(CultureInfo.CurrentCulture.Name)
                .Init(GetType().GetTypeInfo().Assembly);

            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(TvShowMainPage)}");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<TvShowMainPage>();
            Container.RegisterTypeForNavigation<TvShowSearchPage>();
            Container.RegisterTypeForNavigation<TvShowDetailPage>();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }
    }
}
