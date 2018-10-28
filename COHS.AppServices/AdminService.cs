using COHS.AppServices.Interfaces;
using Dapper;
using System.Collections.Generic;

namespace COHS.AppServices
{
	public class AdminService : BaseService, IAdminService
	{
		public AdminService(IDbService dbService) : base(dbService)
		{
		}

	}
}
