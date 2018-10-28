using COHS.AppServices.Interfaces;
using COHS.WebCore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace COHS.WebCore.Controllers
{
	public class HomeController : Controller
    {
		private IAppUserService userService;
		public HomeController(IAppUserService userService)
		{
			this.userService = userService;
		}

        public IActionResult Index()
        {
			var userDto = userService.GetAvailableUserList();
			List<AppUserViewModel> users = new List<AppUserViewModel>();
			if (userDto.Any())
			{
				foreach (var user in userDto)
				{
					users.Add(new AppUserViewModel() {
						UserId = user.UserId,
						UserName= user.UserName,
						Password=user.Password,
						Title = user.Title,
						RealName = user.RealName,
						ContactPhone = user.ContactPhone,
						ContactAddress = user.ContactAddress,
						UrgentContactPerson = user.UrgentContactPerson,
						UrgentContactPhone = user.UrgentContactPhone,
						HospitalName = user.HospitalName,
						CreateDate = user.CreateDate,
						ApproveFlag = user.ApproveFlag
					});
				}
			}
            return View(users);
        }
    }
}