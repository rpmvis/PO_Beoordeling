using System;
using System.Text; 

namespace BO
{
	/// <summary>
	/// Summary description for cNogTeDoen.
	/// </summary>
	public class cNogInTeVullen
	{
		string msBeoId = "";
		cBeo moBeo = null;
		string msVoorblad ="-", msFunctie="-", msScores = "-", msDoelen = "-";
		bool mbKlaar = true; // to falsify

		public cNogInTeVullen(DAL_OleDb oDal, string sBeoId)
		{
			msBeoId = sBeoId;
			moBeo = new cBeo();
			moBeo.Load(oDal, sBeoId); 
			NogTedoen();
		}

		public void NogTedoen()
		{
			StringBuilder sb;

			// voorblad
			sb = new StringBuilder("");
			if (moBeo.BeoNaam == "") sb.Append("Persoon<br>");
			
			if (moBeo.BeoFunc == "") sb.Append("Functie<br>");
			// if (moBeo.BeoAfde == "") sb.Append("Afdeling<br>");
			if (moBeo.Beo1 == "") sb.Append("1e beoordelaar<br>");
			if (sb.Length > 0)
			{
				mbKlaar = false;
				msVoorblad = sb.ToString(); 
			}

			//functie-inhoud
			sb = new StringBuilder("");
			if (moBeo.Prof == "") sb.Append("Leidinggevend<br>"); 
			if (moBeo.Voll == "") sb.Append("Volledige vervulling van de functie?<br>");  
			if (moBeo.Voll == "nee" & moBeo.VolT == "") sb.Append("Toelichting geen volledige vervulling van de functie<br>");  
			if (moBeo.Nive == "") sb.Append("Functievervulling op een ander niveau?<br>");  
			if (moBeo.Nive == "ja" & moBeo.NivT == "") sb.Append("Toelichting functievervulling op een ander niveau<br>");  
			if (sb.Length > 0)
			{
				mbKlaar = false;
				msFunctie = sb.ToString(); 
			}

			// scores/toelichtingen
			sb = new StringBuilder("");
			
			if (moBeo.BeoElems.Count == 0)
			{
				sb.Append("Alle scores");
				sb.Append("<br>"); 
			}
			else
			{
				foreach (cBeoElem oBE in moBeo.BeoElems)
				{
					if(oBE.Scor == "")
					{
						sb.Append("Score op "); 
						sb.Append(oBE.ElemDescr);
						sb.Append("<br>"); 
						mbKlaar = false;
					}
					else
					{
						if (oBE.Scor == "O")
						{
							
							if (oBE.Toel == "")
							{
								sb.Append("Toelichting op "); 
								sb.Append(oBE.ElemDescr);
								sb.Append("<br>"); 
								mbKlaar = false;
							}
						}
					}
				}
			}
			if (sb.Length > 0)
			{
				mbKlaar = false;
				msScores= sb.ToString(); 
			}

			// doelen/afspraken
			sb = new StringBuilder("");
			foreach (cBeoElem oBE in moBeo.BeoElems)
			{
				if (oBE.Scor == "O")
				{
					if (oBE.Doel == "")
					{
						sb.Append("Doel m.b.t. "); 
						sb.Append(oBE.ElemDescr);
						sb.Append("<br>"); 
						mbKlaar = false;
					}

					if (oBE.Afsp == "")
					{
						sb.Append("Afspraak m.b.t. "); 
						sb.Append(oBE.ElemDescr);
						sb.Append("<br>"); 
						mbKlaar = false;
					}
				}

			}
			if (sb.Length > 0)
			{
				mbKlaar = false;
				msDoelen= sb.ToString(); 
			}
 		}

		public string Voorblad
		{
			get
			{
				return msVoorblad;
			}
		}

		public string Functie
		{
			get
			{
				return msFunctie;
			}
		}

		public string Scores
		{
			get
			{
				return msScores;
			}
		}

		public string Doelen
		{
			get
			{
				return msDoelen;
			}
		}

		public string Klaar
		{
			get
			{
				if (mbKlaar) return "ja";
				else return "nee";
			}
		}
	}
}
