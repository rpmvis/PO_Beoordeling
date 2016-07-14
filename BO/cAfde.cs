using System;


namespace BO
{
	/// <summary>
	/// Summary description for cAfde.
	/// </summary>
	[DataTable("tAfde", spUPDATE="")]
	public class cAfde: cObj_base
	{
		private string _sAfde, _sOmschr;
		private int _ID;

		public cAfde()
		{
		}

		public bool Load(DAL_OleDb oDal, int iID)
		{
			string sSQL = "SELECT *" +
				" FROM tAfde" +
				" WHERE ID = " + iID.ToString() + ";"; 

			return oDal.FillObject(sSQL, this); 
		}

		[KeyReadOnlyField("ID", 4, 1)]
		public int ID
		{ 
			get{ return _ID; }
			set{ _ID= value; }
		}

		[BaseField("Afde", 10)]
		public string Afde
		{ 
			get{ return _sAfde; }
			set{ _sAfde= value; }
		}

		[BaseField("Omschr", 45)]
		public string Omschr
		{ 
			get{ return _sOmschr; }
			set{ _sOmschr= value; }
		}
	}
}
