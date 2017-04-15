using Xamarin.Forms;

namespace MyCalculator
{
	public partial class EditorStyles
	{
		public static Style commonEntryStyle = new Style(typeof(Entry)) 
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
				new Setter {
					Property = Entry.FontFamilyProperty,
					Value = "Helvetica"
				}
			}
		};

		public static Style nameEntryStyle = new Style(typeof(Entry)) {
			BasedOn = commonEntryStyle,
			Setters = {
				new Setter {
					Property = VisualElement.WidthRequestProperty,
					Value = DependencyService.Get<DisplaySize>().getWidth() * .75
				},
				new Setter {
					Property = Entry.PlaceholderProperty,
					Value = "一覧で表示する名前を入力"
				}
			}
		};

		public static Style explainEntryStyle = new Style(typeof(Entry)) {

			BasedOn = commonEntryStyle,
			Setters = {
				new Setter {
					Property = VisualElement.WidthRequestProperty,
					Value = DependencyService.Get<DisplaySize>().getWidth() * .75
				},
				new Setter {
					Property = Entry.PlaceholderProperty,
					Value = "ステップで表示する説明を入力"
				}
			}
		};

		public static Style constantEntryStyle = new Style(typeof(Entry)) {
			Setters = {
				new Setter {
					Property = View.VerticalOptionsProperty,
					Value = LayoutOptions.Center
				},
				new Setter {
					Property = Entry.PlaceholderProperty,
					Value = "数値を入力"
				},
				new Setter {
					Property = VisualElement.WidthRequestProperty,
					Value = DependencyService.Get<DisplaySize>().getWidth() * .75
				},
				new Setter {
					Property = InputView.KeyboardProperty,
					Value = Keyboard.Plain
				}
			}
		};
	}
}
