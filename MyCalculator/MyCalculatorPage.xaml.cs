using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms;
using System.Linq;

namespace MyCalculator
{
	public partial class MyCalculatorPage : TabbedPage
	{
		public void OnPastNumber(object sender, EventArgs e)
		{
			var clipboardText = DependencyService.Get<IClipBoard>().GetTextFromClipBoard();

			decimal result;

			if (decimal.TryParse(clipboardText, out result)) {
				resultText.Text = clipboardText;
				F.insertValue(resultText.Text);
			}
		}

		public void OnCopyNumber(object sender, EventArgs e)
		{
			DependencyService.Get<IClipBoard>().SetTextToClipBoard(resultText.Text);
		}

		public MyCalculatorPage()
		{
			InitializeComponent();
			Cul.Icon = "culicon20";
			CulList.Icon = "listicon20";
			CreateCul.Icon = "editicon20";

			var db = new SQLiteConnection(S.dbPath);
			db.CreateTable<UserCulculateModel>();
			db.CreateTable<DefaultCulculateModel>();

			//delete();

			setup();

			CulList.Content = new CreateList().createTable(this);
			CreateCul.Content = new CreateMode().createTable(this);

			OnClear(clearButton, null);

			S.DisplayWidth = DependencyService.Get<DisplaySize>().getWidth();

			S.DisplayHieght = DependencyService.Get<DisplaySize>().getHieght();


			//文字数からフォントサイズを計算してフォントサイズを変更します
			resultText.PropertyChanged += (sender, args) => {
				//--- 文字数に合わせてフォントサイズを変更
				if (resultText.Text.Length > 8) {
					double size = (resultText.Width / resultText.Text.Length) * 1.6;

					if (resultText.FontSize != size)resultText.FontSize = size;
					
				} else {
					double size = V.FontSizeBase;
					if (resultText.FontSize != size)resultText.FontSize = size;
				}
			};
		}

		void setup(){
			if(UserCulculateUtil.getAllUserCulculate().Count == 0){

				UserCulculateModel u = new UserCulculateModel();

				u.DisplayName = "円の面積";
				u.Name = "Special";
				u.Culculate = "{\"formula\":[{\"explain\":\"半径を入力して下さい\",\"execute\":[],\"last\":false},{\"explain\":\"円の面積を計算しました\",\"execute\":[{\"xtype\":\"input_value\",\"xtarget\":\"0\",\"ytype\":\"input_value\",\"ytarget\":\"0\",\"culculateMethod\":\"multiplication\"},{\"xtype\":\"previous_culculate_result_value\",\"xtarget\":null,\"ytype\":\"constant_value\",\"ytarget\":\"3.14\",\"culculateMethod\":\"multiplication\"}],\"last\":true}]}";

				UserCulculateUtil.insertUserCulculate(u);

			}
		}

		/// <summary>
		/// 画面上にアラートを出すための共通処理
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		public void errorMessageShow(string title, string message)
		{
			DisplayAlert(title, message, "OK");
		}
		/// <summary>
		/// ヘルプ画面を表示する(モーダル)
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public async void onPopupHelpPage(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new HelpPage());
		}

		void delete()
		{
			List<UserCulculateModel> usrs = UserCulculateUtil.getAllUserCulculate();

			foreach (UserCulculateModel usr in usrs) {
				UserCulculateUtil.deleteCulculateById(usr.Id);
			}
		}

		/// <summary>
		/// 数字のボタンが押された時に実行される
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnSelectNumber(object sender, EventArgs e)
		{
			onButtonBorderClear();

			Integrity.IntegrityMain(sender, resultText, explainText);

			if (S.formulaQueue.Count==0)
				explainText.Text = "";

			if (!resultText.Text.Equals("0"))
				clearButton.Text = "C";
		}

		/// <summary>
		/// 数字・計算・特殊以外のボタンが押された時に実行される
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnSelectOption(object sender, EventArgs e)
		{
			var button = (Button)sender;
			string pressed = button.Text;

			resultText.Text = D.getDelegate(typeof(OP), U.getMethodNameFromMark(pressed))
				.DynamicInvoke()
				.ToString();
			
			F.insertValue(resultText.Text);
		}

		/// <summary>
		/// 特殊ボタンが押された時に実行される
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public void OnSpecial(object sender, EventArgs e)
		{
			CurrentPage = Children.First(page => page == Cul);

			onButtonBorderClear();
			enterButton.Text = V.NEXT_MARK;

			S.selectSpecialClass = Type.GetType("MyCalculator." + ((Button)sender).Text);

			CulculateJsonModel cj = JsonConvert.DeserializeObject<CulculateJsonModel>(S.selectUserModel.Culculate);

			cj.formula.ForEach(fl => S.formulaQueue.Enqueue(fl));

			setResultDic((Dictionary<string, string>)
			             D.getDelegate(S.selectSpecialClass, "execute")
			             .DynamicInvoke(new object[] { S.formulaQueue.Dequeue() }));
		}

		/// <summary>
		/// 演算ボタンが押された時に実行される
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnSelectOperator(object sender, EventArgs e)
		{
			var button = (Button)sender;
			string pressed = button.Text;

			onButtonBorderClear();

			if (S.formulaQueue.Count != 0) return;

			if(F.isLastNum() && F.getLastMethod() != null){
				resultText.Text = F.culc();
				//F.insertValue(resultText.Text);
			}

			button.BorderWidth = 2.0;

			F.insertValue(U.getMethodNameFromMark(pressed));
		}

		/// <summary>
		/// クリアボタンが押された時に実行される
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnClear(object sender, EventArgs e)
		{
			var btn = (Button)sender;

			if ("C".Equals(btn.Text)) {

				resultText.Text = "0";

				clearButton.Text = "AC";

			} else {
				AllClear();
			}
		}

		/// <summary>
		/// ACオールクリアの処理
		/// </summary>
		void AllClear(){
			//電卓表示値を0に戻します
			resultText.Text = "0";
			//説明テキストをからにします
			explainText.Text = "";
			//=ボタンを元に戻します
			enterButton.Text = "=";
			//ボタンのボーダーを元に戻します
			onButtonBorderClear();
			//共有パラメータを初期化します
			S.InitParameter();
		}

		/// <summary>
		/// =ボタンが押された時に実行される
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnCalculate(object sender, EventArgs e)
		{
			try {
				if (S.formulaQueue.Count != 0) {

					S.inputValue.Add(decimal.Parse(resultText.Text));

					setResultDic((Dictionary<string, string>)D.getDelegate(S.selectSpecialClass, "execute")
					             .DynamicInvoke(new object[] { S.formulaQueue.Dequeue() }));

					if (S.finalFlag) {
						S.finalFlag = false;
						S.formulaQueue.Clear();
						enterButton.Text = "=";
					}
				} else if(F.getLastMethod() != null) {

					if (!F.isLastNum()) {
						F.insertValue(F.getLastNum());
					}

					resultText.Text = F.culc(); 

					S.inputFormula.Clear();

					F.insertValue(resultText.Text);

					onButtonBorderClear();
				}
			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine(ex);

				AllClear();
				explainText.Text = "エラーが発生しました。";
			}
		}

		/// <summary>
		/// 作成画面を初期状態に戻します
		/// </summary>
		public void refreshEditList()
		{
			CulList.Content = new CreateList().createTable(this);
			CreateCul.Content = new CreateMode().createTable(this);
		}

		/// <summary>
		/// 作成画面を更新モードで開きます
		/// </summary>
		/// <param name="target">Target.</param>
		public void updateCulculate(int target)
		{
			CreateCul.Content = new UpdateMode().updateTable(this, UserCulculateUtil.getUserCulculateById(target));
			CurrentPage = Children.First(page => page == CreateCul);
		}

		/// <summary>
		/// 特殊計算の各ステップで共通ディクショナリに格納された値を反映する
		/// </summary>
		/// <param name="result">Result.</param>
		void setResultDic(Dictionary<string, string> result)
		{
			resultText.Text = result[V.MainDicKeyName_ResultText];
			explainText.Text = result[V.MainDicKeyName_ExplainText];
			S.finalFlag = bool.Parse(result[V.MainDicKeyName_Final]);
			F.insertValue(resultText.Text);
		}

		/// <summary>
		/// 演算ボタンの枠線を初期化
		/// </summary>
		void onButtonBorderClear()
		{
			additionButton.BorderWidth = 0;
			divisionButton.BorderWidth = 0;
			multiplicationButton.BorderWidth = 0;
			subtractionButton.BorderWidth = 0;
		}
	}
}