using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyCalculator
{
	public class Special
	{
		public Special()
		{
		}

		public static Dictionary<string, string> execute(CulculateFormulaJsonModel formula)
		{

			if (!S.firstFormula) {
				U.specialStackDic();
			}else{
				S.firstFormula = false;
			}

			S.LastCulculateResult = SpecialUtil.execute(formula);

			var dic = new Dictionary<string, string>();

			/*if(formula.last){
				S.LastCulculateResult = decimal.Parse(
					S.stackDictionary[
						formula.execute.Where(
							m => S.calculateType.Count < (int.Parse(Regex.Match(m.steps, @"[\d]+").Captures[0].Value))
						)
						.Last(m => m.execute.Count() > 0).steps
					]
					[V.MainDicKeyName_LastCulculateResult]);
			}*/

			dic.Add(V.MainDicKeyName_ResultText, formula.last ? S.LastCulculateResult.ToString() : "0");
			dic.Add(V.MainDicKeyName_ExplainText, formula.explain);
			dic.Add(V.MainDicKeyName_XText, formula.last ? S.LastCulculateResult.ToString() : "0");
			dic.Add(V.MainDicKeyName_Final, formula.last.ToString());

			return dic;
		}
	}
}
