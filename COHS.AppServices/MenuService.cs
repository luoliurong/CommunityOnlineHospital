using COHS.AppServices.Interfaces;
using COHS.DataModel;
using System.Collections.Generic;

namespace COHS.AppServices
{
	public class MenuService : IMenuService
	{
		public List<MainMenu> GetMenuItemsData(string preFix, string menuId, string userName)
		{
			var root = new List<MainMenu>();
			var url = string.Empty;

			if (!string.IsNullOrEmpty(userName) && userName.ToLower() == "admin")
			{
				var systemManageMenu = new MainMenu();
				systemManageMenu.name = "系统管理";
				systemManageMenu.children = new List<SubMenu>();
				url = string.Format("{0}/{1}", preFix, "Admin/UserMangement");
				systemManageMenu.children.Add(new SubMenu() { id = "userManagementNav", name = "用户管理", target = "navtab", url = url });
				url = string.Format("{0}/{1}", preFix, "Admin/ClinicMangement");
				systemManageMenu.children.Add(new SubMenu() { id = "hospitalManagementNav", name = "诊所管理", target = "navtab", url = url });
				//url = string.Format("{0}/{1}", preFix, "Admin/ChargeTypeMangement");
				//systemManageMenu.children.Add(new SubMenu() { id = "chargeTypeManagementNav", name = "费别类型管理", target = "navtab", url = url });
				root.Add(systemManageMenu);

				return root;
			}

			if (menuId == "1")
			{
				var myPatientMenu = new MainMenu();
				myPatientMenu.name = "我的病人";
				myPatientMenu.children = new List<SubMenu>();
				url = string.Format("{0}/{1}", preFix, "MyPatients/PatientsInfo");
				myPatientMenu.children.Add(new SubMenu() { id = "myPatientsNav", name = "就诊记录", target = "navtab", url = url });
				//url = string.Format("{0}/{1}", preFix, "Diagnostics/ExamInfo");
				//myPatientMenu.children.Add(new SubMenu() { id = "examInfoNav", name = "检查单列表", target = "navtab", url = url });
				//url = string.Format("{0}/{1}", preFix, "Diagnostics/ExamInfo2");
				//myPatientMenu.children.Add(new SubMenu() { id = "examInfo2Nav", name = "检验单列表", target = "navtab", url = url });
				//url = string.Format("{0}/{1}", preFix, "Diagnostics/ElectrocardiogramInfo");
				//myPatientMenu.children.Add(new SubMenu() { id = "diagramInfoNav", name = "心电图列表", target = "navtab", url = url });
				root.Add(myPatientMenu);

				var managePatientMenu = new MainMenu();
				managePatientMenu.name = "病人管理";
				managePatientMenu.children = new List<SubMenu>();
				url = string.Format("{0}/{1}", preFix, "MyPatients/MyPatients");
				managePatientMenu.children.Add(new SubMenu() { id = "managePatientsNav", name = "病人管理", target = "navtab", url = url });
				root.Add(managePatientMenu);

				var diagnoseMenu = new MainMenu();
				diagnoseMenu.name = "在线诊疗";
				diagnoseMenu.children = new List<SubMenu>();
				url = string.Format("{0}/{1}", preFix, "Diagnostics/Diagnostics");
				diagnoseMenu.children.Add(new SubMenu() { id = "onlineDiagnoseNav", name = "网上门诊", target = "navtab", url = url });
				url = string.Format("{0}/{1}", preFix, "Diagnostics/QueryResult");
				diagnoseMenu.children.Add(new SubMenu() { id = "diagnoseResultNav", name = "结果查询", target = "navtab", url = url });
				root.Add(diagnoseMenu);

				var personalSettingsMenu = new MainMenu();
				personalSettingsMenu.name = "个人信息";
				personalSettingsMenu.children = new List<SubMenu>();
				url = string.Format("{0}/{1}", preFix, "Personal/PersonalBasicInfo");
				personalSettingsMenu.children.Add(new SubMenu() { id = "personalBasicInfoNav", name = "基本信息", target = "navtab", url = url });
				url = string.Format("{0}/{1}", preFix, "Personal/ChangePassword");
				personalSettingsMenu.children.Add(new SubMenu() { id = "changePasswordNav", name = "密码设置", target = "navtab", url = url });
				root.Add(personalSettingsMenu);
			}
			else if (menuId == "2")
			{
				var zhuyuanMenu = new MainMenu();
				zhuyuanMenu.name = "住院部";
				zhuyuanMenu.children = new List<SubMenu>();
				zhuyuanMenu.children.Add(new SubMenu() { id = "zhuyuanApplyNav", name = "住院申请", target = "navtab", url = "~/Diagnostics/Diagnostics" });
				zhuyuanMenu.children.Add(new SubMenu() { id = "zhuyuanQueryNav", name = "住院查询", target = "navtab", url = "~/Home/QueryResult/2" });
				root.Add(zhuyuanMenu);
			}

			return root;
		}
	}
}
