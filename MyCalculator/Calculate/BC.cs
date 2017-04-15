using System;

namespace MyCalculator
{
	//基本演算クラス
	public static class BC
	{

		public static decimal addition(decimal x, decimal y)
		{
			return x + y;
		}

		public static decimal subtraction(decimal x, decimal y)
		{
			return x - y;
		}

		public static decimal multiplication(decimal x, decimal y)
		{
			return x * y;
		}

		public static decimal division(decimal x, decimal y)
		{
			return x / y;
		}

		public static decimal exponentiation(decimal x,decimal y){
			return (decimal)Math.Pow((double)x,(double)y);
		}

	}
}
