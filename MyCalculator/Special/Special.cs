using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyCalculator
{
	public class Special
	{
		public Special()
		{
		}

		public static Dictionary<string, string> Step()
		{
			var target =  "Step" + (S.calculateType.Count).ToString();

			if(!"Step1".Equals(target)){
				U.specialStackDic(target);
			}

			var usr = S.selectUserModel;
			var model = JsonConvert.DeserializeObject<CulculateJsonModel>(usr.Culculate);
			var step = model.step.Find(s => s.steps.Equals(target));

			S.LastCulculateResult = "Step1".Equals(target) ? 0 : SpecialUtil.execute(step);

			var dic = new Dictionary<string, string>();

			dic.Add(V.MainDicKeyName_ResultText, step.last ? S.LastCulculateResult.ToString() : "0");
			dic.Add(V.MainDicKeyName_ExplainText, step.explain);
			dic.Add(V.MainDicKeyName_XText, step.last ? S.LastCulculateResult.ToString() : "0");
			dic.Add(V.MainDicKeyName_YText, null);
			dic.Add(V.MainDicKeyName_Final, step.last.ToString());
			dic.Add(V.MainDicKeyName_SpecKeyFlg, "false");
			dic.Add(V.MainDicKeyName_SpecKeyIndex, "0");

			return dic;
		}
	}
}
