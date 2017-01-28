using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyCalculator
{
	[JsonObject("culculate")]
	public class CulculateJsonModel
	{
		[JsonProperty("step")]
		public List<StepModel> step { get; set;}
	}
}
