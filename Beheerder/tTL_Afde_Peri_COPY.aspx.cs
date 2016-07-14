using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BO;
using FUNC;

namespace PO_Beoordeling
{
	/// <summary>
	/// This control is used to display and edit content of the 'tTL_Afde_Peri' table.
	/// </summary>
	public class tTL_Afde_Peri_COPY: frmTable
	{
		private cTL_Afde_Peris _TL_Afde_Peris = null;
		private DataTable _dt_ddlSelGridItem = new DataTable();
		protected System.Web.UI.WebControls.DataGrid _grid;
		protected System.Web.UI.WebControls.DropDownList ddlSelGridItem;
		protected System.Web.UI.WebControls.DropDownList ddlSelPeri;
		protected System.Web.UI.WebControls.Button btnSelect;
		

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
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			this._grid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this._grid_ItemCommand);
			this._grid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this._grid_PageIndexChanged);
			this._grid.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this._grid_CancelCommand);
			this._grid.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this._grid_EditCommand);
			this._grid.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this._grid_SortCommand);
			this._grid.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this._grid_UpdateCommand);
			this._grid.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this._grid_DeleteCommand);
			this._grid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this._grid_ItemDataBound);
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
						cSession oS = new cSession();
						if (!oS.Load(oDal, this.SessionId(oDal)))
							this.Redirect_ErrorPage();  
						
						this.BindGrid(-1, sender.ToString()); 
						if(_grid.Items.Count > 0)
						{
							_grid_ItemDataBound(_grid, new DataGridItemEventArgs(_grid.Items[0])); // doorverwijzing 
						}
					}
				}
				catch (Exception ex) 
				{
					this.ErrMsg = ex.Message; 
				}
			else
				this.ErrMsg = "";	
		}

		// Invoked when a column sort label is clicked.
		private void OnSortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			ViewState.Add("sort", e.SortExpression);
			BindGrid(-1, source.ToString());
		}
		
		// Loads data from the database and binds the UI controls.
		private void BindGrid(int editIndex, string senderName)
		{
			using (DAL_OleDb oDal = new DAL_OleDb())
			{
				_TL_Afde_Peris = new cTL_Afde_Peris(oDal); 				

				// 1) bind ddl's
				switch(senderName)
				{
					case "ddlSelPeri":
						break;
					case "ddlSelGridItem":
						break;
					default:
						try
						{
							// load Peri combo
							ddlSelPeri.DataSource = _TL_Afde_Peris.dt_ddlPeri; 
							ddlSelPeri.DataTextField = "Peri";
							ddlSelPeri.DataValueField = "Peri";
							ddlSelPeri.DataBind();

							// load Afde combo
							ddlSelGridItem.DataSource = _TL_Afde_Peris.dt_ddlAfdOmschr; 
							ddlSelGridItem.DataValueField = "Afde";
							ddlSelGridItem.DataTextField = "Afde_Omschr";
							ddlSelGridItem.DataBind();
						}
						catch(Exception ex)
						{
							this.ErrMsg = ex.Message;
						}
						break;
				}
				
			}
				
			// 2 load datagrid
			_grid.DataSource = _TL_Afde_Peris.GetAsList(); 
			_grid.EditItemIndex = editIndex;			

			_grid.DataBind();
		}

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			string sPeri = this.ddlSelPeri.SelectedItem.Value;
			string sAfde = this.ddlSelGridItem.SelectedItem.Value;

      string sWhere = "";  
			if (sPeri != "<ALLE>") sWhere += " Peri = '" + sPeri + "' AND ";
			if (sAfde != "<ALLE>") sWhere += " Afde = '" + sAfde + "' AND ";

			if (sWhere != "") sWhere = sWhere.Substring(0, sWhere.Length - " AND ".Length); 
  
			

			int iGridIndex = cControl.Get_Dg_Item_Index(ref _grid, ref this.ddlSelGridItem);
			BindGrid(iGridIndex, this.ddlSelGridItem.ID);
		}


		public DataTable Bind_ddlAfde()
		// 
		{
			if (_dt_ddlSelGridItem == null) // defensief
			{
				using (DAL_OleDb oDal = new DAL_OleDb())
				{
					// load combo voor selectie dg-item 
					string sSQL = "SELECT Afde, Afde + ' ' + Omschr AS Afde_Omschr"  +
						" FROM tAfde" + 
						" ORDER BY Afde;";

					oDal.ExecQuery_DataTable(sSQL, ref _dt_ddlSelGridItem);
				}
			}
			return _dt_ddlSelGridItem;
		}

		private void _grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			bool IsItem = false;

			// er voor zorgen dat de index van de DropDownList id Item template goed komt te staan als ItemDataBound event
			switch (e.Item.ItemType)
			{
				case (ListItemType.Item):
					IsItem = true;	
					break;
				case (ListItemType.AlternatingItem):
					IsItem = true;
					break;
				case (ListItemType.EditItem):
					// toevoegen Javasript dialoog "Wilt u dit record echt verwijderen?"
					ImageButton imgBtn = (ImageButton)e.Item.Cells[0].FindControl("_delButton");
					if (imgBtn != null)
					{
						imgBtn.Attributes.Add("onclick", "return confirm_delete();");
					}
					break;
				case (ListItemType.Footer):
					// e.Item.Enabled = false; 
					break;
				default:
					break;
			}

			if (IsItem)
			{
				Set_ddl(ref e, "lblAfde", "ddl_Afde");
			}
		}

		private void _grid_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int iGridIndex = e.Item.ItemIndex;

			// het label in het 'item' item bevat de actuele waarde 
			DataGridItemEventArgs eItem1 = new DataGridItemEventArgs(e.Item); 
			BindGrid(iGridIndex , source.ToString());

			// de ddl in het 'edititem' item bevat de dropdowlist
			DataGridItemEventArgs eItem2 = new DataGridItemEventArgs(_grid.Items[iGridIndex]); 

			this.Set_ddl(ref eItem1, ref eItem2, "lblAfde", "_edt_ddlAfde");
		}

		private void _grid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "_ins":
					SetInsMode(true, e); 
					break;
				case "_edtUpdate":
					_grid_UpdateCommand(source, e); // doorverwijzing
					break;
				case "_edtCancel":
					_grid_CancelCommand(source, e); // doorverwijzing
					break;

				case "_insUpdate":
					_grid_UpdateCommand(source, e); // doorverwijzing
					SetInsMode(false, e); 
					break;
				case "_insCancel":
					_grid_CancelCommand(source, e); // doorverwijzing
					SetInsMode(false, e); 
					break;
				case "_del":
					_grid_DeleteCommand(source, e);  // doorverwijzing
					break;
			}
		}

		private void _grid_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			ViewState.Add("sort", e.SortExpression);
			BindGrid(-1, source.ToString());
		}

		private void _grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			_grid.CurrentPageIndex = e.NewPageIndex;
			BindGrid(-1, source.ToString());
		}

		private void _grid_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			// Command name van button MOET 'Update' zijn.
			try
			{
				TextBox tbx= null;
				using(DAL_OleDb oDal = new DAL_OleDb())
				{
					tbx = (TextBox)e.Item.Cells[1].Controls[1];
					string sId = tbx.Text; 
					
					cTL_Afde_Peri row = new cTL_Afde_Peri(); 
					switch(e.CommandName)
					{
						case "_insUpdate":
							break;
						case "_edtUpdate":
							if (!row.Load(oDal, sId)) row = null; 
							break;
					}
					
					if(null != row)
					{
						FillRow(e.Item , row, e.CommandName);
						row.Update(oDal); 
					}
					BindGrid(-1, source.ToString());
				}
			}
			catch(Exception ex)
			{
				this.ErrMsg = ex.Message; 
			}
		}

		// Fills the specified row object with data from the DataGrid.
		private void FillRow(DataGridItem dgRow, cTL_Afde_Peri row, string sCommandName)
		{
			DropDownList ddl;
			string sPrefix = string.Empty;
			
			switch (sCommandName)
			{
				case "_edtUpdate": sPrefix = "_edt"; break;
				case "_insUpdate": sPrefix = "_ins"; break;
			}

			string sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "peri")).Text;
			row.peri = sValue;

			ddl = (DropDownList)dgRow.FindControl(sPrefix + "_ddlAfde");
			sValue = ddl.SelectedItem.Value;
			row.Afde = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "TLMsnr")).Text;
			row.TLMsnr = System.Convert.ToInt32(sValue);
		}

		private void _grid_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				TextBox tbx = (TextBox)e.Item.Cells[1].Controls[1];
				string sKeyValue = tbx.Text; 
				int iRows = 0;
				  
				using (DAL_OleDb oDal = new DAL_OleDb())
				{
					_TL_Afde_Peris = new cTL_Afde_Peris(oDal).GetAsList();
					iRows = _TL_Afde_Peris.Delete(sKeyValue);
				}
				if (iRows == 1)
				{
					BindGrid(-1, source.ToString());
				}
			}
			catch(Exception ex)
			{
				this.ErrMsg = ex.Message;  
			}
		}

		private void _grid_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			BindGrid(-1, source.ToString());
		}

	}
}
