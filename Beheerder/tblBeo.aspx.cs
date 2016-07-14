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
	/// This control is used to display and edit content of the 'tblBeo' table.
	/// </summary>
	public class tblBeo: frmTable
	{
		protected System.Web.UI.WebControls.DataGrid _grid;
		protected System.Web.UI.WebControls.DropDownList ddlSelGridItem;
		protected System.Web.UI.WebControls.DropDownList ddlSelPeri;
		private cBeos _Beos = null;
		private cFuncs _Funcs = null;
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
			this.ddlSelPeri.SelectedIndexChanged += new System.EventHandler(this.ddlSelPeri_SelectedIndexChanged);
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
						
						ddlSelPeri.DataSource = new cPeris(oDal).GetAsList_werkn();
						ddlSelPeri.DataTextField = "Peri";
						ddlSelPeri.DataValueField = "Peri";
						ddlSelPeri.DataBind();
					
						ddlSelPeri_SelectedIndexChanged(this, null); // calls BindGrid
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

		private void ddlSelPeri_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// aanpassen combox Beoordelingen en datagrid aan geselecteerde periode 
			BindGrid(-1, false);
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
				string sPeri = this.ddlSelPeri.SelectedItem.Value;
				_Beos = new cBeos(oDal).GetAsList(sPeri); 
				_grid.DataSource = _Beos;
				_grid.EditItemIndex = editIndex;			
			
				_grid.DataBind();

				// handle drop down lists
				if(_grid.Items.Count > 0 & editIndex > 0)
				{
					_grid.SelectedIndex = editIndex;
					DataGridItem dgi = _grid.SelectedItem;
					Set_ddls(ref dgi);
				}

				if (!bSelecting)
				{
					// load combo voor selectie dg-item
					ddlSelGridItem.DataSource = _Beos;
					ddlSelGridItem.DataTextField = "BeoID_BeoNaam";
					ddlSelGridItem.DataValueField = "BeoId";
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
			DataGridItem dgi = e.Item; 
			// er voor zorgen dat de index van de DropDownList id Item template goed komt te staan als ItemDataBound event
			switch (dgi.ItemType)
			{
				case (ListItemType.Item):
					Set_ddls(ref dgi);
					break;

				case (ListItemType.AlternatingItem):
					Set_ddls(ref dgi);
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

			// het label in het 'item' item bevat de actuele waarde 
			DataGridItemEventArgs eItem1 = new DataGridItemEventArgs(e.Item); 
			BindGrid(iGridIndex , false);

			DataGridItem dgi = _grid.Items[iGridIndex]; 
			
			Set_ddls(ref dgi);
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
					string sKeyValue = tbx.Text; 
					
					cBeo row = new cBeo(); 
					switch(e.CommandName)
					{
						case "_insUpdate":
							break;
						case "_edtUpdate":
							if (!row.Load(oDal, sKeyValue)) row = null; 
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
		private void FillRow(DataGridItem dgRow, cBeo row, string sCommandName)
		{
			string sPrefix = string.Empty;
			switch (sCommandName)
			{
				case "_edtUpdate": sPrefix = "_edt"; break;
				case "_insUpdate": sPrefix = "_ins"; break;
			}

			string sValue="";
			DropDownList ddl;
			bool bValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "BeoID")).Text;
			row.BeoID = sValue;

			ddl = (DropDownList)dgRow.FindControl(sPrefix + "_ddlBeoFunc");
			sValue = ddl.SelectedItem.Value;
			row.BeoFunc = sValue;

			bValue = ((CheckBox)dgRow.FindControl(sPrefix + "Geselecteerd")).Checked;
			row.Geselecteerd = bValue;

			ddl = (DropDownList)dgRow.FindControl(sPrefix + "_ddlProf");
			sValue = ddl.SelectedItem.Value;
			row.Prof = sValue;

			bValue = ((CheckBox)dgRow.FindControl(sPrefix + "Bevroren")).Checked;
			row.Bevroren = bValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Voll")).Text;
			row.Voll = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "VolT")).Text;
			row.VolT = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Nive")).Text;
			row.Nive = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "NivT")).Text;
			row.NivT = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Bere")).Text;
			row.Bere = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Beo1")).Text;
			row.Beo1 = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Beo2")).Text;
			row.Beo2 = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Poad")).Text;
			row.Poad = sValue;
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
					_Beos = new cBeos(oDal).GetAsList();
					iRows = _Beos.Delete(sKeyValue);
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

		public cFuncs Bind_ddlBeoFunc()
		{
			if (_Funcs == null) // defensief
			{
				using (DAL_OleDb oDal = new DAL_OleDb())
				{
					return _Funcs = new cFuncs(oDal).GetAsList();
				}
			}
			return _Funcs;
		}

		public cProfs Bind_ddlProf()
		{
			if (_Profs == null) // defensief
			{
				using (DAL_OleDb oDal = new DAL_OleDb())
				{
					return _Profs = new cProfs(oDal).GetAsList();
				}
			}
			return _Profs;
		}


		private void Set_ddls(ref DataGridItem dgi)
		{
			// bijwerken van 1/meer DropDownLists in item met index <iGridIndex>
			switch (dgi.ItemType)
			{
				case (ListItemType.AlternatingItem):
					goto case ListItemType.Item;
				
				case (ListItemType.SelectedItem): 
					goto case ListItemType.Item;

				case (ListItemType.Item):

					this.Set_ddl(ref dgi, "lblBeoFunc", "ddl_BeoFunc");
					this.Set_ddl(ref dgi, "lblProf", "ddl_Prof");
					break;
				
				case (ListItemType.EditItem):
					this.Set_ddl(ref dgi, "_edt_lblBeoFunc", "_edt_ddlBeoFunc");
					this.Set_ddl(ref dgi, "_edt_lblProf", "_edt_ddlProf");

					break;
				default:
					throw new Exception("Set_ddls: unknown DataGrid Item Type!"); 
			}
		}
	}
}
