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
	/// This control is used to display and edit content of the 'tAfde' table.
	/// </summary>
	public class tAfde: frmTable
	{
	protected System.Web.UI.WebControls.DataGrid _grid;
		protected System.Web.UI.WebControls.DropDownList ddlSelGridItem;
		private DataTable _dt_ddlSelGridItem = new DataTable();
		private cAfdeen _Afdeen = null;
  
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
			this.ddlSelGridItem.SelectedIndexChanged += new System.EventHandler(this.ddlSelGridItem_SelectedIndexChanged);
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
						if (!oS.Load(oDal, this.SessionId(oDal))) this.Redirect_ErrorPage();  
						
						// bind grid
						this.BindGrid(-1, sender.ToString()); 
						if(_grid.Items.Count > 0)
						{
							_grid_ItemDataBound(_grid, new DataGridItemEventArgs(_grid.Items[0])); // doorverwijzing 
						}

					}
				}
				catch (Exception ex) 
				{
					this.Message = ex.Message; 
				}
					else
						this.Message = "";	
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
				_Afdeen = new cAfdeen(oDal);
 
				// 1) bind ddl's
				switch(senderName)
				{
					case "ddlSelGridItem":
						break;
					default:
						_dt_ddlSelGridItem = _Afdeen.dt_ddlAfdOmschr;
						if (_dt_ddlSelGridItem != null)
						{
							ddlSelGridItem.DataSource = _dt_ddlSelGridItem;
							ddlSelGridItem.DataValueField = "ID";
							ddlSelGridItem.DataTextField = "Afde_Omschr";
							ddlSelGridItem.DataBind();
						}
						break;
				}

				// 2) bind grid
				_grid.DataSource = _Afdeen.GetAsList_afde();
				_grid.EditItemIndex = editIndex;			
				_grid.DataBind();
			}
		}

		void ddlSelGridItem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int iGridIndex = cControl.Get_Dg_Item_Index(ref _grid, ref this.ddlSelGridItem);
			BindGrid(iGridIndex, this.ddlSelGridItem.ID);
		}

		private void _grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// er voor zorgen dat de index van de DropDownList id Item template goed komt te staan als ItemDataBound event
			switch (e.Item.ItemType)
			{
				case (ListItemType.Item):
					break;
				case (ListItemType.AlternatingItem):
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
		}

		private void _grid_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int iGridIndex = e.Item.ItemIndex;
			BindGrid(iGridIndex , source.ToString());
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
					
					cAfde row = new cAfde(); 
					switch(e.CommandName)
					{
						case "_insUpdate":
							break;
						case "_edtUpdate":
							tbx = (TextBox)e.Item.Cells[1].Controls[0];
							int iID = Convert.ToInt32(tbx.Text); 

							if (!row.Load(oDal, iID)) row = null; 	
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
				this.Message = ex.Message; 
			}
		}

		// Fills the specified row object with data from the DataGrid.
		private void FillRow(DataGridItem dgRow, cAfde row, string sCommandName)
		{
			string sPrefix = "";
			switch (sCommandName)
			{
				case "_edtUpdate":
					sPrefix = "_edt";
					break;
				case "_insUpdate":
					sPrefix = "_ins";
					break;
			}

			string value;
//			value = ((TextBox)dgRow.FindControl(sPrefix + "ID")).Text;
//			int iID = Convert.ToInt32(value);
//			row.ID = iID;
			
			value = ((TextBox)dgRow.FindControl(sPrefix + "Afde")).Text;
			row.Afde = value;

			value = ((TextBox)dgRow.FindControl(sPrefix + "Omschr")).Text;
			row.Omschr  = value;
		}

		private void _grid_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				TextBox tbx = (TextBox)e.Item.Cells[1].Controls[0];
				int iID = Convert.ToInt32(tbx.Text); 

				int iRows = 0;
				
				using (DAL_OleDb oDal = new DAL_OleDb())
				{
					_Afdeen = new cAfdeen(oDal).GetAsList_afde();
					iRows = _Afdeen.Delete(iID);
				}
				if (iRows == 1)
				{
					BindGrid(-1, source.ToString());
				}
			}
			catch(Exception ex)
			{
				this.Message = ex.Message;  
			}
		}

		private void _grid_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			BindGrid(-1, source.ToString());
		}

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
		}

	}
}
