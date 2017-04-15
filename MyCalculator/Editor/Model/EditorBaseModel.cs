using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyCalculator
{
	public class EditorBaseModel
	{
		public Entry explain { get; set;}
		public Switch final { get; set;}
		public List<CulcModel> culculates { get; set;}
	}
}
