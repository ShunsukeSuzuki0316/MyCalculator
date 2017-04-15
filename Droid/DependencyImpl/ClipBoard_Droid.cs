using Android.Content;
using MyCalculator.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(ClipBoard_Droid))]

namespace MyCalculator.Droid
{
	
	public class ClipBoard_Droid : IClipBoard
	{
		public string GetTextFromClipBoard()
		{
			var clipboardmanager = (ClipboardManager)Forms.Context.GetSystemService(Context.ClipboardService);
			var item = clipboardmanager.PrimaryClip.GetItemAt(0);
			var text = item.Text;
			return text;
		}

		public bool SetTextToClipBoard(string text)
		{
			var clipboardManager = (ClipboardManager)Forms.Context.GetSystemService(Context.ClipboardService);
			ClipData clip = ClipData.NewPlainText("", text);
			clipboardManager.PrimaryClip = clip;

			return true;
		}
	}
}
