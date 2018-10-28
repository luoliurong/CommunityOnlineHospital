namespace COHS.AppServices
{
	internal class SqlQueryConstant
	{
		internal static string ConfigQuery = @"select Config_Key, Config_Value from dbo.[Config];";

		#region tables and keys of tables
		internal static readonly string PAT_MASTER_INDEX_TABLE = @"PAT_MASTER_INDEX";
		internal static readonly string PAT_MASTER_INDEX_KEY = @"PATIENT_ID";
		internal static readonly string EXAM_MASTER_TABLE = @"EXAM_MASTER";
		internal static readonly string EXAM_MASTER_KEY = @"EXAM_NO";
		internal static readonly string DIAGNOSIS_TABLE = @"DIAGNOSIS";
		internal static readonly string DIAGNOSIS_KEY_VISIT_ID = @"VISIT_ID";
		internal static readonly string DIAGNOSIS_KEY_DIAGNOSIS_NO = @"DIAGNOSIS_NO";
		#endregion

		#region stored procedures
		internal static readonly string SP_LAB_TEST_MASTER_XML = @"dbo.sp_lab_test_master_xml";
		internal static readonly string SP_LAB_TEST_RESULT_XML = @"dbo.sp_lab_test_result_xml";
		internal static readonly string SP_PAT_MASTER_INDEX_DOCTOR_XML = @"dbo.sp_pat_master_index_doctor_xml";
		#endregion

		//获取费别类型列表查询语句
		internal static string GetChargeTypeQuery = @"select Charge_Type_Code ChargeTypeCode, Charge_Type_Name ChargeTypeName from dbo.CHARGE_TYPE_DICT;";

		//根据所选的检查检验类别获得项目信息列表
		internal static string GetCheckItemPriceList = @"select input_code inputCode, Id ItemId, Item_Code itemCode, Item_Name itemName, Units Units, Price UnitPrice, 1 Quality from dbo.CURRENT_PRICE_LIST";

		//
		internal static string GetTemplateDetailQuery = @"select Id ItemId, Item_Code itemCode, Item_Name ItemName, Units, Price UnitPrice, 1 Quality from dbo.CURRENT_PRICE_LIST where Id = @ItemId";

		internal static readonly string QueryPatientInfoUsingIdentityNo = @"select Patient_Id, INP_No, Name, Sex, Age, ID_No, Phone_INT_Business from [dbo].[PAT_MASTER_INDEX] where [ID_NO] = @IdNo";

		internal static readonly string UpdatePatientPhoneQuery = @"
									update dbo.pat_master_index
									set PHONE_INT_BUSINESS = @CellPhone
									where Patient_ID = @PatientId";

		internal static readonly string InsertPatientInfoQuery2 = @"insert into dbo.pat_master_index (
																		patient_id, 
																		[name], 
																		sex, 
																		date_of_birth, 
																		age, 
																		id_no, 
																		phone_int_business, 
																		operator, create_date)
																	values (
																		@PatientId, 
																		@Name, 
																		@Gender, 
																		@Birthdate, 
																		@Age,
																		@IDNo,
																		@CellPhone, 
																		@Operator, 
																		@CreateDate)";

		internal static readonly string UpdatePatientInfoQuery = @"UPDATE [dbo].[PAT_MASTER_INDEX] 
																   SET [NAME] = @Name,
																	  [SEX] = @Sex,
																	  [AGE] = @Age,
																	  [Charge_TYPE] = @ChargeType,
																	  [PHONE_INT_BUSINESS] = @Phone,
																	  [OPERATOR] = @Operator,
																	  [ID_NO] = @IdNo
																   WHERE [Patient_Id] = @PatientId";

		internal static readonly string InsertPatientInfoQuery = @"INSERT INTO [dbo].[PAT_MASTER_INDEX] (  [PATIENT_ID]
																		, [INP_NO]
																		, [NAME]
																		, [SEX]
																		, [AGE]
																		, [DATE_OF_BIRTH]
																		, [ID_NO]
																		, [CHARGE_TYPE]
																		, [PHONE_INT_BUSINESS]
																		, [CREATE_DATE]
																		, [OPERATOR]) 
																		VALUES (@PatientId
																		, @INPNo
																		, @Name
																		, @Sex
																		, @Age
																		, @Birthday
																		, @IdNo
																		, @ChargeType
																		, @Phone
																		, @CreateDate
																		, @Operator)";

		internal static readonly string GetPatientDiagnoseQuery = @"select pat.PATIENT_ID PatientId,
																		d.DIAGNOSIS_NO DiagnoseNo, 
																		pat.Name PatientName, 
																		[DIAGNOSIS_DATE] DiagnosisDate, 
																		(select HOSPITAL_NAME from dbo.[USERS] where [USER_ID] = pat.OPERATOR) HospitalName, 
																		u.[USER_NAME] DoctorName, 
																		[DIAGNOSIS_TYPE] DiagnosisType, 
																		pat.OPERATOR 
																		from dbo.DIAGNOSIS d
																		inner join dbo.PAT_MASTER_INDEX pat on d.PATIENT_ID = pat.PATIENT_ID
																		inner join dbo.USERS u on u.[USER_ID] = pat.OPERATOR
																		where pat.Operator=@DoctorId and d.DIAGNOSIS_DATE between @StartTime and @EndTime";

		internal static readonly string GetExamMasterByPatientIdAndDiagnoseNo = @"select exam_no from dbo.exam_master where PATIENT_ID=@PatientId and DIAGNOSIS_NO=@DiagnoseNo";

		internal static readonly string InsertIntoDiagnoseQuery = @"INSERT INTO [dbo].[DIAGNOSIS] (
		[Patient_Id], 
		[DIAGNOSIS_TYPE], 
		[DIAGNOSIS_CHARGE_TYPE],
		[SYMPTOM_DESC],
		[DIAGNOSIS_DATE], 
		[DIAGNOSIS_DESC],
		[VISIT_ID]) 
		VALUES (@PatientId, @DiagnoseType, @DiagnoseChargeType, @SymptomDesc, @DiagnoseDate, @DiagnoseDesc, @VisitId);select @@IDENTITY;";

		internal static readonly string InsertIntoSymptomQuery = @"INSERT INTO [dbo].[CLINIC_MASTER]([Patient_Id], [SYMPTOM], [VISIT_DATE], [VISIT_NO]) VALUES (@PatientId, @Symptom, @VisitDate, @VisitNo)";

		internal static readonly string InsertIntoExamMasterQuery = @"INSERT INTO [dbo].[EXAM_MASTER] (
				DIAGNOSIS_NO,
				PATIENT_ID, 
				NAME, 
				SEX, 
				COSTS, 
				Exam_date_time) 
				VALUES (
				@DiagnoseNo,
				@PatientId, 
				@Name, 
				@Sex, 
				@Costs, 
				@ExamDate); SELECT @@IDENTITY;";

		internal static readonly string InsertIntoExamItemQuery = @"INSERT INTO [dbo].[EXAM_ITEMS](
		EXAM_NO, 
		EXAM_ITEM_NO, 
		EXAM_ITEM, 
		EXAM_ITEM_CODE, 
		COSTS,
		UNIT_PRICE,
		UNIT_NAME,
		QUANTITY
		) VALUES (
		@ExamNo, 
		@ItemNo, 
		@ItemName, 
		@ItemCode,
		@ItemCost,
		@UnitPrice,
		@UnitName,
		@Quantity
		)";

		internal static readonly string SelectHospitalQuery = @"select HOSPITAL_CODE HospitalCode, HOSPITAL_NAME HospitalName, CONTACT_PHONE ContactPhone, DETAIL_ADDRESS DetailAddress from dbo.HOSPITAL_INDEX;";

		#region printController
		internal static readonly string DiagnosticsPrintQuery = @"
		select pat.PATIENT_ID PatientId, 
		pat.[NAME] PatientName, 
		pat.[SEX] PatientGender, 
		pat.AGE PatientAge, 
		pat.DATE_OF_BIRTH PatientBirth, 
		pat.ID_NO PatientIdCardNo, 
		pat.CHARGE_TYPE ChargeType, 
		pat.PHONE_INT_BUSINESS PatientPhone, 
		pat.CREATE_DATE CreateDate
		,d.DIAGNOSIS_TYPE DiagnoseType
		,d.DIAGNOSIS_CHARGE_TYPE
		,d.SYMPTOM_DESC SymptomDesc
		,d.DIAGNOSIS_DESC DiagnoseDesc
		,em.EXAM_NO ExamNo
		,em.COSTS as TotalCost
		from dbo.DIAGNOSIS as d
		inner join dbo.PAT_MASTER_INDEX as pat
		on d.PATIENT_ID = pat.PATIENT_ID
		inner join dbo.EXAM_MASTER as em
		on d.DIAGNOSIS_NO = em.DIAGNOSIS_NO
		where d.DIAGNOSIS_NO = @DiagnoseNo";

		internal static readonly string ExamItemPrintQuery = @"
		select Exam_No ExamNo
		,Exam_Item_No ItemNo
		,Exam_Item ItemName
		,Exam_Item_Code ItemCode
		,Costs ItemCost
		,Unit_Price UnitPrice
		,Unit_Name UnitName
		,Quantity Quantity
		from dbo.Exam_Items
		where Exam_No = @ExamNo
		";
		#endregion


		#region user/account related query
		internal static readonly string SelectUserByUserIdQuery = @"select db_user DbUser, 
																User_ID UserId, 
																User_Name UserName, 
																User_Dept UserDept, 
																Create_Date CreateDate, 
																Title, Password, 
																Hospital_NAME HospitalName, 
																Stop_Station StopStation,
																REAL_NAME RealName,
																CONTACT_PHONE ContactPhone,
																CONTACT_ADDR ContactAddr,
																URGENT_CONTACT_PERSON UrgentContactPerson,
																URGENT_CONTACT_PHONE UrgentContactPhone 
																from dbo.USERS where [db_user] = @UserId or [User_ID] = @UserId";

		internal static readonly string ValidateUserWhenLoginQuery = @"select db_user DbUser, 
																	User_ID UserId, 
																	User_Name UserName, 
																	User_Dept UserDept, 
																	Create_Date CreateDate, 
																	Title, Password, 
																	Hospital_NAME HospitalName, 
																	Stop_Station StopStation,
																	REAL_NAME RealName,
																	CONTACT_PHONE ContactPhone,
																	CONTACT_ADDR ContactAddr,
																	URGENT_CONTACT_PERSON UrgentContactPerson,
																	URGENT_CONTACT_PHONE UrgentContactPhone 
																	from dbo.USERS 
																	where [User_Name] = @DbUser and [Db_User] = @DbUser and ([Stop_Station] = 'Y' or [Stop_Station] = 'y')";

		internal static readonly string InsertUserQuery = @"insert into dbo.[USERS] (	[DB_USER], 
														[USER_ID], 
														[USER_NAME], 
														[USER_DEPT], 
														[CREATE_DATE], 
														[TITLE], 
														[PASSWORD],
														[Hospital_Name],
														[STOP_STATION],
														[REAL_NAME],
														[CONTACT_PHONE],
														[CONTACT_ADDR],
														[URGENT_CONTACT_PERSON],
														[URGENT_CONTACT_PHONE]
														)
														VALUES (
														@DBUser,
														@USERID,
														@UserName,
														@UserDept,
														@CreateDate,
														@Title,
														@Password,
														@HospitalName,
														@StopStation,
														@RealName,
														@CPhone,
														@CAddr,
														@UCPerson,
														@UCPhone
													)";

		internal static readonly string UpdateUserPasswordQuery = @"update dbo.[Users] set Password = @PWD where [User_ID] = @UserId";

		internal static readonly string SelectSingleUserByUserId = @"select db_user DbUser, 
																User_ID UserId, 
																User_Name UserName, 
																User_Dept UserDept, 
																Create_Date CreateDate, 
																Title, Password, 
																Hospital_NAME HospitalName, 
																Stop_Station StopStation,
																REAL_NAME RealName,
																CONTACT_PHONE ContactPhone,
																CONTACT_ADDR ContactAddr,
																URGENT_CONTACT_PERSON UrgentContactPerson,
																URGENT_CONTACT_PHONE UrgentContactPhone
																from dbo.USERS where [User_Id] = @UserId";

		internal static readonly string SelectTop1UserIdQuery = @"select top 1 [user_id] from dbo.[users] where len([user_id]) = (select max(len([user_id])) from dbo.[users]) order by [user_id] desc";

		internal static readonly string SelectAllUserQuery = @"select db_user DbUser, 
																User_ID UserId, 
																User_Name UserName, 
																User_Dept UserDept, 
																Create_Date CreateDate, 
																Title, 
																Password, 
																Hospital_NAME HospitalName, 
																Stop_Station StopStation,
																REAL_NAME RealName,
																CONTACT_PHONE ContactPhone,
																CONTACT_ADDR ContactAddr,
																URGENT_CONTACT_PERSON UrgentContactPerson,
																URGENT_CONTACT_PHONE UrgentContactPhone
																from dbo.USERS;";

		internal static readonly string GetAvailableUsersQuery = @"select User_ID UserId, 
																User_Name UserName,
																Password,
																Title,
																REAL_NAME RealName,
																CONTACT_PHONE ContactPhone,
																CONTACT_ADDR ContactAddress,
																URGENT_CONTACT_PERSON UrgentContactPerson,
																URGENT_CONTACT_PHONE UrgentContactPhone,
																Hospital_NAME HospitalName,
																Create_Date CreateDate,
																Approve_Flag ApproveFlag
																from dbo.AppUser";

		internal static readonly string UpdateUserInfoQuery = @"update dbo.[USERS] set [Stop_Station] = @StopStation [DB_User] = @UserName, [USER_Name] = @UserName, [Password] = @Password, [Contact_Phone]=@ContactPhone where [User_Id] = @UserId";

		internal static readonly string GetHospitalListQuery = @"select HOSPITAL_CODE HospitalCode, HOSPITAL_NAME HospitalName, CONTACT_PHONE ContactPhone, DETAIL_ADDRESS HospitalAddress from dbo.HOSPITAL_INDEX";

		internal static readonly string UpdateHospitalQuery = @"merge dbo.HOSPITAL_INDEX as t
																using (select @HospitalCode, @HospitalName, @ContactPhone, @HospitalAddress) as s (hCode, hName, hPhone, hAddress)
																on t.HOSPITAL_CODE = s.hCode
																when matched then
																update set HOSPITAL_NAME=@HospitalName, CONTACT_PHONE=@ContactPhone, DETAIL_ADDRESS=@HospitalAddress
																when not matched then
																insert (HOSPITAL_CODE, HOSPITAL_NAME, CONTACT_PHONE, DETAIL_ADDRESS)
																VALUES (s.hCode, s.hName, s.hPhone, s.hAddress);";

		internal static readonly string DeleteHospitalQuery = @"Delete from HOSPITAL_INDEX where HOSPITAL_CODE=@HospitalCode";
		#endregion
	}
}
