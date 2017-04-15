using Xamarin.Forms;

namespace MyCalculator
{
	public partial class EditorStyles
	{
		public static Style buttonBaeStyle = new Style(typeof(Button))
		{
			Setters = {

				new Setter {
					Property = Button.BorderColorProperty,
					Value = Color.FromHex("#676767")
				},
				new Setter {
					Property = Button.TextColorProperty,
					Value = Color.Black
				}
			}
		};

		public static Style helpButtonStyle = new Style(typeof(Button))
		{
			BasedOn = buttonBaeStyle,

			Setters = {
				new Setter {
					Property = Button.ImageProperty,
					Value = "help"
				},
				new Setter {
					Property = View.HorizontalOptionsProperty,
					Value = LayoutOptions.End
				},
				new Setter {
					Property = View.VerticalOptionsProperty,
					Value = LayoutOptions.End
				},
				new Setter {
					Property = VisualElement.BackgroundColorProperty,
					Value = Color.FromHex("#F8F8F8")
				}
			}
		};

		public static Style saveButtonStyle = new Style(typeof(Button))
		{
			BasedOn = buttonBaeStyle,

			Setters = {
				new Setter {
					Property = VisualElement.WidthRequestProperty,
					Value = 150.0
				},
				new Setter {
					Property = Button.BorderWidthProperty,
						Value = 0.5
				},
				new Setter {
					Property = VisualElement.BackgroundColorProperty,
					Value = Color.White
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness{Top=50.0,Bottom=50.0/*,Left=5.0,Right=5.0*/}
				}
			}
		};

		public static Style editButtonStyle = new Style(typeof(Button))
		{
			BasedOn = buttonBaeStyle,

			Setters = {
				new Setter {
					Property = VisualElement.WidthRequestProperty,
					Value = 100.0
				},
				new Setter {
					Property = Button.BorderWidthProperty,
					Value = 0.5
				},
				new Setter {
					Property = VisualElement.BackgroundColorProperty,
					Value = Color.White
				}			
			}
		};

		public static Style deleteButtonStyle = new Style(typeof(Button))
		{
			BasedOn = buttonBaeStyle,

			Setters = {

				new Setter {
					Property = View.HorizontalOptionsProperty,
					Value = LayoutOptions.Center
				},
				new Setter {
					Property = VisualElement.WidthRequestProperty,
					Value = 150.0
				},
				new Setter {
					Property = Button.BorderWidthProperty,
					Value = 0.5
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness { Top=15,Bottom=15 }
					},
				new Setter {
					Property = VisualElement.BackgroundColorProperty,
					Value = Color.FromHex("#f79b92")
				}
			}
		};

		public static Style addStepButtonStyle = new Style(typeof(Button))
		{
			BasedOn = buttonBaeStyle,

			Setters = {
				new Setter {
					Property = VisualElement.WidthRequestProperty,
					Value = 150.0
				},
				new Setter {
					Property = Button.BorderWidthProperty,
					Value = 0.5
				},
				new Setter {
					Property = VisualElement.BackgroundColorProperty,
					Value = Color.White
				}
			}
		};
	}
}
