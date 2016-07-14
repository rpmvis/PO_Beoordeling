using System;
using System.Text; 

namespace BO
{
	
	[DataTable("tSetting", spUPDATE="")]
	public class cSetting: cObj_base
	{
		int miID;
		string msProgrammaNaam;
		DateTime mdDatumLaatsteOnderhoud;

		public cSetting()
		{
		}

		[KeyField("ID", 0, 1)]
		public int ID
		{ 
			get{ return miID; }
			set{miID=value;}
		}

		[BaseField("ProgrammaNaam", 4)]
		public string ProgrammaNaam
		{ 
			get{ return msProgrammaNaam; }
			set{msProgrammaNaam=value;}
		}

		[BaseField("DatumLaatsteOnderhoud", 0)]
		public DateTime DatumLaatsteOnderhoud
		{ 
			get{ return mdDatumLaatsteOnderhoud; }
			set{mdDatumLaatsteOnderhoud=value;}
		}
	}
}
