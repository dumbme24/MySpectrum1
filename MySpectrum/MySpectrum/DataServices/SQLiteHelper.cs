using MySpectrum.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MySpectrum.DataAccessLayer
{
   public class SQLiteHelper
    {
        public SQLiteHelper(){}

        public static bool createDatabase(string db_path)
        {
            try
            {
                var connection = new SQLiteConnection(db_path);
                {
                    connection.CreateTable<User>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public static bool InsertData<T>(string database_path, ref T data)
        {
            using (var SQLiteConnection = new SQLite.SQLiteConnection(database_path))
            {
                //SQLiteConnection.CreateTable<T>();
                if (SQLiteConnection.Insert(data) != 0)
                {
                    return true;
                }
            }

                return false;
        }

        public static List<User> ReadData(string database_path)
        {
            List<User> user_list = new List<User>();

            using (var SQLiteConnection = new SQLite.SQLiteConnection(database_path))
            {
                user_list = SQLiteConnection.Table<User>().ToList();
            }

                return user_list;
        }

        public static bool DeletData(string database_path)
        {

            using (var SQLiteConnection = new SQLite.SQLiteConnection(database_path))
            {
                if (SQLiteConnection.DeleteAll<User>() != 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
