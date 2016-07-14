using System;

namespace BO
{
	/// <summary>
	/// Summary description for cMenuItem.
	/// </summary>
	public class cMenuItem: cObj_base
	{
		private string msMenuItemId = "", msMenuName = "";

		public cMenuItem()
		{
		}

		[KeyField("MenuItemId", 40, 1)]
		public string MenuItemId
		{ 
			get{ return msMenuItemId; }
			set{ msMenuItemId = value; }
		}

		[BaseField("Map", 40)]
		public string MenuName
		{ 
			get{ return msMenuName; }
			set{ msMenuName = value; }
		}
	}
}
