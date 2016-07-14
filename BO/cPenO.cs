using System;
using System.Text; 

namespace BO
{
	/// <summary>
	/// Summary description for cPenO.
	/// </summary>
	[DataTable("tPenO", spUPDATE="")]
	public class cPenO: cObj_base
	{
		int miPenOid = 0;
		string msNaam = "";
		string msFunc = "";
		string msTelefoon = "";
		bool mbIsHoofd = false;
		bool mbIsTechContact = false;
		string msEmai = "";
		string _BehGebruikersNaam = "";
		string _BehWachtwoord = "";

		// constr 1		
		public cPenO()
		{
		}

		[KeyReadOnlyField("PenOId", 0 , 1)]
		public int PenOId
		{ 
			get{ return miPenOid; }
			set{ miPenOid = value; }
		}

		[BaseField("Naam",50)]
		public string Naam
		{ 
			get{ return msNaam; }
			set{ msNaam = value; }
		}

		public bool Load(DAL_OleDb oDal, string sPenOId)
		{
			string sSQL = "SELECT *" +
				" FROM tPenO" +
				" WHERE PenOId = " + sPenOId;
		
			return oDal.FillObject(sSQL, this); 
		}

		public bool Load(DAL_OleDb oDal, string sUserName, string sWW)
		{
			string sWhere = "BehGebruikersNaam = '" + sUserName + "' AND BehWachtwoord = '" + sWW + "'";
			
			string sSQL = "SELECT *" +
				" FROM tPenO" +
				" WHERE " + sWhere;
		
			return oDal.FillObject(sSQL, this); 
		}

		public bool Load_HoofdPO(DAL_OleDb oDal)
		{
			string sSQL = "SELECT *" +
				" FROM tPenO" +
				" WHERE IsHoofd = 1;";

			return oDal.FillObject(sSQL, this); 
		}

		public bool Load_TechContactPO(DAL_OleDb oDal)
		{
			string sSQL = "SELECT *" +
				" FROM tPenO" +
				" WHERE IsTechContact = 1;";

			return oDal.FillObject(sSQL, this); 
		}

		[BaseField("Func", 50)]
		public string Func
		{ 
			get{ return msFunc; }
			set{ msFunc= value; }
		}

		[BaseField("Telefoon", 20)]
		public string Telefoon
		{ 
			get{ return msTelefoon; }
			set{ msTelefoon= value; }
		}

		[BaseField("Emai", 40)]
		public string Emai
		{ 
			get{ return msEmai; }
			set{ msEmai= value; }
		}

		[BaseField("IsHoofd",0)]
		public bool IsHoofd
		{
			get{ return mbIsHoofd; }
			set{ mbIsHoofd= value; }
		}

		[BaseField("IsTechContact",0)]
		public bool IsTechContact
		{
			get{ return mbIsTechContact; }
			set{ mbIsTechContact= value; }
		}

		[BaseField("BehGebruikersNaam", 10)]
		public string BehGebruikersNaam
		{ 
			get{ return _BehGebruikersNaam; }
			set{ _BehGebruikersNaam= value; }
		}

		[BaseField("BehWachtwoord", 10)]
		public string BehWachtwoord
		{ 
			get{ return _BehWachtwoord; }
			set{ _BehWachtwoord= value; }
		}

		public string Einde_beheerder()
		{
			StringBuilder sText= new StringBuilder();
			sText.Append("Einde beheer 360° feedback door "); 
			sText.Append("<br><br>"); 
			sText.Append(this.Naam); 
			sText.Append("<br>"); 
			sText.Append(this.Func);
			sText.Append("<br>Dienst Ruimtelijke Ordening Amsterdam<br><br>");
			sText.Append("<br><br>"); 
			return sText.ToString(); 
		}
	}
}




