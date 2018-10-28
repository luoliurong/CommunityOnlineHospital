using System.Collections.Generic;

namespace COHS.DataModel
{
	public class SubMenu
	{
		public string id { get; set; }
		public string name { get; set; }
		public string target { get; set; }
		public string url { get; set; }
	}

	public class MainMenu
	{
		public string name { get; set; }
		public List<SubMenu> children { get; set; }
	}
}
