using System;
using System.Collections.Generic;

namespace MyCalculator
{
	public static class GetDay
	{

		public static Dictionary<string, string> Step1()
		{
			var dic = new Dictionary<string, string>();

			dic.Add(V.MainDicKeyName_ResultText, "0");
			dic.Add(V.MainDicKeyName_ExplainText, "曜日を取得したい年月日を入力してください\nYYYYMMDD形式で入力してください。");
			dic.Add(V.MainDicKeyName_XText, "0");
			dic.Add(V.MainDicKeyName_YText, null);
			dic.Add(V.MainDicKeyName_Final, "false");
			dic.Add(V.MainDicKeyName_SpecKeyFlg, "false");
			dic.Add(V.MainDicKeyName_SpecKeyIndex, "0");

			return dic;
		}

		public static Dictionary<string, string> Step2()
		{

			U.stackDic();

			var dic = new Dictionary<string, string>();

			string cul1 = S.stackDictionary["Step2"][V.MainDicKeyName_XText];

			try
			{
				DateTime dateValue = DateTime.Parse(cul1.Insert(4, "/").Insert(7, "/"));
				dic.Add(V.MainDicKeyName_ResultText, ((int)dateValue.DayOfWeek).ToString());
				dic.Add(V.MainDicKeyName_XText, ((int)dateValue.DayOfWeek).ToString());
				dic.Add(V.MainDicKeyName_ExplainText, cul1 + "の曜日を取得\n" + getdayname((int)dateValue.DayOfWeek) + "です。");

			}
			catch (Exception e)
			{
				dic.Add(V.MainDicKeyName_ResultText, "-1");
				dic.Add(V.MainDicKeyName_XText, "-1");
				dic.Add(V.MainDicKeyName_ExplainText, "エラーが発生しました。");

				System.Diagnostics.Debug.WriteLine(e);
			}

			dic.Add(V.MainDicKeyName_YText, null);
			dic.Add(V.MainDicKeyName_Final, "true");
			dic.Add(V.MainDicKeyName_SpecKeyFlg, "false");
			dic.Add(V.MainDicKeyName_SpecKeyIndex, "0");

			return dic;
		}

		private static string getdayname(int i)
		{
			var list = new List<string> { "日曜日", "月曜日", "火曜日", "水曜日", "木曜日", "金曜日", "土曜日" };
			return list[i];
		}
	}
}
