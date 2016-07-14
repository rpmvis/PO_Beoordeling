using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BO;

namespace PO_Beoordeling
{
	/// <summary>
	/// Summary description for Handleiding.
	/// </summary>
	public class CompetentieLijst : frmBase
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		public string GetLijst()
		{
			string sClassName= "DROTable_report";
			string sLijst = "";
			using(DAL_OleDb oDal = new DAL_OleDb())
			{
				cComps oComps = new cComps(oDal);
				oComps = oComps.GetAsList(); 
				sLijst = oComps.Competentielijst(sClassName); 
			}
			return sLijst;
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
