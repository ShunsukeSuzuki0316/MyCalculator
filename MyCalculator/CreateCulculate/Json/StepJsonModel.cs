using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyCalculator
{
	[JsonObject("step")]
	public class StepModel
	{
		[JsonProperty("steps")]
		public string steps { get; set; }
		[JsonProperty("explain")]
		public string explain { get; set;}
		[JsonProperty("execute")]
		public List<ExecuteJsonModel> execute { get; set;}
		[JsonProperty("last")]
		public bool last { get; set;}
	}
}
