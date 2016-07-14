using System;
using System.Data; 
using System.Text;
using System.Collections; 
using FUNC;


namespace BO
{
	#region class TL
	/// <summary>
	/// Summary description for cTL.
	/// </summary>
	/// 
	[DataTable("tTL", spUPDATE="")]
	public class cTL: cObj_base
	{
		int _TLMsnr = 0;
		string _TLGebruikersNaam="";
		string _TLWachtwoord="";
		string _TLNaam = "";
		string msSQL_base = "";
		string msSQL_orderby = "";

		public cTL()
		{
			StringBuilder sb = new StringBuilder();
			// selecteer tevens Naam uit laatste periode mbv TOP 1 / LEFT JOIN constructie
			sb.Append("SELECT TOP 1 tl.TLMsnr, tl.TLGebruikersNaam, tl.TLWachtwoord, w.Anaam + ', ' + w.Aanhef + ' ' + w.Titel + ' ' + w.Init + w.Tussenv TLNaam FROM tTl tl");
			sb.Append(" LEFT JOIN tWerkn w ON tl.TLMsnr = w.Msnr"); 
			
			
			msSQL_base = sb.ToString(); 
			msSQL_orderby = "\nORDER BY w.Peri DESC, tl.TLMsnr;";
		}

		public bool Load(DAL_OleDb oDal, int iTLMsnr)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(msSQL_base); 
			sb.Append("\n"); 
			sb.Append("\nWHERE tl.TLMsnr = "); 
			sb.Append(iTLMsnr.ToString()); 
			sb.Append(msSQL_orderby);
 
			string sSQL = sb.ToString(); 
			return oDal.FillObject(sSQL, this); 
		}

		public bool Load(DAL_OleDb oDal, string sTLGebruikersNaam, string sTLWachtwoord)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(msSQL_base); 
			sb.Append("\nWHERE tl.TLGebruikersNaam = '");
			sb.Append(sTLGebruikersNaam);
			sb.Append("' AND tl.TLWachtwoord = '"); 
			sb.Append(sTLWachtwoord);
			sb.Append("'");
			sb.Append(msSQL_orderby);
 
			string sSQL = sb.ToString(); 
			return oDal.FillObject(sSQL, this); 
		}

		[KeyField("TLMsnr", 0 ,1)]
		public int TLMsnr
		{
			get { return _TLMsnr; }
			set {_TLMsnr = value;}
		}

		[BaseField("TLGebruikersnaam", 6)]
		public string TLGebruikersNaam
		{ 
			get{ return _TLGebruikersNaam; }
			set{ _TLGebruikersNaam= value; }
		}

		[BaseField("TLWachtwoord", 6)]
		public string TLWachtwoord
		{ 
			get{ return _TLWachtwoord; }
			set{ _TLWachtwoord= value; }
		}

		[ReadOnlyField("TLNaam", 50)]
		public string TLNaam
		{ 
			get{return _TLNaam;}
			set{ _TLNaam= value; }
		}

		public string TLGebruikersnaam_Naam
		{
			get
			{
				return this.TLGebruikersNaam + " - " + this.TLNaam;
			}
		}
	}
	#endregion

}
