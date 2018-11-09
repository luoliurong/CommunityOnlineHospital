using COHS.AppServices.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace COHS.AppServices
{
	public abstract class BaseService : IBaseService
	{
		private IDbService dbService;
		private object syncObj = new object();

		public BaseService(IDbService dbService)
		{
			this.dbService = dbService;
		}

		public virtual IDbConnection DatabaseConnection
		{
			get { return dbService.GetDbConnection(); }
		}

		public string GetSerialStringOfTable(string tableName, string keyName)
		{
			lock (syncObj)
			{
				var newSerialNo = string.Empty;
				var query = string.Format("select max({0}) MaxSerial from {1}", keyName, tableName);
				var currentdatetime = DateTime.Now.ToString("yyyyMMdd");
				using (var conn = DatabaseConnection)
				{
					var queryResult = conn.QueryFirst(query);
					if (queryResult != null && queryResult.MaxSerial != null)
					{
						string maxserial = queryResult.MaxSerial.ToString();
						var datepart = maxserial.Substring(0, 8);
						var samedatetime = string.Compare(currentdatetime, datepart) == 0;
						if (samedatetime)
						{
							string numberpartStr = maxserial.Substring(8);
							var serialNo = Convert.ToInt32(numberpartStr);
							newSerialNo = (serialNo + 1).ToString().PadLeft(8, '0');
						}
						else
						{
							newSerialNo = "0".PadLeft(8, '0');
						}
					}
				}
				return string.Format("{0}{1}", currentdatetime, newSerialNo);
			}
		}

		public int GetSerialNumberOfTable(string tableName, string keyName)
		{
			lock (syncObj)
			{
				var newSerialNo = 1;
				var query = string.Format("select max({0}) MaxSerial from {1}", keyName, tableName);
				using (var conn = DatabaseConnection)
				{
					var queryResult = conn.QueryFirst(query);
					if (queryResult != null && queryResult.MaxSerial != null)
					{
						var parseRes = 0;
						string maxSerialStr = queryResult.MaxSerial.ToString();
						if (int.TryParse(maxSerialStr, out parseRes))
						{
							newSerialNo = parseRes + 1;
						}
					}
				}
				return newSerialNo;
			}
		}
	}

	public sealed class SqlServerDbService : IDbService, IDisposable
	{
		private IDbConnection _sqlConnection;

		public SqlServerDbService()
		{
			_sqlConnection = new SqlConnection();
			_sqlConnection.ConnectionString = new ConfigurationBuilder()
												 .AddJsonFile("configs/dbconfig.json")
												 .Build().GetConnectionString("DefaultConnection");
		}

		public IDbConnection GetDbConnection()
		{
			return _sqlConnection;
		}

		public void Dispose()
		{
			Dispose(true);
			System.GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
				{
					_sqlConnection.Close();
					_sqlConnection.Dispose();
				}
			}
		}

		~SqlServerDbService()
		{
			Dispose(false);
		}
	}
}
