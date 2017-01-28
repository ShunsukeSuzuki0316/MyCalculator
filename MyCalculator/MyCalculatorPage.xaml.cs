using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms;


//クリップボード

namespace MyCalculator
{
	public partial class MyCalculatorPage : TabbedPage
	{
		void  copyResult(){



		}

		public void OnPastNumber(object sender, EventArgs e)
		{

			if (S.specKey) return;

			var clipboardText = DependencyService.Get<IClipBoard>().GetTextFromClipBoard();

			decimal result;

			if(decimal.TryParse(clipboardText.ToString(),out result)){

				resultText.Text = clipboardText;

				if (S.xText != null && S.calculateType.Count != 0)
				{
					S.yText = resultText.Text;
				}
				else {

					S.xText = resultText.Text;
				}
			}
		}

		public void OnCopyNumber(object sender, EventArgs e){
			if (S.specKey) return;
			DependencyService.Get<IClipBoard>().SetTextToClipBoard(resultText.Text);
		}

		public MyCalculatorPage()
		{
			InitializeComponent();

			var db = new SQLiteConnection(S.dbPath);
			//db.DropTable<UserCulculateModel>();
			db.CreateTable<UserCulculateModel>();
			//UserCulculateUtil.insertUserCulculate("CurrencyChange","","通貨取得2",1);
			//UserCulculateUtil.insertUserCulculate("GetDay", "", "曜日取得2",1);
			db.CreateTable<DefaultCulculateModel>();

			delete();

			CulList.Content = new CL().createTable(this);
			CreateCul.Content = new CreateMode().createTable(this);

			OnClear(this.clearButton, null);
		}

		public void errorMessageShow(string title,string message){

			DisplayAlert(title, message, "OK");

		}

		void delete(){
			List<UserCulculateModel> usrs = UserCulculateUtil.getAllUserCulculate();

			foreach(UserCulculateModel usr in usrs){

				bool flg = true;

				if(usr.Culculate == null || usr.Culculate.Equals("")){
					UserCulculateUtil.deleteCulculateById(usr.Id);
					continue;
				}

				var model = JsonConvert.DeserializeObject<CulculateJsonModel>(usr.Culculate);
				foreach(StepModel step in model.step){
					
					if(step.last){
						flg = false;
					}
				}

				if(flg){
					UserCulculateUtil.deleteCulculateById(usr.Id);
				}

			}
		}

		/// <summary>
		/// 数字のボタンが押された時に実行される
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
	   void OnSelectNumber(object sender, EventArgs e)
		{

			if (S.specKey)
			{
				var button = (Button)sender;
				string pressed = button.Text;
				if (!pressed.Equals(V.PeriodMarkS)) resultText.Text = pressed;
			}
			else {
				Integrity.IntegrityMain(sender, resultText, explainText);
			}

			if (!S.specFlag) explainText.Text = "";

			if (!resultText.Text.Equals("0")) clearButton.Text = "C";
			U.setFontSize(resultText);
		}

		/// <summary>
		/// 数字・計算・特殊以外のボタンが押された時に実行される
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnSelectOption(object sender, EventArgs e)
		{
			if (!S.specKey)
			{
				var button = (Button)sender;
				string pressed = button.Text;

				resultText.Text = D.getDelegate(typeof(OP), U.getMethodNameFromMark(pressed))
					.DynamicInvoke(new object[] { S.xText, S.yText })
					.ToString();
			}

			U.setFontSize(resultText);
		}

		/// <summary>
		/// 特殊ボタンが押された時に実行される
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public void OnSpecial(object sender, EventArgs e)
		{
			this.CurrentPage = this.Children[0];

			S.AllClear();
			onButtonBorderClear();
			enterButton.Text = "→";
			var button = (Button)sender;
			string pressed = button.Text;

			S.specFlag = true;

			S.calculateType.Clear();

			S.selectSpecialClass = Type.GetType("MyCalculator." + pressed);

			S.calculateType.Add(D.getDelegate(S.selectSpecialClass, S.selectSpecialClass.Name.Equals("Special") ? "Step" : "Step1"));

			setResultDic((Dictionary<string, string>)S.calculateType[0].DynamicInvoke());

			U.setFontSize(resultText);

		}

		/// <summary>
		/// 演算ボタンが押された時に実行される
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnSelectOperator(object sender, EventArgs e)
		{
			if (!S.specKey)
			{
				var button = (Button)sender;
				string pressed = button.Text;

				onButtonBorderClear();

				if (S.calculateType.Count == 0)
				{
					S.calculateType.Add(D.getDelegate(typeof(BC), U.getMethodNameFromMark(pressed)));
					button.BorderWidth = 2.0;
				}
				else {
					S.calculateType.Remove(S.calculateType[0]);
					S.calculateType.Add(D.getDelegate(typeof(BC), U.getMethodNameFromMark(pressed)));
					button.BorderWidth = 2.0;
				}
				S.x = decimal.Parse(resultText.Text);
				S.xText = resultText.Text;
			}

			U.setFontSize(resultText);
		}

		/// <summary>
		/// クリアボタンが押された時に実行される
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnClear(object sender, EventArgs e)
		{
			var btn = (Button)sender;

			if("C".Equals(btn.Text)){

				resultText.Text = "0";

				if (S.xText != null && S.calculateType.Count != 0)
				{
					S.yText = resultText.Text;
				}
				else {

					S.xText = resultText.Text;
				}

				clearButton.Text = "AC";

			}else{
				
				resultText.Text = "0";
				explainText.Text = "";
				enterButton.Text = "=";
				setButton(V.NormalKeyListIndex);
				onButtonBorderClear();
				S.Clear();
			
			}

			U.setFontSize(resultText);
		}

		/// <summary>
		/// =ボタンが押された時に実行される
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnCalculate(object sender, EventArgs e)
		{
			try
			{
				if (S.specFlag)
				{
					S.xText = resultText.Text;
					S.calculateType.Add(D.getDelegate(S.selectSpecialClass, S.selectSpecialClass.Name.Equals("Special") ? "Step" : "Step" + (S.calculateType.Count + 1)));
					setResultDic((Dictionary<string, string>)S.calculateType[S.calculateType.Count - 1].DynamicInvoke());

					if (S.finalFlag)
					{
						S.specFlag = false;
						S.finalFlag = false;
						S.calculateType.Clear();
						enterButton.Text = "=";
					}

				}
				else {

					if (S.xText != null && S.yText != null)
					{
						resultText.Text = S.calculateType[0].DynamicInvoke(new object[] { decimal.Parse(S.xText), decimal.Parse(S.yText) }).ToString();

						S.yText = null;
						S.xText = resultText.Text;
						S.calculateType.Clear();

						onButtonBorderClear();
					}

				}

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);

				resultText.Text = "0";
				explainText.Text = "エラーが発生しました。";
				S.yText = null;
				S.xText = resultText.Text;
				S.calculateType.Clear();

				onButtonBorderClear();
			}

			U.setFontSize(resultText);
		}

		public void refreshEditList(){
			CulList.Content = new CL().createTable(this);
			CreateCul.Content = new CreateMode().createTable(this);
		}

		public void updateCulculate(int target)
		{
			CreateCul.Content = new UpdateMode().updateTable(this, UserCulculateUtil.getUserCulculateById(target));
			this.CurrentPage = this.Children[2];
		}

		protected void OnViewCellTapped(object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("Tapped");
		}

		/// <summary>
		/// 特殊計算の各ステップで共通ディクショナリに格納された値を反映する
		/// </summary>
		/// <param name="result">Result.</param>
		void setResultDic(Dictionary<string, string> result)
		{
			{
				resultText.Text = result[V.MainDicKeyName_ResultText];
				explainText.Text = result[V.MainDicKeyName_ExplainText];
				S.xText = result[V.MainDicKeyName_XText];
				S.yText = result[V.MainDicKeyName_YText];
				S.finalFlag = bool.Parse(result[V.MainDicKeyName_Final]);
				S.specKey = bool.Parse(result[V.MainDicKeyName_SpecKeyFlg]);
				if (S.specKey)
				{
					setButton(int.Parse(result[V.MainDicKeyName_SpecKeyIndex]));
				}
				else {
					setButton(V.NormalKeyListIndex);
				}
			}
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

		/// <summary>
		/// キーインデックスに従ってボタンの表記を変更
		/// </summary>
		/// <param name="keyListIndex">Key list index.</param>
		void setButton(int keyListIndex)
		{

			List<string> keys = V.KeyList[keyListIndex];
			_0Button.Text = keys[0];
			_1Button.Text = keys[1];
			_2Button.Text = keys[2];
			_3Button.Text = keys[3];
			_4Button.Text = keys[4];
			_5Button.Text = keys[5];
			_6Button.Text = keys[6];
			_7Button.Text = keys[7];
			_8Button.Text = keys[8];
			_9Button.Text = keys[9];
			if (keyListIndex == V.NormalKeyListIndex)
			{
				S.specKey = false;
			}
			else {
				resultText.Text = keys[0];
				S.xText = keys[0];
			}
		}
	}
}