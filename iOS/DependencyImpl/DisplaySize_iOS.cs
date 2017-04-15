using System;
using System.Runtime.CompilerServices;
using Foundation;
using MyCalculator.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DisplaySize_iOS))]

namespace MyCalculator.iOS
{
	public class DisplaySize_iOS:DisplaySize
	{
		public DisplaySize_iOS()
		{
		}

		public double getHieght()
		{
			return UIScreen.MainScreen.Bounds.Height;
		}

		public double getWidth()
		{
			return UIScreen.MainScreen.Bounds.Width;
		}
	}
}
