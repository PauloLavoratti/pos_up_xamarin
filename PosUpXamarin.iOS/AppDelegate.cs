using FFImageLoading.Forms.Touch;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace PosUpXamarin.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			CachedImageRenderer.Init();

			Xamarin.Forms.Forms.Init();
			LoadApplication(new Core.App());

			return base.FinishedLaunching(app, options);
		}
	}
}
