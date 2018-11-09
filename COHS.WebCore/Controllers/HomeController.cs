using AutoMapper;
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
		private IMapper mapper;

		public HomeController(IAppUserService userService, IMapper mapper)
		{
			this.userService = userService;
			this.mapper = mapper;
		}

        public IActionResult Index()
        {
			var userDto = userService.GetAvailableUserList();
			List<AppUserViewModel> users = new List<AppUserViewModel>();
			if (userDto.Any())
			{
				foreach (var user in userDto)
				{
					users.Add(mapper.Map<AppUserViewModel>(user));
				}
			}
            return View(users);
        }
    }
}