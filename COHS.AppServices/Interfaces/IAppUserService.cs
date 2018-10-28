using COHS.DataModel;
using System.Collections.Generic;

namespace COHS.AppServices.Interfaces
{
	public interface IAppUserService
	{
		string ValidateAccount(string userAccount, string userKey);
		AppUser GetUserInfo(long userId);
		IEnumerable<AppUser> GetAvailableUserList();
		void UpdateUserInfo(AppUser userInfo);
	}
}
