using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MyCalculator
{
	public class CreateMode
	{

		MyCalculatorPage mypair = null;

		Dictionary<string, StackLayout> stepList = new Dictionary<string, StackLayout>();

		Dictionary<string, EditListBaseModel> editListDic = new Dictionary<string, EditListBaseModel>();

		StackLayout baseContentLayout = new StackLayout { Orientation = StackOrientation.Vertical };

		ModeCommon common = null;

		public ContentView createSaveButton()
		{

			var saveBtn = new Button { Text = "保存", Style = EditListStyles.saveButtonStyle };
			var cancelBtn = new Button { Text = "やり直す", Style = EditListStyles.saveButtonStyle };
			var layout = new StackLayout { Orientation = StackOrientation.Horizontal,HorizontalOptions = LayoutOptions.Center };
			saveBtn.Clicked += onSave;
			cancelBtn.Clicked += common.onCancel;
			layout.Children.Add(saveBtn);
			layout.Children.Add(cancelBtn);
			return new ContentView { Content = layout };
		}


		public ScrollView createTable(MyCalculatorPage pair)
		{

			mypair = pair;

			common = new ModeCommon(stepList, editListDic, baseContentLayout,mypair);

			var baseContent = new ScrollView();

			var step1layout = new StackLayout { Orientation = StackOrientation.Vertical };
			var step2layout = new StackLayout { Orientation = StackOrientation.Vertical };


			editListDic.Add("Step1", new EditListBaseModel());
			editListDic.Add("Step2", new EditListBaseModel());

			editListDic["Step2"].culculates = new List<CulcModel>();

			baseContentLayout.Children.Add(common.createNameContent(null));

			step1layout.Children.Add(common.createStepTitleContent("Step1"));
			step1layout.Children.Add(common.createExplainContent("Step1",null));


			step2layout.Children.Add(common.createStepTitleContent("Step2"));
			step2layout.Children.Add(common.createExplainContent("Step2",null));
			step2layout.Children.Add(common.createLastContent("Step2",null));
			step2layout.Children.Add(common.createEditButtonContent("Step2",true));

			baseContentLayout.Children.Add(step1layout);
			baseContentLayout.Children.Add(step2layout);

			stepList.Add("Step1", step1layout);
			stepList.Add("Step2", step2layout);

			var layout = new StackLayout { Orientation = StackOrientation.Vertical };

			layout.Children.Add(baseContentLayout);
			layout.Children.Add(createSaveButton());

			baseContent = new ScrollView { Content = layout };

			return baseContent;

		}

		public void onSave(object sender, EventArgs e)
		{

			if (!common.validate()) return;

			var newstepList = new List<StepModel>();

			foreach (var key in editListDic.Keys)
			{
				var newstep = new StepModel();

				newstep.explain = editListDic[key].explain.Text;
				newstep.steps = key;

				if (key.Equals("Step1"))
				{
					newstepList.Add(newstep);
					continue;
				}

				newstep.last = editListDic[key].final.IsToggled;

				var newstepexelist = new List<ExecuteJsonModel>();

				foreach (CulcModel cul in editListDic[key].culculates)
				{
					var newexe = new ExecuteJsonModel();

					string xtypeselect = SpecialUtil.typeConvertList[cul.xtypePick.Items[cul.xtypePick.SelectedIndex]];
					string ytypeselect = SpecialUtil.typeConvertList[cul.ytypePick.Items[cul.ytypePick.SelectedIndex]];

					newexe.culculateMethod = U.getMethodNameFromMark(cul.culcPick.Items[cul.culcPick.SelectedIndex]);
					newexe.xtype = xtypeselect;
					newexe.ytype = ytypeselect;

					switch (xtypeselect)
					{
						case JAN.Type_Constant_Value:
							newexe.xtarget = cul.xConstantEntry.Text;
							break;
						case JAN.Type_Input_Value:
							newexe.xtarget = cul.xstepPick.Items[cul.xstepPick.SelectedIndex];
							break;
						case JAN.Type_Previous_Culculate_Result_Value:
							break;
						case JAN.Type_Last_Culculate_Result_Value:
							newexe.xtarget = cul.xstepPick.Items[cul.xstepPick.SelectedIndex];
							break;
						default: break;
					}

					switch (ytypeselect)
					{
						case JAN.Type_Constant_Value:
							newexe.ytarget = cul.yConstantEntry.Text;
							break;
						case JAN.Type_Input_Value:
							newexe.ytarget = cul.ystepPick.Items[cul.ystepPick.SelectedIndex];
							break;
						case JAN.Type_Previous_Culculate_Result_Value:
							break;
						case JAN.Type_Last_Culculate_Result_Value:
							newexe.ytarget = cul.ystepPick.Items[cul.ystepPick.SelectedIndex];
							break;
						default: break;
					}


					newstepexelist.Add(newexe);

				}
				newstep.execute = newstepexelist;

				newstepList.Add(newstep);
			}

			var cj = new CulculateJsonModel();

			cj.step = newstepList;

			string json = Newtonsoft.Json.JsonConvert.SerializeObject(cj);

			UserCulculateModel um = new UserCulculateModel();
			um.Culculate = json;
			um.Name = "Special";
			um.DisplayName = common.nameEntry.Text;
			um.Index = 0;
			UserCulculateUtil.insertUserCulculate(um);

			mypair.refreshEditList();

			mypair.DisplayAlert("確認", "新しい計算を作成しました。", "OK");

		}
	}
}
