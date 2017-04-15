
using Xamarin.Forms;

namespace MyCalculator
{
	public partial class EditorStyles
	{
		public static Style basePickerStyle = new Style(typeof(Picker))
		{
			Setters = {
				new Setter {
					Property = View.VerticalOptionsProperty,
					Value = LayoutOptions.Center
				},
				new Setter {
					Property = VisualElement.WidthRequestProperty,
					Value = DependencyService.Get<DisplaySize>().getWidth() * .75
				},
				new Setter {
					Property = Picker.TextColorProperty,
					Value = Color.Black
				}
			}
		};

		public static Style typePickerStyle = new Style(typeof(Picker))
		{
			BasedOn = basePickerStyle,
			Setters = {
			}
		};

		public static Style stepPickerStyle = new Style(typeof(Picker))
		{
			BasedOn = basePickerStyle,
			Setters = {
			}
		};

		public static Style culcPickerStyle = new Style(typeof(Picker))
		{
			BasedOn = basePickerStyle,
			Setters = {
			}
		};
	}
}
