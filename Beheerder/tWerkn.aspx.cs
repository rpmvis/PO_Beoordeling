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
	/// This control is used to display and edit content of the 'tWerkn' table.
	/// </summary>
	public class tWerkn: frmTable
	{
		protected System.Web.UI.WebControls.DropDownList ddlSelPeri;
		protected System.Web.UI.WebControls.DropDownList ddlSelGridItem;
		// private DataTable _dt_ddlSelGridItem = new DataTable();
		private DataTable _dt_ddlSelGridItem = null;

		private cFuncs _Funcs = null;
		protected System.Web.UI.WebControls.DataGrid _grid;
		private cWerkns _Werkns = null;

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
			this._grid.SelectedIndexChanged += new System.EventHandler(this._grid_SelectedIndexChanged);
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
						
						// set Period filter combobox
						ddlSelPeri.DataSource = new cPeris(oDal).GetAsList_werkn();
						ddlSelPeri.DataTextField = "Peri";
						ddlSelPeri.DataValueField = "Peri";
						ddlSelPeri.DataBind();
					
						// triggers BindGrid
						ddlSelPeri_SelectedIndexChanged(this, null); 

						if(_grid.Items.Count > 0)
						{
							// doorverwijzing
 						   DataGridItem dgi = _grid.Items[0];
							_grid_ItemDataBound(_grid, new DataGridItemEventArgs(dgi)); 
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
				// _Afdeen = new cAfdeen(oDal).GetAsList_omschr();
				_Funcs = new cFuncs(oDal).GetAsList();

				// alleen uit 1 periode werknemers laden
				string sPeri = this.ddlSelPeri.SelectedItem.Value;
				_Werkns = new cWerkns(oDal).GetAsList(sPeri); 

				_grid.DataSource = _Werkns;
			
				_grid.EditItemIndex = editIndex; 
				_grid.SelectedIndex = editIndex;

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
					// laad werknemers in selectie combobox
					ddlSelGridItem.DataSource = _Werkns;
					ddlSelGridItem.DataTextField = "Anaam";
					ddlSelGridItem.DataValueField = "BeoId";
					ddlSelGridItem.DataBind();
				}
			}
		}

		void ddlSelGridItem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			int iGridIndex = cControl.Get_Dg_Item_Index(ref _grid, ref ddlSelGridItem);
			BindGrid(iGridIndex, true);
			
			_grid.SelectedIndex = iGridIndex;
 		}

		public DataTable Bind_ddlAfde()
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

		public cFuncs Bind_ddlFunc()
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
		
		private void _grid_ItemDataBound(object sender
			                             , DataGridItemEventArgs e)
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

				case (ListItemType.Header):
					break;

				default:
					break;
			}
		}

		private void _grid_EditCommand(object source
			                           , System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int iGridIndex = e.Item.ItemIndex;

			BindGrid(iGridIndex, false);
		}

		private void _grid_ItemCommand(object source 
									   , System.Web.UI.WebControls.DataGridCommandEventArgs e)
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
				TextBox tb = null;
				bool bNew = false;

				using(DAL_OleDb oDal = new DAL_OleDb())
				{
					tb = (TextBox)e.Item.Cells[1].Controls[1];
					string sBeoId = tb.Text; 
					
					cWerkn row = new cWerkn(); 
					switch(e.CommandName)
					{
						case "_insUpdate":
							bNew = true;
							break;
						case "_edtUpdate":
							bNew = false;
							if (!row.Load(oDal, sBeoId)) row = null; 
							break;
					}
					
					if(null != row)
					{
						FillRow(e.Item , row, e.CommandName);
						if (row.Update(oDal))
						{
							// voor een nieuwe werknemer ook even beoordelingsrecord toevoegen
							if (bNew)
							{
								string sSQL = "SELECT BeoId FROM tblBeo" +
									            " WHERE BeoId = '" + row.BeoId + "'" ;
								if (!oDal.ObjectExists(sSQL))
								{
									cBeo oBeo = new cBeo();
									oBeo.BeoID = row.BeoId;
									oBeo.BeoFunc = row.Func;
									oBeo.Update(oDal); 
								}
							}
						}
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
		private void FillRow(DataGridItem dgRow, cWerkn row, string sCommandName)
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

			string sValue = "";

			DropDownList ddl;
			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Peri")).Text;
			row.Peri = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Msnr")).Text;
			row.Msnr = Convert.ToInt32(sValue);

			sValue = row.Peri + "_" + row.Msnr; 
			row.BeoId = sValue;
			
			ddl = (DropDownList)dgRow.FindControl(sPrefix + "_ddlAfde");
			sValue = ddl.SelectedItem.Value;
			row.Afde = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Anaam")).Text;
			row.Anaam = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Aanhef")).Text;
			row.Aanhef = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Titel")).Text;
			row.Titel = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Init")).Text;
			row.Init = sValue;

			sValue = ((TextBox)dgRow.FindControl(sPrefix + "Tussenv")).Text;
			row.Tussenv = sValue;
		
			ddl = (DropDownList)dgRow.FindControl(sPrefix + "_ddlFunc");
			sValue = ddl.SelectedItem.Value;
			row.Func = sValue;
		}

		private void ddlSelPeri_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// aanpassen combox werknemers en datagrid aan geselecteerde periode 
			BindGrid(-1, false);
		}

		private void _grid_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				TextBox tb = (TextBox)e.Item.Cells[1].Controls[1];
				string sBeoId = tb.Text; 
				int iRows = 0;
				
				using (DAL_OleDb oDal = new DAL_OleDb())
				{
					string sPeri = this.ddlSelPeri.SelectedItem.Value;
					_Werkns = new cWerkns(oDal);
					iRows = _Werkns.Delete(sBeoId);
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

		private void _grid_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Console.WriteLine(_grid.SelectedIndex); 
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

					this.Set_ddl(ref dgi, "lblAfde", "ddl_Afde");
					this.Set_ddl(ref dgi, "lblFunc", "ddl_Func");
					break;
				
				case (ListItemType.EditItem):
					this.Set_ddl(ref dgi, "_edt_lblAfde", "_edt_ddlAfde"); 
					this.Set_ddl(ref dgi, "_edt_lblFunc", "_edt_ddlFunc"); 

					break;
				default:
					throw new Exception("Set_ddls: unknown DataGrid Item Type!"); 
			}
		}
	}
}
