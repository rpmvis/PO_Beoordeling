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
	/// Summary description for Beoordelingen.
	/// </summary>
	public class Beoordelingen: frmBase
	{
	
		private cList moList1 = new cList(); 
		private cList moList2 = new cList();
		protected System.Web.UI.WebControls.TextBox txtInvoerKlaar; 
		private cList moList3 = new cList();
		protected System.Web.UI.WebControls.ListBox ListBox0;
		protected System.Web.UI.WebControls.Button btnAdd1;
		protected System.Web.UI.WebControls.Button btnRemove1;
		protected System.Web.UI.WebControls.ListBox ListBox1;
		protected System.Web.UI.WebControls.Button btnAdd2;
		protected System.Web.UI.WebControls.ListBox ListBox2;
		protected System.Web.UI.HtmlControls.HtmlTableCell txtPeri;
		protected System.Web.UI.HtmlControls.HtmlTableCell txtNaam;
		private cBeos moBeos = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Page.IsPostBack) return;
			try
			{
				using(DAL_OleDb oDal = new DAL_OleDb())
				{
					cSession oS = new cSession();
					if (oS.Load(oDal, this.SessionId(oDal)))
					{
						Load_Controls(oDal);
					}
					else this.Redirect_ErrorPage(); 
				}
			}
			catch(Exception ex)
			{
				this.Message = ex.Message; 
			}
		}

		private void Load_Controls(DAL_OleDb oDal)
		{
			string sErrMsg="";

			cSession oS = new cSession();
			if(oS.Load(oDal, this.SessionId(oDal))) 
			{
				cTL oTL = new cTL();
				oTL.Load(oDal, oS.UserId); 
 
				Set_moBeos(oDal);
 
				this.txtPeri.InnerText = oS.Periode;   
				this.txtNaam.InnerText= oTL.TLNaam;

				this.ListBox0.DataSource = moBeos.TeBeoordelen;
				this.ListBox1.DataSource = moBeos.InBeoordeling;
				this.ListBox2.DataSource = moBeos.Bevroren; 

				this.ListBox0.DataValueField = "BeoId";
				this.ListBox1.DataValueField = "BeoId";
				this.ListBox2.DataValueField = "BeoId";

				this.ListBox0.DataTextField = "BeoNaam";
				this.ListBox1.DataTextField = "BeoNaam";
				this.ListBox2.DataTextField = "BeoNaam";

				this.ListBox0.DataBind(); 
				this.ListBox1.DataBind(); 
				this.ListBox2.DataBind(); 
		
				if (ListBox0.Items.Count > 0) ListBox0.Items[0].Selected = true; 
				if (ListBox1.Items.Count > 0) ListBox1.Items[0].Selected = true; 
				if (ListBox2.Items.Count > 0) ListBox2.Items[0].Selected = true; 

				// opslaan BeoId uit ListBox1
				if (this.ListBox1.Items.Count>0)
				{ 
					try
					{
						string sBeoId = this.ListBox1.Items[0].Value;
						oS.BeoId= sBeoId;

						oS.Update(oDal); 
					}
					catch(Exception e)
					{
						sErrMsg = "Fout: " + e.Message  + "\n\nBron: Opslaan persoon voor beoordeling";
						this.Message = sErrMsg; 
					}
				}
				ListBox1_SelectedIndexChanged(this.ListBox1, new System.EventArgs()); 
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
		private void InitializeComponent()
		{    
			this.btnAdd1.Click += new System.EventHandler(this.btnAdd1_Click);
			this.btnRemove1.Click += new System.EventHandler(this.btnRemove1_Click);
			this.ListBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
			this.btnAdd2.Click += new System.EventHandler(this.btnAdd2_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		private void btnAdd1_Click(object sender, System.EventArgs e)
		{
			string sBeoId;
			if (this.ListBox0.SelectedItem != null)
			{
				sBeoId = ListBox0.SelectedItem.Value;
				using(DAL_OleDb oDal = new DAL_OleDb())
				{
					Set_moBeos(oDal);
					if (moBeos.Selecteer(sBeoId)>0)
					Load_Controls(oDal);
				}
			}
		}

		private void btnRemove1_Click(object sender, System.EventArgs e)
		{
			string sBeoId;
			if (ListBox1.SelectedItem != null)
			{
				sBeoId = ListBox1.SelectedItem.Value;
				using(DAL_OleDb oDal = new DAL_OleDb())
				{
					Set_moBeos(oDal);
					if (moBeos.DeSelecteer(sBeoId)>0)
						Load_Controls(oDal);
				}
			}
		}

		private void btnAdd2_Click(object sender, System.EventArgs e)
		{
			string sBeoId;
			if (ListBox1.SelectedItem != null)
			{
				sBeoId = ListBox1.SelectedItem.Value; 
				using(DAL_OleDb oDal = new DAL_OleDb())
				{
					Set_moBeos(oDal);
					if (moBeos.Bevries(sBeoId)>0)
						Load_Controls(oDal);
				}
  		}
		}

		private void btnRemove2_Click(object sender, System.EventArgs e)
		{
			string sBeoId;
			if (ListBox2.SelectedItem != null)
			{
				sBeoId = ListBox2.SelectedItem.Value;
				using(DAL_OleDb oDal = new DAL_OleDb())
				{
					Set_moBeos(oDal);
					if (moBeos.Ontdooi(sBeoId)>0)
						Load_Controls(oDal);
				}
			}
		}

		private void ListBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.ListBox1.SelectedItem != null)
			{
				string sBeoId = this.ListBox1.SelectedItem.Value; 
				using(DAL_OleDb oDal = new DAL_OleDb())
				{
					cNogInTeVullen oNitv = new cNogInTeVullen(oDal, sBeoId);
					this.txtInvoerKlaar.Text = oNitv.Klaar; 
				}
			}
			else
			{
				this.txtInvoerKlaar.Text = "nee";
			}
		}

		private void Set_moBeos(DAL_OleDb oDal)
		{

			cSession oS = new cSession();
			if (oS.Load(oDal, this.SessionId(oDal)))
			{
				cTL oTL = new cTL();
				oTL.Load(oDal, oS.UserId);  

				int iTLMsnr = oS.UserId; 
				moBeos= new cBeos(oDal); 
				moBeos = moBeos.GetAsList(oS.Periode, iTLMsnr);
			}
		}
	}
}