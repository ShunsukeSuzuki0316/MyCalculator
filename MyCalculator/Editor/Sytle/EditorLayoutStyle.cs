using Xamarin.Forms;
namespace MyCalculator
{
	public partial class EditorStyles
	{

		public static Style helpContentLayoutStyle = new Style(typeof(StackLayout))
		{
			Setters = {
				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Horizontal
				},
				new Setter {
					Property = View.HorizontalOptionsProperty,
					Value = LayoutOptions.End
				},
				new Setter{
					Property = View.MarginProperty,
					Value = new Thickness{Top=20}
				}
			}
		};

		public static Style baseContentLayoutStyle = new Style(typeof(Layout))
		{
			Setters = {
				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Vertical
				}
			}
		};

		public static Style nameContentLayoutStyle = new Style(typeof(StackLayout))
		{
			Setters = {
				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Horizontal
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness{Top=10}
				}
			}
		};

		public static Style stepTitleContentLayoutStyle = new Style(typeof(StackLayout))
		{
			Setters = {
				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Vertical
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness{Top=10}
				}
			}
		};

		public static Style editButtonContentLayoutStyle = new Style(typeof(StackLayout))
		{
			Setters = {
				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Horizontal
				},
				new Setter {
					Property = View.HorizontalOptionsProperty,
					Value = LayoutOptions.Center
				},
				new Setter{
					Property = View.MarginProperty,
					Value = new Thickness{Top=20.0,Left=5.0,Right=5.0}
				}
			}
		};

		public static Style deleteCulcButtonContentStyle = new Style(typeof(StackLayout))
		{
			Setters = {
				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Horizontal
				}
			}
		};

		public static Style nextStepLayoutStyle = new Style(typeof(StackLayout))
		{
			Setters = {
				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Vertical
				},
				new Setter {
					Property = View.VerticalOptionsProperty,
					Value = LayoutOptions.Center
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness{Top=20}
				}
			}
		};

		public static Style culculateContentLayoutStyle = new Style(typeof(StackLayout))
		{
			Setters = {
				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Vertical
				}
			}
		};

		public static Style lastContentLayoutStyle = new Style(typeof(StackLayout))
		{
			Setters = {
				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Horizontal
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness{ Left = 10.0 }
				}
			}
		};

		public static Style lastLableStyle = new Style(typeof(Label))
		{
			Setters = {
				new Setter{
					Property = View.MarginProperty,
					Value = new Thickness{ Top = 10, Bottom = 10 }
				}
			}
		};

		public static Style finalSwitchStyle = new Style(typeof(Switch))
		{
			Setters = {
				new Setter {
					Property = View.VerticalOptionsProperty,
					Value = LayoutOptions.Start
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness{ Top = 10, Bottom = 10 }
				}
			}
		};

		public static Style explainContentLayoutStyle = new Style(typeof(StackLayout))
		{
			Setters = {
				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Horizontal
				}/*,
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness{Top=5}
				}*/
			}
		};

		public static Style stepContentLayoutStyle = new Style(typeof(Layout))
		{
			Setters = {
				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Vertical
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness{Top=20}
				}
			}
		};

		public static Style culcLayoutStyle = new Style(typeof(StackLayout))
		{
			Setters = {
				new Setter {
					Property = StackLayout.OrientationProperty,
					Value = StackOrientation.Horizontal
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness{Top=10}
				}
			}
		};
	}
}
