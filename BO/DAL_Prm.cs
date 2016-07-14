using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace BO
{
	public class DALprm
	{
		string msName = "";
		object moValue;
		int miSize = 0;
		// DAL parameter is default een Input parameter
		ParameterDirection mPrmDirection = ParameterDirection.Input;
		DbType type = DbType.String;
		
		
		public DALprm(string pName, object pValue)
		{
			msName  = pName;
			moValue = pValue;
		}
		
		public DALprm(string pName, DbType pType, object pValue)
		{
			msName  = pName;
			moValue = pValue;
			type  = pType;
		}
		
		public DALprm(string pName, DbType pType, ParameterDirection pDirection, object pValue)
		{
			msName      = pName;
			moValue     = pValue;
			type      = pType;
			mPrmDirection = pDirection;
			
		}
		
		public DALprm(string pName, DbType pType, int pSize, object pValue)
		{
			msName  = pName;
			moValue = pValue;
			type  = pType;
			miSize  = pSize;
		}
		
		public DALprm(string pName, DbType pType, int pSize, ParameterDirection pDirection, object pValue)
		{
			msName      = pName;
			moValue     = pValue;
			type      = pType;
			miSize      = pSize;
			mPrmDirection = pDirection;
		}
		
		public string Name
		{
			get { return msName;  }
			set { msName = value; }
		}
		
		public object Value
		{
			get { return this.moValue;  }
			set { this.moValue = value; }
		}
		
		public int Size
		{
			get { return miSize;  }
			set { miSize = value; }
		}		
		
		public ParameterDirection Direction
		{
			get { return mPrmDirection;  }
			set { mPrmDirection = value; }
		}	
	
		public DbType Type
		{
			get { return type;  }
			set { type = value; }
		}	
		
	}





}