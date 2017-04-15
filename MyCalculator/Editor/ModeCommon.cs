using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MyCalculator
{
	public class ModeCommon
	{
		MyCalculatorPage mypair;

		List<StackLayout> formulaList;

		List<EditorBaseModel> editList;

		StackLayout baseContentLayout;

		public Entry nameEntry = null;


		public ModeCommon(List<StackLayout> formulaList,
		                  List<EditorBaseModel> editList, 
		                  StackLayout baseContentLayout, MyCalculatorPage mypair
		                 )
		{
			this.formulaList = formulaList;
			this.editList = editList;
			this.baseContentLayout = baseContentLayout;
			this.mypair = mypair;
		}

		public StackLayout createTypePicker()
		{
			var layout = new StackLayout { 
				Style = EditorStyles.culcLayoutStyle 
			};
			var label = new Label { 
				Style = EditorStyles.culcLabelStyle,
				Text = "対象　" 
			};
			var picker = new Picker { 
				Style = EditorStyles.typePickerStyle 
			};

			SpecialUtil.typeList.ForEach((item) => picker.Items.Add(item));
			picker.SelectedIndex = 0;

			layout.Children.Add(label);
			layout.Children.Add(picker);

			return layout;
		}
		public StackLayout createCalculateTypePicker()
		{
			var layout = new StackLayout { 
				Style = EditorStyles.culcLayoutStyle 
			};
			var label = new Label { 
				Style = EditorStyles.culcLabelStyle, Text = "演算子" 
			};
			var picker = new Picker { 
				Style = EditorStyles.culcPickerStyle
			};

			SpecialUtil.culcTypeList.ForEach((item) => picker.Items.Add(item));
			picker.SelectedIndex = 0;

			layout.Children.Add(label);
			layout.Children.Add(picker);

			return layout;
		}
		public StackLayout createFormulaPicker(int calledIndex)
		{
			var layout = new StackLayout { 
				Style = EditorStyles.culcLayoutStyle 
			};
			var label = new Label { 
				Style = EditorStyles.culcLabelStyle,
				Text = "値　　" 
			};
			var picker = new Picker { 
				Style = EditorStyles.stepPickerStyle 
			};

			Enumerable.Range(1, calledIndex).ToList().ForEach(i => picker.Items.Add("ステップ"+i));

			picker.SelectedIndex = 0;

			layout.Children.Add(label);
			layout.Children.Add(picker);

			return layout;
		}

		/// <summary>
		/// 計算項目作成
		/// </summary>
		/// <returns>The culculate content.</returns>
		/// <param name="targetModel">更新モードの場合に指定</param>
		public ContentView createCulculateContent(ExecuteJsonModel targetModel,int calledIndex)
		{
			string uuid = Guid.NewGuid().ToString();

			var culcModel = new CulcModel();

			//ベースコンテント
			var returnContetnView = new ContentView { BackgroundColor = Color.FromHex("#E8E8E8") };

			//ベースレイアウト
			var layout = new StackLayout { Style = EditorStyles.culculateContentLayoutStyle, AutomationId = uuid };
			returnContetnView.Content = layout;

			//--- X種別ピッカーの作成
			var xtypePickLayout = createTypePicker();
			var xtypePick = (Picker)xtypePickLayout.Children.First(i => i is Picker);

			//--- Y種別ピッカーの作成
			var ytypePickLayout = createTypePicker();
			var ytypePick = (Picker)ytypePickLayout.Children.First(i => i is Picker);

			//--- 演算子ピッカーの作成
			var culcPickLayout = createCalculateTypePicker();
			var culcPick = (Picker)culcPickLayout.Children.First(i => i is Picker);

			//--- XStepピッカーの作成
			var xFormulaPickLayout = createFormulaPicker(calledIndex);
			var xFormulaPick = (Picker)xFormulaPickLayout.Children.First(i => i is Picker);

			//--- YStepピッカーの作成
			var yFormulaPickLayout = createFormulaPicker(calledIndex);
			var yFormulaPick = (Picker)yFormulaPickLayout.Children.First(i => i is Picker);

			//--- 計算削除ボタンの作成
			var deleteCulculateButton = new Button { 
				Text = "計算削除",
				Style = EditorStyles.deleteButtonStyle 
			};
			var deleteCulculateButtonLayout = new StackLayout { 
				Style = EditorStyles.culcLayoutStyle, 
				HorizontalOptions = LayoutOptions.Center 
			};

			deleteCulculateButtonLayout.Children.Add(deleteCulculateButton);

			//--- 削除ボタンのクリックイベント
			int deleteTarget = formulaList.Count - 1;
			deleteCulculateButton.Clicked += (sender, args) => {
				editList[deleteTarget].culculates.Remove(culcModel);
				((StackLayout)returnContetnView.Parent).Children.Remove(returnContetnView);
			};

			//--- Y定数エントリーの作成
			var yConstantEntry = new Entry {
				Style = EditorStyles.constantEntryStyle
			};
			var yConstantEntryLayout = new StackLayout { 
				Style = EditorStyles.culcLayoutStyle, 
				IsVisible = false 
			};
			var yConstantEntryLabel = new Label { 
				Style = EditorStyles.culcLabelStyle, 
				Text = "値　　" 
			};
			yConstantEntryLayout.Children.Add(yConstantEntryLabel);
			yConstantEntryLayout.Children.Add(yConstantEntry);

			//--- Y定数エントリーのテキスト変更時のイベント
			yConstantEntry.TextChanged += (sender, args) => {
				//--- 文字数が最大値の場合はテキストを変更しない
				string _text = yConstantEntry.Text;
				if (_text.Length > V.numTextLengthMax) {
					_text = _text.Remove(_text.Length - 1);
					yConstantEntry.Text = _text;
				}
			};

			//--- X定数エントリーの作成
			var xConstantEntry = new Entry {
				Style = EditorStyles.constantEntryStyle
			};
			var xConstantEntryLayout = new StackLayout { 
				Style = EditorStyles.culcLayoutStyle, 
				IsVisible = false 
			};
			var xConstantEntryLabel = new Label { 
				Style = EditorStyles.culcLabelStyle, 
				Text = "値　　" 
			};
			xConstantEntryLayout.Children.Add(xConstantEntryLabel);
			xConstantEntryLayout.Children.Add(xConstantEntry);

			//--- X定数エントリーのテキスト変更時のイベント
			xConstantEntry.TextChanged += (sender, args) => {
				//--- 文字数が最大値の場合はテキストを変更しない
				string _text = xConstantEntry.Text;
				if (_text.Length > V.numTextLengthMax) {
					_text = _text.Remove(_text.Length - 1);
					xConstantEntry.Text = _text;
				}
			};

			//--- ベースレイアウトに作成したパーツを格納
			layout.Children.Add(xtypePickLayout);
			layout.Children.Add(xFormulaPickLayout);
			layout.Children.Add(xConstantEntryLayout);
			layout.Children.Add(culcPickLayout);
			layout.Children.Add(ytypePickLayout);
			layout.Children.Add(yFormulaPickLayout);
			layout.Children.Add(yConstantEntryLayout);
			layout.Children.Add(deleteCulculateButtonLayout);

			//--- 計算モデルに作成したパーツを格納
			culcModel.xtypePick = xtypePick;
			culcModel.xstepPick = xFormulaPick;
			culcModel.xConstantEntry = xConstantEntry;
			culcModel.culcPick = culcPick;
			culcModel.ytypePick = ytypePick;
			culcModel.ystepPick = yFormulaPick;
			culcModel.yConstantEntry = yConstantEntry;
			culcModel.uuid = uuid;

			//--- 作成したモデルを作成リストに格納
			editList.Last().culculates.Add(culcModel);

			//--- X種別ピッカーのインデックス変更イベント
			xtypePick.SelectedIndexChanged += (sender, args) => {
				if (layout == null)
					return;
				
				//--- 選択種別によって表示する項目を制御する
				switch (SpecialUtil.typeConvertList[xtypePick.Items[xtypePick.SelectedIndex]]) {
					case JAN.Type_Constant_Value:
						xConstantEntryLayout.IsVisible = true;
						xFormulaPickLayout.IsVisible = false;
						break;
					case JAN.Type_Input_Value:
					case JAN.Type_Last_Culculate_Result_Value:
						xConstantEntryLayout.IsVisible = false;
						xFormulaPickLayout.IsVisible = true;
						break;
					case JAN.Type_Previous_Culculate_Result_Value:
						xConstantEntryLayout.IsVisible = false;
						xFormulaPickLayout.IsVisible = false;
						break;
				}
			};

			//--- Y種別ピッカーのインデックス変更イベント
			ytypePick.SelectedIndexChanged += (sender, args) => {
				if (layout == null)
					return;

				//--- 選択種別によって表示する項目を制御する
				switch (SpecialUtil.typeConvertList[ytypePick.Items[ytypePick.SelectedIndex]]) {
					case JAN.Type_Constant_Value:
						yConstantEntryLayout.IsVisible = true;
						yFormulaPickLayout.IsVisible = false;
						break;
					case JAN.Type_Input_Value:
					case JAN.Type_Last_Culculate_Result_Value:
						yConstantEntryLayout.IsVisible = false;
						yFormulaPickLayout.IsVisible = true;
						break;
					case JAN.Type_Previous_Culculate_Result_Value:
						yConstantEntryLayout.IsVisible = false;
						yFormulaPickLayout.IsVisible = false;
						break;
				}
			};

			//--- 更新モードから呼ばれた場合は、各項目を読み込んだ状態に変更する
			if (targetModel != null) {

				xtypePick.SelectedIndex = SpecialUtil.realTypeList.IndexOf(targetModel.xtype);
				ytypePick.SelectedIndex = SpecialUtil.realTypeList.IndexOf(targetModel.ytype);

				culcPick.SelectedIndex = culcPick.Items.IndexOf(U.getMarkFromMethodName(targetModel.culculateMethod));

				if (JAN.Type_Constant_Value.Equals(targetModel.xtype))
					xConstantEntry.Text = targetModel.xtarget;

				if (JAN.Type_Input_Value.Equals(targetModel.xtype)
				   || JAN.Type_Last_Culculate_Result_Value.Equals(targetModel.xtype)) {
					xFormulaPick.SelectedIndex = int.Parse(targetModel.xtarget);
				}


				if (JAN.Type_Constant_Value.Equals(targetModel.ytype))
					yConstantEntry.Text = targetModel.ytarget;

				if (JAN.Type_Input_Value.Equals(targetModel.ytype)
				   || JAN.Type_Last_Culculate_Result_Value.Equals(targetModel.ytype)) {
					yFormulaPick.SelectedIndex = int.Parse(targetModel.ytarget);
				}
			}
			return returnContetnView;
		}

		/// <summary>
		/// 最終ステップのフラグのコンテントを作成
		/// </summary>
		/// <returns>The last content.</returns>
		/// <param name="targetModel">Target model.</param>
		public ContentView createLastContent(CulculateFormulaJsonModel targetModel)
		{
			var layout = new StackLayout { 
				Style = EditorStyles.lastContentLayoutStyle 
			};

			layout.Children.Add(new Label {
				Text = "最終ステップ",
				Style = EditorStyles.lastLableStyle
			});

			var final = new Switch {
				Style = EditorStyles.finalSwitchStyle
			};

			if (targetModel != null)
				final.IsToggled = targetModel.last;

			layout.Children.Add(final);

			editList.Last().final = final;

			return new ContentView { Content = layout };
		}

		/// <summary>
		/// 説明のコンテントを作成
		/// </summary>
		/// <returns>The explain content.</returns>
		/// <param name="targetMpdel">Target mpdel.</param>
		public ContentView createExplainContent(CulculateFormulaJsonModel targetMpdel)
		{
			var layout = new StackLayout { 
				Style = EditorStyles.explainContentLayoutStyle 
			};

			layout.Children.Add(new Label {
				Text = "説明　",
				Style = EditorStyles.culcLabelStyle
			});

			var explain = new Entry {
				Style = EditorStyles.explainEntryStyle
			};

			if (targetMpdel != null)
				explain.Text = targetMpdel.explain;

			layout.Children.Add(explain);

			editList.Last().explain = explain;

			return new ContentView { Content = layout };
		}

		/// <summary>
		/// ヘルプボタンのコンテントを作成
		/// </summary>
		/// <returns>The help content.</returns>
		public ContentView createHelpContent()
		{
			var layout = new StackLayout { 
				Style = EditorStyles.helpContentLayoutStyle 
			};

			var btn = new Button { 
				Style = EditorStyles.helpButtonStyle 
			};

			btn.Clicked += mypair.onPopupHelpPage;

			layout.Children.Add(btn);

			return new ContentView { Content = layout };
		}

		/// <summary>
		/// 名前のコンテントを作成
		/// </summary>
		/// <returns>The name content.</returns>
		/// <param name="name">Name.</param>
		public ContentView createNameContent(string name)
		{
			var layout = new StackLayout {
				Style = EditorStyles.nameContentLayoutStyle
			};

			layout.Children.Add(new Label {
				Text = "名前　",
				Style = EditorStyles.culcLabelStyle
			});

			nameEntry = new Entry {
				Style = EditorStyles.nameEntryStyle,
				Text = name
			};

			layout.Children.Add(nameEntry);

			return new ContentView { Content = layout };
		}


		/// <summary>
		/// 式のタイトルコンテントを作成
		/// </summary>
		/// <returns>The formula title content.</returns>
		public ContentView createFormulaTitleContent()
		{
			var layout = new StackLayout { 
				Style = EditorStyles.stepTitleContentLayoutStyle 
			};

			layout.Children.Add(new Label {
				Text = "ステップ" + editList.Count,
				Style = EditorStyles.culcLabelStyle,
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.Start
			});

			return new ContentView { Content = layout };
		}

		/// <summary>
		/// 編集ボタンのコンテントを作成
		/// </summary>
		/// <returns>The edit button content.</returns>
		/// <param name="showFormulaEdit">If set to <c>true</c> show step.</param>
		public ContentView createEditButtonContent(bool showFormulaEdit)
		{
			var layout = new StackLayout { 
				Style = EditorStyles.editButtonContentLayoutStyle 
			};

			var addCulculateButton = new Button {
				Text = "計算追加",
				Style = EditorStyles.editButtonStyle,
				BackgroundColor = Color.FromHex("#5beedc")
			};

			var addStepButton = new Button {
				Text = "ステップ追加",
				Style = EditorStyles.editButtonStyle,
				IsVisible = showFormulaEdit,
				BackgroundColor = Color.FromHex("#5beedc")
			};

			var deleteStepButton = new Button {
				Text = "ステップ削除",
				Style = EditorStyles.editButtonStyle,
				BackgroundColor = Color.FromHex("#f79b92"),
				IsVisible = showFormulaEdit
			};

			addCulculateButton.Clicked += onCulculateAdd;
			addStepButton.Clicked += onFormulaAdd;
			deleteStepButton.Clicked += onFormulaDelete;

			layout.Children.Add(addCulculateButton);
			layout.Children.Add(addStepButton);
			if (formulaList.Count > 2)
				layout.Children.Add(deleteStepButton);

			return new ContentView { Content = layout, Style = EditorStyles.editButtonContentStyle };
		}

		public void onFormulaDelete(object sender, EventArgs e)
		{
			var btn = (Button)sender;
			var target = btn.AutomationId;

			baseContentLayout.Children.Remove(formulaList.Last());

			formulaList.Remove(formulaList.Last());
			editList.Remove(editList.Last());

			formulaList.Last().Children.Add(createEditButtonContent(true));
		}

		public void onCulculateAdd(object sender, EventArgs e)
		{
			var btn = (Button)sender;
			var paretn = btn.Parent;

			StackLayout targetFormula = null;

			//押された計算追加ボタンがどこの式のボタンかを検索します
			while(targetFormula == null){
				//ボタンの親要素が式のレイアウトかどうか確認します
				var result = formulaList.Where(target => target.Equals(paretn));
				if (result.Count() != 0) targetFormula = result.First();
				//違った場合はさらにもう一つ上の親要素を確認します
				paretn = paretn.Parent;
			}

			int calledIndex = formulaList.IndexOf(targetFormula);

			formulaList[calledIndex].Children.Remove((formulaList[calledIndex].Children.Last()));
			formulaList[calledIndex].Children.Add(createCulculateContent(null,calledIndex));

			if (formulaList.Count < 2 ) {
				formulaList[calledIndex].Children.Add(createEditButtonContent(false));
			} else {
				if (formulaList.Count - 1 != formulaList.IndexOf(targetFormula)) {
					formulaList[calledIndex].Children.Add(createEditButtonContent(false));
				} else {
					formulaList[calledIndex].Children.Add(createEditButtonContent(true));
				}
			}
		}

		public void onFormulaAdd(object sender, EventArgs e)
		{
			var btn = (Button)sender;
			string step = btn.AutomationId;

			formulaList.Last().Children.Remove((formulaList.Last().Children.Last()));
			formulaList.Last().Children.Add(createEditButtonContent(false));

			var nextFormulaLayout = new StackLayout { Style = EditorStyles.nextStepLayoutStyle };

			formulaList.Add(nextFormulaLayout);

			editList.Add(new EditorBaseModel());

			editList.Last().culculates = new List<CulcModel>();

			formulaList.Last().Children.Add(createFormulaTitleContent());
			formulaList.Last().Children.Add(createExplainContent(null));
			formulaList.Last().Children.Add(createLastContent(null));
			formulaList.Last().Children.Add(createEditButtonContent(true));

			baseContentLayout.Children.Add(formulaList.Last());

		}

		public bool validate()
		{
			bool final = false;

			if (nameEntry.Text == null || nameEntry.Text.Equals("")) {
				mypair.errorMessageShow("エラー", "名前を入力してください");
				return false;
			}

			foreach (var target in editList) {
				if (target.final == null)
					continue;

				if (!final)
					final = target.final.IsToggled;

				foreach (CulcModel cul in target.culculates) {

					string xtypeselect = SpecialUtil.typeConvertList[cul.xtypePick.Items[cul.xtypePick.SelectedIndex]];
					string ytypeselect = SpecialUtil.typeConvertList[cul.ytypePick.Items[cul.ytypePick.SelectedIndex]];

					switch (xtypeselect) {
						case JAN.Type_Constant_Value:
							decimal value;
							if (!decimal.TryParse(cul.xConstantEntry.Text, out value)) {
								mypair.errorMessageShow("エラー", "定数には数字を入力してください");
								return false;
							}
							break;
					}

					switch (ytypeselect) {
						case JAN.Type_Constant_Value:
							decimal value;
							if (!decimal.TryParse(cul.yConstantEntry.Text, out value)) {
								mypair.errorMessageShow("エラー", "定数には数字を入力してください");
								return false;
							}
							break;
					}
				}
			}

			if (!final) {
				mypair.errorMessageShow("エラー", "いずれかのStepを最終ステップに設定してください");
				return false;
			}

			return true;
		}
	}
}
