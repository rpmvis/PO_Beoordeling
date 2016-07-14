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
using System.Text;

namespace PO_Beoordeling
{
	/// <summary>
	/// Summary description for Handleiding.
	/// </summary>
	public class Contact : frmBase
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
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

		public string Reponse_Functioneel_contactpersoon()
		{
			string s;

			using(DAL_OleDb oDal = new DAL_OleDb())
			{

				BO.cPenO oPenO = new cPenO();
				if (oPenO.Load_HoofdPO(oDal))
					s = persoon_to_html(oPenO, "Functioneel");
				else s = "";
			}
			return s;
		}

		public string Reponse_Technisch_contactpersoon()
		{
			string s;

			using(DAL_OleDb oDal = new DAL_OleDb())
			{

				BO.cPenO oPenO = new cPenO();
				if (oPenO.Load_TechContactPO(oDal))
					s = persoon_to_html(oPenO, "Technisch");
				else s = "";
			}
			return s;
		}

		private string persoon_to_html(cPenO oPenO, string sTypeContactPersoon)
		{
			/* purpose: build string like 
			<strong>Functioneel contactpersoon</strong>
			<br>
			Dhr. J. Wijdeveld
			<br>
			Tel. 020 - 552 7720
			<br>
			Email <A href="mailto:wvd@dro.amsterdam.nl">wvd@dro.amsterdam.nl</A>			*/
			StringBuilder sb = new StringBuilder();
			sb.Append("<strong>" + sTypeContactPersoon + " contactpersoon</strong>");
			sb.Append("<br>");
			sb.Append(oPenO.Naam);
			sb.Append("<br>");
			sb.Append(oPenO.Func);
			sb.Append("<br>");
			sb.Append("Tel. ");
			sb.Append(oPenO.Telefoon);
			sb.Append("<br>");
			sb.Append("Email <A href=\"mailto:");
			sb.Append(oPenO.Emai);			
			sb.Append("\">");
			sb.Append(oPenO.Emai);
			sb.Append("</A>"); 
			
			return sb.ToString(); 
		}


	}
}
