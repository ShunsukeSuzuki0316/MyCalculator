using System;
using Xamarin.Forms;

namespace MyCalculator
{
	/// <summary>
	/// Integrity.
	/// 整合性チェッククラス
	/// </summary>
	public static class Integrity
	{
		public static void IntegrityMain(object sender, Label resultText, Label explainText)
		{
			var button = (Button)sender;
			string pressed = button.Text;

			//現在の表示値を一度クリアする必要があるか判定します。(演算方法を選択された直後か判定)
			if (checkNeedNowResultTextClear(resultText))
			{
				//一度現在の表示値を0に設定
				resultText.Text = "0";
			}

			//押された数字が追加可能か確認を行います
			if (checkEnablePressedNumberAdd(pressed, resultText.Text))
			{
				if (resultText.Text.Equals("0") && !pressed.Equals(V.PeriodMarkS))
				{
					//表示値が0の時にピリオド以外の数字が入力された場合は、連結ではなく押された数値をそのまま表示値にする
					resultText.Text = pressed;

				}
				else {
					resultText.Text += pressed;
				}

				if (S.xText != null && S.calculateType.Count != 0)
				{
					S.yText = resultText.Text;
				}
				else {

					S.xText = resultText.Text;
				}
			}

		}

		/// <summary>
		/// 現在入力されている値をクリアする必要があるか判定します
		/// </summary>
		/// <returns><c>true</c>, if need now result text clear was checked, <c>false</c> otherwise.</returns>
		public static bool checkNeedNowResultTextClear(Label resultText)
		{

			//値が指数表記の場合は例外的にクリアを実施する
			if (System.Text.RegularExpressions.Regex.Matches(resultText.Text, @".*\d+[eE].*").Count > 0)
			{
				return true;
			}

			if (S.calculateType.Count == 0) { return false; }
			if (S.yText != null) { return false; }

			return true;

		}

		/// <summary>
		/// 現在の数字文字列に入力された数字文字列が追加できるかチェックします
		/// </summary>
		/// <returns><c>true</c>, if enable pressed number add was checked, <c>false</c> otherwise.</returns>
		/// <param name="pressed">入力された数字文字列</param>
		/// <param name="nowText">現在の数字文字列</param>
		public static bool checkEnablePressedNumberAdd(string pressed, string nowText)
		{
			//最大数字数のチェック(ピリオドを除く)
			if (System.Text.RegularExpressions.Regex.Matches(nowText, @"\d").Count >= V.numTextLengthMax)
			{
				return false;
			}

			if (pressed.Equals(V.PeriodMarkS) && nowText.IndexOf(V.PeriodMarkC) != -1)
			{
				//押下されたボタンがピリオドで、且つ現在表示されている数字にピリオドが存在している場合はリターン
				return false;
			}
			else if (pressed.Equals("0") && nowText.IndexOf(V.PeriodMarkC) == -1 && nowText.StartsWith("0", StringComparison.CurrentCulture))
			{
				//押下されたボタンが0で、且つ現在表示されている数字にピリオドが含まれておらず0から数字が開始されている場合はリターン
				return false;
			}

			return true;
		}
	}
}
