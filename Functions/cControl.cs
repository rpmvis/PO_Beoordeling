using System;
using System.Web.UI.WebControls;
using System.Data;  

namespace FUNC
{
	public class cControl
	{
		public cControl()
		{
		}
//		public static void LoadCombo(DropDownList ddl, DataTable dt, 
//			string sValueField, string sTextField)
//		{
//			ddl.Items.Add("Selecteer ..");
//  
//			foreach (DataRow dr in dt.Rows)
//			{
//				string sText, sValue;
//				sText = dr[sTextField].ToString();
//				sValue = dr[sValueField].ToString();
//				ddl.Items.Add(new ListItem(sText, sValue));
//			}
//		}

		public static void BindCombo(DropDownList ddl
			, DataTable dt 
			, string sValueField
			, string sTextField)
		{
			ddl.DataSource = dt;
			ddl.DataValueField = sValueField;
			ddl.DataTextField = sTextField;
			ddl.DataBind(); 
		}

		public static int Get_Dg_Item_Index(ref DataGrid dg, ref DropDownList ddl)
		{
			int iPageIndex, iSelectedIndex= -1;
 
			Console.WriteLine(ddl.SelectedItem.Value.ToString());

			if (ddl.SelectedIndex <= 0)
			{
				iSelectedIndex = ddl.SelectedIndex;
				iPageIndex = 0;
			}
			else
			{
				int iModulus = cMath.Modulus( ddl.SelectedIndex, dg.PageSize);
				iPageIndex = (ddl.SelectedIndex - iModulus) / dg.PageSize;
				iSelectedIndex = iModulus;
			}

			dg.CurrentPageIndex = iPageIndex;
			return iSelectedIndex;
		}
	}
}
