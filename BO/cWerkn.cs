using System;
using System.Data; 
using System.Text;
using System.Collections; 
using FUNC;


namespace BO
{
	#region class Werkn
	/// <summary>
	/// Summary description for cWerkn.
	/// </summary>
	/// 
	[DataTable("tWerkn", spUPDATE="")]
	public class cWerkn: cObj_base
	{
		string msPeri= ""; int miMsnr;
		string msFunc="";
		string msAfde="";
		string msAanhef = "", msTitel = "", msInit = "", msTussenv = "", msAnaam = "";
		string msBeoId = "";

		public cWerkn()
		{
		}

		public bool Load(DAL_OleDb oDal, string sBeoId)
		{
			string sSQL = "SELECT *" +
				" FROM tWerkn" +
				" WHERE BeoId = '" + sBeoId + "'";

			return oDal.FillObject(sSQL, this); 
		}



		[KeyField("BeoId",20, 1)]
		public string BeoId
		{ 
			get{ return msBeoId; }
			set{ msBeoId= value; }
		}

		[BaseField("Peri", 10)]
		public string Peri
		{ 
			get{ return msPeri; }
			set{ msPeri= value; }
		}

		[BaseField("Msnr", 0)]
		public int Msnr
		{ 
			get{ return miMsnr; }
			set{ miMsnr = value; }
		}


		// Naam is GEEN BaseField maar een derived field
		public string Naam
		{ 
			get
			{
				StringBuilder sb = new StringBuilder("");
				if (this.Aanhef != ""){
					sb.Append(this.Aanhef);  
					sb.Append(" ");
				}
				if (this.Titel != "")
				{
					sb.Append(this.Titel);  
					sb.Append(" ");
				}
				if (this.Init != "")
				{
					sb.Append(this.Init );  
					sb.Append(" ");
				}
				if (this.Tussenv != "")
				{
					sb.Append(this.Tussenv );  
					sb.Append(" ");
				}
				sb.Append(this.Anaam);
				return sb.ToString(); 
			}
		}

		[BaseField("Aanhef", 4)]
		public string Aanhef
		{ 
			get{ return msAanhef; }
			set{ msAanhef= value; }
		}

		[BaseField("Titel", 15)]
		public string Titel
		{ 
			get{ return msTitel; }
			set{ msTitel= value; }
		}

		[BaseField("Init", 15)]
		public string Init
		{ 
			get{ return msInit; }
			set{ msInit= value; }
		}

		[BaseField("Tussenv", 10)]
		public string Tussenv
		{ 
			get{ return msTussenv; }
			set{ msTussenv= value; }
		}

		[BaseField("Anaam", 50)]
		public string Anaam
		{ 
			get{ return msAnaam; }
			set{ msAnaam= value; }
		}

		[BaseField("Func", 45)]
		public string Func
		{ 
			get
			{
				return msFunc; }
			set{ msFunc= value; }
		}

		[BaseField("Afde", 45)]
		public string Afde
		{ 
			get{ return msAfde; }
			set{ msAfde= value; }
		}
	}
	#endregion
}
