using System;

namespace BO
{
	/// <summary>
	/// Summary description for cJoin_Menu_MenuItem.
	/// </summary>
	public class cMenu_MenuItem
	{
		private string msMenuId = "", msMenuItemId="", msLinkText = "", msMap = "";
		private int miUI_Index = 0;

		public cMenu_MenuItem()
		{
		}

		[KeyField("MenuId", 30, 1)]
		public string MenuId
		{ 
			get{ return msMenuId; }
			set{ msMenuId = value; }
		}

		[KeyField("MenuItemId", 40, 2)]
		public string MenuItemId
		{ 
			get{ return msMenuItemId; }
			set{ msMenuItemId = value; }
		}

		[BaseField("UI_Index", 0)]
		public int UI_Index
		{ 
			get{ return miUI_Index; }
			set{ miUI_Index = value; }
		}

		[BaseField("LinkText", 30)]
		public string  LinkText
		{ 
			get{ return msLinkText; }
			set{ msLinkText = value; }
		}

		[ReadOnlyField("Map", 40)]
		public string Map
		{ 
			get{ return msMap; }
			set{ msMap = value; }
		}

	}
}
