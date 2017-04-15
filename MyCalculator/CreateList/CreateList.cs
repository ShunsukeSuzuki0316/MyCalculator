using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace MyCalculator
{
	public class CreateList
	{

		MyCalculatorPage myPair;

		public TableView createTable(MyCalculatorPage pair)
		{
			myPair = pair;

			var table = new TableView(){RowHeight=70};
			table.BackgroundColor = Color.FromHex("#F8F8F8");
			table.Intent = TableIntent.Settings;

			var lists = new List<ViewCell>();

			List<UserCulculateModel> ul = UserCulculateUtil.getAllUserCulculate();

			foreach (UserCulculateModel u in ul)
			{
				var layout = new StackLayout { Style=CreateListStyles.cellLayoutStyle };

				layout.Children.Add(new Label
				{
					Text = u.DisplayName,
					Style=CreateListStyles.cellLabelStyle                            
				});

				var deleteBtn = new Button { 
					Style=CreateListStyles.deleteButtonStyle,
					AutomationId = u.Id.ToString() 
				};

				deleteBtn.Clicked += async (sender, args) =>
				{
					var result = await myPair.DisplayAlert("確認", "選択した計算を削除しますか？", "OK", "キャンセル");

					if (result)
					{
						UserCulculateUtil.deleteCulculateById(u.Id);
						myPair.refreshEditList();

					}
				};

				var updateBtn = new Button
				{
					Style=CreateListStyles.editButtonStyle,
					AutomationId = u.Id.ToString()
				};

				updateBtn.Clicked += (sender, args) =>{myPair.updateCulculate(u.Id);};

				layout.Children.Add(updateBtn);
				layout.Children.Add(deleteBtn);

				var cell = new ViewCell { View = layout,AutomationId=u.Id.ToString()};


				cell.Tapped += (sender, args) =>
				{
					S.InitParameter();
					S.selectUserModel = UserCulculateUtil.getUserCulculateById(u.Id);
					myPair.OnSpecial(new Button{Text=u.Name}, args);
				};


				lists.Add(cell);
			}

			table.Root.Add(new TableRoot { new TableSection("マイ計算リスト") { lists } });

			return table;

		}
	}
}
