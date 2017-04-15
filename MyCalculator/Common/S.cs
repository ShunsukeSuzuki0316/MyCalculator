using System;
using System.Collections.Generic;

namespace MyCalculator
{
	/// <summary>
	/// 共有値クラス
	/// </summary>
	public static class S
	{
		/// <summary>
		/// 入力が確定された数値を格納します
		/// </summary>
		public static List<decimal> inputValue = new List<decimal>();
		/// <summary>
		/// 入力された数値・演算子を格納します
		/// </summary>
		public static List<string> inputFormula = new List<string>();
		/// <summary>
		/// 選択状態にある特別計算のクラス名を保持します
		/// </summary>
		public static Type selectSpecialClass = null;
		/// <summary>
		/// 特別計算クラス終了フラグ
		/// </summary>
		public static bool finalFlag = false;
		/// <summary>
		/// 特別計算クラス途中経過スタックディクショナリ
		/// </summary>
		public static List<Dictionary<string, string>> stackDictionary = new List<Dictionary<string, string>>();
		/// <summary>
		/// 各プラットフォームのDBのパス
		/// </summary>
		public static String dbPath = null;
		/// <summary>
		/// 選択されたユーザの計算
		/// </summary>
		public static UserCulculateModel selectUserModel = null;
		/// <summary>
		/// 特別計算の最後の結果
		/// </summary>
		public static decimal LastCulculateResult = 0;

		public static double DisplayWidth = 0.0d;

		public static double DisplayHieght = 0.0d;

		/// <summary>
		/// 特別計算で使用する式を格納します
		/// </summary>
		public static Queue<CulculateFormulaJsonModel> formulaQueue = new Queue<CulculateFormulaJsonModel>();

		/// <summary>
		/// 最初の式であるかのフラグ
		/// </summary>
		public static bool firstFormula = true;


		/// <summary>
		/// 各種共通で扱う静的変数を初期化します
		/// </summary>
		public static void InitParameter()
		{
			//最初の式フラグを戻します
			firstFormula = true;
			//式のキューをリセットします
			formulaQueue.Clear();
			//選択したクラスをリセットします
			selectSpecialClass = null;
			//計算の途中結果をスタックするディクショナリをクリアします
			stackDictionary.Clear();
			//最終式フラグを戻します
			finalFlag = false;
			//選択したユーザモデルをクリアします
			selectUserModel = null;

			inputValue.Clear();
			inputFormula.Clear();
		}
	}
}
