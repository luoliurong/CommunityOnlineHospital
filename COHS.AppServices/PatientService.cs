using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using COHS.DataModel;
using COHS.AppServices.Interfaces;

namespace COHS.AppServices
{
	public class PatientService : BaseService, IPatientService
	{
		public PatientService(IDbService dbService) : base(dbService)
		{
		}
	}
}
