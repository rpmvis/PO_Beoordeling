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
	/// This control is used to display and edit content of the 'tProf' table.
	/// </summary>
	public class tProf: frmTable
	{
		protected System.Web.UI.WebControls.DataGrid _grid;
		protected System.Web.UI.WebControls.DropDownList ddlSelGridItem;
		private cProfs _Profs = null;

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
						if (!oS.Load(oDal, this.SessionId(oDal)))
							this.Redirect_ErrorPage();  
						
						this.BindGrid(-1, false); 
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
			BindGrid(-1, false);
		}
		
		// Loads data from the database and binds the UI controls.
		private void BindGrid(int editIndex, bool bSelecting)
		{
			using (DAL_OleDb oDal = new DAL_OleDb())
			{
				_Profs = new cProfs(oDal).GetAsList(); 
				_grid.DataSource = _Profs;
				_grid.EditItemIndex = editIndex;			
			
				_grid.DataBind();

				if (!bSelecting)
				{
					// load combo voor selectie dg-item
					ddlSelGridItem.DataSource = _Profs;
					ddlSelGridItem.DataTextField = "Prof";
					ddlSelGridItem.DataValueField = "Prof";
					ddlSelGridItem.DataBind();
				}
			}
		}

		void ddlSelGridItem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int iGridIndex = cControl.Get_Dg_Item_Index(ref _grid, ref ddlSelGridItem);
			BindGrid(iGridIndex, true);
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
			BindGrid(iGridIndex , false);
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
			BindGrid(-1, false);
		}

		private void _grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			_grid.CurrentPageIndex = e.NewPageIndex;
			BindGrid(-1, false);
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
					string sProf = tbx.Text; 
					
					cProf row = new cProf(); 
					switch(e.CommandName)
					{
						case "_insUpdate":
							break;
						case "_edtUpdate":
							if (!row.Load(oDal, sProf)) row = null; 
							break;
					}
					
					if(null != row)
					{
						FillRow(e.Item , row, e.CommandName);
						row.Update(oDal); 
					}
					BindGrid(-1, false);
				}
			}
			catch(Exception ex)
			{
				this.Message = ex.Message; 
			}
		}

		// Fills the specified row object with data from the DataGrid.
		private void FillRow(DataGridItem dgRow, cProf row, string sCommandName)
		{
			string sPrefix = string.Empty;
			switch (sCommandName)
			{
				case "_edtUpdate": sPrefix = "_edt"; break;
				case "_insUpdate": sPrefix = "_ins"; break;
			}

			string sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Prof")).Text;
			row.Prof = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Omschr")).Text;
			row.Omschr = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Opm")).Text;
			row.Opm = sValue;
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
					_Profs = new cProfs(oDal).GetAsList();
					iRows = _Profs.Delete(sKeyValue);
				}
				if (iRows == 1)
				{
					BindGrid(-1, false);
				}
			}
			catch(Exception ex)
			{
				this.Message = ex.Message;  
			}
		}

		private void _grid_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			BindGrid(-1, false);
		}
	}
}
