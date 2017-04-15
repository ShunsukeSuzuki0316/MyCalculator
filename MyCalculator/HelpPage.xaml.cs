using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyCalculator
{
	public partial class HelpPage : ContentPage
	{
		public HelpPage()
		{
			InitializeComponent();
		}
		async void OnDismissButtonClicked(object sender, EventArgs args)
		{
			await Navigation.PopModalAsync();
		}
	}
}
