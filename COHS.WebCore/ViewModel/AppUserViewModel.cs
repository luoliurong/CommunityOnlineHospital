using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COHS.WebCore.ViewModel
{
	public class AppUserViewModel
	{
		[Display(Name = "用户Id", AutoGenerateField = true)]
		[Required]
		public long UserId { get; set; }
		
		[Display(Name = "用户名")]
		[Required]
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Title { get; set; }
		public string RealName { get; set; }
		public string ContactPhone { get; set; }
		public string ContactAddress { get; set; }
		public string UrgentContactPerson { get; set; }
		public string UrgentContactPhone { get; set; }
		public string HospitalName { get; set; }
		public string CreateDate { get; set; }
		public bool ApproveFlag { get; set; }
	}
}
