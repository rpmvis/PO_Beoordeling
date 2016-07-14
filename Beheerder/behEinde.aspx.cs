using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BO;
using System.Text; 

namespace PO_Beoordeling
{
	/// <summary>
	/// Summary description for Welkom.
	/// </summary>
	public class Einde : frmBase
	{
		protected System.Web.UI.WebControls.Button bntClose;
		private string msEinde = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (this.IsPostBack) return;

			using(DAL_OleDb oDal = new DAL_OleDb())
			{
				cSession oS = new cSession();
				if (oS.Load(oDal, this.SessionId(oDal)))
				{
					SetEinde(oS.UserName);
				}
				else this.Redirect_ErrorPage(); 
			}			
		}

		private void SetEinde(string sUserName)
		{
			StringBuilder sb = new StringBuilder(); 
			sb.Append("<br><br>"); 

			sb.Append(sUserName);
			sb.Append("<br><br>");

			sb.Append("Als u op onderstaande knop drukt, komt u op de website van DRO Amsterdam."); 
			msEinde = sb.ToString();
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

		public string GetEinde()
		{
			return msEinde;
		}
	}
}
