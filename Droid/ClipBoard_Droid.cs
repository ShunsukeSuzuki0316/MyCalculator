using System;
using Android.Content;
using MyCalculator.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(ClipBoard_Droid))]

namespace MyCalculator.Droid
{
	
	public class ClipBoard_Droid : IClipBoard
	{
		public ClipBoard_Droid()
		{
		}

		public string GetTextFromClipBoard()
		{
			var clipboardmanager = (ClipboardManager)Forms.Context.GetSystemService(Context.ClipboardService);
			var item = clipboardmanager.PrimaryClip.GetItemAt(0);
			var text = item.Text;
			return text;
		}

		public bool SetTextToClipBoard(string text)
		{
			// Get the Clipboard Manager
			var clipboardManager = (ClipboardManager)Forms.Context.GetSystemService(Context.ClipboardService);

			// Create a new Clip
			ClipData clip = ClipData.NewPlainText("xxx_title", text);

			// Copy the text
			clipboardManager.PrimaryClip = clip;

			return true;
		}
	}
}
