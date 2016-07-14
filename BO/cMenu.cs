using System;
using System.Text; 

namespace BO
{
	/// <summary>
	/// Summary description for cMenu.
	/// </summary>
	public class cMenu: cObj_base
	{
		private cMenu_MenuItems mcolMenu_MenuItems = null;
		private string msMenuId = "";
		private bool mbIsHorizontal = false;
		private int miMenuLevel = 0;

		public cMenu()
		{
		}

		public bool Load(DAL_OleDb oDal, string sMenuId)
		{
			bool bRet = false;

			// msMenuId = sMenuId;
			// query over de 3 menu tabellen (n op m relatie)
			string sSQL = "SELECT m.MenuId, m.IsHorizontal, m.MenuLevel" +
				" FROM tMenu m" +
				" WHERE (m.MenuId = '" + sMenuId + "')";
			
			bRet = oDal.FillObject(sSQL, this); 

			if (bRet)
			{
				mcolMenu_MenuItems = new cMenu_MenuItems(oDal);
				mcolMenu_MenuItems.GetAsList(sMenuId);
			}
			return bRet;
		}

		public cMenu_MenuItems MenuItems
		{
			get
			{
				if (mcolMenu_MenuItems == null)
				{
				}
				return mcolMenu_MenuItems;
			}
		}

		[KeyField("MenuId", 30, 1)]
		public string MenuId
		{ 
			get{ return msMenuId; }
			set{ msMenuId = value; }
		}

		[BaseField("IsHorizontal",0)]
		public bool IsHorizontal
		{
			get{ return mbIsHorizontal; }
			set{ mbIsHorizontal= value; }
		}

		[BaseField("MenuLevel", 0)]
		public int MenuLevel
		{ 
			get{ return miMenuLevel; }
			set{ miMenuLevel = value; }
		}

		public string GetMenu(string sActivePage)
		{
			string sRet = "LEEG";
			string sClass = "";
			string sMenuId = "";

			if (this.IsHorizontal)
			{
				sClass = "links_horz";
				sMenuId = "Menu_" + this.MenuId + "_horz";
			}
			else
			{
				sClass = "links_vert";
				sMenuId = "Menu_" + this.MenuId + "_vert";
			}

			StringBuilder sb = new StringBuilder();
			sb.Append("<ul id='"); 
			sb.Append(this.MenuId); 
			sb.Append("' class = '"); 
			sb.Append(sClass); 
			sb.Append("'>"); 

			foreach(cMenu_MenuItem oMI in this.MenuItems)
			{
				sb.Append("<li>"); 
				if (this.IsHorizontal) sb.Append("&nbsp;&nbsp;"); 
				sb.Append(getAnchor(sActivePage, oMI.MenuItemId, oMI.Map, oMI.LinkText)); 
				sb.Append("</li>"); 
			}
			sb.Append("</ul>"); 
			
			sRet = sb.ToString(); 
			return sRet;
		}

		private string getAnchor(string sActivePage, string sMenuItemId, 
			                       string sMap, string sLinkText) 
		{
			// example: <a id='MenuItem' href="Javascript: GoTo('Welkom_TL.aspx');">Welkom</a>
			StringBuilder sb = new StringBuilder();
	
			sb.Append("<a id='MenuItem_"); 

			sb.Append(sMenuItemId);
			sb.Append("'"); 

			// aparte opmaak voor actieve link
			if (sMenuItemId == sActivePage)
			{
				sb.Append(" class='current' style='COLOR: red' "); 
			}
			sb.Append(" href=\"Javascript: GoTo('"); 
			sb.Append(sMap);
			sb.Append(sMenuItemId); 
			sb.Append("');\""); 
			sb.Append(">"); 
			sb.Append(sLinkText); 
			sb.Append("</a>"); 

			return sb.ToString();
		}
	}
}
