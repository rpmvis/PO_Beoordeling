using System;

namespace BO
{
	/// <summary>
	/// Summary UI_Indexiption for cElem.
	/// </summary>
	[DataTable("tElem", spUPDATE="")]
	public class cElem: cObj_base
	{
		string msComp;
		int miUI_Index;
  	string msElem, msElemDescr, msIndi;
		string msElemDescr_break;

		public cElem()
		{
		}

		public bool Load(DAL_OleDb oDal, string sElem )
		{
			string sSQL = "SELECT *" +
				" FROM tElem" +
				" WHERE Elem = '" + sElem + "'";  

			return oDal.FillObject(sSQL, this); 
		}

		[KeyField("Elem", 5, 1)]
		public string Elem
		{ 
			get{ return msElem; }
			set{ msElem = value; }
		}

		// [KeyField("Comp", 4, 1)]
		[ForeignKeyFieldAttribute("Comp", 4)]
		public string Comp
		{ 
			get{ return msComp; }
			set{ msComp = value; }
		}

		[BaseField("ElemDescr", 50)]
		public string  ElemDescr
		{ 
			get{ return msElemDescr; }
			set{ msElemDescr = value; }
		}

		[BaseField("ElemDescr_break", 50)]
		public string  ElemDescr_break
		{ 
			get{ return msElemDescr_break; }
			set{ msElemDescr_break = value; }
		}

		
		[BaseField("Indi", 300)]
		public string  Indi
		{ 
			get{ return msIndi; }
			set{ msIndi = value; }
		}

		[ReadOnlyField("Indi", 300)]
		public string Indi_with_ElemDescr
		{ 
			get
			{
				// toevoegen elem. omschr.  aan indicatoren
				return "<b>" + this.ElemDescr + "</b>" + msIndi;
			}
			set{ msIndi = value; }
		}


		[BaseField("UI_Index", 0)]
		public int UI_Index
		{ 
			get{ return miUI_Index; }
			set{ miUI_Index = value; }
		}

	}
}
