using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COHS.AppServices.Interfaces
{
	public interface ICommonService
	{
		Dictionary<string, string> GetConfigItems();
	}
}
