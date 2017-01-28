using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace MyCalculator
{
	public static class CurrencyChange
	{

		public static Dictionary<string, string> Step1()
		{
			var dic = new Dictionary<string, string>();

			dic.Add(V.MainDicKeyName_ResultText, "0");
			dic.Add(V.MainDicKeyName_ExplainText, "通貨を選択してください");
			dic.Add(V.MainDicKeyName_XText, "0");
			dic.Add(V.MainDicKeyName_YText, null);
			dic.Add(V.MainDicKeyName_Final, "false");
			dic.Add(V.MainDicKeyName_SpecKeyFlg, "true");
			dic.Add(V.MainDicKeyName_SpecKeyIndex, V.CurrencyKeyListIndex.ToString());

			return dic;
		}

		public static Dictionary<string, string> Step2()
		{

			U.stackDic();

			var dic = new Dictionary<string, string>();

			dic.Add(V.MainDicKeyName_ResultText, "0");
			dic.Add(V.MainDicKeyName_ExplainText, "対通貨を入力してください");
			dic.Add(V.MainDicKeyName_XText, "0");
			dic.Add(V.MainDicKeyName_YText, null);
			dic.Add(V.MainDicKeyName_Final, "false");
			dic.Add(V.MainDicKeyName_SpecKeyFlg, "true");
			dic.Add(V.MainDicKeyName_SpecKeyIndex, V.CurrencyKeyListIndex.ToString());

			return dic;
		}

		public static Dictionary<string, string> Step3()
		{

			U.stackDic();

			var dic = new Dictionary<string, string>();

			dic.Add(V.MainDicKeyName_ResultText, "0");
			dic.Add(V.MainDicKeyName_ExplainText, "金額を入力してください");
			dic.Add(V.MainDicKeyName_XText, "0");
			dic.Add(V.MainDicKeyName_YText, null);
			dic.Add(V.MainDicKeyName_Final, "false");
			dic.Add(V.MainDicKeyName_SpecKeyFlg, "false");
			dic.Add(V.MainDicKeyName_SpecKeyIndex, "");

			return dic;
		}

		public static Dictionary<string, string> Step4()
		{
			U.stackDic();

			var dic = new Dictionary<string, string>();
			var httpClient = new HttpClient();

			string cul1 = S.stackDictionary["Step2"][V.MainDicKeyName_XText];
			string cul2 = S.stackDictionary["Step3"][V.MainDicKeyName_XText];

			string cule1 = V.CurrencyExplainList[V.CurrencyList.IndexOf(cul1)];
			string cule2 = V.CurrencyExplainList[V.CurrencyList.IndexOf(cul2)];

			HttpResponseMessage response = httpClient.GetAsync("https://www.google.com/finance/converter?a=" + S.xText + "&from=" + cul1 + "&to=" + cul2).Result;

			if (response.IsSuccessStatusCode)
			{
				var data = response.Content.ReadAsStringAsync().Result;

				Match mt = Regex.Match(data, @"<span class=bld>[\d\.]*\s\w*</span>");

				string one = mt.Captures[0].Value;

				Match mt2 = Regex.Match(one, @"[\d\.]+");

				dic.Add(V.MainDicKeyName_ResultText, mt2.Captures[0].Value);
				dic.Add(V.MainDicKeyName_XText, mt2.Captures[0].Value);

				System.Diagnostics.Debug.WriteLine(data);
			}
			else
			{
				System.Diagnostics.Debug.WriteLine(response);
			}

			dic.Add(V.MainDicKeyName_ExplainText, cule1 + "から\n" + cule2 + "に変換しました");
			dic.Add(V.MainDicKeyName_YText, null);
			dic.Add(V.MainDicKeyName_Final, "true");
			dic.Add(V.MainDicKeyName_SpecKeyFlg, "false");
			dic.Add(V.MainDicKeyName_SpecKeyIndex, "");

			return dic;
		}
	}
}
