using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using BO;

namespace PO_Beoordeling
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class behImport_XML_ActX_XMLDOM : frmBase
	{
		protected Label lblFile;
		protected System.Web.UI.WebControls.Label lbStatus;
		protected System.Web.UI.WebControls.TextBox txtXML;
		protected System.Web.UI.WebControls.TextBox txtFilePath;
		protected System.Web.UI.WebControls.Button btnLoad2;

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{    
			this.btnLoad2.Click += new System.EventHandler(this.btnLoad2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}

		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		private void btnLoad2_Click(object sender, System.EventArgs e)
		{
			string sMessage = ""; 
			string sFilePath = this.txtFilePath.Text; 
			string sXML = this.txtXML.Text;  
				
			try
			{
				lbStatus.Visible = true;
				cImportXML oImportXML = new BO.cImportXML();
				oImportXML.Import(sFilePath
					              , sXML 
					              , ref sMessage);
				this.Message = sMessage;

			}
			catch(Exception ex)
			{
				this.Message = ex.Message; 
			}
			finally
			{
				lbStatus.Visible = false;
			}
		}
	}
}
