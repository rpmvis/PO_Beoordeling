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
using System.Text; 
using PO_Beoordeling;

namespace PO_Beoordeling
{

	/// <summary>
	/// Summary description for Rapport.
	/// </summary>
	public class Beoordelingsrapport: frmBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell Beoordelaar1;
		protected System.Web.UI.HtmlControls.HtmlTableCell PenOadviseur;
		protected System.Web.UI.HtmlControls.HtmlTableCell hdrPeriode;
		protected System.Web.UI.HtmlControls.HtmlTableCell Naam;
		protected System.Web.UI.HtmlControls.HtmlTableCell Periode;
		protected System.Web.UI.HtmlControls.HtmlTableCell Functie;
		protected System.Web.UI.HtmlControls.HtmlTableCell Afdeling;
		protected System.Web.UI.HtmlControls.HtmlTableCell LeidType;
		protected System.Web.UI.HtmlControls.HtmlTableCell Voll;
		protected System.Web.UI.HtmlControls.HtmlTableCell VolT;
		protected System.Web.UI.HtmlControls.HtmlTableCell Nive;
		protected System.Web.UI.HtmlControls.HtmlTableCell NivT;
		protected System.Web.UI.HtmlControls.HtmlTableCell Bere;
		protected System.Web.UI.HtmlControls.HtmlTableCell Beoordelaar2;
		protected System.Web.UI.WebControls.Label lblAfdrukdatum;

		private cReport moReport = null;
		private cBeo moBeo = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			using(DAL_OleDb oDal = new DAL_OleDb())
			{

				cSession oS = new cSession();
				if (oS.Load(oDal, this.SessionId(oDal)))
				{
					moBeo = new cBeo();
					moBeo.Load(oDal, oS.BeoId); 
					
					this.LoadVoorblad_FunctieInhoud();

					LoadBeoordelaaars();	

					this.lblAfdrukdatum.Text = "Afdrukdatum: " + System.DateTime.Now.ToShortDateString();    

					moReport = new cReport();
				}
				else this.Redirect_ErrorPage(); 
			}
		}

		public void LoadVoorblad_FunctieInhoud()
		{
			string sErrMsg = "";
	
			try
			{
				cBeo oBeo = moBeo; 

				// 1. voorblad
				this.Naam.InnerHtml  = moBeo.BeoNaam;
				this.hdrPeriode.InnerHtml  = oBeo.peri_omschr;
				this.Periode.InnerHtml  = oBeo.peri_omschr;
				this.Functie.InnerHtml  = moBeo.BeoFunc;
				this.Afdeling.InnerHtml  = moBeo.Afde;

				// 2. functie-inhoud
				this.LeidType.InnerHtml = moBeo.LeidType; 
				this.Voll.InnerHtml = moBeo.Voll;
				this.VolT.InnerHtml = moBeo.VolT; 
				this.Nive.InnerHtml = moBeo.Nive; 
				this.NivT.InnerHtml = moBeo.NivT; 
				this.Bere.InnerHtml = moBeo.Bere; 
			}
			catch(Exception except)
			{
				sErrMsg = "Fout: " + except.Message  + "\n\nBron: Laden rapport ter bespreking";
			}

			this.Message = sErrMsg; 
		}


		public void LoadBeoordelaaars()
		{
			string sErrMsg = "";

			try
			{
				Beoordelaar1.InnerHtml = moBeo.Beo1;
				if (moBeo.Beo2 != "") Beoordelaar2.InnerHtml = moBeo.Beo2;
				if (moBeo.Poad != "") this.PenOadviseur.InnerHtml = moBeo.Poad; 
			}
			catch(Exception except)
			{
				sErrMsg = "Fout: " + except.Message  + "\n\nBron: Laden Beoordelingsrapport";
			}

			this.Message  = sErrMsg;
		}

		public string Response_Scores(bool bMetIndicatoren)
		{
			string sErrMsg = "";
			string s = (moReport != null?
				          moReport.Response_Scores(bMetIndicatoren, moBeo, ref sErrMsg) :
                  "");  
			this.Message  =sErrMsg; 
			return s;
		}

		public string Response_Afspraken()
		{
			return (moReport != null? moReport.Response_Afspraken(moBeo): "");  
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
