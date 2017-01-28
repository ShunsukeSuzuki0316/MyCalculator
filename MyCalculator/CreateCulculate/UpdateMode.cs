using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MyCalculator
{
	public class UpdateMode
	{

		MyCalculatorPage mypair = null;
		UserCulculateModel myUserCulculateModelTarget = null;

		Dictionary<string, StackLayout> stepList = new Dictionary<string, StackLayout>();

		Dictionary<string, EditListBaseModel> editListDic = new Dictionary<string, EditListBaseModel>();

		StackLayout baseContentLayout = new StackLayout { Orientation = StackOrientation.Vertical };

		ModeCommon common = null;

		public ContentView createUpdateButton()
		{

			var saveBtn = new Button { Text = "更新", Style=EditListStyles.saveButtonStyle };
			var cancelBtn = new Button { Text = "キャンセル", Style = EditListStyles.saveButtonStyle };
			var layout = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Center };
			saveBtn.Clicked += onSave;
			cancelBtn.Clicked += common.onCancel;
			layout.Children.Add(saveBtn);
			layout.Children.Add(cancelBtn);
			return new ContentView { Content = layout };
		}

		public ScrollView updateTable(MyCalculatorPage pair, UserCulculateModel target)
		{

			var culcModel = JsonConvert.DeserializeObject<CulculateJsonModel>(target.Culculate);

			mypair = pair;
			myUserCulculateModelTarget = target;

			common = new ModeCommon(stepList, editListDic, baseContentLayout,mypair);

			var baseContent = new ScrollView();
			var step1layout = new StackLayout { Orientation = StackOrientation.Vertical };


			editListDic.Add("Step1", new EditListBaseModel());

			baseContentLayout.Children.Add(common.createNameContent(target.DisplayName));

			step1layout.Children.Add(common.createStepTitleContent(culcModel.step[0].steps));
			step1layout.Children.Add(common.createExplainContent(culcModel.step[0].steps, culcModel.step[0]));

			baseContentLayout.Children.Add(step1layout);
			stepList.Add("Step1", step1layout);
			var layout = new StackLayout { Orientation = StackOrientation.Vertical };

			layout.Children.Add(baseContentLayout);
			layout.Children.Add(createUpdateButton());

			baseContent = new ScrollView { Content = layout };


			string laststep = "";

			foreach (StepModel model in culcModel.step)
			{

				if ("Step1".Equals(model.steps)) continue;


				var nextsteplayout = new StackLayout { Orientation = StackOrientation.Vertical };
				string nextstep = model.steps;

				stepList.Add(nextstep, nextsteplayout);

				editListDic.Add(nextstep, new EditListBaseModel());

				editListDic[nextstep].culculates = new List<CulcModel>();

				stepList[nextstep].Children.Add(common.createStepTitleContent(nextstep));
				stepList[nextstep].Children.Add(common.createExplainContent(nextstep, model));
				stepList[nextstep].Children.Add(common.createLastContent(nextstep, model));

				baseContentLayout.Children.Add(stepList[nextstep]);

				foreach (ExecuteJsonModel exec in model.execute)
				{
					stepList[model.steps].Children.Add(common.createCulculateContent(model.steps, exec));
				}

				stepList[model.steps].Children.Add(common.createEditButtonContent(model.steps, false));

				laststep = model.steps;
			}
			stepList[laststep].Children.RemoveAt(stepList[laststep].Children.Count - 1);
			stepList[laststep].Children.Add(common.createEditButtonContent(laststep,true));

			return baseContent;

		}

		public async void onSave(object sender, EventArgs e)
		{
			var result = await mypair.DisplayAlert("確認", "計算を更新しますか？", "OK", "キャンセル");
			if (!result) return;
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

			UserCulculateUtil.updateCulculateById(myUserCulculateModelTarget.Id,"Special",json,common.nameEntry.Text);

			mypair.refreshEditList();

			mypair.DisplayAlert("確認","更新が完了しました","OK");
		}
	}
}
