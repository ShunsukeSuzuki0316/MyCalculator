using System;
using Newtonsoft.Json;

namespace MyCalculator
{
	[JsonObject("execute")]
	public class ExecuteJsonModel
	{
		[JsonProperty("xtype")]
		public string xtype { get; set;}
		[JsonProperty("xtarget")]
		public string xtarget { get; set;}
		[JsonProperty("ytype")]
		public string ytype { get; set; }
		[JsonProperty("ytarget")]
		public string ytarget { get; set; }
		[JsonProperty("culculateMethod")]
		public string culculateMethod { get; set;}
	}
}
