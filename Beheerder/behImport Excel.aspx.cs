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
	public class behImport_Excel : frmBase
	{
		protected Label lblFile;
		protected System.Web.UI.WebControls.Button btnLoad;
		protected System.Web.UI.WebControls.Label lbStatus;
		protected HtmlInputFile inpFile;

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{    
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}

		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		private void btnLoad_Click(object sender, System.EventArgs e)
		{
			System.Web.HttpPostedFile postedFile = (System.Web.HttpPostedFile)this.inpFile.PostedFile;
			string sFilePath = postedFile.FileName;
			string sMessage = "";
 
				if  (sFilePath == "")
				{
					this.Message = "Selecteer eerst een Excel bestand!";
					return;
				}

				if (!sFilePath.ToLower().EndsWith(".xls"))
				{
					this.Message = "Selecteer een Excel bestand (extensie .xls)!";
					return;
				}

				cImportExcel oImportXL = new BO.cImportExcel();
			try
			{
				lbStatus.Visible = true;
				oImportXL.Import(sFilePath, ref sMessage);
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
