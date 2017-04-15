using Xamarin.Forms;

namespace MyCalculator
{
	public partial class EditorStyles
	{
		public static Style baseContentStyle = new Style(typeof(ScrollView))
		{
			Setters = {
				new Setter {
					Property = VisualElement.BackgroundColorProperty,
					Value = Color.FromHex("#F8F8F8")
				}
			}
		};

		public static Style editButtonContentStyle = new Style(typeof(ContentView))
		{
			Setters = {
				new Setter {
					Property = View.VerticalOptionsProperty,
					Value = LayoutOptions.Center
				}
			}
		};

		public static Style saveButtonContentStyle = new Style(typeof(ScrollView))
		{
			Setters = {
				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Horizontal
				},
				new Setter {
					Property = View.HorizontalOptionsProperty,
					Value = LayoutOptions.Center
				}
			}
		};
	}
}
