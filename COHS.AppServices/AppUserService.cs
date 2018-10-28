using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using COHS.DataModel;
using Dapper;
using COHS.AppServices.Interfaces;

namespace COHS.AppServices
{
	public class AppUserService : BaseService, IAppUserService
	{
		private static readonly char USERIDZERO = '0';
		private static readonly int USERIDLENGTH = 8;

		public AppUserService(IDbService dbService) : base(dbService)
		{ }

		public IEnumerable<AppUser> GetAvailableUserList()
		{
			using (var conn = DatabaseConnection)
			{
				return conn.Query<AppUser>(SqlQueryConstant.GetAvailableUsersQuery);
			}
		}

		public void UpdateUserInfo(AppUser userInfo)
		{

		}

		public AppUser GetUserInfo(long userId)
		{
			using (var conn = DatabaseConnection)
			{
				var selectQuery = SqlQueryConstant.SelectUserByUserIdQuery;
				return conn.Query<AppUser>(selectQuery, new { UserId = userId }).First();
			}
		}

		public IEnumerable<AppUser> GetSuggestedUser(string searchString)
		{
			var allUsers = GetAllUsers();
			var existsSameUser = allUsers.Any(u => u.UserName.Equals(searchString, StringComparison.Ordinal));
			if (existsSameUser)
			{
				return GetAllUsers().Where(u => u.UserName.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase) >= 0);
			}
			else
			{
				return null;
			}
		}

		public string ValidateAccount(string userAccount, string userKey)
		{
			if (string.IsNullOrWhiteSpace(userAccount) && string.IsNullOrWhiteSpace(userKey))
			{
				return string.Empty;
			}

			var result = string.Empty;
			using (var conn = DatabaseConnection)
			{
				var query = SqlQueryConstant.ValidateUserWhenLoginQuery;
				var userList = conn.Query<AppUser>(query, new { DbUser = userAccount});
				if(userList.Any())
				{
					return ValidateUserPassKeyByTimespan(userList, userAccount, userKey);
				}
			}

			return result;
		}

		private string ValidateUserPassKeyByTimespan(IEnumerable<AppUser> userList, string userAccount, string userkey)
		{
			try
			{
				foreach (var user in userList)
				{
					var actualPasskey = user.Password;
					if (actualPasskey == userkey)
					{
						return user.UserId.ToString();
					}
				}
			}
			catch
			{

			}
			return string.Empty;
		}

		private AppUser GetUserInfoByUserId(string userId)
		{
			using (var conn = DatabaseConnection)
			{
				var query = SqlQueryConstant.SelectSingleUserByUserId;
				var userList = conn.Query<AppUser>(query, new { UserId = userId });
				if (userList.Count() > 1)
				{
					return null;
				}
				return userList.Single();
			}
		}

		private string GenerateLatestUserId()
		{
			var latestUserId = GetLatestUserIdFromDb();
			var initialLength = 0;
			var actualUserId = 1;

			if(!string.IsNullOrEmpty(latestUserId))
			{
				initialLength = latestUserId.Length;
				try
				{
					var trimedLatestUserId = latestUserId.Trim(USERIDZERO);
					actualUserId = Convert.ToInt32(trimedLatestUserId) + 1;
				}
				catch
				{
				}
			}
			else
			{
				initialLength = USERIDLENGTH;
				latestUserId = actualUserId.ToString();
			}

			return actualUserId.ToString().PadLeft(initialLength, USERIDZERO);
		}

		private string GetLatestUserIdFromDb()
		{
			using (var conn = DatabaseConnection)
			{
				var query = SqlQueryConstant.SelectTop1UserIdQuery;
				var queryResult = conn.Query<string>(query);
				return queryResult.FirstOrDefault();
			}
		}

		private IEnumerable<AppUser> GetAllUsers()
		{
			using (var conn = DatabaseConnection)
			{
				var selectQuery = SqlQueryConstant.SelectAllUserQuery;
				return conn.Query<AppUser>(selectQuery);
			}
		}

	}
}
