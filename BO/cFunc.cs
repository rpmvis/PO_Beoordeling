using System;

namespace BO
{
	/// <summary>
	/// Summary description for cFunc.
	/// </summary>
	[DataTable("tFunc", spUPDATE="")]
	public class cFunc: cObj_base
	{
		private int miFuncId = 0;
		private string msFunc="";
		
		public cFunc()
		{
		}

		public bool Load(DAL_OleDb oDal, string sFuncId)
		{

			string sSQL = "SELECT *" +
				" FROM tFunc" +
				" WHERE FuncId = " + sFuncId;

			return oDal.FillObject(sSQL, this); 
		}

		[KeyReadOnlyField("FuncId", 0 , 1)]
		public int FuncId
		{
			get{return miFuncId;}
			set{ miFuncId= value; }
		}

		[BaseField("Func", 60)]
		public string Func
		{ 
			get{ return msFunc; }
			set{ msFunc= value; }
		}
	}
}