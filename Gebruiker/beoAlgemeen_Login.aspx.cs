using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using BO;

namespace PO_Beoordeling
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Default : frmBase
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				using(DAL_OleDb oDal = new DAL_OleDb())
				{
					if (oDal == null)
					{
						string sMsg = "Er is op dit moment geen verbinding mogelijk met de database\n" +
							"en u kunt daarom niet inloggen.\n\n" +
							"Neem zonodig contact op met de technisch contactpersoon.";
						this.Message  = sMsg; 
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
