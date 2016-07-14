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
using System.Data.SqlClient;
  

namespace PO_Beoordeling
{
	/// <summary>
	/// Summary description for Test.
	/// </summary>
	public class Test2 : frmBase
	{
		protected System.Web.UI.WebControls.Button btnTest;
		protected System.Web.UI.WebControls.Button btnTest2;
	
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
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			this.btnTest2.Click += new System.EventHandler(this.btnTest2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnTest_Click(object sender, System.EventArgs e)
		{
			DataSet ds = new DataSet(); 
			DataTable dt = new DataTable();
			string sSQL = "SELECT * FROM tWerkn;";

			try
			{
				using (DAL_OleDb oDal = new DAL_OleDb())
				{
					oDal.ExecQuery_DataTable(sSQL, ref dt); 
					ds.Tables.Add(dt);
				
					ds.WriteXml("c:\\test.xml");  
				
					this.Message = "test klaar";
				}
			}
			catch(Exception ec)
			{
				this.Message =  ec.Message; 
			}
			
		}

		private void btnTest2_Click(object sender, System.EventArgs e)
		{
			DataSet ds = new DataSet(); 
			// DataTable dt = null;

			ds.ReadXml("c:\\test.xml");

			this.Message = "test2 klaar";
		}
	}
}
