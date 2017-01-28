﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MyCalculator
{
	/// <summary>
	/// ユーティリティクラス
	/// </summary>
	public static class U
	{

		/// <summary>
		/// 計算マークから対応するメソッド名を返します。
		/// </summary>
		/// <returns>The method name from mark.</returns>
		/// <param name="mark">Mark.</param>
		public static string getMethodNameFromMark(string mark)
		{
			if (V.AdditionMark.Equals(mark)) return V.AdditionMethodName;
			if (V.DivisionMark.Equals(mark)) return V.DivisionMethodName;
			if (V.MultiplicationMark.Equals(mark)) return V.MultiplicationMethodName;
			if (V.SubtractionMark.Equals(mark)) return V.SubtractionMethodName;
			if (V.PlusMinusMark.Equals(mark)) return V.PlusMinusMethodName;
			if (V.ParsentMark.Equals(mark)) return V.ParsetMethodName;
			if (V.ExponentiationMark.Equals(mark)) return V.ExponentiationMethodName;

			return "";
		}

		/// <summary>
		/// 計算マークから対応するメソッド名を返します。
		/// </summary>
		/// <returns>The method name from mark.</returns>
		/// <param name="mark">Mark.</param>
		public static string getMarkFromMethodName(string name)
		{
			if (V.AdditionMethodName.Equals(name)) return V.AdditionMark;
			if (V.DivisionMethodName.Equals(name)) return V.DivisionMark;
			if (V.MultiplicationMethodName.Equals(name)) return V.MultiplicationMark;
			if (V.SubtractionMethodName.Equals(name)) return V.SubtractionMark;
			if (V.PlusMinusMethodName.Equals(name)) return V.PlusMinusMark;
			if (V.ParsetMethodName.Equals(name)) return V.ParsentMark;
			if (V.ExponentiationMethodName.Equals(name)) return V.ExponentiationMark;

			return "";
		}

		/// <summary>
		/// 特殊計算のステップごとのディクショナリを格納します
		/// </summary>
		/// <param name="name">Name.</param>
		public static void stackDic([CallerMemberName] string name = "")
		{
			var dic = new Dictionary<string, string>();

			dic.Add(V.MainDicKeyName_XText, S.xText);
			dic.Add(V.MainDicKeyName_YText, S.yText);
			dic.Add(V.MainDicKeyName_LastCulculateResult, S.LastCulculateResult.ToString());

			S.stackDictionary.Add(name, dic);

		}

		/// <summary>
		/// 特殊計算のステップごとのディクショナリを格納します
		/// </summary>
		/// <param name="name">Name.</param>
		public static void specialStackDic([CallerMemberName] string name = "")
		{
			var dic = new Dictionary<string, string>();

			dic.Add(V.MainDicKeyName_XText, S.xText);
			dic.Add(V.MainDicKeyName_YText, S.yText);
			dic.Add(V.MainDicKeyName_LastCulculateResult, S.LastCulculateResult.ToString());

			var target = "Step" + (int.Parse(Regex.Match(name, @"[\d]+").Captures[0].Value) - 1);

			S.stackDictionary.Add(target, dic);

		}


		/// <summary>
		/// 文字数からフォントサイズを計算してフォントサイズを変更します
		/// </summary>
		/// <param name="resultText">Result text.</param>
		public static void setFontSize(Label resultText){

			if(resultText.Text.Length > 8){
				resultText.FontSize = (resultText.Width / resultText.Text.Length) * 1.6;
			}else{
				resultText.FontSize = V.FontSizeBase;
			}

		}
	}
}
