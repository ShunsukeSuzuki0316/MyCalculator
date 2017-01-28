using SQLite;
using Xamarin.Forms;

namespace MyCalculator
{
	public partial class App : Application
	{
		public App(string displayText)
		{
			S.dbPath = displayText;

			InitializeComponent();

			MainPage = new MyCalculatorPage();

		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
