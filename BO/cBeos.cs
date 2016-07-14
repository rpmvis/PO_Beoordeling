using System;
using System.Collections;
using System.Data;
using System.Text; 

namespace BO
{
	/// <summary>
	/// Summary description for cBeos.
	/// </summary>
	public class cBeos: cColl_base
	{
		private IList moTeBeos=null;
		private IList moInBeos = null; 
		private IList moBevrs=null;
		private string msPeri="";
		private string msSQL_base = "";
		private string msOrderBy = "";

		public cBeos(DAL_OleDb oDal) : base(oDal, typeof(cBeo))
		{
			StringBuilder sb = new StringBuilder();
 
			sb.Append("SELECT ISNULL(w.Anaam, '') + ', ' + ISNULL(w.Aanhef + ' ', '') +");
			sb.Append(" ISNULL(w.Titel + ' ','') + ISNULL(w.Init + ' ', '') +");
			sb.Append(" ISNULL(w.Tussenv + ' ', '') AS BeoNaam,"); 
			sb.Append(" w.peri AS Peri, w.Msnr AS Msnr, b.*, a.Omschr AS Afde"); 
			sb.Append(" FROM tblBeo b INNER JOIN tWerkn w"); 
			sb.Append(" ON b.BeoID = w.BeoID"); 
			sb.Append(" LEFT JOIN tAfde a"); 
			sb.Append(" ON w.Afde = a.Afde"); 

			msSQL_base = sb.ToString(); 	

			msOrderBy = " ORDER BY w.Peri DESC, BeoNaam ASC;";
		}

		public cBeos GetAsList()
		{
			string sSQL = msSQL_base + msOrderBy;
			return (cBeos)this.GetAs_IList(sSQL);  
		}

		public cBeos GetAsList(string sPeri)
		{
			// beoordelingen per periode
			string sSQL = msSQL_base + 
										" WHERE w.Peri = '" + sPeri + "'" +
										msOrderBy;
			return (cBeos)this.GetAs_IList(sSQL);  
		}

		public cBeos GetAsList(string sPeri, int iTLMsnr)
		{
			msPeri = sPeri;

			this.Dal.ClearParameters();  
			this.Dal.AddInputParameter("@Peri", sPeri);
			this.Dal.AddInputParameter("@TLMsnr", iTLMsnr); 

			int iRows = this.Dal.RetrieveChildObjects("sp_Beos", SourceType.sp, this, typeof(cBeo));  
			return this;
		}

		public IList TeBeoordelen
		{
			get
			{
				moTeBeos = new System.Collections.ArrayList();
				foreach(cBeo oBeo in this)
				{
					if(!oBeo.Geselecteerd & !oBeo.Bevroren)
					{
						moTeBeos.Add(oBeo);
					}
				}
				return moTeBeos;
			}
		}

		public int Selecteer(string sBeoId)
		{
			string sSQL = "UPDATE tblBeo"+ 
				" SET geselecteerd = 1" +
				" WHERE BeoId = '" + sBeoId + "';";
			int iRows = this.Dal.Exec_ActionQuery(sSQL); 
			return iRows;
		}

		public int DeSelecteer(string sBeoId)
		// vb: UPDATE tblBeo SET geselecteerd = 1 WHERE BeoId = '2003_650107_133110'
		{
			string sSQL = "UPDATE tblBeo"+ 
				" SET geselecteerd = 0" +
				" WHERE BeoId = '" + sBeoId + "';";
			int iRows = this.Dal.Exec_ActionQuery(sSQL); 
			return iRows;
		}

		public int Bevries(string sBeoId)
		{
			// vb: UPDATE tblBeo SET bevroren = 1 WHERE BeoId = '2003_650107_133110'
			string sSQL = "UPDATE tblBeo"+ 
				" SET bevroren = 1" +
				" WHERE BeoId = '" + sBeoId + "';";
			int iRows = this.Dal.Exec_ActionQuery(sSQL); 
			return iRows;
		}

		public int Ontdooi(string sBeoId)
		{
			// vb: UPDATE tblBeo SET bevroren = 0 WHERE BeoId = '2003_650107_133110'
			string sSQL = "UPDATE tblBeo"+ 
				" SET bevroren = 0" +
				" WHERE BeoId = '" + sBeoId + "';";
			int iRows = this.Dal.Exec_ActionQuery(sSQL); 
			return iRows;
		}

		public IList InBeoordeling
		{
			get
			{
				moInBeos = new System.Collections.ArrayList();
				foreach(cBeo oBeo in this)
				{
					if(oBeo.Geselecteerd & !oBeo.Bevroren)
					{
						moInBeos.Add(oBeo);
					}
				}
				return moInBeos;
			}
		}

		public IList Bevroren
		{
			get
			{
				moBevrs = new System.Collections.ArrayList();
				foreach(cBeo oBeo in this)
				{
					if(oBeo.Bevroren)
					{
						moBevrs.Add(oBeo);
					}
				}
				return moBevrs;
			}
		}
	}
}
