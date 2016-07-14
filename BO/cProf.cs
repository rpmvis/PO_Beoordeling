using System;

namespace BO
{
	[DataTable("tProf", spUPDATE="")]
	public class cProf: cObj_base
	{
		string msProf;
		string msOmschr;
		string msOpm;

		public cProf()
		{
		}

		public bool Load(DAL_OleDb oDal, string sProf)
		{

			string sSQL = "SELECT *" +
				" FROM tProf" +
				" WHERE Prof = '" + sProf + "'";  

			return oDal.FillObject(sSQL, this); 
		}

		[KeyField("Prof", 12, 1)]
		[ForeignKeyFieldAttribute("Prof", 12)] // @@@ testen of nu goed
		public string Prof
		{ 
			get{ return msProf; }
			set{ msProf = value; }
		}
		
		[BaseField("Omschr", 100)]
		public string  Omschr
		{ 
			get{ return msOmschr; }
			set{ msOmschr = value; }
		}


		[BaseField("Opm", 255)]
		public string  Opm
		{ 
			get{ return msOpm; }
			set{ msOpm = value; }
		}
	}
}
