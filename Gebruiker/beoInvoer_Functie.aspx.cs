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
	/// Summary description for Invoer_functie.
	/// </summary>
	public class Invoer_Functie : frmBase
	{
		protected System.Web.UI.WebControls.RadioButtonList lstProf;
		protected System.Web.UI.WebControls.RadioButtonList lstVoll;
		protected System.Web.UI.WebControls.RadioButtonList lstNive;
		protected System.Web.UI.HtmlControls.HtmlTextArea VolT;
		protected System.Web.UI.HtmlControls.HtmlTextArea NivT;
		protected System.Web.UI.HtmlControls.HtmlTextArea Bere;
		protected System.Web.UI.HtmlControls.HtmlInputText Functie;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Page.IsPostBack) return;
			using(DAL_OleDb oDal = new DAL_OleDb())
			{
				cSession oS = new cSession();
				if (oS.Load(oDal, this.SessionId(oDal)))
				{
					GetRow(oS.BeoId, oDal);

					// add client side javascript
					this.lstVoll.Attributes.Add("onclick", "enable_VolT(GetRadioValue('lstVoll_'));");
					this.lstNive.Attributes.Add("onclick", "enable_NivT(GetRadioValue('lstNive_'));");
				}
				else this.Redirect_ErrorPage(); 
			}
		}

		public void GetRow(string sBeoId, DAL_OleDb oDal)
		{
			string sMsg = "";
					cBeo oBeo = new cBeo();
					oBeo.Load(oDal, sBeoId);   

					this.Functie.Value = oBeo.BeoFunc;
					switch (oBeo.Prof.ToLower())
					{
						case "nietleid":
							this.lstProf.SelectedIndex = 0; 
							break;
						case "funcleid":
							this.lstProf.SelectedIndex = 1; 
							break;
						case "hierfuncleid":
							this.lstProf.SelectedIndex = 2; 
							break;
					}

					switch (oBeo.Voll.ToLower())
					{
						case "ja":
							this.lstVoll.SelectedIndex = 0; 
							break;
						case "nee":
							this.lstVoll.SelectedIndex = 1; 
							break;
					}
					this.VolT.Value = oBeo.VolT; 

					switch (oBeo.Nive.ToLower())
					{
						case "ja":
							this.lstNive.SelectedIndex = 0; 
							break;
						case "nee":
							this.lstNive.SelectedIndex = 1; 
							break;
					}
					this.NivT.Value = oBeo.NivT; 
					this.Bere.Value = oBeo.Bere;
			this.Message  = sMsg; 
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
