using System;
namespace MyCalculator
{
	public static class OP
	{
		public static decimal parsent(string x, string y)
		{

			if (y != null)
			{
				decimal val = "0".Equals(y) ? 0 : decimal.Parse(y) / 100;
				S.yText = val.ToString();
				return val;
			}
			else {
				decimal val = "0".Equals(x) ? 0 :  decimal.Parse(x) / 100;
				S.xText = val.ToString();
				return val;
			}

		}
		public static decimal plusminus(string x, string y)
		{

			if (y != null)
			{
				decimal val = "0".Equals(y) ? 0 :  decimal.Parse(y) * -1;
				S.yText = val.ToString();
				return val;
			}
			else {
				decimal val = "0".Equals(x) ? 0 : decimal.Parse(x) * -1;
				S.xText = val.ToString();
				return val;
			}
		}

	}
}
