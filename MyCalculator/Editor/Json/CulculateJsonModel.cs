using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyCalculator
{
	[JsonObject("culculate")]
	public class CulculateJsonModel
	{
		[JsonProperty("formula")]
		public List<CulculateFormulaJsonModel> formula { get; set;}
	}
}
