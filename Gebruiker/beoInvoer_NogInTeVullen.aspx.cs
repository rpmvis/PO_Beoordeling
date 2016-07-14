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
using PO_Beoordeling;

namespace PO_Beoordeling
{
	/// <summary>
	/// Summary description for NogTeDoen.
	/// </summary>
	public class NogInTeVullen : frmBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell voorblad;
		protected System.Web.UI.HtmlControls.HtmlTableCell functie;
		protected System.Web.UI.HtmlControls.HtmlTableCell scores;
		protected System.Web.UI.HtmlControls.HtmlTableCell klaar;
		protected System.Web.UI.HtmlControls.HtmlTable tblNogInTevullen;
		protected System.Web.UI.HtmlControls.HtmlTableCell doelen;
	
		private void Page_Load(object sender, System.EventArgs e)
		{


			using(DAL_OleDb oDal = new DAL_OleDb())
			{
				cSession oS = new cSession();
				if (oS.Load(oDal, this.SessionId(oDal)))
				{
  					string sBeoId = oS.BeoId; 
						cNogInTeVullen oNitv = new cNogInTeVullen(oDal, sBeoId);
				
						this.voorblad.InnerHtml = oNitv.Voorblad;
						this.functie.InnerHtml = oNitv.Functie;
						this.scores.InnerHtml =  oNitv.Scores;
						this.doelen.InnerHtml = oNitv.Doelen;  
						this.klaar.InnerHtml = oNitv.Klaar;  
				}
				else this.Redirect_ErrorPage(); 
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
