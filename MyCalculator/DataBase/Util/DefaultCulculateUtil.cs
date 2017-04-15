using System;
using System.Collections.Generic;
using SQLite;

namespace MyCalculator
{
	public class DefaultCulculateUtil
	{


		public static List<DefaultCulculateModel> getAllDefaultCulculate()
		{



			SQLiteConnection db = null;
			try
			{
				db = new SQLiteConnection(S.dbPath);

				var query = db.Query<DefaultCulculateModel>("SELECT * FROM [DefaultCulculate] ");

				return query;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e);
				return null;
			}
			finally
			{
				db.Close();
			}

		}

		public static DefaultCulculateModel getDefaultCulculateByName(string target)
		{

			SQLiteConnection db = null;
			try
			{
				db = new SQLiteConnection(S.dbPath);

				var query = db.Query<DefaultCulculateModel>("SELECT * FROM [DefaultCulculate] WHERE Name = ?", target);

				return query[0];
			}
			catch (Exception e)
			{

				System.Diagnostics.Debug.WriteLine(e);
				return null;

			}
			finally
			{
				db.Close();
			}
		}

		public static bool insertDefaultCulculate(string name, string culculate, string displayName)
		{

			SQLiteConnection db = null;

			try
			{

				db = new SQLiteConnection(S.dbPath);

				db.Insert(new DefaultCulculateModel() { Name = name, Culculate = culculate ,DisplayName = displayName});

				db.Commit();

			}
			catch (Exception e)
			{

				System.Diagnostics.Debug.WriteLine(e);
				return false;

			}
			finally
			{
				db.Close();
			}

			return true;
		}

		public static Boolean deleteCulculateById(int targetId)
		{

			SQLiteConnection db = null;

			try
			{
				db = new SQLiteConnection(S.dbPath);

				db.Delete(new DefaultCulculateModel() { Id = targetId });

				db.Commit();

				return true;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e);
				return false;
			}
			finally
			{
				db.Close();
			}
		}

		public static Boolean updateCulculateById(int targetId, string Name, string Culculate)
		{

			SQLiteConnection db = null;

			try
			{
				db = new SQLiteConnection(S.dbPath);

				db.Update(new DefaultCulculateModel() { Id = targetId });

				db.Commit();

				return true;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e);
				return false;

			}
			finally
			{
				db.Close();
			}
		}

		public static DefaultCulculateModel getDefaultCulculateById(int targetId)
		{
			SQLiteConnection db = null;
			try
			{
				db = new SQLiteConnection(S.dbPath);

				var returned = db.Get<DefaultCulculateModel>(new DefaultCulculateModel() { Id = targetId });

				return returned;
			}
			catch (Exception e)
			{

				System.Diagnostics.Debug.WriteLine(e);

				return null;

			}
			finally
			{
				db.Close();
			}
		}
	}
}
