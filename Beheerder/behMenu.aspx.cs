using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BO;

namespace PO_Beoordeling
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class behMenu : frmBase
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				using(BO.DAL_OleDb oDal = new DAL_OleDb())  
				{
					if (oDal == null)
					{
						string sMsg = "Er is op dit moment geen verbinding mogelijk met de database\n" +
							"en u kunt daarom niet inloggen.\n\n" +
							"Neem zonodig contact op met de technisch contactpersoon.";
						this.Message = sMsg; 
					}

					cSession oS = new cSession();
					if (oS.Load(oDal, this.SessionId(oDal)))
					{
					}
				}
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
