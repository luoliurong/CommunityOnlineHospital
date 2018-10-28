using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Xml.Linq;
using System.IO;
using System.Transactions;
using System.Diagnostics.CodeAnalysis;
using COHS.DataModel;
using COHS.AppServices.Interfaces;
using Microsoft.Extensions.Configuration;

namespace COHS.AppServices
{
	public class DiagnosticsService : BaseService, IDiagnosticService
	{
		public DiagnosticsService(IDbService dbService) : base(dbService)
		{
		}

		private readonly string XMLEMPTY = "检验结果信息不能为空";
		#region API for first step
		/// <summary>
		/// 就诊类别信息：static, 不需要从数据库读取。
		/// </summary>
		/// <returns></returns>
		[ExcludeFromCodeCoverage]
		public IEnumerable<DiagnosticCategory> GetDiagnosticCategoryList()
		{
			List<DiagnosticCategory> entityList = new List<DiagnosticCategory>();
			XElement element = ReadDataFromConfig("zhenduanleixingConfig");
			var categoriesItems = element.Element("items");
			if (categoriesItems != null)
			{
				var categories = categoriesItems.Elements("item");
				var serialNo = 1;
				foreach (var item in categories)
				{
					DiagnosticCategory category = new DiagnosticCategory();
					category.CategoryId = serialNo;
					category.CategoryName = item.Value.Trim();
					entityList.Add(category);
					serialNo++;
				}
			}
			return entityList;
		}

		/// <summary>
		/// 获取费别类型，数据表：CHARGE_TYPE_DICT
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ChargeType> GetChargeTypeList()
		{
			using (var conn = DatabaseConnection)
			{
				return conn.Query<ChargeType>(SqlQueryConstant.GetChargeTypeQuery);
			}
		}
		#endregion


		private XElement ReadDataFromConfig(string configName)
		{
			var fullPath = Environment.CurrentDirectory;
			string relativePath = new ConfigurationBuilder().AddJsonFile("configs/appSettings.json").Build().GetSection("AppSettings")[configName];
			if (!string.IsNullOrWhiteSpace(relativePath))
			{
				fullPath = Path.Combine(fullPath, relativePath);
			}
			FileInfo xmlFile = new FileInfo(fullPath);
			if (xmlFile.Exists)
			{
				return XElement.Load(fullPath);
			}
			throw new FileNotFoundException(string.Format("没有找到文件：{0}", fullPath));
		}
	}
}