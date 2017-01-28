using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MyCalculator
{
	public class ModeCommon
	{
		MyCalculatorPage mypair = null;

		Dictionary<string, StackLayout> stepList = null;

		Dictionary<string, EditListBaseModel> editListDic = null;

		StackLayout baseContentLayout = null;

		public Entry nameEntry = null;


		public int getCulCuModelIndexByUUID(List<CulcModel> culList, string uuid)
		{
			for (int i = 0; i < culList.Count; i++)
			{
				if (culList[i].uuid.Equals(uuid))
				{
					return i;
				}
			}

			return 0;
		}

		public ModeCommon(Dictionary<string, StackLayout> stepList, Dictionary<string, EditListBaseModel> editListDic, StackLayout baseContentLayout, MyCalculatorPage mypair)
		{

			this.stepList = stepList;
			this.editListDic = editListDic;
			this.baseContentLayout = baseContentLayout;
			this.mypair = mypair;
		}

		public Picker createTypePicker(string step)
		{
			var picker = new Picker { Style = EditListStyles.typePickerStyle, AutomationId = step };
			foreach (string type in SpecialUtil.typeList) { picker.Items.Add(type); }
			picker.SelectedIndex = 0;
			picker.SelectedIndexChanged += onPickerChangeIndex;
			return picker;
		}
		public Picker createCalculateTypePicker(string step)
		{
			var picker = new Picker { Style = EditListStyles.culcPickerStyle, AutomationId = step };
			foreach (string type in SpecialUtil.culcTypeList) { picker.Items.Add(type); }
			picker.SelectedIndex = 0;
			return picker;
		}
		public Picker createStepPicker(string step)
		{
			var picker = new Picker { Style = EditListStyles.stepPickerStyle, AutomationId = step };
			foreach (var key in stepList.Keys)
			{
				if (step.Equals(key)) continue;
				picker.Items.Add(key);
			}
			picker.SelectedIndex = 0;
			return picker;
		}

		public ContentView createNameContent(string name)
		{

			var layout = new StackLayout { Orientation = StackOrientation.Horizontal };

			layout.Children.Add(new Label
			{
				Text = "名前",
				Style = EditListStyles.commonLabelStyle
			});

			nameEntry = new Entry
			{
				Style = EditListStyles.commonEntryStyle,
				Text = name,
				WidthRequest = 250.0

			};

			layout.Children.Add(nameEntry);

			layout.Margin = new Thickness
			{
				Top = 60,
				Left = 15.0
			};

			return new ContentView { Content = layout };

		}


		public ContentView createStepTitleContent(string step)
		{

			var layout = new StackLayout { Orientation = StackOrientation.Horizontal };

			layout.Children.Add(new Label
			{
				Text = step,
				Style = EditListStyles.commonLabelStyle
			});

			layout.Margin = new Thickness
			{
				Top = 20.0,
				Left = 15.0
			};

			return new ContentView { Content = layout };
		}

		public ContentView createEditButtonContent(string step,bool showStep)
		{

			var layout = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Center };

			var addCulculateButton = new Button { Text = "計算追加", AutomationId = step, Style = EditListStyles.editButtonStyle };
			var addStepButton = new Button { Text = "ステップ追加", AutomationId = step, Style = EditListStyles.editButtonStyle,IsVisible = showStep };
			var deleteStepButton = new Button { Text = "ステップ削除", AutomationId = step, Style = EditListStyles.editButtonStyle, IsVisible = showStep };

			addCulculateButton.Clicked += onCulculateAdd;
			addStepButton.Clicked += onStepAdd;
			deleteStepButton.Clicked += onStepDelete;

			layout.Children.Add(addCulculateButton);
			layout.Children.Add(addStepButton);
			if (stepList.Count > 2) layout.Children.Add(deleteStepButton);

			return new ContentView { Content = layout, VerticalOptions = LayoutOptions.Center };
		}

		public ContentView createDeleteCulcButtonContent(string step)
		{

			var layout = new StackLayout { Orientation = StackOrientation.Horizontal };

			var addStepButton = new Button { Text = "ステップ削除", WidthRequest = 150.0, AutomationId = step };


			layout.Children.Add(addStepButton);

			return new ContentView { Content = layout };
		}


		public void onStepDelete(object sender, EventArgs e)
		{
			var btn = (Button)sender;
			var target = btn.AutomationId;

			baseContentLayout.Children.Remove(stepList[target]);

			stepList.Remove(target);
			editListDic.Remove(target);

			var prevtarget = "Step" + (int.Parse(Regex.Match(target, @"[\d]+").Captures[0].Value) - 1);

			stepList[prevtarget].Children.Add(createEditButtonContent(prevtarget,true));

		}

		public void onDeleteCulc(object sender, EventArgs e)
		{

			var btn = (Button)sender;
			var step = btn.AutomationId;
			var parLayout = (StackLayout)btn.Parent;
			var parContent = (ContentView)parLayout.Parent;
			var baseLayout = (StackLayout)parContent.Parent;

			var targetuuid = parLayout.AutomationId;
			editListDic[step].culculates.RemoveAt(getCulCuModelIndexByUUID(editListDic[step].culculates, targetuuid));
			baseLayout.Children.Remove(parContent);

		}

		public void onCulculateAdd(object sender, EventArgs e)
		{

			var btn = (Button)sender;
			string step = btn.AutomationId;

			stepList[step].Children.RemoveAt((stepList[step].Children.Count - 1));
			stepList[step].Children.Add(createCulculateContent(step, null));
			if (stepList.ContainsKey("Step" + (int.Parse(Regex.Match(step, @"[\d]+").Captures[0].Value) + 1)))
			{
				stepList[step].Children.Add(createEditButtonContent(step, false));
			}
			else {
				stepList[step].Children.Add(createEditButtonContent(step, true));
			}
		}

		public void onStepAdd(object sender, EventArgs e)
		{
			var btn = (Button)sender;
			string step = btn.AutomationId;

			stepList[step].Children.RemoveAt((stepList[step].Children.Count - 1));
			stepList[step].Children.Add(createEditButtonContent(step, false));

			var nextsteplayout = new StackLayout { Orientation = StackOrientation.Vertical };
			string nextstep = "Step" + (stepList.Count + 1);

			stepList.Add(nextstep, nextsteplayout);

			editListDic.Add(nextstep, new EditListBaseModel());

			editListDic[nextstep].culculates = new List<CulcModel>();

			stepList[nextstep].Children.Add(createStepTitleContent(nextstep));
			stepList[nextstep].Children.Add(createExplainContent(nextstep, null));
			stepList[nextstep].Children.Add(createLastContent(nextstep, null));
			stepList[nextstep].Children.Add(createEditButtonContent(nextstep,true));

			baseContentLayout.Children.Add(stepList[nextstep]);

		}

		public ContentView createCulculateContent(string step, ExecuteJsonModel targetModel)
		{
			string uuid = Guid.NewGuid().ToString();
			var layout = new StackLayout { Orientation = StackOrientation.Vertical, AutomationId = uuid, VerticalOptions = LayoutOptions.Center };

			var xtypePick = createTypePicker(step);
			var ytypePick = createTypePicker(step);
			var culcPick = createCalculateTypePicker(step);
			var xstepPick = createStepPicker(step);
			var ystepPick = createStepPicker(step);

			var deleteCulculateButton = new Button { Text = "計算削除", AutomationId = step, Style = EditListStyles.deleteButtonStyle };

			deleteCulculateButton.Clicked += onDeleteCulc;

			var yConstantEntry = new Entry
			{
				Style = EditListStyles.constantEntryStyle,
				AutomationId = step
			};

			var xConstantEntry = new Entry
			{
				Style = EditListStyles.constantEntryStyle,
				AutomationId = step
			};

			if (targetModel != null)
			{

				xtypePick.SelectedIndex = SpecialUtil.realTypeList.IndexOf(targetModel.xtype);
				ytypePick.SelectedIndex = SpecialUtil.realTypeList.IndexOf(targetModel.ytype);

				culcPick.SelectedIndex = culcPick.Items.IndexOf(U.getMarkFromMethodName(targetModel.culculateMethod));

				switch (targetModel.xtype)
				{
					case JAN.Type_Constant_Value:
						xConstantEntry.IsVisible = true;
						xstepPick.IsVisible = false;
						xConstantEntry.Text = targetModel.xtarget;
						break;
					case JAN.Type_Input_Value:
						xConstantEntry.IsVisible = false;
						xstepPick.IsVisible = true;
						xstepPick.SelectedIndex = xstepPick.Items.IndexOf(targetModel.xtarget);
						break;
					case JAN.Type_Previous_Culculate_Result_Value:
						xConstantEntry.IsVisible = false;
						xstepPick.IsVisible = false;
						break;
					case JAN.Type_Last_Culculate_Result_Value:
						xConstantEntry.IsVisible = false;
						xstepPick.IsVisible = true;
						xstepPick.SelectedIndex = xstepPick.Items.IndexOf(targetModel.xtarget);
						break;
					default: break;
				}

				switch (targetModel.ytype)
				{
					case JAN.Type_Constant_Value:
						yConstantEntry.IsVisible = true;
						ystepPick.IsVisible = false;
						yConstantEntry.Text = targetModel.ytarget;
						break;
					case JAN.Type_Input_Value:
						yConstantEntry.IsVisible = false;
						ystepPick.IsVisible = true;
						ystepPick.SelectedIndex = ystepPick.Items.IndexOf(targetModel.ytarget);
						break;
					case JAN.Type_Previous_Culculate_Result_Value:
						yConstantEntry.IsVisible = false;
						ystepPick.IsVisible = false;
						break;
					case JAN.Type_Last_Culculate_Result_Value:
						yConstantEntry.IsVisible = false;
						ystepPick.IsVisible = true;
						ystepPick.SelectedIndex = ystepPick.Items.IndexOf(targetModel.ytarget);
						break;
					default: break;
				}

			}

			layout.Children.Add(xtypePick);
			layout.Children.Add(xstepPick);
			layout.Children.Add(xConstantEntry);
			layout.Children.Add(culcPick);
			layout.Children.Add(ytypePick);
			layout.Children.Add(ystepPick);
			layout.Children.Add(yConstantEntry);
			layout.Children.Add(deleteCulculateButton);

			var culcModel = new CulcModel();
			culcModel.xtypePick = xtypePick;
			culcModel.xstepPick = xstepPick;
			culcModel.xConstantEntry = xConstantEntry;
			culcModel.culcPick = culcPick;
			culcModel.ytypePick = ytypePick;
			culcModel.ystepPick = ystepPick;
			culcModel.yConstantEntry = yConstantEntry;
			culcModel.uuid = uuid;

			editListDic[step].culculates.Add(culcModel);

			return new ContentView { Content = layout, BackgroundColor = Color.Silver };
		}

		public ContentView createLastContent(string step, StepModel targetModel)
		{

			var layout = new StackLayout { Orientation = StackOrientation.Horizontal };

			layout.Children.Add(new Label
			{
				Text = "最終ステップ",
				Margin = new Thickness{Top=10,Bottom=10},
				Style = EditListStyles.commonLabelStyle
			});

			var final = new Switch
			{
				Margin = new Thickness { Top = 10, Bottom = 10 },
				VerticalOptions = LayoutOptions.Start
			};

			if (targetModel != null) final.IsToggled = targetModel.last;

			layout.Children.Add(final);

			layout.Margin = new Thickness
			{
				Left = 15.0
			};

			editListDic[step].final = final;

			return new ContentView { Content = layout };
		}



		public ContentView createExplainContent(string step, StepModel targetMpdel)
		{

			var layout = new StackLayout { Orientation = StackOrientation.Horizontal };

			layout.Children.Add(new Label
			{
				Text = "説明",
				Style = EditListStyles.commonLabelStyle
			});

			var explain = new Entry
			{
				Style = EditListStyles.commonEntryStyle,
				WidthRequest = 250.0

			};

			if (targetMpdel != null) explain.Text = targetMpdel.explain;

			layout.Children.Add(explain);

			editListDic[step].explain = explain;

			layout.Margin = new Thickness
			{
				Left = 15.0
			};

			return new ContentView { Content = layout };

		}

		public void onPickerChangeIndex(object sender, EventArgs e)
		{

			var picker = (Picker)sender;
			var step = picker.AutomationId;
			var parLayout = (StackLayout)picker.Parent;

			if (parLayout == null) return;

			var parContent = (ContentView)parLayout.Parent;
			var baseLayout = (StackLayout)parContent.Parent;

			var targetuuid = parLayout.AutomationId;
			var targetmodel = editListDic[step].culculates[getCulCuModelIndexByUUID(editListDic[step].culculates, targetuuid)];

			string xtypeselect = SpecialUtil.typeConvertList[targetmodel.xtypePick.Items[targetmodel.xtypePick.SelectedIndex]];
			string ytypeselect = SpecialUtil.typeConvertList[targetmodel.ytypePick.Items[targetmodel.ytypePick.SelectedIndex]];

			switch (xtypeselect)
			{
				case JAN.Type_Constant_Value:
					targetmodel.xConstantEntry.IsVisible = true;
					targetmodel.xstepPick.IsVisible = false;
					break;
				case JAN.Type_Input_Value:
					targetmodel.xConstantEntry.IsVisible = false;
					targetmodel.xstepPick.IsVisible = true;
					break;
				case JAN.Type_Previous_Culculate_Result_Value:
					targetmodel.xConstantEntry.IsVisible = false;
					targetmodel.xstepPick.IsVisible = false;
					break;
				case JAN.Type_Last_Culculate_Result_Value:
					targetmodel.xConstantEntry.IsVisible = false;
					targetmodel.xstepPick.IsVisible = true;
					break;
				default: break;
			}

			switch (ytypeselect)
			{
				case JAN.Type_Constant_Value:
					targetmodel.yConstantEntry.IsVisible = true;
					targetmodel.ystepPick.IsVisible = false;
					break;
				case JAN.Type_Input_Value:
					targetmodel.yConstantEntry.IsVisible = false;
					targetmodel.ystepPick.IsVisible = true;
					break;
				case JAN.Type_Previous_Culculate_Result_Value:
					targetmodel.yConstantEntry.IsVisible = false;
					targetmodel.ystepPick.IsVisible = false;
					break;
				case JAN.Type_Last_Culculate_Result_Value:
					targetmodel.yConstantEntry.IsVisible = false;
					targetmodel.ystepPick.IsVisible = true;
					break;
				default: break;
			}
		}

		public void onCancel(object sender,EventArgs e){

			mypair.refreshEditList();

		}

		public bool validate()
		{

			bool final = false;

			if (nameEntry.Text == null || nameEntry.Text.Equals(""))
			{
				mypair.errorMessageShow("エラー", "名前を入力してください");
				return false;
			}

			foreach (var key in editListDic.Keys)
			{
				if (key.Equals("Step1"))continue;
				

				if(!final)final = editListDic[key].final.IsToggled;

				foreach (CulcModel cul in editListDic[key].culculates)
				{

					string xtypeselect = SpecialUtil.typeConvertList[cul.xtypePick.Items[cul.xtypePick.SelectedIndex]];
					string ytypeselect = SpecialUtil.typeConvertList[cul.ytypePick.Items[cul.ytypePick.SelectedIndex]];

					switch (xtypeselect)
					{
						case JAN.Type_Constant_Value:
							int value;
							if (!int.TryParse(cul.xConstantEntry.Text, out value))
							{
								mypair.errorMessageShow("エラー", "定数には数字を入力してください");
								return false;
							}
							break;
						case JAN.Type_Input_Value:
							break;
						case JAN.Type_Previous_Culculate_Result_Value:
							break;
						case JAN.Type_Last_Culculate_Result_Value:
							break;
						default: break;
					}

					switch (ytypeselect)
					{
						case JAN.Type_Constant_Value:
							int value;
							if (!int.TryParse(cul.yConstantEntry.Text, out value)) {
								mypair.errorMessageShow("エラー", "定数には数字を入力してください");
								return false;
							}
							break;
						case JAN.Type_Input_Value:
							break;
						case JAN.Type_Previous_Culculate_Result_Value:
							break;
						case JAN.Type_Last_Culculate_Result_Value:
							break;
						default: break;
					}
				}
			}

			if(!final){
				mypair.errorMessageShow("エラー", "いずれかのStepを最終ステップに設定してください");
				return false;
			}

			return true;
		}
	}
}
