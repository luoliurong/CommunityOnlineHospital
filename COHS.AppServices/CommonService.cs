using COHS.AppServices.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace COHS.AppServices
{
	public class CommonService : BaseService, ICommonService
	{
		public CommonService(IDbService dbService) : base(dbService)
		{
		}

		public Dictionary<string, string> GetConfigItems()
		{
			Dictionary<string, string> configItems = new Dictionary<string, string>();
			using (var conn = DatabaseConnection)
			{
				var result = conn.Query(SqlQueryConstant.ConfigQuery);
				if (result.Any())
				{
					foreach (var pairs in result)
					{
						configItems.Add(pairs.Config_Key, pairs.Config_Value);
					}
				}
			}
			return configItems;
		}
	}
}
