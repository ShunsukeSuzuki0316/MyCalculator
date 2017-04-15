using System;
using System.Collections.Generic;
using SQLite;

namespace MyCalculator
{
	public class UserCulculateUtil
	{


		public static List<UserCulculateModel> getAllUserCulculate()
		{



			SQLiteConnection db = null;
			try
			{
				db = new SQLiteConnection(S.dbPath);

				var query = db.Query<UserCulculateModel>("SELECT * FROM [UserCulculate] ");

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

		public static UserCulculateModel getUserCulculateByName(string target)
		{

			SQLiteConnection db = null;
			try
			{
				db = new SQLiteConnection(S.dbPath);

				var query = db.Query<UserCulculateModel>("SELECT * FROM [UserCulculate] WHERE Name = ?", target);

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

		public static bool insertUserCulculate(string name, string culculate, string displayName, int index)
		{

			SQLiteConnection db = null;

			try
			{

				db = new SQLiteConnection(S.dbPath);

				db.Insert(new UserCulculateModel() { Name = name, Culculate = culculate ,DisplayName = displayName, Index = index});

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

		public static bool insertUserCulculate(UserCulculateModel um)
		{

			SQLiteConnection db = null;

			try
			{

				db = new SQLiteConnection(S.dbPath);

				db.Insert(um);

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

				db.Delete(new UserCulculateModel() { Id = targetId });

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

		public static Boolean updateCulculateById(int targetId, string name, string culculate, string displayName)
		{

			SQLiteConnection db = null;

			try
			{
				db = new SQLiteConnection(S.dbPath);

				db.Update(new UserCulculateModel() { Id = targetId,Name = name,Culculate = culculate,DisplayName = displayName, Index = 0});

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

		public static UserCulculateModel getUserCulculateById(int targetId)
		{
			SQLiteConnection db = null;
			try
			{
				db = new SQLiteConnection(S.dbPath);

				//var returned = db.Get<UserCulculateModel>(new UserCulculateModel() { Id = targetId });
				var returned = db.Get<UserCulculateModel>(targetId);

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
