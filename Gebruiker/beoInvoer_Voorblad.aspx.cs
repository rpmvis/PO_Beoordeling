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
	/// Summary description for Invoer_Voorblad.
	/// </summary>
	public class Voorblad : frmBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText Persoon;
		protected System.Web.UI.HtmlControls.HtmlInputText Functie;
		protected System.Web.UI.HtmlControls.HtmlInputText Afdeling;
		protected System.Web.UI.HtmlControls.HtmlInputText Beoordelaar1;
		protected System.Web.UI.HtmlControls.HtmlInputText PenO_adviseur;
		protected System.Web.UI.HtmlControls.HtmlInputText peri_omschr;
		protected System.Web.UI.HtmlControls.HtmlInputText Beoordelaar2;
			
		private void Page_Load(object sender, System.EventArgs e)
		{
			using(DAL_OleDb oDal = new DAL_OleDb())
			{
				cSession oS = new cSession();
				if (oS.Load(oDal, this.SessionId(oDal)))
				{
					GetRow(oS.BeoId, oDal);
				}
				else this.Redirect_ErrorPage(); 
			}
		}

		public string GetRow(string sBeoId, DAL_OleDb oDal)
		{
			string s = "";

			cBeo oBeo = new cBeo();
			oBeo.Load(oDal, sBeoId);  

			this.Persoon.Value = oBeo.BeoNaam; 
			this.peri_omschr.Value  = oBeo.peri_omschr; 
			this.Functie.Value = oBeo.BeoFunc;
			this.Afdeling.Value = oBeo.Afde;
			this.Beoordelaar1.Value = oBeo.Beo1;
			this.Beoordelaar2.Value = oBeo.Beo2;
			this.PenO_adviseur.Value = oBeo.Poad; 
			return s;
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
