using System.Data;

namespace COHS.AppServices.Interfaces
{
	public interface IBaseService
	{

	}

	public interface IDbService
	{
		IDbConnection GetDbConnection();
	}
}
