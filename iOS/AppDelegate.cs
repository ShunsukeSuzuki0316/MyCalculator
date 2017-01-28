using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace MyCalculator.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			string dbPath = FileAccessHelper.GetLocalFilePath("culculate.db3");

			LoadApplication(new MyCalculator.App(dbPath));

			return base.FinishedLaunching(app, options);
		}
	}
}
