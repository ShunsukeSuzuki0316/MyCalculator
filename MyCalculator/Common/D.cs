using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MyCalculator
{
	/// <summary>
	/// デリデート専用クラス
	/// </summary>
	public static class D
	{

		//クラスとメソッド名からdelegateを返却します
		public static Delegate getDelegate(Type clazz, string methodName)
		{
			MethodInfo method = clazz.GetTypeInfo().GetDeclaredMethod(methodName);
			var paramTypes = method.GetParameters().Select(p => p.ParameterType);

			Type delegateType = Expression.GetDelegateType(Append(paramTypes, method.ReturnType).ToArray());
			return method.CreateDelegate(delegateType, null);

		}
		/// <summary>
		/// 対象メソッドの引数リストを取得します
		/// </summary>
		/// <param name="collection">Collection.</param>
		/// <param name="element">Element.</param>
		/// <typeparam name="TSource">The 1st type parameter.</typeparam>
		public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> collection, TSource element)
		{
			if (collection == null) throw new ArgumentNullException(nameof(collection));

			foreach (TSource element1 in collection) yield return element1;
			yield return element;
		}

	}
}
