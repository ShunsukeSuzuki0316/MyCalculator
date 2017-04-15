using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;

namespace MyCalculator.Droid
{
	public static class EmptyClass
	{

		public static void aa(){

			IWindowManager windowManager = Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

		}
	}
}
