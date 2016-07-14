using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BO;
using FUNC;
using System.Text; 

namespace PO_Beoordeling
{
	/// <summary>
	/// This control is used to display and edit content of the 'tWerkn' table.
	/// </summary>
	public class behRapport_onvoldoendes: frmTable
	{
		protected System.Web.UI.WebControls.Button btnMakeReport;
		protected System.Web.UI.WebControls.DropDownList ddlSelPeri;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divReport;

		private string msPeri = "";

		private string msReport = "";

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
			this.ddlSelPeri.SelectedIndexChanged += new System.EventHandler(this.ddlSelPeri_SelectedIndexChanged);
			this.btnMakeReport.Click += new System.EventHandler(this.btnMakeReport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
				try
				{
					using (DAL_OleDb oDal = new DAL_OleDb())
					{
//						cSession oS = new cSession();
//						if (!oS.Load(oDal, this.SessionId(oDal)))
//							this.Redirect_ErrorPage();  

						ddlSelPeri.DataSource = new cPeris(oDal).GetAsList_werkn();
						ddlSelPeri.DataTextField = "Peri";
						ddlSelPeri.DataValueField = "Peri";
						ddlSelPeri.DataBind();
					
						// ddlSelPeri_SelectedIndexChanged(this, null); // triggers BindGrid
					}
				}
				catch (Exception ex) 
				{
					this.Message = ex.Message; 
				}
					else
						this.Message = "";	
		}

		private void ddlSelPeri_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// aanpassen rapport aan geselecteerde periode 
		}

		private void btnMakeReport_Click(object sender, System.EventArgs e)
		{
			msPeri = ddlSelPeri.SelectedIndex == -1 ? "" : ddlSelPeri.SelectedItem.Value;
			MakeReport();
		}

		private void MakeReport()
		{
			string sMsg = "";
			StringBuilder sb = new StringBuilder(""); 
			if (msPeri == "")
			{
				sMsg = "Selecteer een jaar!";
				this.Message = sMsg;
				return;
			}

			cBeos oBeos = null;

			using (DAL_OleDb oDal = new DAL_OleDb())
			{
				oBeos = new cBeos(oDal);
				oBeos = oBeos.GetAsList(msPeri);
				
				foreach(cBeo oBeo in oBeos)
				{
					oBeo.Dal = oDal;
					if (oBeo.BeoElems_onvold_scor.Count > 0)
					{
						sb.Append("<tr><td colspan=2><strong>");
						sb.Append(oBeo.BeoNaam); 
						sb.Append("</strong></td></tr>");
						sb.Append("<tr style='TEXT-DECORATION: underline'>"); 
						sb.Append("<td>Beoordelingselement</td>");
						sb.Append("<td>Competentie</td>");
						sb.Append("</tr>"); 

						foreach (cBeoElem oBE in oBeo.BeoElems_onvold_scor) 
						{
							sb.Append("<tr>"); 
							sb.Append("<td>"); 
							sb.Append(oBE.ElemDescr);
							sb.Append("</td>");
							sb.Append("<td>"); 
							sb.Append(oBE.CompOmschr);
							sb.Append("</td>");
							sb.Append("</tr>"); 
						}
					} // if
				} // foreach oBeo

				msReport = "<table class = 'DROTable' cellpadding='2' borderColor='gray' border='0'>";
				if (sb.ToString() != "")
				{
					msReport  += sb.ToString() + "</table>";
				}
				else
				{
					msReport += "<tr><td>" + "Er zijn geen personen gevonden met een onvoldoende beoordeling in periode " + msPeri + "." + "</tr></td>" +
						          "</table>";
				}

				this.divReport.InnerHtml = msReport;
			} // using
		} // void

		public string GetReport()
		{
			return msReport;
		}
	} // class
} // namespace
