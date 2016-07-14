using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BO;
using FUNC;

namespace PO_Beoordeling
{
	/// <summary>
	/// Summary description for IFrame_server.
	/// </summary>
	public class IFrame_server : frmBase
	{
		protected System.Web.UI.WebControls.Label lblWebPage;  
		protected System.Web.UI.WebControls.Label lblSessionId;
		protected System.Web.UI.WebControls.Label lblReqType;  
		private bool mbCan_GoTo; // false with false login

		private void Page_Load(object sender, System.EventArgs e)
		{
			string sPathName = "", sWebPage_from = "";   
			try
			{
				using(DAL_OleDb oDal = new DAL_OleDb())
				{
					cURL oURL = this.URL();
					sPathName = oURL.GetValue("pathname");
					string sReqType = oURL.GetValue("requesttype");
					this.lblReqType.Text = sReqType; 

					char[] sep ="/".ToCharArray();
					string[] sSplit = sPathName.Split(sep);   
					sWebPage_from = sSplit[sSplit.GetUpperBound(0)]; 

					mbCan_GoTo = true;	

					// SessionId gelijk weer terugschrijven
					// id toekomst: hier wisselen van sessie id!
					cSession oS = new cSession();
					if (oS.Load(oDal, this.SessionId(oDal)))
					{ 
						this.lblSessionId.Text = oS.SessionId;   
					}
					
					switch (sWebPage_from)
					{
						case "behAlgemeen_Login.aspx":
							Resp_Login_Beh(oURL, oDal);
							if (this.Message != "") mbCan_GoTo = false;
							break;

						case "beoAlgemeen_Login.aspx":
							Resp_Login_TL(oURL, oDal);
							if (this.Message != "") mbCan_GoTo = false;
							break;

						case "beoWelkom_TL.aspx":
							Resp_Welkom_TL(oURL, oS, oDal); 
							break;

						case "beoBeoordelingen.aspx":
							  Resp_Beoordelingen(oURL, oS, oDal); 
							break;

						case "beoInvoer_Voorblad.aspx":
							Resp_Invoer_Voorblad(oURL, oDal, oS.BeoId); 
							break;

						case "beoInvoer_Functie.aspx":
							Resp_Invoer_Functie(oURL, oDal, oS.BeoId); 
							break;
							
						case "beoInvoer_Scores.aspx":
							Resp_Invoer_Scores(oURL, oDal, oS.BeoId ); 
							break;

						case "beoInvoer_Doelen.aspx":
							Resp_Invoer_Doelen(oURL, oDal, oS.BeoId);
							break;
					}

					bool bGoto = false;
					switch (sReqType)	
					{
						case ("goto"):
							bGoto = true;
							break;
						case "update_values_goto":
							bGoto = true;
							break;
						case "update_values":
							bGoto = false;
							break;
					}

					if (bGoto)
					{
						if (mbCan_GoTo)
						{
							this.lblWebPage.Text = oURL.GetValue("menuitemid_to");
						}
						else
						{
							this.lblWebPage.Text = sPathName; // terug bij foute login
						}
					}
				}
			}
			catch(Exception ex)
			{
				this.Message = ex.Message; 
			}
		}

		public void Resp_Login_Beh(cURL oURL, DAL_OleDb oDal)
		{
			this.Message = ""; 
			try
			{
				cPenOs colPOs = new cPenOs(oDal);
				
				string sUserName = oURL.GetValue("Gebruikersnaam");
				string sWW = oURL.GetValue("Wachtwoord");

				cPenO oPO = new cPenO();
				if (oPO.Load(oDal, sUserName, sWW))
				{
					// verwijderen oude sessie record
					string sSQL = "DELETE FROM tSession WHERE UserId = " + oPO.PenOId.ToString() + ";";
					oDal.Exec_ActionQuery(sSQL); 

					cSession oS= new cSession();
					string sSessionId = oS.MakeSessionId(oDal, oPO.PenOId);

					this.lblSessionId.Text = sSessionId; // for PostBack from client

					// bijwerken Sessie tabel
					oS.SessionId = sSessionId;
					oS.UserId = oPO.PenOId;
					oS.UserName = oPO.Naam; 
					oS.Periode = ""; // pas later bekend
					oS.Datum = System.DateTime.Now;  

					bool bRet = oS.Update(oDal); 
				}
				else
				{
					this.Message = "Foutieve gebruikersnaam of wachtwoord.\nProbeer het nog een keer.";
				}
			}
			catch(Exception e)
			{
				this.Message = e.Message;
			}
		}

		public void Resp_Login_TL(cURL oURL, DAL_OleDb oDal)
		{
			this.Message = ""; 
			try
			{
					cTLs colTLs = new cTLs(oDal);

					string sUserName = oURL.GetValue("Gebruikersnaam");
					string sWW = oURL.GetValue("Wachtwoord");

					bool bLoginTL = false;
					string sSessionId="";

					cTL oTL = new cTL(); 
					bLoginTL = oTL.Load(oDal,sUserName, sWW);  

					if (bLoginTL)
					{
						// verwijderen oude sessie record
						string sSQL = "DELETE FROM tSession WHERE UserId = " + oTL.TLMsnr.ToString() + ";";
						oDal.Exec_ActionQuery(sSQL); 

						cSession oS= new cSession();
						sSessionId = oS.MakeSessionId(oDal, oTL.TLMsnr);
						oS.SessionId = sSessionId;

						this.lblSessionId.Text = sSessionId; // for PostBack from client

						// bijwerken Sessie tabel
						oS.UserId = oTL.TLMsnr;
						oS.UserName = oTL.TLNaam; 
						oS.Periode = ""; // pas later bekend
						oS.Datum = System.DateTime.Now;  

						bool bRet = oS.Update(oDal); 
					}
					else
					{
						if (oURL.GetValue("menuitemid_to") == "Gebruiker/beoWelkom_TL.aspx")
						{
							this.Message = "Foutieve gebruikersnaam of wachtwoord.\nProbeer het nog een keer.";
						}
					}
			}
			catch(Exception e)
			{
				this.Message = e.Message;
			}
		}

		public void Resp_Welkom_TL(cURL oURL, cSession oS, DAL_OleDb oDal)
		{
			// opslaan peri
			this.Message = ""; 

			try
			{
				string sPeri = oURL.GetValue("lstPeri");
					
				oS.Periode = sPeri; 
				if (oS.Update(oDal) == false)
				{
					this.Message = "Opslaan van periode is niet gelukt!";
				}
			}
			catch(Exception e)
			{
				this.Message = e.Message; 
			}
		}

		public void Resp_Beoordelingen(cURL oURL, cSession oS, DAL_OleDb oDal)
		{
			try
			{
				// opslaan BeoId in sessie record
				this.Message = ""; 
				
				string sBeoId = oURL.GetValue("beoid");

				oS.BeoId= sBeoId;
				oS.Update(oDal);
			}
			catch(Exception e)
			{
				this.Message = e.Message; 
			}
		}

		public void Resp_Invoer_Voorblad(cURL oURL, DAL_OleDb oDal, string sBeoId)
		{
			try
			{
				this.Message = "";

				cBeo oBeo = new cBeo();
				oBeo.Load(oDal, sBeoId);

				oBeo.BeoNaam = oURL.GetValue("Persoon");
				oBeo.peri_omschr = oURL.GetValue("peri_omschr"); 
				oBeo.BeoFunc = oURL.GetValue("Functie");
				// oBeo.Afde = oURL.GetValue("Afdeling");  // read only
				oBeo.Beo1 =  oURL.GetValue("Beoordelaar1");
				oBeo.Beo2 =  oURL.GetValue("Beoordelaar2");
				oBeo.Poad =  oURL.GetValue("PenO_adviseur");

				if (oBeo.Update(oDal) == false)
				{
					this.Message = "Opslaan van voorblad is niet gelukt!";
				}
			}
			catch(Exception e)
			{
				this.Message  = e.Message; 
			}
		}

		public void Resp_Invoer_Functie(cURL oURL, DAL_OleDb oDal, string sBeoId)
		{
			try
			{
				this.Message  = ""; 

				cBeo oBeo = new cBeo();
				oBeo.Load(oDal, sBeoId);

				oBeo.BeoFunc = oURL.GetValue("Functie");
				oBeo.Prof = oURL.GetValue("lstProf");
				oBeo.Voll = oURL.GetValue("lstVoll");
				oBeo.VolT = oURL.GetValue("VolT");
				oBeo.Nive = oURL.GetValue("lstNive");
				oBeo.NivT = oURL.GetValue("NivT");
				oBeo.Bere = oURL.GetValue("Bere");

				if (oBeo.Update(oDal) == false)
				{
					this.Message = "Opslaan van functie-inhoud is niet gelukt!";
				}
			}
			catch(Exception e)
			{
				this.Message  = e.Message; 
			}
		}

		public void Resp_Invoer_Scores(cURL oURL, DAL_OleDb oDal, string sBeoId)
		{
			this.Message  = ""; 

			string sElem = oURL.GetValue("elem");
			string sAction = oURL.GetValue("action");

			cBeoElem oBE = new cBeoElem(); 
			oBE.BeoID = sBeoId; 
			oBE.Elem = sElem;

			 switch(sAction)
			{
				case "savescor":
					try
					{
						string sScor = oURL.GetValue("scor");
						oBE.Scor = sScor;
						if(oBE.Update_Property(oDal, "Scor") == false) 
						{
							this.Message  = "Opslaan van score is niet gelukt!";
						}
					}
					catch(Exception e)
					{
						this.Message  = "Bij opslaan score:\n" + e.Message; 
					}
					break;

				
				case "savetoel":
					try
					{
						string sToel = oURL.GetValue("toel");
						oBE.Toel = sToel;

						if(oBE.Update_Property(oDal, "Toel") == false)
						{
							this.Message = "Opslaan van toelichting is niet gelukt!";
						}
					}
					catch(Exception e)
					{
						this.Message  = "Bij opslaan toelichting:\n" + e.Message; 
					}
					break;
			}
		}

		public void Resp_Invoer_Doelen(cURL oURL, DAL_OleDb oDal, string sBeoId)
		{
			// 'sBeoId' uit sessie-record moet gelijk zijn aan 'mcolNV["beoid"]' 
			this.Message  = "";

			string sElem = oURL.GetValue("elem");
			string sAction = oURL.GetValue("action");

			cBeoElem oBE = new cBeoElem(); 
			oBE.BeoID = sBeoId;
			oBE.Elem = sElem;


			switch(sAction)
			{
				case "savedoel":
					try
					{
						string doel = oURL.GetValue("doel");
						oBE.Doel = doel;
						if(oBE.Update_Property(oDal, "Doel") == false) 
						{
							this.Message  = "Opslaan van doelstelling is niet gelukt!";
						}
					}
					catch(Exception e)
					{
						this.Message   = "Bij opslaan doelstelling:\n" + e.Message; 
					}
					break;
				
				case "saveafsp":
					try
					{
						string afsp = oURL.GetValue("afsp");
						oBE.Afsp  = afsp;

						if(oBE.Update_Property(oDal, "Afsp") == false)
						{
							this.Message  = "Opslaan van afspraak is niet gelukt!";
						}
					}
					catch(Exception e)
					{
						this.Message  = "Bij opslaan afspraak:\n" + e.Message; 
					}
					break;
			}
		}
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
