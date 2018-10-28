using System;

namespace COHS.DataModel
{
	public class AppUser
	{
		public long UserId { get; set; }
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
