using System;
using System.Linq;
using System.Collections.Generic;
namespace MyCalculator
{
	public static class OP
	{
		public static decimal parsent()
		{
			decimal val;

			if(V.SubtractionMethodName.Equals(F.getLastMethod())||
			   V.AdditionMethodName.Equals(F.getLastMethod())){

				val = decimal.Parse(F.getLastMethdPreNumber()) * (decimal.Parse(F.getLastNum()) / 100);

			}else{
				val = decimal.Parse(F.getLastNum()) / 100;
			}

			return val;
		}

		public static decimal plusminus()
		{
			decimal val = "0".Equals(F.getLastNum()) ? 0 :  decimal.Parse(F.getLastNum()) * -1;
			return val;
		}

	}
}
