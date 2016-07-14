using System;
using System.Collections;
using System.Web; // HttpUtility
using System.Text; 
using System.Web.UI; // UI.Page  

namespace BO
{
	
	[DataTable("tSession", spUPDATE="")]
	public class cSession: cObj_base
	{
		int miUserId=0;
		string msUserName="", msSessionId=null, msPeri="", msBeoId="";
		string msMenu_horizontal = "", msMenu_vertical = "";
		System.DateTime mdDatum; 
		private const int LEN_SESSION_ID = 16;

		public cSession()
		{
		}

		public bool Load(DAL_OleDb oDal, string sSessId)
		{
			string sSQL = "SELECT *" +
				" FROM tSession" +
				" WHERE SessionId = '" + sSessId + "'";
			bool bRet = oDal.FillObject(sSQL, this); 
			return bRet;
		}


		[KeyField("SessionId", 128, 1)]
		public string SessionId
		{ 
			get{return msSessionId; }
			set{ msSessionId = value; }
		}

		[BaseField("UserId", 0)]
		public int UserId
		{ 
			get{ return miUserId; }
			set{miUserId=value;}
		}

		[BaseField("UserName", 50)]
		public string UserName
		{ 
			get{ return msUserName; }
			set{msUserName=value;}
		}

		[BaseField("Datum", 0)]
		public DateTime Datum
		{
			get{return mdDatum;}
			set{mdDatum = (value);}
		}

		[BaseField("periode", 10)]
		public string Periode
		{ 
			get{ return msPeri; }
			set{ msPeri= value; }
		}

		[BaseField("BeoId", 20)]
		public string BeoId
		{ 
			get{ return msBeoId; }
			set{ msBeoId= value; }
		}



		[BaseField("Menu_horizontal", 8000)]
		public string Menu_horizontal
		{ 
			get{ return msMenu_horizontal; }
			set{ msMenu_horizontal= value; }
		}

		[BaseField("Menu_vertical", 8000)]
		public string Menu_vertical
		{ 
			get{ return msMenu_vertical; }
			set{ msMenu_vertical= value; }
		}

		//		[BaseField("LastWebPage", 50)]
		//		public string LastWebPage
		//		{ 
		//			get{ return msLastWebPage; }
		//			set{ msLastWebPage= value; }
		//		}

		//		public bool Save_LastWebPage(DAL_OleDb oDal, string sLastWebPage)
		//		{
		//			this.LastWebPage = sLastWebPage;
		//			bool bRet = this.Update(oDal);
		//			return bRet;
		//		}
		//


		public string MakeSessionId(DAL_OleDb oDal, int iUserId)
		{
			StringBuilder sb = new StringBuilder(); 
			sb.Append("delete from tSession");
			sb.Append(" where UserId = ");
			sb.Append(iUserId);
			sb.Append(";");
			
			string sSQL = sb.ToString(); 

			oDal.Exec_ActionQuery(sSQL);

			cPW oPW = new cPW(); 

			string sNewId = "";
			int j;
			// 10 pogingen om een wachtwoord in te stellen
			for (j= 0; j < 10; j++)
			{
				sNewId = oPW.GeneratePW(LEN_SESSION_ID);
					
				if (!this.SessionId_exists(oDal, sNewId))
				{
					break;
				}
			} // for
			
			return sNewId;
		}

		public new bool Update(DAL_OleDb oDal)
		{
			bool bRet = false;
			if (this.SessionId_IsValid()) 
			{
				bRet = oDal.UpdateObject(this);
			}
			return bRet;
		}

		public bool SessionId_exists(DAL_OleDb oDal, string sId)
		{
			cSession oCheck = new cSession();
			bool bRet = oCheck.Load(oDal, sId);  
			return bRet;
		}

		public bool SessionId_IsValid()
		{
			return this.SessionId.Length == LEN_SESSION_ID;
		}

	}
}
