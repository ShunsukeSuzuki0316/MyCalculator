using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace MyCalculator
{
	public class CreateMode
	{

		MyCalculatorPage mypair = null;

		List<StackLayout> formulaList = new List<StackLayout>();

		List<EditorBaseModel> editList = new List<EditorBaseModel>();

		StackLayout baseContentLayout = new StackLayout { Style=EditorStyles.baseContentLayoutStyle };

		ModeCommon common = null;

		public ContentView createSaveButton()
		{

			var saveBtn = new Button { Text = "保存", Style = EditorStyles.saveButtonStyle,BackgroundColor=Color.FromHex("#4bb573") };
			var cancelBtn = new Button { Text = "やり直す", Style = EditorStyles.saveButtonStyle,BackgroundColor=Color.FromHex("#f0e275") };
			var layout = new StackLayout { Style=EditorStyles.saveButtonContentStyle };
			saveBtn.Clicked += onSave;
			cancelBtn.Clicked += (sender,args)=>{mypair.refreshEditList();};
			layout.Children.Add(saveBtn);
			layout.Children.Add(cancelBtn);
			return new ContentView { Content = layout };
		}

		public ScrollView createTable(MyCalculatorPage pair)
		{

			mypair = pair;

			common = new ModeCommon(formulaList, editList, baseContentLayout,mypair);

			var baseContent = new ScrollView();

			var formula1layout = new StackLayout { Style=EditorStyles.stepContentLayoutStyle };
			var formula2layout = new StackLayout { Style = EditorStyles.stepContentLayoutStyle };


			baseContentLayout.Children.Add(common.createHelpContent());
			baseContentLayout.Children.Add(common.createNameContent(null));

			editList.Add(new EditorBaseModel());
			formula1layout.Children.Add(common.createFormulaTitleContent());
			formula1layout.Children.Add(common.createExplainContent(null));
			baseContentLayout.Children.Add(formula1layout);
			formulaList.Add(formula1layout);


			editList.Add(new EditorBaseModel());
			formulaList.Add(formula2layout);
			editList.Last().culculates = new List<CulcModel>();
			formula2layout.Children.Add(common.createFormulaTitleContent());
			formula2layout.Children.Add(common.createExplainContent(null));
			formula2layout.Children.Add(common.createLastContent(null));
			formula2layout.Children.Add(common.createEditButtonContent(true));
			baseContentLayout.Children.Add(formula2layout);


			var layout = new StackLayout { Style=EditorStyles.baseContentLayoutStyle};

			layout.Children.Add(baseContentLayout);
			layout.Children.Add(createSaveButton());

			baseContent = new ScrollView { Style=EditorStyles.baseContentStyle, Content = layout };

			return baseContent;

		}

		public void onSave(object sender, EventArgs e)
		{

			if (!common.validate()) return;

			var newstepList = new List<CulculateFormulaJsonModel>();

			foreach (var target in editList)
			{
				var newstep = new CulculateFormulaJsonModel();
				var newstepexelist = new List<ExecuteJsonModel>();
				newstep.explain = target.explain.Text;


				if(target.final == null){
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
						default: break;
					}


					newstepexelist.Add(newexe);

				}
				newstep.execute = newstepexelist;

				newstepList.Add(newstep);
			}

			var cj = new CulculateJsonModel();

			cj.formula = newstepList;

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
