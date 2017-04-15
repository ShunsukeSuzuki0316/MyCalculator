using Xamarin.Forms;

namespace MyCalculator
{
	public partial class EditorStyles
	{
		public static Style commonLabelStyle = new Style(typeof(Label))
		{
			Setters = {
				new Setter {
					Property = Label.TextColorProperty,
					Value = Color.Black
				},
				new Setter{
					Property = View.VerticalOptionsProperty,
					Value = LayoutOptions.Center
				},
				new Setter{
					Property = View.HorizontalOptionsProperty,
					Value = LayoutOptions.Center
				},
				new Setter {
					Property = Label.FontFamilyProperty,
					Value = "Helvetica"
				}
			}
		};

		public static Style culcLabelStyle = new Style(typeof(Label))
		{
			BasedOn = commonLabelStyle,
			Setters = {
				new Setter {
					Property = View.VerticalOptionsProperty,
					Value = LayoutOptions.Center
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness { Left = 10.0 }
				}
			}
		};
	}
}
