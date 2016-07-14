using System;
using System.Data; 
using FUNC;

namespace BO
{
	/// <summary>
	/// Summary description for cFld.
	/// </summary>
	public class cFld
	{

		private string msName;
		private DbType mDBType;
		private int miSize;
		private object moValue;

		public cFld()
		{
		}

		public string Name
		{
			get
			{
				if (msName != null)
          return "[" + msName + "]";
				else
					return null;
			}
			set{msName = value;}
		}

		public DbType DBType
		{
			get{return mDBType;}
			set{mDBType = value;}
		}

		public int Size
		{
			get{return miSize;}
			set{miSize = value;}
		}

		public object Value
		{
			get{return moValue;}
			set{moValue = value;}
		}

		public string SQLvalue
		{
			get
			{
				string sVal ="";

				if (moValue  == null)
				{
					if (mDBType == DbType.String)
						sVal = "''";
					else
						sVal = "null";
				}
				else
				{
					sVal = moValue.ToString();

					if (mDBType== DbType.String)
					{
						sVal = sVal.Replace("'", "''"); // oplossen qoute probleem 
						sVal = "'" + sVal + "'";	
						if (sVal.Length > 0)
						{
							if (miSize>0)
								// bv "LEFT('PersCode', 6)"
								sVal = "LEFT(" + sVal + ", " + miSize.ToString() + ")" ;
						}
					}
					else
					{
						if (mDBType== DbType.DateTime || mDBType== DbType.Date)
						{
							DateTime dVal = (DateTime)moValue;
							if (dVal.Equals(new System.DateTime(1,1,1)) ) 
								sVal = "null";
							else
								// sVal = "'" + cDate.SQLdate(dVal) + "'"; 
								sVal = cDate.SQLdate(dVal);
						}
					}
				}
				return sVal;
			}
		}
	}
}
