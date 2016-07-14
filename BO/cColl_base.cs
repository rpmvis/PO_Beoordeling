using System;
using System.Collections; 
using System.Data; 
using System.Reflection; 
using System.Text; 
using FUNC;

namespace BO
{
	/// <summary>
	/// Summary description for cWerkns.
	/// </summary>
	public class cColl_base: CollectionBase
	{
		private System.Type mType = null;

		private string msTable ="";
		private string msKeyField =""; 
		private DAL_OleDb mDal;

		public cColl_base(DAL_OleDb oDal, System.Type ChildType)
		{
			mDal = oDal;
			mType= ChildType;

			// vaststellen mbv Reflectie 1) datatabel 2) sleutelveld
			// 1) datatabel
			DataTableAttribute[] DataTables = (DataTableAttribute[])mType.GetCustomAttributes(typeof(DataTableAttribute), true);
			if (DataTables.Length > 0)
			{
				msTable = DataTables[0].TableName;}
			// else throw new BOException("variabele 'msTable' is leeg na reflectie!"); 

			// 2) sleutelveld
			PropertyInfo[] arrFldInfo = mType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			for (int i=0; i < arrFldInfo.Length; i++)
			{
				KeyFieldAttribute[] keys = (KeyFieldAttribute[])arrFldInfo[i].GetCustomAttributes(typeof(KeyFieldAttribute), true);
				if (keys.Length > 0)
				{
					msKeyField = keys[0].ColumnName; 
					break;
				}
			}
			if (msKeyField == "") throw new BOException("variabele 'msKeyField' is leeg na reflectie!"); 
		}

		public System.Type ChildType
		{
			get
			{
				return mType;
			}
			// geen set; ChildType zit in de constructor
		}

		public DAL_OleDb Dal
		{
			get
			{
				return mDal; 
			}
		}

		protected IList GetAs_IList(string sSQL)
		{
			int iRows = mDal.RetrieveChildObjects(sSQL, SourceType.SQL, this, mType);
			return this;
		}

		public int Add(object oItem)
		{
			if (oItem.GetType() != mType)
			{
				throw new BOException("Kan geen object van het verkeerde type toevoegen aan de collectie!");
			}
			int i =List.Add(oItem); 
			return i;
		}

		public void Insert(int i, object oItem)
		{
			if (oItem.GetType() != mType)
			{
				throw new BOException("Kan geen object van het verkeerde type invoegen in de collectie!");
			}
			List.Insert( i, oItem);
		}
		
		public void Remove(object oItem)
		{
			if (oItem.GetType() != mType)
			{
				throw new BOException("Kan geen object van het verkeerde type verwijderen uit de collectie!");
			}
			List.Remove(oItem);
		} 

//		public bool Update(object oItem)
//		{
//			if (oItem.GetType() != mType)
//			{
//				throw new BOException("Kan geen object van het verkeerde type bijwerken!");
//			}
//
//			bool bRet = this.Dal.UpdateObject(oItem);
//			return bRet;
//
//		}

//		public bool Update_Property(object oItem, string sPropertyName)
//		{
//			if (oItem.GetType() != mType)
//			{
//				throw new BOException("Kan geen object van het verkeerde type bijwerken!");
//			}
//
//			// update ONE prop of this object
//			bool bRet = this.Dal.UpdateObject_Property(oItem, sPropertyName);
//			return bRet;
//		}

		public object GetObject(string sWhere)
		{
			object instance = Activator.CreateInstance(mType, true);

			string sFld;
			object oVal;

			DataTableAttribute[] DataTables = (DataTableAttribute[])this.ChildType.GetCustomAttributes(typeof(DataTableAttribute), false);
			
			if (DataTables.Length == 0)
			{
				throw new DALException("Geen Datatabel attribuut gevonden bij type '" + this.ChildType + "'!\n" + 
					                     "Bron: 'GetObject_ByKey'");  
			}
			
			string sTable = DataTables[0].TableName;

			string sSQL = "SELECT * FROM " + sTable + " WHERE " + sWhere;

			IDataReader dr = mDal.ExecQuery_DataReader(sSQL);

			PropertyInfo[] aPropInfo = mType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			
			// itereren over de object properties
			// toekennen van waarden uit de datareader aan deze properties

			try
			{
				dr.Read();
				
				// probeer of we de waarde vd 1e kolom kunnen uitlezen
				oVal = dr.GetValue(0); 
			}
			catch
			{
				return null;
			}

			for (int i=0; i < aPropInfo.Length; i++)
			{
				BaseFieldAttribute[] fields = (BaseFieldAttribute[])aPropInfo[i].GetCustomAttributes(typeof(BaseFieldAttribute), true);

				if (fields.Length > 0)

				{

					sFld = fields[0].ColumnName;

					try
					{
						oVal = dr[sFld];
					}
					catch (Exception e)
					{
						throw new DALException("Veld [" + sFld+ "] bestaat niet in de Data reader voor object van type " +  mType.ToString() + "!" , e);
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

			dr.Close(); 
			dr.Dispose(); 

			return instance;
		}


		public int Delete(int KeyValue)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("DELETE FROM ");
			sb.Append(msTable); 
			sb.Append(" WHERE ");
			sb.Append(msKeyField);
			sb.Append(" = ");
			sb.Append(KeyValue.ToString());
			sb.Append(";"); 
			string sSQL = sb.ToString(); 
			int iRows = this.Dal.Exec_ActionQuery(sSQL);
			return iRows;
		}

		public int Delete(string KeyValue)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("DELETE FROM ");
			sb.Append(msTable); 
			sb.Append(" WHERE ");
			sb.Append(msKeyField);
			sb.Append(" = '");
			sb.Append(KeyValue);
			sb.Append("';"); 
			string sSQL = sb.ToString(); 
			int iRows = this.Dal.Exec_ActionQuery(sSQL);
			return iRows;
		}

		public bool Contains(object oItem)
		{
			return List.Contains(oItem);
		}
		
		public int IndexOf(object oItem)
		{
			return List.IndexOf(oItem);
		}
		
		public void CopyTo(object[] array, int i)
		{
			List.CopyTo(array, i);
		}

		// indexer property
		public object this[int i]
		{
			get { return List[i]; }
			set {List[i] = value; }
		}

		public object GetObject_ByKey(object KeyValue)
		{
			object[] KeyValues = new object[] {KeyValue};
			object obj = this.Dal.LoadObject(KeyValues, mType);
			return obj;
		}

		public void Sort(string sSortField)
		{
			FUNC.cObject.Sort(InnerList, sSortField);
		}
	}

	public class BOException : System.Exception
	{
		public BOException(string message):base(message)
		{
			Console.Write(message);
		}
		
		public BOException(string message, Exception innerException) : base(message, innerException)
		{
			string s="";
			s = innerException.Message;
			Console.Write(innerException.Message + "-" + innerException.Source);
		}
	}

}
