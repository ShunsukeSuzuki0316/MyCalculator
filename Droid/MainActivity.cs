﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

namespace MyCalculator.Droid
{
	[Activity(Label = "MyCalculator.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			string dbPath = FileAccessHelper.GetLocalFilePath("culculate.db3");

			LoadApplication(new App(dbPath));
		}

		public string GetTextFromClipBoard()
		{
			var clipboardmanager = (ClipboardManager)Forms.Context.GetSystemService(Context.ClipboardService);
			var item = clipboardmanager.PrimaryClip.GetItemAt(0);
			var text = item.Text;
			return text;
		}
	}
}
