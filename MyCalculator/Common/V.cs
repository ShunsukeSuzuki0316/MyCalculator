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
		///// 共通ディクショナリキー:yテキスト
		///// </summary>
		//public static readonly string MainDicKeyName_YText = "y";
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

		public static readonly string MainDicKeyName_LastCulculateResult = "lastculculateresult";

		public static readonly double FontSizeBase = 70.0;

		public static readonly string NEXT_MARK = "次へ";


	}
}
