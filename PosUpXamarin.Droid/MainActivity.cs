using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading.Forms.Droid;
using PosUpXamarin.Core;
using Xamarin.Forms.Platform.Android;

namespace PosUpXamarin.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabbar;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            CachedImageRenderer.Init();
            UserDialogs.Init(this);

            Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}