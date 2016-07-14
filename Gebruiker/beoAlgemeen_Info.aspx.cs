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

namespace PO_Beoordeling
{
	/// <summary>
	/// Summary description for Info.
	/// </summary>
	public class Info : frmBase
	{

		private cSetting moSett;
		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		public string GetProgrammaNaam()
		{
			return this.Setting.ProgrammaNaam; 
		}

		public string GetDatumLaatsteOnderhoud()
		{
			DateTime d = this.Setting.DatumLaatsteOnderhoud;
			string s = FUNC.cDate.Date_UI(d); 
			return s;
		}

		private cSetting Setting
		{
			get
			{
				if (moSett == null) moSett = new cSetting();
				return moSett; 
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
