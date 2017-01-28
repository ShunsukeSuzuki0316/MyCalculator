using System;
using Xamarin.Forms;

namespace MyCalculator
{
	public class CulcModel
	{

		public Picker xtypePick {get;set;}

		public Picker ytypePick { get; set; }

		public Picker culcPick { get; set; }

		public Picker xstepPick { get; set; }

		public Picker ystepPick { get; set; }

		public Entry yConstantEntry { get; set; }

		public Entry xConstantEntry { get; set; }

		public string uuid { get; set;}
	}
}
