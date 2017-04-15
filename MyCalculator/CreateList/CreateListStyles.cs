using System;
using Xamarin.Forms;

namespace MyCalculator
{
	public class CreateListStyles
	{
		public CreateListStyles()
		{
		}

		public static Style cellLayoutStyle = new Style(typeof(StackLayout))
		{
			Setters = {

				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Horizontal
				},
				new Setter {
					Property = View.HorizontalOptionsProperty,
					Value = LayoutOptions.StartAndExpand
				},
				new Setter {
					Property = StackLayout.BackgroundColorProperty,
					Value = Color.FromHex("#E8E8E8")
				},
				new Setter {
					Property = StackLayout.MarginProperty,
					Value = new Thickness{Top=10}
				}
			}
		};


		public static Style cellLabelStyle = new Style(typeof(Label))
		{
			Setters = {

				new Setter {
					Property = Label.TextColorProperty,
					Value = Color.FromHex("#f35e20")
					},
				new Setter{
					Property = View.VerticalOptionsProperty,
					Value = LayoutOptions.Center
				},
				new Setter{
					Property = VisualElement.WidthRequestProperty,
					Value = 500
				},
				new Setter{
					Property = View.MarginProperty,
					Value = new Thickness{Left=20.0},
				}

			}
		};


		public static Style deleteButtonStyle = new Style(typeof(Button))
		{

			Setters = {
				new Setter {
					Property = Button.ImageProperty,
					Value = "delete"
				},
				new Setter {
					Property = Button.BorderWidthProperty,
					Value = 0.2
				},
				new Setter {
					Property = VisualElement.WidthRequestProperty,
					Value = 50.0
				},
				new Setter {
					Property = VisualElement.HeightRequestProperty,
					Value = 50.0
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness{Left = 20.0,Right=20,Top=5,Bottom=5}
				}
			}
		};

		public static Style editButtonStyle = new Style(typeof(Button))
		{

			Setters = {
				new Setter {
					Property = Button.ImageProperty,
					Value = "edit"
				},
				new Setter {
					Property = Button.BorderWidthProperty,
					Value = 0.2
				},
				new Setter {
					Property = VisualElement.WidthRequestProperty,
					Value = 50.0
				},
				new Setter {
					Property = VisualElement.HeightRequestProperty,
					Value = 50.0
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness{Top=5,Bottom=5}
				}
			}
		};

	}
}
