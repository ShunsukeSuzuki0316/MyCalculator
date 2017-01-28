using System;
using Xamarin.Forms;

namespace MyCalculator
{
	public class EditListStyles
	{
		public EditListStyles()
		{
		}

		public static Style commonLabelStyle = new Style(typeof(Label))
		{
			Setters = {
					/*new Setter {
					Property = Label.FontSizeProperty,
						Value = 18.0
					},*/

				new Setter {
					Property = Label.TextColorProperty,
					Value = Color.Black
					},
				new Setter{
					Property = View.VerticalOptionsProperty,
					Value = LayoutOptions.Center
				},
				new Setter{
					Property = Label.HorizontalOptionsProperty,
					Value = LayoutOptions.Center
				}

			}
		};

		public static Style commonEntryStyle = new Style(typeof(Entry))
		{
			Setters = {

				new Setter {
					Property = Label.TextColorProperty,
					Value = Color.Black
					},
				new Setter{
					Property = Label.VerticalOptionsProperty,
					Value = LayoutOptions.Center
				},
				new Setter{
					Property = Label.HorizontalOptionsProperty,
					Value = LayoutOptions.Center
				}
			}
		};

		public static Style saveButtonStyle = new Style(typeof(Button))
		{
			Setters = {
				new Setter {
					Property = VisualElement.WidthRequestProperty,
					Value = 150.0
				},

				new Setter {
					Property = Button.BorderWidthProperty,
						Value = 1.0
				},
				new Setter {
					Property = Button.BackgroundColorProperty,
					Value = Color.White
				},
				new Setter {
					Property = Button.MarginProperty,
					Value = new Thickness{Top=50.0,Bottom=50.0/*,Left=5.0,Right=5.0*/}
				}
			}
		};

		public static Style editButtonStyle = new Style(typeof(Button))
		{
			Setters = {
					new Setter {
					Property = VisualElement.WidthRequestProperty,
						Value = 100.0
					},

				new Setter {
					Property = Button.BorderWidthProperty,
						Value = 1.0
					},
				new Setter {
					Property = Button.BackgroundColorProperty,
					Value = Color.White
				},
				new Setter {
					Property = Button.MarginProperty,
					Value = new Thickness{Top=5.0,Bottom=5.0,Left=5.0,Right=5.0}
				}
			}
		};

		public static Style deleteButtonStyle = new Style(typeof(Button))
		{
			Setters = {
					new Setter {
					Property = VisualElement.WidthRequestProperty,
						Value = 150.0
					},

				new Setter {
					Property = Button.BorderWidthProperty,
						Value = 1.0
					},

				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness { Left = 50, Right = 50, Top=15,Bottom=15 }
					},
				new Setter {
					Property = Button.BackgroundColorProperty,
					Value = Color.White
				}
			}
		};

		public static Style typePickerStyle = new Style(typeof(Picker))
		{
			Setters = {
				new Setter {
					Property = View.VerticalOptionsProperty,
						Value = LayoutOptions.Start
				},

				new Setter {
					Property = Picker.TextColorProperty,
						Value = Color.Black
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness { Left = 50, Right = 50,Top=10.0 }
				}
			}
		};

		public static Style stepPickerStyle = new Style(typeof(Picker))
		{
			Setters = {
				new Setter {
					Property = View.VerticalOptionsProperty,
					Value = LayoutOptions.Start
				},

				new Setter {
					Property = Picker.TextColorProperty,
					Value = Color.Black
					},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness { Left = 50, Right = 50 }
				}
			}
		};

		public static Style culcPickerStyle = new Style(typeof(Picker))
		{
			Setters = {
				new Setter {
					Property = View.VerticalOptionsProperty,
					Value = LayoutOptions.Start
				},

				new Setter {
					Property = Picker.TextColorProperty,
					Value = Color.Black
				},
				new Setter {
					Property = View.MarginProperty,
					Value = new Thickness { Left = 100, Right = 100,Top = 10 }
				}
			}
		};

		public static Style constantEntryStyle = new Style(typeof(Entry))
		{
			Setters = {
				new Setter {
					Property = View.VerticalOptionsProperty,
						Value = LayoutOptions.Center
				},

				new Setter {
					Property = Entry.PlaceholderProperty,
						Value = "数値を入力してください"
					},
				new Setter {
					Property = View.MarginProperty,
						Value = new Thickness { Left = 50, Right = 50 }
					},

				new Setter {
					Property = InputView.KeyboardProperty,
					Value = Keyboard.Numeric
					},
				new Setter {
					Property = VisualElement.IsVisibleProperty,
					Value = false
				}
			}
		};

	}
}
