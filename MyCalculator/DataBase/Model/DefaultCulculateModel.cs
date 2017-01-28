using System;
using SQLite;

namespace MyCalculator
{

	[Table("DefaultCulculate")]
	public class DefaultCulculateModel
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }
		public string Name { get; set;}
		public string Culculate { get; set; }
		public string DisplayName { get; set;}
		public int Index { get; set; }
	}
}
