using System;
using System.Collections.Generic;

namespace MyCalculator
{
	/// <summary>
	/// 定数クラス
	/// </summary>
	public static class V
	{
		/// <summary>
		/// 加算マーク
		/// </summary>
		public static readonly string AdditionMark = "+";
		/// <summary>
		/// 加算メソッド名
		/// </summary>
		public static readonly string AdditionMethodName = "addition";

		/// <summary>
		/// 減算マーク
		/// </summary>
		public static readonly string SubtractionMark = "-";
		/// <summary>
		/// 減算メソッド名
		/// </summary>
		public static readonly string SubtractionMethodName = "subtraction";

		/// <summary>
		/// 乗算マーク
		/// </summary>
		public static readonly string MultiplicationMark = "X";
		/// <summary>
		/// 乗算メソッド名
		/// </summary>
		public static readonly string MultiplicationMethodName = "multiplication";

		/// <summary>
		/// 除算マーク
		/// </summary>
		public static readonly string DivisionMark = "/";
		/// <summary>
		/// 除算メソッド名
		/// </summary>
		public static readonly string DivisionMethodName = "division";

		/// <summary>
		/// 累乗マーク
		/// </summary>
		public static readonly string ExponentiationMark = "^";
		/// <summary>
		/// 累乗メソッド名
		/// </summary>
		public static readonly string ExponentiationMethodName = "exponentiation";

		/// <summary>
		/// パーセントマーク
		/// </summary>
		public static readonly string ParsentMark = "%";
		/// <summary>
		/// パーセントメソッド名
		/// </summary>
		public static readonly string ParsetMethodName = "parsent";

		/// <summary>
		/// プラスマイナスマーク
		/// </summary>
		public static readonly string PlusMinusMark = "+/-";
		/// <summary>
		/// プラスマイナスマーク
		/// </summary>
		public static readonly string PlusMinusMethodName = "plusminus";

		/// <summary>
		/// ピリオドマーク
		/// </summary>
		public static readonly char PeriodMarkC = '.';
		/// <summary>
		/// ピリオドマーク
		/// </summary>
		public static readonly string PeriodMarkS = ".";

		/// <summary>
		/// 表示できる数字の最大文字数
		/// </summary>
		public static readonly int numTextLengthMax = 28;

		/// <summary>
		/// 共通ディクショナリキー:xテキスト
		/// </summary>
		public static readonly string MainDicKeyName_XText = "x";
		/// <summary>
		/// 共通ディクショナリキー:yテキスト
		/// </summary>
		public static readonly string MainDicKeyName_YText = "y";
		/// <summary>
		/// 共通ディクショナリキー:リザルトテキスト
		/// </summary>
		public static readonly string MainDicKeyName_ResultText = "result";
		/// <summary>
		/// 共通ディクショナリキー:説明テキスト
		/// </summary>
		public static readonly string MainDicKeyName_ExplainText = "explain";
		/// <summary>
		/// 共通ディクショナリキー:終了フラグ
		/// </summary>
		public static readonly string MainDicKeyName_Final = "final";
		/// <summary>
		/// 共通ディクショナリキー:エラーフラグ
		/// </summary>
		public static readonly string MainDicKeyName_Error = "error";
		/// <summary>
		/// 共通ディクショナリキー:特殊入力モードフラグ
		/// </summary>
		public static readonly string MainDicKeyName_SpecKeyFlg = "speckey";
		/// <summary>
		/// 共通ディクショナリキー:キーインデックス
		/// </summary>
		public static readonly string MainDicKeyName_SpecKeyIndex = "specindex";

		public static readonly string MainDicKeyName_LastCulculateResult = "lastculculateresult";

		public static readonly List<string> CurrencyList = new List<string> { "USD", "JPY", "EUR", "GBP", "AUD", "NZD", "CHF", "CAD", "ZAR", "CNY" };
		public static readonly List<string> CurrencyExplainList = new List<string> { "US Dollar ($)", "Japanese Yen (¥)", "Euro (€)", "British Pound (£)","Australian Dollar (A$)","New Zealand Dollar (NZ$)","Swiss Franc (CHF)","Canadian Dollar (CA$)","South African Rand (ZAR)","Chinese Yuan (CN¥)" };

		public static readonly List<string> NormalKeyList = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

		public static readonly List<List<string>> KeyList = new List<List<string>> { CurrencyList,NormalKeyList };

		public static readonly List<string> DefaultCuculateList = new List<string> { "CurrencyChange", "GetDay" };
		public static readonly List<string> DefaultCuculateNameList = new List<string> { "通貨計算", "曜日取得" };

		public static readonly int CurrencyKeyListIndex=0;
		public static readonly int NormalKeyListIndex=1;

		public static readonly double FontSizeBase = 70.0;


	}
}
