using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyCalculator
{
	public class CL
	{

		MyCalculatorPage myPair;

		public TableView createTable(MyCalculatorPage pair)
		{
			myPair = pair;

			var table = new TableView(){RowHeight=50};
			table.Intent = TableIntent.Settings;

			var dlists = new List<ViewCell>();
			var lists = new List<ViewCell>();

			List<DefaultCulculateModel> dl = DefaultCulculateUtil.getAllDefaultCulculate();
			List<UserCulculateModel> ul = UserCulculateUtil.getAllUserCulculate();

			if (dl.Count == 0)
			{

				for (int i = 0; i < V.DefaultCuculateList.Count; i++)
				{
					DefaultCulculateUtil.insertDefaultCulculate(V.DefaultCuculateList[i], "", V.DefaultCuculateNameList[i]);
				}

				dl = DefaultCulculateUtil.getAllDefaultCulculate();

			}

			foreach (DefaultCulculateModel u in dl)
			{
				var layout = new StackLayout { Orientation = StackOrientation.Horizontal };

				layout.Children.Add(new Label
				{
					Text = u.DisplayName,
					TextColor = Color.FromHex("#f35e20"),
					VerticalOptions = LayoutOptions.Center
				});

				layout.Children.Add(new Label
				{
					Text = u.Name,
					IsVisible = false,
					VerticalOptions = LayoutOptions.Center
				});

				var cell = new ViewCell { View = layout };
				cell.Tapped += OnSpecial;

				dlists.Add(cell);


			}

			table.Root.Add(new TableRoot { new TableSection("デフォルト計算リスト") { dlists } });

			foreach (UserCulculateModel u in ul)
			{
				var layout = new StackLayout { Orientation = StackOrientation.Horizontal,HorizontalOptions=LayoutOptions.StartAndExpand };

				layout.Children.Add(new Label
				{
					Text = u.DisplayName,
					TextColor = Color.FromHex("#f35e20"),
					//HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					Margin=new Thickness{Left=20.0},
					WidthRequest = 500
					                                 
				});

				var deleteBtn = new Button { 
					Text = "削除", 
					WidthRequest = 50.0, 
					HorizontalOptions = LayoutOptions.End, 
					BorderWidth = 1.0,
					Margin = new Thickness{Left = 20.0,Right=20,Top=5,Bottom=5},
					AutomationId = u.Id.ToString() };
				deleteBtn.Clicked += onDeleteCulc;

				var updateBtn = new Button
				{
					Text = "編集",
					WidthRequest = 50.0,
					HorizontalOptions = LayoutOptions.End,
					BorderWidth = 1.0,
					Margin=new Thickness{ Top = 5, Bottom = 5 },
					AutomationId = u.Id.ToString()
				};

				updateBtn.Clicked += onUpdateCulc;

				layout.Children.Add(updateBtn);
				layout.Children.Add(deleteBtn);

				var cell = new ViewCell { View = layout,AutomationId=u.Id.ToString(),Height=500 };
				cell.Tapped += OnMySpecial;
				lists.Add(cell);
			}

			table.Root.Add(new TableRoot { new TableSection("マイ計算リスト") { lists } });



			return table;

		}

		async void onDeleteCulc(object sender,EventArgs e){

			var result = await myPair.DisplayAlert("確認", "選択した計算を削除しますか？", "OK", "キャンセル");

			if(result){
			
				var btn = (Button)sender;
				int target = int.Parse(btn.AutomationId);
				UserCulculateUtil.deleteCulculateById(target);
				myPair.refreshEditList();

			}
		}

		void onUpdateCulc(object sender, EventArgs e)
		{
			var btn = (Button)sender;
			int target = int.Parse(btn.AutomationId);
			myPair.updateCulculate(target);
		}

		void OnMySpecial(object sender, EventArgs e)
		{

			var cell = (ViewCell)sender;

			var layout = (StackLayout)cell.View;

			int id = int.Parse(cell.AutomationId);

			S.selectUserModel = UserCulculateUtil.getUserCulculateById(id);


			Button btn = new Button
			{
				Text = S.selectUserModel.Name
			};

			myPair.OnSpecial(btn, e);

		}

		void OnSpecial(object sender, EventArgs e)
		{


			var cell = (ViewCell)sender;
			var layout = (StackLayout)cell.View;

			string name = null;

			for (int i = 0; i < layout.Children.Count; i++)
			{

				var label = (Label)layout.Children[i];

				if (!label.IsVisible)
				{
					name = label.Text;
					break;
				}

			}

			Button btn = new Button
			{
				Text = name
			};

			myPair.OnSpecial(btn, e);
		}
	}
}
