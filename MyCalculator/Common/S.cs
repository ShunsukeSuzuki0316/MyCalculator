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
		/// x数値
		/// </summary>
		public static decimal x;
		/// <summary>
		/// y数値
		/// </summary>
		public static decimal y;
		/// <summary>
		/// 表示されているxのテキスト
		/// </summary>
		public static string xText;
		/// <summary>
		/// 表示されているyのテキスト
		/// </summary>
		public static string yText;

		/// <summary>
		/// 基本の四則演算モード以外のモードが有効か判定
		/// </summary>
		public static bool specFlag = false;
		/// <summary>
		/// 特殊入力モードか判定します
		/// </summary>
		public static bool specKey = false;

		/// <summary>
		/// 計算結果を格納します
		/// </summary>
		public static List<decimal> result = new List<decimal>();
		/// <summary>
		/// 計算方法を格納します
		/// </summary>
		public static List<Delegate> calculateType = new List<Delegate>();
		/// <summary>
		/// xとyの組み合わせを格納します
		/// </summary>
		public static Dictionary<decimal, decimal> nums = new Dictionary<decimal, decimal>();
		/// <summary>
		/// 選択状態にある特別計算のクラス名を保持します
		/// </summary>
		public static Type selectSpecialClass = null;
		/// <summary>
		/// 選択中のdelegateを保持します
		/// </summary>
		public static Delegate selectDelegate = null;
		/// <summary>
		/// 特別計算クラス終了フラグ
		/// </summary>
		public static bool finalFlag = false;
		/// <summary>
		/// 特別計算クラス途中経過スタックディクショナリ
		/// </summary>
		public static Dictionary<string, Dictionary<string, string>> stackDictionary = new Dictionary<string, Dictionary<string, string>>();

		public static String dbPath = null;

		public static UserCulculateModel selectUserModel = null;

		public static decimal LastCulculateResult = 0;

		public static void Clear()
		{
			x = 0;
			y = 0;
			xText = null;
			yText = null;

			//計算結果を格納します。
			result.Clear();
			//計算方法を格納します。
			calculateType.Clear();
			//xとyの組み合わせを格納します
			nums.Clear();
			selectSpecialClass = null;
			specFlag = false;
			finalFlag = false;
			selectDelegate = null;
			stackDictionary.Clear();
		}

		public static void AllClear()
		{
			x = 0;
			y = 0;
			xText = null;
			yText = null;

			//計算結果を格納します。
			result.Clear();
			//計算方法を格納します。
			calculateType.Clear();
			//xとyの組み合わせを格納します
			nums.Clear();
			selectSpecialClass = null;
			specFlag = false;
			finalFlag = false;
			selectDelegate = null;
			stackDictionary.Clear();
		}
	}
}
