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
	public class behImport_XML : frmBase
	{
		protected Label lblFile;
		protected System.Web.UI.WebControls.Button btnLoad2;
		protected HtmlInputFile inpFile;

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
			string sXML = "";
			string sMessage = "";

			System.Web.HttpPostedFile postedFile = (System.Web.HttpPostedFile)this.inpFile.PostedFile;
			int iFileLen = postedFile.ContentLength;
			byte[] aBytes = new byte[iFileLen];

			string sFilePath = postedFile.FileName;

 
			if  (sFilePath == "")
			{
				this.Message = "Selecteer eerst een XML bestand!";
				return;
			}

			if (!sFilePath.ToLower().EndsWith(".xml"))
			{
				this.Message = "Selecteer een XML bestand (extensie .xml)!";
				return;
			}
				
				// postedFile.InputStream
			
			try
			{
				// Initialize the stream
				System.IO.Stream strm= postedFile.InputStream;
	 
				// Read the file into the byte array.
				strm.Read(aBytes, 0, iFileLen);
	 
				// Copy the byte array into a string.
				// XML is usually written in UTF8 code
				System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
				sXML = enc.GetString(aBytes);
				
				cImportXML oImportXML = new cImportXML(); 
				oImportXML.Import(postedFile.FileName
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
			}
		}
	}
}
