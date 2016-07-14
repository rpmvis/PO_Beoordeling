using System;
using System.Collections; 


namespace BO
{
	[DataTable("tProf_Comp", spUPDATE="")]
	public class cProf_Comp: cObj_base
	{
		string msProf;
		string msComp;
		int miUI_Index;
		int miProf_CompId; 

		public cProf_Comp()
		{
		}

		public bool Load(DAL_OleDb oDal, string sProf_CompId)
		{
			string sSQL = "SELECT *" +
				" FROM tProf_Comp" +
				" WHERE Prof_CompId = " + sProf_CompId + ";";

			return oDal.FillObject(sSQL, this); 
		}

		[KeyReadOnlyField("Prof_CompId", 0, 1)]
		public int Prof_CompId
		{ 
			get{ return miProf_CompId; }
			set{ miProf_CompId = value; }
		}

		[BaseField("Prof", 12)]
		public string Prof
		{ 
			get{ return msProf; }
			set{ msProf = value; }
		}
		
		[BaseField("Comp", 4)]
		public string Comp
		{ 
			get{ return msComp; }
			set{ msComp = value; }
		}

		// voor ddl boven datagrid
		public string Prof_Comp
		{
			get
			{
				return this.Prof + " - " + this.Comp; 
			}
		}

		[BaseField("UI_Index", 0)]
		public int UI_Index
		{ 
		get{ return miUI_Index; }
		set{ miUI_Index = value; }
		}
	}
}
