using AutoMapper;
using COHS.DataModel;
using COHS.WebCore.ViewModel;

namespace COHS.WebCore.AutoMapper
{
	public class ProfileConfig : Profile
	{
		public ProfileConfig()
		{
			CreateMap<AppUser, AppUserViewModel>();
			CreateMap<AppUserViewModel, AppUser>();
		}
	}
}
