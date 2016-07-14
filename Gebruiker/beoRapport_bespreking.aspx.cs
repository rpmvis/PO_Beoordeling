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
	public class Rapport_bespreking: frmBase
	{
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
		protected System.Web.UI.HtmlControls.HtmlTableCell hdrPeriode;
		protected System.Web.UI.WebControls.Label lblAfdrukdatum;

		private cBeo moBeo = null;

		// protected members are inherited by Rapport.aspx.cs
		private void Page_Load(object sender, System.EventArgs e)
		{
			using(DAL_OleDb oDal = new DAL_OleDb())
			{
				cSession oS = new cSession();
				if (oS.Load(oDal, this.SessionId(oDal)))
				{
					moBeo = new cBeo();
					moBeo.Load(oDal, oS.BeoId); 
					
					LoadVoorblad_FunctieInhoud();

					this.lblAfdrukdatum.Text = "Afdrukdatum: " + System.DateTime.Now.ToShortDateString();    
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
				this.Naam.InnerHtml  = oBeo.BeoNaam;
				this.hdrPeriode.InnerHtml  = oBeo.peri_omschr;
				this.Periode.InnerHtml  = oBeo.peri_omschr;
				this.Periode.InnerHtml  = oBeo.peri_omschr;
				this.Functie.InnerHtml  = oBeo.BeoFunc;
				this.Afdeling.InnerHtml  = oBeo.Afde;

				// 2. functie-inhoud
				this.LeidType.InnerHtml = oBeo.LeidType; 
				this.Voll.InnerHtml = oBeo.Voll;
				this.VolT.InnerHtml = oBeo.VolT; 
				this.Nive.InnerHtml = oBeo.Nive; 
				this.NivT.InnerHtml = oBeo.NivT; 
				this.Bere.InnerHtml = oBeo.Bere; 
			}
			catch(Exception except)
			{
				sErrMsg = "Fout: " + except.Message  + "\n\nBron: Laden rapport ter bespreking";
			}

			this.Message  = sErrMsg; 
		}

		public string Response_Scores(bool bMetIndicatoren)
		{
			string sErrMsg = "";
			string s = "";

			cReport oReport = new cReport();
			s = oReport.Response_Scores(bMetIndicatoren, moBeo, ref sErrMsg);
			this.Message  =sErrMsg; 
			return s;
		}

		public string Response_Afspraken()
		{
			cReport oReport = new cReport();
			return oReport.Response_Afspraken(moBeo);  
		}

		private void SetCell(string sTableName, string cellName, string Value)
		{
			Control ctlTable;

			ctlTable = this.FindCtl(this, sTableName);
			if (ctlTable !=null)
			{
				SetCell(ctlTable, cellName, Value);  // in frmBase
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
