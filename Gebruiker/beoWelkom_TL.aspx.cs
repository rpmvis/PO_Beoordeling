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

namespace PO_Beoordeling
{
		/// <summary>
		/// Summary description for Welkom.
		/// </summary>
		public class frmWelkom: frmBase
		{
			protected System.Web.UI.WebControls.ListBox lstPeri;

			private void Page_Load(object sender, System.EventArgs e)
			{
				if (this.IsPostBack) return;

				using(DAL_OleDb oDal = new DAL_OleDb())
				{
					cSession oS = new cSession();
					if (oS.Load(oDal, this.SessionId(oDal)))
					{
						cPeris oPeris = new cPeris(oDal);
						oPeris = oPeris.GetAsList_werkn(); 

						this.lstPeri.DataSource = oPeris; 
						this.lstPeri.DataTextField = "Peri";
						this.lstPeri.DataValueField = "Peri";
						this.lstPeri.DataBind(); 
						if (lstPeri.Items.Count > 0) lstPeri.Items[0].Selected = true; 
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
				this.ID = "frmWelkom";
				this.Load += new System.EventHandler(this.Page_Load);

			}
		#endregion
		}
	}
