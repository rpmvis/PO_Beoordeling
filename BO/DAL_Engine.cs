using System;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Text;
//using System.Data.SqlClient;
using FUNC; 

namespace BO
{
	public enum SourceType{SQL = 1, sp};

	internal struct strSel
	{
		public DataTableAttribute[] DataTables;
		public bool SelectWithPK; // bij afhankelijke tabellen selecteeer je NIET met de PK maar met de FK (Foreign Key)
		public string SQL_select;
		public string SelectFields;
		public string WhereClause;
	}

	internal struct strUpd
	{
		public DataTableAttribute[] DataTables;
		public bool ExecSP;
		public string SQL_update;
	}

	internal struct strDel
	{
		public DataTableAttribute[] DataTables;
		public string SQL_delete;
	}

	public abstract class DALEngine : IDisposable
	{		
		// 
		// private data members
		//
		IDbConnection moConn = null;
		string msConnect  = "";
		ArrayList mDALprms = new ArrayList();
		static string msDbsType = "";

		// 2 struct var//s
		strSel mstrSel;
		strUpd mstrUpd;
		strDel mstrDel;

		public DALEngine()
		{
		}

		public DALEngine(string sDbsType) 
		{
			msDbsType = sDbsType;
		}

		~DALEngine()
		{
			this.Close(); 
		}
		
		public string ConnectionString
		{
			get
			{
				if (msConnect == "")
				{
					string sAppPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();

					string filePath = sAppPath + "XML/Settings.xml";
					DataSet dsSettings = new DataSet("Settings");
					dsSettings.ReadXml(filePath);
					Console.WriteLine(dsSettings.ToString());  
					DataTable tbl = dsSettings.Tables["tblSettings"];

					string sWhere = "dbs ='" + msDbsType + "'";
					// Sort descending by column named CompanyName.
					// Use the Select method to find all rows matching the filter.
					DataRow[] rows = tbl.Select(sWhere);
					DataColumn col = tbl.Columns["ConnString"]; 

					if (rows.Length == 0)
					{
						string sMsg = "Er kan geen database connectiestring worden aangemaakt a.d.h.v. het XML bestand '" + filePath + "'!";
						throw new DALException(sMsg); 
					}
					else
					{
						DataRow row = rows[0];
						msConnect = rows[0].ItemArray[col.Ordinal].ToString();  
					}
				}
				return msConnect;
			}
		}
		
		public IDbConnection ConnObject
		{
			get 
			{ 
				if (moConn == null)
					moConn = GetConnection();

				if (moConn.ConnectionString.ToString() == string.Empty) 
					moConn.ConnectionString = this.ConnectionString; 

				if (moConn.State != ConnectionState.Open)
					moConn.Open(); 

				return moConn;  
			}
		}
		
		protected ArrayList DAL_Parameters
		{
			get { return mDALprms; }
		}
		
		public abstract IDbConnection GetConnection();
		// protected abstract void CloseConnection();

		protected abstract IDbCommand CreateCommand(string spName);
		//// in overridden methods worden de DAL parameters toegevoegd

//		protected abstract SqlCommand Create_SqlCommand(string spName);
//		// RV;  in overridden methods worden de DAL parmaters toegevoegd

		public virtual void Close()
		{
			// try to close
			if(null != moConn)
			{
				try
				{
					if (moConn.State != System.Data.ConnectionState.Closed)
					{
						moConn.Close(); 
					}
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// Rolls back any pending transactions and closes the DB connection.
		/// </summary>
		public virtual void Dispose()
		{
			this.Close();
			if(null != moConn)
			{
				moConn.Dispose();
			}
		}
			
		public DALprm GetParameter(string sName)
		{
			foreach (DALprm param in DAL_Parameters)
			{
				if (param.Name == sName)
					return param;
			}
			
			return null;
		}
		
		void UpdateOutputParameters(IDbCommand cmd)
		{
			int i = 0;
			foreach (DALprm oPrm in DAL_Parameters)
			{
				if (oPrm.Direction == ParameterDirection.Output || 
					oPrm.Direction == ParameterDirection.InputOutput)
				{
					oPrm.Value = ((IDataParameter)cmd.Parameters[i]).Value;
				
				}
				
				i++;
			}			
		}
		
		public void AddParameter(DALprm oPrm)
		{
			DAL_Parameters.Add(oPrm);
		}
		
		public void AddInputParameter(string pName, object pValue)
		{
			DALprm oPrm = new DALprm(pName, pValue);
			
			System.Type type = pValue.GetType();
 
			switch(type.ToString())
			{
				case "System.Int32":
					oPrm.Type = DbType.Int32; 
					break;
				default:
					oPrm.Type = DbType.String; 
					break;
			}

			oPrm.Direction = ParameterDirection.Input;

			DAL_Parameters.Add(oPrm);
		}

		
		public void ClearParameters()
		{
			DAL_Parameters.Clear();
		}

		public abstract void ExecSP_DataSet(string spName, DataSet ds, string sTblName);

	
		public IDataReader ExecSP_DataReader(string spName)
		{
			return ExecSP_DataReader(spName, CommandBehavior.Default);
		}
		
		public IDataReader ExecSP_DataReader(string spName, CommandBehavior behavior)
		{
			IDataReader dr = null;
			
			try
			{
				IDbCommand cmd = CreateCommand(spName);
				
				dr = cmd.ExecuteReader(behavior);
			}
			catch (Exception e)
			{
				if (dr != null)
				{
					dr.Close();
					dr = null;
				}
					
				throw new DALException("DALEngine::ExecSP_DataReader\nin stored procedure '" + spName + "'", e);
			}		
			
			return dr;		
		
		}

		public object ExecSP_Scalar(string spName)
		{
			object result = null;
			
			try
			{			
				IDbCommand cmd = CreateCommand(spName);
				
				result = cmd.ExecuteScalar();
				
				UpdateOutputParameters(cmd);
									
			}
			catch (Exception e)
			{				
				throw new DALException("Error in DALEngine::ExecSP_Scalar, in stored procedure '" + spName + "'", e);
			}		
			
			return result;		
		
		}

		public int ExecSP_NonQuery(string spName)
		{
			int result = -1;
			
			try
			{
				IDbCommand cmd = CreateCommand(spName);
				// result = Rows affected
				result = cmd.ExecuteNonQuery();
				
				UpdateOutputParameters(cmd);
									
			}
			catch (Exception e)
			{				
				throw new DALException("Error in DALEngine::ExecSP_NonQuery, in stored procedure '" + spName + "'", e);
			}		
			
			return result;				
		}

		// using strict SQL queries
		public abstract void ExecQuery_DataSet(string sSQL, DataSet ds, string sTblName);
		

		public IDataReader ExecQuery_DataReader(string sSQL, CommandBehavior behavior)
		{
			IDataReader dr = null;
			try
			{			
				IDbCommand cmd = this.ConnObject.CreateCommand();
				cmd.CommandText = sSQL;
				dr = cmd.ExecuteReader(behavior);
			}
			catch (Exception e)
			{
				throw new DALException("DALEngine::ExecQuery_DataReader\nin query '" + sSQL + "'", e);
			}		
			
			return dr;		
		}

		public IDataReader ExecQuery_DataReader(string sSQL)
		{
			return ExecQuery_DataReader(sSQL, CommandBehavior.CloseConnection);	
		}

		public object ExecQuery_Scalar(string sSQL)
		{
			object result = null;
			
			try
			{
				IDbCommand cmd = this.ConnObject.CreateCommand();
				cmd.CommandText = sSQL;
				result = cmd.ExecuteScalar();
			}
			catch (Exception e)
			{
				throw new DALException("DALEngine::ExecQuery_Scalar\nin query '" + sSQL + "'", e);
			}
			
			return result;
		}

		public int Exec_ActionQuery(string sSQL)	
		{
			// returns the number of rows affected.
			int iRows = -1;
			
			try
			{
				IDbCommand cmd = this.ConnObject.CreateCommand();
				cmd.CommandText = sSQL;
				iRows = cmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				throw new DALException("DALEngine::Exec_ActionQuery\nin query '" + sSQL + "'", e);
			}
			return iRows;
		}
	
		public bool FillFromReader(IDataReader dr, ref object obj)
		{
			ReadFromReader(dr, ref obj);

			return true;
		}

		public void ReadFromReader(IDataReader dr, ref object instance)
		{
			string sFld;
			Type objType = instance.GetType(); 

			PropertyInfo[] aPropInfo = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			
			// itereren over de object properties
			// toekennen van waarden uit de datareader aan deze properties

			if (dr.IsClosed == true)
			{
				dr.Read(); 
			}


			for (int i=0; i < aPropInfo.Length; i++)
			{
				BaseFieldAttribute[] fields = (BaseFieldAttribute[])aPropInfo[i].GetCustomAttributes(typeof(BaseFieldAttribute), true);

				if (fields.Length > 0)

				{
					object oVal;

					sFld = fields[0].ColumnName;

					try
					{
						oVal = dr[sFld];
					}
					catch (Exception e)
					{
						throw new DALException("Veld [" + sFld+ "] bestaat niet in de Data reader voor object van type " +  objType.ToString() + "!" , e);
					}


					if (aPropInfo[i].PropertyType.IsEnum)
					{
						oVal = Enum.Parse(aPropInfo[i].PropertyType, oVal.ToString());
					}
					
					string sPropType = aPropInfo[i].PropertyType.ToString();
					bool IsNull = System.Convert.IsDBNull(oVal); // tackle null value problem

					switch (sPropType)
					{
						case "System.String":
							if (IsNull)
								aPropInfo[i].SetValue(instance, "", null);
							else
							{
								if ( oVal.GetType() == typeof(string) )
									aPropInfo[i].SetValue(instance, oVal, null);
								else
									// System.Convert.ToString(oVal) in geval van Datum veld --> String Prop
									aPropInfo[i].SetValue(instance, System.Convert.ToString(oVal), null);
							}
							break;	
	
						case "System.Boolean":
							if (IsNull) oVal = false;
							aPropInfo[i].SetValue(instance, oVal, null);
							break;
						case "System.DateTime":
							if (IsNull) oVal = new DateTime(1,1,1);
							aPropInfo[i].SetValue(instance, oVal, null);
							break;
						case "System.Byte":
							if (IsNull) oVal = 255;
							aPropInfo[i].SetValue(instance, System.Convert.ToByte(oVal), null);
							break;
						default:
							if (IsNull) oVal = 0;
							aPropInfo[i].SetValue(instance, oVal, null);
							break;
					}
				}
			}				
		}
		
		public object CreateNewObject(object[] keyValues, Type objType)
		{
			// creëeer instantie van het type objType
			// deze instantie bestaat nog niet in de database
			object instance = Activator.CreateInstance(objType, true);
			
			PropertyInfo[] aPropInfo = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			
			int j = -1;
			for (int i=0; i < aPropInfo.Length; i++)
			{
				KeyFieldAttribute[] keys = (KeyFieldAttribute[])aPropInfo[i].GetCustomAttributes(typeof(KeyFieldAttribute), true);

				if (keys.Length > 0)
				{
					j++;
					aPropInfo[i].SetValue(instance, keyValues[j], null);
				}
			}
			return instance;
		}

		public object LoadObject(object[] keyValues, Type objType)
		{
			object instance = null;

			// instance = RetrieveObject(keyValues, objType);
			Fill_mstrSel(keyValues, objType, true);
			string sSQL = mstrSel.SQL_select;
			IDataReader dr = ExecQuery_DataReader(sSQL);						
				
			try
			{
				if (dr.Read())
				{
					// creëer instantie van het type objType
					instance = Activator.CreateInstance(objType, true);
					ReadFromReader(dr, ref instance);
				}
			}
			catch (Exception e)
			{
				throw new DALException("Ophalen mislukt van object [" + objType + "] met SQL statement \n" + sSQL.ToString(), e);
			}
			finally
			{
				if (dr != null)
				{
					dr.Close(); 
					dr.Dispose();
				}
			}
			return instance;
		}


		public object RetrieveObject(object[] keyValues, Type objType)
		{
			Fill_mstrSel(keyValues, objType, true);

			return RetrieveObject(mstrSel.SQL_select , objType);
		}

		public bool FillObject(string sSQL, object instance)
		{
			bool bRet = false;
			IDataReader dr = ExecQuery_DataReader(sSQL);						
		
			try
			{
				if (dr.Read())
				{
					bRet = this.FillFromReader(dr, ref instance);  
				}
			}
			catch (Exception e)
			{
				throw new DALException("Vullen van object  [" + instance.GetType().ToString() +
									              "] met SQL statement \n" + sSQL.ToString() + " is mislukt.", e);

			}
			finally
			{
				if (dr != null)
				{
					dr.Close(); 
					dr.Dispose();
				}
			}
			return bRet;
		}

		public object RetrieveObject(string sSQL, Type objType)
		{
			object instance = null;

			IDataReader dr = ExecQuery_DataReader(sSQL);						
				
			try  
			{
				if (dr.Read())
				{
					// creëer instantie van het type objType
					instance = Activator.CreateInstance(objType, true);
					ReadFromReader(dr, ref instance);
				}
			}
			catch (Exception e)
			{
				throw new DALException("Ophalen mislukt van object [" + objType.ToString()  + "] met SQL statement \n" + sSQL.ToString(), e);
			}
			finally
			{
				if (dr != null)
				{
					dr.Close(); 
					dr.Dispose();
				}
			}
			return instance;		
		}
		
		public bool ObjectExists(string sSQL)
		{
			bool result = false;

			IDataReader dr = ExecQuery_DataReader(sSQL);						
				
			try
			{
				if (dr.Read())
				{
					result = true;
				}
			}
			finally
			{
				if (dr != null)
				{
					dr.Close(); 
					dr.Dispose();
				}
			}
			
			return result;		
		}
	
		private int UpdateObject_with_sp(object oBO)
		{
			int iRows = -1;

			DALprm oPrm_Key = null;
			PropertyInfo oPropInfo_Key  = null;

			PropertyInfo[] aPropInfo = oBO.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			
			ClearParameters();
			
			for (int i=0; i < aPropInfo.Length; i++)
			{
				BaseFieldAttribute[] aBaseFld = (BaseFieldAttribute[])aPropInfo[i].GetCustomAttributes(typeof(BaseFieldAttribute), true);

				if (aBaseFld.Length > 0)
				{
					object oValue  = aPropInfo[i].GetValue(oBO, null);
					if (oValue.GetType().IsEnum)
					{
						oValue = Convert.ToInt32(oValue);
					}

					// DAL oPrm. initiliseren op @+veldnaam en veldwaarde
					DALprm oPrm = new DALprm("@" + aBaseFld[0].ColumnName, oValue);
					

					// instellen type + size (indien > 0) van oPrm
					BaseFieldAttribute oBase = (BaseFieldAttribute)aBaseFld[0];
					if (oBase != null)
					{
						oPrm.Type = oBase.Type;

						if (oBase.Size != 0)
						{
							oPrm.Size = oBase.Size;
						}
					}
					else if (aBaseFld[0] is KeyFieldAttribute)
					{
						oPropInfo_Key  = aPropInfo[i];
						oPrm_Key = oPrm;
						
						//oPrm.Direction = ParameterDirection.InputOutput;
						oPrm.Direction = ParameterDirection.Input;
					}

					AddParameter(oPrm);
				}
			}				

			if (oPropInfo_Key == null || oPrm_Key == null)
			{
				throw new ArgumentException("Het object " + oBO + " heeft geen KeyField attribuut");
			}

			string spName = mstrUpd.DataTables[0].spUPDATE;

			iRows = ExecSP_NonQuery(spName);

				// bv. "sp_UpdateContact"
			
			// vb.: oPropInfo_Key.PropertyType= {"System.Int32"}
			oPropInfo_Key.SetValue(oBO, Convert.ChangeType(oPrm_Key.Value, oPropInfo_Key.PropertyType), null);

			return iRows;
		}
		
		public bool UpdateObject(object oBO)
		{	
			int iRows = 0;
			try
			{
				
				Fill_mstrUpd(oBO, null);
				
				if (mstrUpd.ExecSP == true) 
				{
					iRows = UpdateObject_with_sp(oBO);
				}
				else
				{
					string sSQL = mstrUpd.SQL_update;
					Exec_ActionQuery(sSQL); // affected record's WERKT NIET!
					iRows = 1;
				}			
				
				// just retrieve if it//s a numeric key. probably an auto number column.
				//			if (qBuilder.IsInserting && keyProperty.PropertyType != typeof(string) && keyProperty.PropertyType != typeof(char))
				//			{
				//				object newKey = ExecQuery_Scalar("SELECT @@IDENTITY AS //Identity'");
				//				keyProperty.SetValue(oBO, Convert.ChangeType(newKey, keyProperty.PropertyType), null);
				//			}
			}
			catch(Exception ex)
			{
				iRows = -1;
				throw new DALException(ex.Message);   
			}
			
			return(iRows ==1);
		}

		public bool UpdateObject_Property(object oBO, 
			                              string sPropertyName)
		{	
			int iRows = -1;
			Fill_mstrUpd(oBO, sPropertyName);

			string sSQL = mstrUpd.SQL_update; 
			iRows = Exec_ActionQuery(sSQL);
			return (iRows==1);
		}


		public int UpdateObjects(IEnumerable enumObjects)
		{
			int iTeller =0;
			foreach(object oBO in enumObjects)
			{
				if (UpdateObject(oBO) == true) iTeller +=1;
			}
			return iTeller;
		}
		
		public int DeleteObject(object oBO)
		{	
			int iRows = -1;
			Fill_mstrDel(oBO, null);
			
			string sSQL = mstrDel.SQL_delete; 
			if (sSQL != "")
				iRows = Exec_ActionQuery(sSQL);
			else // bijv. record is reeds verwijderd / niet aanwezig
				iRows = -1;
			return iRows;
		}

		public int RetrieveChildObjects(object[] foreignKeyValues, 
			                              System.Collections.IList childObjects,
                                   	Type childType)
		{
			int iRowsAffected =0;

			Fill_mstrSel(foreignKeyValues, childType, false);
				
			iRowsAffected = RetrieveChildObjects(mstrSel.SQL_select, SourceType.SQL,  childObjects, childType);

			return iRowsAffected;
		}		


		public int RetrieveChildObjects(string sSource,
																		SourceType srctype, 
																		IList childObjects,
																		Type childType)
		{
			IDataReader dr = null;

			switch(srctype)
			{
				case SourceType.SQL:
					dr = ExecQuery_DataReader(sSource);
					break;
				case SourceType.sp:
					dr = ExecSP_DataReader(sSource); 	
					break;
			}
			using(dr)
			{
				return RetrieveChildObjects(dr, childObjects, childType);
			}
		}

		public int RetrieveChildObjects(IDataReader dr,
																		IList childObjects,
																		Type childType)
		{
			int iRowsAffected = 0;
			try
			{
				while (dr.Read())
				{
					iRowsAffected++;
					// creëer instantie van het type objType
					object obj = Activator.CreateInstance(childType, true);
					ReadFromReader(dr, ref obj);
					childObjects.Add(obj);
				}
			}
			catch (Exception e)
			{
				iRowsAffected = -1;
				string sMsg = "Ophalen mislukt van verzameling objecten  [" + childType + "] met SQL statement \n" + mstrSel.SQL_select;
				throw new DALException(sMsg.ToString() , e);
			}
			finally
			{
				if (dr != null)
				{
					dr.Close(); // close to avoid error //a DataReader already open on this connnection//
					dr.Dispose();
				}
			}
			return iRowsAffected;
		}

		private bool keyValues_Ok(object[] keyValues, Type objType)
		{
			bool bRet = true;
			
			// try to falsify
			if(keyValues.Length > 0){
				for(int i = 0; i<keyValues.Length;i++)
				{
					if(keyValues[i]==null)
					{
						bRet = false;
						string sMsg = "De " + System.Convert.ToString(i+1) + 
							            "e parameter = null bij het ophalen van object met type '" +
							            objType.ToString() + "'!" ;
						throw new DALException(sMsg); 
					}
				}
			}
			return bRet;
		}

		private void Fill_mstrSel(object[] keyValues, Type objType, bool SelectWithPK)
		{
			if (!keyValues_Ok(keyValues, objType)) return;

			mstrSel.SelectWithPK = SelectWithPK;

			mstrSel.DataTables = (DataTableAttribute[])objType.GetCustomAttributes(typeof(DataTableAttribute), true);

			if (mstrSel.DataTables.Length > 0)
			{
				// alle veld props binnen de DataTable
				PropertyInfo[] arrFldInfo = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
				
				string sWhere = "";

				int j = -1; // index for keyvalues
				for (int i=0; i < arrFldInfo.Length; i++)
				{
					if (SelectWithPK)
					{
						KeyFieldAttribute[] keys = (KeyFieldAttribute[])arrFldInfo[i].GetCustomAttributes(typeof(KeyFieldAttribute), true);
						if (keys.Length > 0)
						{
							j++;
							if (arrFldInfo[i].PropertyType == typeof(string) || arrFldInfo[i].PropertyType == typeof(char))
							{
								// bijv. sWhere = "PersCode = //Baate'"
							
								sWhere += "[" + keys[0].ColumnName + "] = '" + keyValues[j] + "'";
							}
							else
							{
								sWhere += "[" + keys[0].ColumnName + "] = " + keyValues[j];
							}
							sWhere += " and ";
						}
					}
					else
					{
						ForeignKeyFieldAttribute[] keys = (ForeignKeyFieldAttribute[])arrFldInfo[i].GetCustomAttributes(typeof(ForeignKeyFieldAttribute), false);
						if (keys.Length > 0)
						{
							j++;
							if (arrFldInfo[i].PropertyType == typeof(string) || arrFldInfo[i].PropertyType == typeof(char))
							{
								if(keyValues[j].ToString() != "%")
									sWhere += "[" + keys[0].ColumnName + "] = '" + keyValues[j] + "'";
								else
									sWhere += "[" + keys[0].ColumnName + "] LIKE //%'";
							}
							else
							{
								sWhere += "[" + keys[0].ColumnName + "] = " + keyValues[j];
							}
							sWhere += " and ";
						}
					}
				}

				mstrSel.WhereClause =  cString.Strip(sWhere, " and ");

				if (mstrSel.WhereClause== "")
				{
					string sMsg;
					if (SelectWithPK){sMsg = "Het object [" + objType + "] heeft geen sleutel attribuut / atributen.";}
					else{sMsg = "Het object [" + objType + "] heeft geen vreemde sleutel attribuut / atributen.";}
					throw new ArgumentException(sMsg);
				}

				string sFlds = "";

				for (int i=0; i < arrFldInfo.Length; i++)
				{
					BaseFieldAttribute[] fields = (BaseFieldAttribute[])arrFldInfo[i].GetCustomAttributes(typeof(BaseFieldAttribute), true);
					
					if (fields.Length > 0)
					{											
						sFlds += "[" + fields[0].ColumnName + "], ";
					}
				}
				mstrSel.SelectFields  = cString.Strip(sFlds, ", ");
				
				string sSel = "SELECT " + mstrSel.SelectFields + 
				              " FROM " + mstrSel.DataTables[0].TableName + "" +
				              " WHERE " + mstrSel.WhereClause   ;

				mstrSel.SQL_select = sSel;
			}
			else
			{
				throw new ArgumentException("Het DataTable attribuut is niet gevonden in de [" + objType.ToString()  + "] parameter");
			}     
		}
		
		private void Fill_mstrUpd(object oBO, string sPropertyName)
		{
			string sPropType;
			cFlds colFields = new cFlds();
			DbType dbType;
			BaseFieldAttribute oBase;
			KeyFieldAttribute[] keys;

			Type objType = oBO.GetType();

			mstrUpd.DataTables = (DataTableAttribute[])objType.GetCustomAttributes(typeof(DataTableAttribute), true);

			if (mstrUpd.DataTables.Length > 0)
			{
				if (mstrUpd.DataTables[0].spUPDATE != "")
					{mstrUpd.ExecSP = true;}
				else
					{mstrUpd.ExecSP = false;}	

				if (mstrUpd.ExecSP == true)
					{
					}
				else
					{
					PropertyInfo[] arrFldInfo = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

						for (int i=0; i < arrFldInfo.Length; i++)
						{
							BaseFieldAttribute[] baseFields = (BaseFieldAttribute[])arrFldInfo[i].GetCustomAttributes(typeof(BaseFieldAttribute), true);
					
							if (baseFields.Length > 0)
							{
								oBase = (BaseFieldAttribute)baseFields[0];

								// skip fields with readonly attribute
								if (oBase.ReadOnly){
									continue;
								}

								if (sPropertyName!= null) // // update only 1 prop
								{
									// include only key fields and field to update
									if (oBase.ColumnName.ToLower() == sPropertyName.ToLower())
									{
										// no skip
									}
									else // skip no key fields
									{
										keys = (KeyFieldAttribute[])arrFldInfo[i].GetCustomAttributes(typeof(KeyFieldAttribute), false);
										if (keys.Length == 0)
										{
											continue;
										}
									}
								}

								Console.WriteLine(arrFldInfo[i].Name);  
								sPropType = arrFldInfo[i].PropertyType.ToString();

								// dbType
								switch (sPropType)
								{
									case "System.String":
										dbType = DbType.String; 
										break;	
	
									case "System.Boolean":
										dbType = DbType.Boolean; 
										break;
									
									case "System.DateTime":
										dbType= DbType.DateTime; 
										break;
									default:
										dbType = DbType.Int32; 
										break;
								}
								
								// waarde uit business object				
								object oVal = arrFldInfo[i].GetValue(oBO, null);

								bool IsNull = (oVal == null); // tackle null value problem
 
								if (IsNull == true)
								{
									switch (dbType)
									{
										case DbType.String:
											oVal = "";
											break;	
		
										case DbType.Boolean:
											oVal = 0;
											break;
										
										case DbType.DateTime:
											oVal = new System.DateTime(1,1,1); // fungeert als null waarde
											break;
										default:
											oVal = 0;
											break;
									}
								}

								switch (dbType)
								{
									case DbType.Boolean:
									{
										bool bVal = System.Convert.ToBoolean(oVal.ToString());
										if (bVal == false) oVal = 0;
										if (bVal == true) oVal = -1;
										break;
									}
									case DbType.DateTime: 
									{
										if (System.DateTime.Equals(oVal, new System.DateTime(1,1,1)))
										{
											oVal = null;
										}
										break;
									}
								}

								if (oVal != null && oVal.GetType().IsEnum)
								{
									oVal = Convert.ToInt32(oVal);
								}

								if (oBase != null)
								{
									cFld oFld = new cFld();
									oFld.Name  = oBase.ColumnName;
									oFld.DBType = dbType;
									oFld.Size = oBase.Size; 
									oFld.Value = oVal;

									if (oFld.Name != null)
										colFields.Add(oFld); 	
								}
							}				
						}

					// PropertyInfo keyProperty = null;

					string sWhere = "";
					string sSQL;
					string sFields = "";
					string sValues = "";

					sWhere = Get_sWhere(oBO, arrFldInfo);

				 if (sWhere != "")
					{
						sFields = Get_KeyFields(arrFldInfo);
							
						bool rowExist = DoesRowExist(sFields, mstrUpd.DataTables[0].TableName, sWhere); 

						sFields = "";
						if (rowExist)
						{
							sSQL = "UPDATE " +  mstrUpd.DataTables[0].TableName  +
								" SET ";

							foreach(cFld oFld in colFields)
							{
								sSQL += oFld.Name + " = ";
								sSQL += oFld.SQLvalue + ", ";
							}

							sSQL = cString.Strip(sSQL, ", ") ;
							sSQL += " WHERE " + sWhere;
							
							mstrUpd.SQL_update = sSQL;
						}
						else
						{
							sSQL = "INSERT INTO " + mstrUpd.DataTables[0].TableName  + " (";

							foreach(cFld oFld in colFields)
							{
								sFields += oFld.Name + ", "; 
								sValues += oFld.SQLvalue + ", "; 
							}

							sFields = cString.Strip(sFields, ", ");
							sValues = cString.Strip(sValues, ", ");

							sSQL += sFields + ")";
							sSQL += " VALUES (" + sValues + ")";

							mstrUpd.SQL_update = sSQL;
						} // rowExist
				 } // if
				} // else for UPDATE
			}

			else
			{
				throw new ArgumentException("Het DataTable attribuut is niet gevonden in de [" + oBO.GetType()  + "] parameter");			
			}
		}	

		private bool DoesRowExist(string sSelectFields, string sTblName, string sWhere)
		{

			string sSQL = "SELECT " + sSelectFields +
							      " FROM " + sTblName + 
							      " WHERE " + sWhere;

			bool bExist = this.ObjectExists(sSQL);
			return bExist;
		}


		private string Get_KeyFields(PropertyInfo[] arrFldInfo)
		{
			string sFields = "";
			KeyFieldAttribute[] keys;

			for (int i=0; i < arrFldInfo.Length; i++)
			{
				keys = (KeyFieldAttribute[])arrFldInfo[i].GetCustomAttributes(typeof(KeyFieldAttribute), false);
				
				if (keys.Length > 0)
				{
					sFields += "[" + keys[0].ColumnName + "]" + ", ";
				}
			}

			sFields = cString.Strip(sFields, ", ");	

			return sFields;
		}


		private string Get_sWhere(object oBO, PropertyInfo[] arrFldInfo)
		{
			string sWhere = "";
			string sFld="";
			string sVal = "";
			KeyFieldAttribute[] keys;
			string sPropType =""; 

			// BEPALEN WHERE CLAUSE
			for (int i=0; i < arrFldInfo.Length; i++)
			{
				sPropType = arrFldInfo[i].PropertyType.ToString();

				keys = (KeyFieldAttribute[])arrFldInfo[i].GetCustomAttributes(typeof(KeyFieldAttribute), false);
				
				if (keys.Length > 0)
				{
					sFld = "[" + keys[0].ColumnName + "]";

					switch (sPropType)
					{
						case "System.String":
							sVal = arrFldInfo[i].GetValue(oBO, null).ToString();
							if (sVal != "")
							{
								sWhere += sFld + " = '" + sVal + "'" + " And "; 
							}							
							break;	

						case "System.Char":
							sVal = arrFldInfo[i].GetValue(oBO, null).ToString();
							if (sVal != "")
							{
								sWhere += sFld + " = '" + sVal + "'" + " And "; 
							}							
							break;	
								
						case "System.DateTime":
							sVal = arrFldInfo[i].GetValue(oBO, null).ToString();
							if (sVal == "1/1/1")
								sWhere += "sFld = null And "; 	
							else
								//sWhere += sFld + " = '" + sVal + "' And "; 
								sWhere += sFld + " = " + sVal + " And "; 
							break;	
								
						case "System.Int32":
							sVal = arrFldInfo[i].GetValue(oBO, null).ToString();
							sWhere += sFld + " = " + sVal + " And "; 
							break;

						default:
							if (sVal != "0")
							{
								sWhere += sFld + " = " + sVal + " And "; 		
							}							
							break;
					}
				}
			}  // for
			sWhere = cString.Strip(sWhere, " And ");
			return sWhere;
		}

		private void Fill_mstrDel(object oBO, string sPropertyName)
		{
			string sSQL="";
			string sWhere = "";
			cFlds colFields = new cFlds();

			Type objType = oBO.GetType();

			mstrDel.DataTables = (DataTableAttribute[])objType.GetCustomAttributes(typeof(DataTableAttribute), true);

			if (mstrDel.DataTables.Length > 0)
			{
				PropertyInfo[] arrFldInfo = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

				sWhere = Get_sWhere(oBO, arrFldInfo);

				if (sWhere != "")
				{
						bool rowExist = DoesRowExist("*", mstrDel.DataTables[0].TableName, sWhere); 

					if (rowExist)
					{
						sSQL = "DELETE FROM " +  mstrDel.DataTables[0].TableName  +
							" WHERE <where>;";

						sSQL = sSQL.Replace("<where>" ,sWhere);  
					}
				} // if

				mstrDel.SQL_delete = sSQL;
			} // else for UPDATE
		
			else
			{
				throw new ArgumentException("Het DataTable attribuut is niet gevonden in de [" + oBO.GetType()  + "] parameter");			
			}
		}	
	}
}