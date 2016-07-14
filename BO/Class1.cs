using System;

namespace BO
{

	[DataTable("tTL_Afde_Peri", spUPDATE="")]
	public class cTL_Afde_Peri: cObj_base
	{
		public cTL_Afde_Peri()
		{
		}

		private int miId;
		private string msperi;
		private string msAfde;
		private int miTLMsnr;

		public bool Load(DAL_OleDb oDal, int iId)
		{
			string sSQL = "SELECT * FROM tTL_Afde_Peri WHERE Id = Id";

			return oDal.FillObject(sSQL, this); }


		[KeyField("Id", -1, 1)]
		public int Id
		{
			get{ return miId; }
			set{ i = value; }
		}

		[BaseField("peri", -1)]
		public string peri
		{
			get{ return msperi; }
			set{ s = value; }
		}

		[BaseField("Afde", -1)]
		public string Afde
		{
			get{ return msAfde; }
			set{ s = value; }
		}

		[BaseField("TLMsnr", -1)]
		public int TLMsnr
		{
			get{ return miTLMsnr; }
			set{ i = value; }
		}
	}
}
