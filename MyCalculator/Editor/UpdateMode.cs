using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Linq;

namespace MyCalculator
{
	public class UpdateMode
	{

		MyCalculatorPage mypair = null;
		UserCulculateModel myUserCulculateModelTarget = null;

		List<StackLayout> formulaList = new List<StackLayout>();

		List<EditorBaseModel> editList = new List<EditorBaseModel>();

		StackLayout baseContentLayout = new StackLayout { Style = EditorStyles.baseContentLayoutStyle };

		ModeCommon common = null;

		public ContentView createUpdateButton()
		{

			var saveBtn = new Button { Text = "更新", Style = EditorStyles.saveButtonStyle, BackgroundColor = Color.FromHex("#4bb573") };
			var cancelBtn = new Button { Text = "キャンセル", Style = EditorStyles.saveButtonStyle, BackgroundColor = Color.FromHex("#f0e275") };
			var layout = new StackLayout { Style = EditorStyles.saveButtonContentStyle };
			saveBtn.Clicked += onSave;
			cancelBtn.Clicked += (sender, args) =>
			{
				mypair.refreshEditList();
			};
			layout.Children.Add(saveBtn);
			layout.Children.Add(cancelBtn);
			return new ContentView { Content = layout };
		}

		public ScrollView updateTable(MyCalculatorPage pair, UserCulculateModel target)
		{

			var culcModel = JsonConvert.DeserializeObject<CulculateJsonModel>(target.Culculate);

			mypair = pair;
			myUserCulculateModelTarget = target;

			common = new ModeCommon(formulaList, editList, baseContentLayout, mypair);

			var baseContent = new ScrollView();
			var formula1layout = new StackLayout { Style = EditorStyles.stepContentLayoutStyle };


			baseContentLayout.Children.Add(common.createHelpContent());
			baseContentLayout.Children.Add(common.createNameContent(target.DisplayName));

			editList.Add(new EditorBaseModel());
			formula1layout.Children.Add(common.createFormulaTitleContent());
			formula1layout.Children.Add(common.createExplainContent(culcModel.formula[0]));
			baseContentLayout.Children.Add(formula1layout);
			formulaList.Add(formula1layout);

			var layout = new StackLayout { Style = EditorStyles.baseContentLayoutStyle };

			layout.Children.Add(baseContentLayout);
			layout.Children.Add(createUpdateButton());

			baseContent = new ScrollView { Style = EditorStyles.baseContentStyle, Content = layout };


			foreach (CulculateFormulaJsonModel model in culcModel.formula)
			{

				if (culcModel.formula[0].Equals(model))
					continue;


				var nextsteplayout = new StackLayout { Style = EditorStyles.stepContentLayoutStyle };

				formulaList.Add(nextsteplayout);

				editList.Add(new EditorBaseModel());

				editList.Last().culculates = new List<CulcModel>();

				formulaList.Last().Children.Add(common.createFormulaTitleContent());
				formulaList.Last().Children.Add(common.createExplainContent(model));
				formulaList.Last().Children.Add(common.createLastContent(model));

				baseContentLayout.Children.Add(nextsteplayout);

				foreach (ExecuteJsonModel exec in model.execute)
				{
					formulaList.Last().Children.Add(
						common.createCulculateContent(
							exec,formulaList.IndexOf(formulaList.Last())
						)
					);
				}

				formulaList.Last().Children.Add(common.createEditButtonContent(false));
			}
			formulaList.Last().Children.Remove(formulaList.Last().Children.Last());
			formulaList.Last().Children.Add(common.createEditButtonContent(true));

			return baseContent;

		}

		public async void onSave(object sender, EventArgs e)
		{
			var result = await mypair.DisplayAlert("確認", "計算を更新しますか？", "OK", "キャンセル");
			if (!result)
				return;
			if (!common.validate())
				return;

			var newstepList = new List<CulculateFormulaJsonModel>();

			foreach (var target in editList)
			{
				var newstep = new CulculateFormulaJsonModel();
				var newstepexelist = new List<ExecuteJsonModel>();
				newstep.explain = target.explain.Text;


				if (target.final == null) {
					newstep.last = false;
					newstep.execute = newstepexelist;
					newstepList.Add(newstep);
					continue;
				}

				newstep.last = target.final.IsToggled;

				foreach (CulcModel cul in target.culculates)
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
						case JAN.Type_Last_Culculate_Result_Value:
							newexe.xtarget = cul.xstepPick.SelectedIndex.ToString();
							break;
					}

					switch (ytypeselect)
					{
						case JAN.Type_Constant_Value:
							newexe.ytarget = cul.yConstantEntry.Text;
							break;
						case JAN.Type_Input_Value:
						case JAN.Type_Last_Culculate_Result_Value:
							newexe.ytarget = cul.ystepPick.SelectedIndex.ToString();
							break;
					}

					newstepexelist.Add(newexe);

				}
				newstep.execute = newstepexelist;

				newstepList.Add(newstep);
			}

			var cj = new CulculateJsonModel();

			cj.formula = newstepList;

			string json = JsonConvert.SerializeObject(cj);

			UserCulculateUtil.updateCulculateById(myUserCulculateModelTarget.Id, "Special", json, common.nameEntry.Text);

			mypair.refreshEditList();

			await mypair.DisplayAlert("確認", "更新が完了しました", "OK");
		}
	}
}
