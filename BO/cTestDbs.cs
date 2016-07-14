using System;
using System.Web;
using DAL; 

namespace BO
{
	/// <summary>
	/// Summary description for cTestDbsConn.
	/// </summary>
	public class cTestDbs
	{
		cDALSqlEngine mDal = null;
		private string msDbsType = "";

		public cTestDbs(string sAppPath, string sDbsType)
		{
//			string s = sAppPath.ToLower();
//			int iPos = s.IndexOf("\\beo"); 
//			if (iPos > -1)
//				msDbsType= "";
//			else
//				msDbsType = "beo";
//			msDbsType = sSubMap;
			msDbsType= sDbsType;
		}

		public cDALSqlEngine Dal
		{
			get
			{
				if (mDal == null)
				{
					mDal = new cDALSqlEngine(msDbsType); 	
				}
				return mDal;
			}
		}

		public bool HasValidConnection
		{
			get
			{
				bool bOK = (this.Dal.GetConnection() != null);
				return bOK;
			}
		}
	}
}
