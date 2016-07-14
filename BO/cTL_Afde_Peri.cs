using System;
using System.Text; 

namespace BO
{

	[DataTable("tTL_Afde_Peri", spUPDATE="")]
	public class cTL_Afde_Peri: cObj_base
	{
		private int miId;
		private string msperi;
		private string msAfde;
		private int miTLMsnr;
		private string _TLNaam = "";
		
		public cTL_Afde_Peri()
		{
		}

		public bool Load(DAL_OleDb oDal, string sId)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("SELECT ISNULL(w.Anaam, '') + ', ' + ISNULL(w.Aanhef + ' ', '') +");
			sb.Append(" ISNULL(w.Titel + ' ','') + ISNULL(w.Init + ' ', '') +");
			sb.Append(" ISNULL(w.Tussenv + ' ', '') AS TLNaam,"); 
			sb.Append(" w.peri AS Peri, w.Msnr AS Msnr, tap.*"); 
			sb.Append(" FROM tTL_Afde_Peri tap LEFT JOIN tWerkn w"); 
			sb.Append(" ON tap.TLMsnr = w.Msnr"); 
			sb.Append(" WHERE tap.Id = ");
			sb.Append(sId);
			sb.Append(";"); 
			
			string sSQL = sb.ToString(); 
			return oDal.FillObject(sSQL, this);
		}

		public bool Load(DAL_OleDb oDal, string sAfde, string sPeri)
		{
			string sSQL = "SELECT *" +
										" FROM tTL_Afde_Peri" +
										" WHERE Afde = '" + sAfde.Replace("'", "''")  + "'" +
										" AND peri = '" + sPeri + "'";
			return oDal.FillObject(sSQL, this); 
		}


		[KeyReadOnlyField("Id", 0, 1)]
		public int Id
		{
			get{ return miId; }
			set{ miId = value; }
		}

		[BaseField("peri", 10)]
		public string peri
		{
			get{ return msperi; }
			set{ msperi = value; }
		}

		[BaseField("Afde", 10)]
		public string Afde
		{
			get{ return msAfde; }
			set{ msAfde = value; }
		}

		[BaseField("TLMsnr", 0)]
		public int TLMsnr
		{
			get{ return miTLMsnr; }
			set{ miTLMsnr = value; }
		}

		public string TLMsnr_Afde
		{
			get
			{
				return this.TLMsnr + " - " + this.Afde;  
			}
		}

		[ReadOnlyField("TLNaam", 50)]
		public string TLNaam
		{ 
			get{return _TLNaam;}
			set{ _TLNaam= value; }
		}

		public string TLNaam_Afde
		{
			get
			{
				return this.TLNaam + " - " + this.Afde;  
			}
		}

	}
}