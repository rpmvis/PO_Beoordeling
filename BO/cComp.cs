using System;
using System.Collections; 


namespace BO
{
	[DataTable("tComp", spUPDATE="")]
	public class cComp: cObj_base
	{
		cElems moElems;

		string msComp;
		string msDescr;
		string msComment;
		int miUI_Index; // afgeleide prop.

		public cComp()
		{
		}
		
		~cComp()
		{
			moElems = null;
		}

		public bool Load(DAL_OleDb oDal, string sComp)
		{

			string sSQL = "SELECT *" +
				" FROM tComp" +
				" WHERE Comp = '" + sComp + "'";  

			return oDal.FillObject(sSQL, this); 
		}

		[KeyField("Comp", 4, 2)]
		[ForeignKeyFieldAttribute("Comp", 4)]
		public string Comp
		{ 
			get{ return msComp; }
			set{ msComp = value; }
		}
		
		[BaseField("Descr", 50)]
		public string  Descr
		{ 
			get{ return msDescr; }
			set{ msDescr = value; }
		}


		[BaseField("Comment", 50)]
		public string  Comment
		{ 
			get{ return msComment; }
			set{ msComment = value; }
		}

		[BaseField("UI_Index", 0)]
		public int UI_Index //  Property afgeleid van cProf_Comp
		{ 
			get{ return miUI_Index; }
			set{ miUI_Index= value; }
		}
		

		public cElems Elems(DAL_OleDb oDal)
		{
			if (moElems==null)
			{
				moElems = new cElems(oDal).GetAsList(this.Comp); 
			}
			return moElems;				
		}
	}
}
