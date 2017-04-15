using System.Collections.Generic;

static class LinqExtention
{
	public static void ForEach<T>(this IEnumerable<T> src, System.Action<T> action)
	{
		foreach (T item in src)
		{
			action(item);
		}
	}
}