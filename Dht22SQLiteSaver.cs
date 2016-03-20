using System;
using System.IO;
using System.Data.Common;
using SQLite;

public class Dht22SQLiteSaver
{
	private string _connString;
	
	public Dht22SQLiteSaver(string connString)
	{
		if (String.IsNullOrEmpty(connString))
			throw new ArgumentNullException("Connection string cannot be null or empty");
		try
		{
			var databasePath = GetDatabasePathFromConnString(connString);
			File.Exists(databasePath);
			using (var conn = new SQLiteConnection(databasePath))
			{
				conn.CreateTable<Dht22Data>();
			}
		}
		catch (DirectoryNotFoundException innerEx)
		{
			throw new ArgumentException("Invalid connection string: the supplied database path is invalid", innerEx);
		}

		_connString = connString;		
	}

	private string GetDatabasePathFromConnString(string connString)
        {
                var connBuilder = new DbConnectionStringBuilder(){ ConnectionString = connString };
                return connBuilder["Data Source"].ToString();
        }


	public void SaveData(Dht22Data data)
	{
		using (var conn = new SQLiteConnection(GetDatabasePathFromConnString(_connString)))
                {
                        conn.Insert(data);
                }
	}
}
