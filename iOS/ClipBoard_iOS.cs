using System;
using System.Runtime.CompilerServices;
using Foundation;
using MyCalculator.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ClipBoard_iOS))]

namespace MyCalculator.iOS
{
	public class ClipBoard_iOS:IClipBoard
	{
		public ClipBoard_iOS()
		{
		}
		public string GetTextFromClipBoard()
		{
			var pb = UIPasteboard.General.GetValue("public.utf8-plain-text");
			return pb.ToString();
		}

		public bool SetTextToClipBoard(string text){
			UIPasteboard.General.SetValue(new NSString(text), MobileCoreServices.UTType.Text);
			return true;
		}
	}
}
