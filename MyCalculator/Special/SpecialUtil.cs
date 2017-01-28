using System;
using System.Collections.Generic;

namespace MyCalculator
{
	public class SpecialUtil
	{

		public static List<string> culcTypeList = new List<string> { V.AdditionMark, V.SubtractionMark, V.MultiplicationMark, V.DivisionMark, V.ExponentiationMark };
		public static List<string> typeList = new List<string> { "入力された値","定数", "前回の計算結果", "前ステップの最後の計算結果"};
		public static Dictionary<string, string> typeConvertList = new Dictionary<string, string>() { { "入力された値", JAN.Type_Input_Value},{ "定数", JAN.Type_Constant_Value },{"前回の計算結果",JAN.Type_Previous_Culculate_Result_Value},{"前ステップの最後の計算結果",JAN.Type_Last_Culculate_Result_Value} };
		public static List<string> realTypeList = new List<string> { JAN.Type_Input_Value, JAN.Type_Constant_Value, JAN.Type_Previous_Culculate_Result_Value, JAN.Type_Last_Culculate_Result_Value };

		public static decimal execute(StepModel sm)
		{

			List<ExecuteJsonModel> ejList = sm.execute;

			decimal previousResult = 0;

			foreach (ExecuteJsonModel ej in ejList)
			{

				decimal xtarget = 0;
				decimal ytarget = 0;

				switch (ej.xtype)
				{
					case JAN.Type_Constant_Value:
						xtarget = decimal.Parse(ej.xtarget);
						break;
					case JAN.Type_Input_Value:
						xtarget = decimal.Parse(S.stackDictionary[ej.xtarget][V.MainDicKeyName_XText]);
						break;
					case JAN.Type_Previous_Culculate_Result_Value:
						xtarget = previousResult;
						break;
					case JAN.Type_Last_Culculate_Result_Value:
						xtarget = decimal.Parse(S.stackDictionary[ej.xtarget][V.MainDicKeyName_LastCulculateResult]);
						break;
					default: break;
				}

				switch (ej.ytype)
				{
					case JAN.Type_Constant_Value:
						ytarget = decimal.Parse(ej.ytarget);
						break;
					case JAN.Type_Input_Value:
						ytarget = decimal.Parse(S.stackDictionary[ej.ytarget][V.MainDicKeyName_XText]);
						break;
					case JAN.Type_Previous_Culculate_Result_Value:
						ytarget = previousResult;
						break;
					case JAN.Type_Last_Culculate_Result_Value:
						ytarget = decimal.Parse(S.stackDictionary[ej.ytarget][V.MainDicKeyName_LastCulculateResult]);
						break;
					default: break;
				}

				previousResult = decimal.Parse(D.getDelegate(typeof(BC), ej.culculateMethod).DynamicInvoke(new object[] { xtarget, ytarget }).ToString());

			}

			return previousResult;

		}
	}
}
