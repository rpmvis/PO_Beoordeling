using System;

namespace BO
{
	/// <summary>
	/// Summary UI_Indexiption for cPeri.
	/// </summary>
	public class cPeri: cObj_base
	{
		string msPeri;

		public cPeri()
		{
		}

		[KeyField("Peri", 10, 1)]
		public string Peri
		{ 
			get{ return msPeri; }
			set{ msPeri = value; }
		}
	}
}
