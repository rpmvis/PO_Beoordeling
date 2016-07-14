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

namespace PO_Beoordeling
{
	/// <summary>
	/// Summary description for frmTable.
	/// </summary>
	public class frmTable : frmBase
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		protected void Set_ddl(ref DataGridItem dgi
			                   , string Name_lbl
			                   , string Name_ddl)
		{
			// instellen van een drop down list in een DataGrid item
			// metode: opzoeken van waarde opgeslagen in lbl.Text in lijst van waarden v/d ComboBox
			Label lbl = (Label)dgi.FindControl(Name_lbl);
			string sForeignKey = lbl.Text; 

			// instellen van index van DropDownList
			DropDownList ddl = (DropDownList)dgi.FindControl(Name_ddl);  

			ListItem li = ddl.Items.FindByValue(sForeignKey);  
			ddl.SelectedIndex = ddl.Items.IndexOf(li); 
		}

		protected void SetInsMode(bool bIns,  System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			// instellen van invoegen modus
			((ImageButton)e.Item.FindControl("_insButton")).Visible = !bIns;
			((ImageButton)e.Item.FindControl("_ins_updateButton")).Visible = bIns;
			((ImageButton)e.Item.FindControl("_ins_cancelButton")).Visible = bIns;
			
			Control ctl = null;
			TableCell cel;

			for(int i = 1;i < e.Item.Cells.Count; i++)
			{
				cel = e.Item.Cells[i];
				if (cel.Controls.Count > 0) 
				{
					ctl = e.Item.Cells[i].Controls[1];
					if (ctl != null)
					{
						ctl.Visible = bIns;
					}
				}
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
