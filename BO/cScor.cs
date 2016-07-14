using System;
using System.Text; 

namespace BO
{
	/// <summary>
	/// Summary description for cScor.
	/// </summary>
	[DataTable("tblBeoScore", spUPDATE="")]
	public class cScor: cObj_base
	{
		int miBeoScoreId;
		string msBeoId;
		string msElem;
		string msScor;
		string msToel;
		string msDoel;
		string msAfsp;
		string msBeoNaam = "";

		public cScor()
		{
		}

		public cScor(string sBeoId, string sElem)
		{
			this.BeoID = sBeoId;
			this.Elem = sElem;
		}

		public bool Load(DAL_OleDb oDal, string sBeoScoreId)
		{
			StringBuilder sb = new StringBuilder();
 
			sb.Append("SELECT ISNULL(w.Anaam, '') + ', ' + ISNULL(w.Aanhef + ' ', '') +");
			sb.Append(" ISNULL(w.Titel + ' ','') + ISNULL(w.Init + ' ', '') +");
			sb.Append(" ISNULL(w.Tussenv + ' ', '') AS BeoNaam,"); 
			sb.Append(" w.peri AS Peri, w.Msnr AS Msnr, s.*"); 
			sb.Append(" FROM tblBeoScore s INNER JOIN tWerkn w"); 
			sb.Append(" ON s.BeoID = w.BeoID"); 
			sb.Append(" WHERE s.BeoScoreId = '");
			sb.Append(sBeoScoreId);
			sb.Append("';"); 
			
			string sSQL = sb.ToString(); 
			bool bRet = oDal.FillObject(sSQL, this); 
			return bRet;
		}

		[KeyReadOnlyField("BeoScoreId", 0, 1)]
		public int BeoScoreId
		{ 
			get{ return miBeoScoreId; }
			set{ miBeoScoreId = value; }
		}

		[BaseField("BeoID", 24)]
		[ForeignKeyFieldAttribute("BeoID", 6)]
		public string BeoID
		{ 
			get{ return msBeoId; }
			set{ msBeoId = value; }
		}

		[BaseField("Elem", 5)]
		[ForeignKeyFieldAttribute("Elem", 10)]
		public string Elem
		{ 
			get{ return msElem; }
			set{ msElem= value; }
		}

		[BaseField("Scor", 1)]
    [ForeignKeyFieldAttribute("Scor", 15)]  
		public string  Scor
		{ 
			get{ return msScor; }
			set{ msScor = value; }
		}

		[BaseField("Doel", 255)]
		public string Doel
		{ 
			get{ return msDoel; }
			set{ msDoel = value; }
		}

		[BaseField("Toel", 255)]
		public string Toel
		{ 
			get{ return msToel; }
			set{ msToel= value; }
		}

		[BaseField("Afsp", 255)]
		public string Afsp
		{
			get {return msAfsp;}
			set {msAfsp = value;}
		}

		[ReadOnlyField("BeoNaam", 50)]
		public string BeoNaam
		{ 
			get{ return msBeoNaam; }
			set{ msBeoNaam= value; }
		}

		// voor selectie-ddl
		public string BeoID_BeoNaam_Elem
		{
			get
			{
				return this.BeoID + " - " + this.BeoNaam + " - " + this.Elem;  
			}
		}
	}
}

