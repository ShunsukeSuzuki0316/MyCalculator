using System;
using Android.Content;
using Android.Content.Res;
using Android.Util;
using Android.Views;
using MyCalculator.Droid;
using Android.Runtime;
using Xamarin.Forms;

[assembly: Dependency(typeof(DisplaySize_Droid))]

namespace MyCalculator.Droid
{
	public class DisplaySize_Droid:DisplaySize
	{ 

		static IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

		public DisplaySize_Droid()
		{
		}

		public double getHieght()
		{

			Display d = windowManager.DefaultDisplay;

			Android.Util.DisplayMetrics m = new Android.Util.DisplayMetrics();
			d.GetMetrics(m);

			return (int)((m.HeightPixels)/m.Density);
		}

		public double getWidth()
		{
			Display d = windowManager.DefaultDisplay;

			Android.Util.DisplayMetrics m = new Android.Util.DisplayMetrics();
			d.GetMetrics(m);

			return (int)((m.WidthPixels) / m.Density);
		}

	}
}
