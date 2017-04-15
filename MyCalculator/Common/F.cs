using System;
using System.Collections.Generic;
using System.Linq;
namespace MyCalculator
{
	public class F
	{

		private static string[] methodNames = new string[] { 
			V.AdditionMethodName, 
			V.DivisionMethodName, 
			V.ExponentiationMethodName, 
			V.MultiplicationMethodName, 
			V.ParsetMethodName, 
			V.PlusMinusMethodName, 
			V.SubtractionMethodName
		};

		static Dictionary<string, int> previousOrder = new Dictionary<string, int>
			{   {V.AdditionMethodName,0},
				{V.SubtractionMethodName,0},
				{V.MultiplicationMethodName,1},
				{V.DivisionMethodName,2}};

		/// <summary>
		/// 入力された数式の最後が数値であるか検証します
		/// </summary>
		/// <returns><c>true</c>, if last number was ised, <c>false</c> otherwise.</returns>
		public static bool isLastNum(){
			if(S.inputFormula.Count != 0){
				return !methodNames.Contains(S.inputFormula.Last());
			}
			return false;
		}

		/// <summary>
		/// 数式リストに値を挿入します
		/// </summary>
		/// <param name="value">Value.</param>
		public static void insertValue(string value){
			if(S.inputFormula.Count == 0){
				//リストが空の場合は、valueが演算子でなければ末尾に追加します
				if(!methodNames.Contains(value))S.inputFormula.Add(value);
			}else{
				if(isLastNum()){
					//リストの末尾が数値の場合数字であれば末尾を入れ替え
					//演算子であれば末尾に追加します
					if(methodNames.Contains(value)){
						S.inputFormula.Add(value);	
					}else{
						S.inputFormula[S.inputFormula.Count - 1] = value;
					}
				}else{
					//リストの末尾が演算子の場合演算子であれば末尾を入れ替え
					//数値であれば末尾に追加します
					if (methodNames.Contains(value)) {
						S.inputFormula[S.inputFormula.Count - 1] = value;
					} else {
						S.inputFormula.Add(value);
					}
				}
			}
		}

		/// <summary>
		/// 最後の演算子を取得します
		/// </summary>
		/// <returns>The last method.</returns>
		public static string getLastMethod(){
			try {
				return S.inputFormula.Last(s => methodNames.Contains(s));
			}catch(Exception e){
				System.Diagnostics.Debug.WriteLine(e);
				return null;
			}
		}

		/// <summary>
		/// 最後の数値を取得します
		/// </summary>
		/// <returns>The last method.</returns>
		public static string getLastNum()
		{
			try {
				return S.inputFormula.Last(s => !methodNames.Contains(s));
			} catch (Exception e) {
				System.Diagnostics.Debug.WriteLine(e);
				return null;
			}
		}
		/// <summary>
		/// 最後の演算子の前にある数値を取得します
		/// </summary>
		/// <returns>The last methd pre number.</returns>
		public static string getLastMethdPreNumber(){
			try {
				return S.inputFormula[S.inputFormula.IndexOf(S.inputFormula.Last(s => methodNames.Contains(s))) - 1];
			} catch (Exception e) {
				System.Diagnostics.Debug.WriteLine(e);
				return null;
			}
		}

		/// <summary>
		/// 通常の数式を逆ポーランドに置き換えたリストを返却します
		/// </summary>
		/// <returns>The to reverse porland.</returns>
		public static List<string> changeToReversePorland (){

			var reversePorland = new List<string>();

			var stack = new Stack<string>();

			foreach(string s in S.inputFormula){

				decimal test;

				if(decimal.TryParse(s,out test)){
					//数字の場合はリストにそのまま追加
					reversePorland.Add(s);
				
				}else{
					if (stack.Count() == 0) {
						//スタックに演算子がなければそのままstackに追加
						stack.Push(s);
					}else{
						if(isPrevious(stack.Peek(),s)){
							//スタックの先頭よりも優先度の高い演算子であればstackに追加
							stack.Push(s);
						}else{
							while(stack.Count > 0){
								reversePorland.Add(stack.Pop());
							}
							stack.Push(s);
						}
					}
				}
			}

			while (stack.Count > 0) {
				reversePorland.Add(stack.Pop());
			}

			return reversePorland;

		}
		/// <summary>
		/// メソッド名から優先順位を取得していtargetがinStackよりも優先順位が高いか調べます
		/// </summary>
		/// <returns><c>true</c>, if previous was ised, <c>false</c> otherwise.</returns>
		/// <param name="inStack">In stack.</param>
		/// <param name="target">Target.</param>
		public static bool isPrevious(string inStack,string target){
			return previousOrder[target] > previousOrder[inStack];
		}

		public static string culc(){

			var reversPorland = changeToReversePorland();

			var stack = new Stack<string>();

			string tmp = null;

			foreach(var s in reversPorland){

				decimal test;

				if (decimal.TryParse(s, out test)) {
					//数字の場合はリストにそのまま追加
					stack.Push(s);
				} else {
					var result = D.getDelegate(typeof(BC), s).DynamicInvoke(
						new object[] { 
							tmp != null ? 
							decimal.Parse(tmp) : 
							       decimal.Parse(stack.Pop()), decimal.Parse(stack.Pop()) 
						}
					).ToString();

					tmp = result;
				}
			}
			return tmp;
		} 
	}
}
