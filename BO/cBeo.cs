using System;
using System.Data; 
using System.Text;
using System.Collections; 
using FUNC;


namespace BO
{
  	#region class Beo
	/// <summary>
	/// Summary description for cBeo.
	/// </summary>
	/// 
	[DataTable("tblBeo", spUPDATE="")]
	public class cBeo: cObj_base
	{
		string msBeoId=""; // bestaat uit periode + msnr's van TL + beo
		int miMsnr;
		string msPeri;
		string msPeri_omschr;
		string msProf;
		string msBeoNaam="";
		string msBeoFunc="";
		string msAfde="";
		string msBeo1 = "", msBeo2 = "", msPoad = ""; 
		string msVoll="", msVolT="", msNive="", msNivT="";  
		string msBere = "";
		cBeoElems mcolBeoElems = null;
		cBeoElems mcolBeoElems_onvold= null; // onvold. score
		// cWerkn moWerkn = null;
		bool mbGeselecteerd, mbBevroren;
		private DAL_OleDb mDal = null;

		public cBeo()
		{
		}


		public bool Load(DAL_OleDb oDal, string sBeoId)
		{
			mDal = oDal;
			bool bRet = false;
			StringBuilder sb = new StringBuilder(); 
			
			sb.Append("SELECT ISNULL(w.Anaam, '') + ', ' + ISNULL(w.Aanhef + ' ', '') +");
			sb.Append(" ISNULL(w.Titel + ' ','') + ISNULL(w.Init + ' ', '') +");
			sb.Append(" ISNULL(w.Tussenv + ' ', '') AS BeoNaam,"); 
			sb.Append(" w.peri AS Peri, w.Msnr AS Msnr, b.*, a.Omschr AS Afde"); 
			sb.Append(" FROM tblBeo b INNER JOIN tWerkn w"); 
			sb.Append(" ON b.BeoID = w.BeoID"); 
			sb.Append(" LEFT JOIN tAfde a"); 
			sb.Append(" ON w.Afde = a.Afde"); 
			sb.Append(" WHERE b.BeoId = '");
			sb.Append(sBeoId);
			sb.Append("';"); 

			string sSQL = sb.ToString(); 

			bRet = mDal.FillObject(sSQL, this); 
			return bRet;
		}

  	[KeyField("BeoID", 24, 1)]
		public string BeoID
		{ 
			get{ return msBeoId; }
			set{ msBeoId = value; }
		}

		[BaseField("Beo1", 50)]
		public string Beo1
		{ 
			get
			{
				return msBeo1;
			}
			set{ msBeo1= value; }
		}

		[BaseField("Beo2", 50)]
		public string Beo2
		{ 
			get
			{
				return msBeo2;
			}
			set{ msBeo2= value; }
		}
		
		[BaseField("Poad", 50)]
		public string Poad
		{ 
			get
			{
				return msPoad;
			}
			set{ msPoad= value; }
		}

		[BaseField("Voll", 3)] // volledige vervulling van functie?
		public string Voll
		{ 
			get{ return msVoll; }
			set{ msVoll= value; }
		}

		[BaseField("VolT", 255)]  // Toelichting volledige vervulling van functie
		public string VolT
		{ 
			get{
				return msVolT;
			}
			set{ msVolT= value; }
		}

		[BaseField("Nive", 3)] // functievervulling op ander niveau?
		public string Nive
		{ 
			get {return msNive;}
			set{ msNive= value; }
		}

		[BaseField("NivT", 255)] // Toelichting functievervulling op ander niveau?
		public string NivT
		{ 
			get{
				return msNivT;
			}
			set{msNivT= value; }
		}

		[BaseField("Bere", 255)] // Toelichting functievervulling op ander niveau?
		public string Bere
		{ 
			get{
				return msBere;
			}
			set{ msBere= value; }
		}

		[BaseField("Geselecteerd", 0)]
		public bool Geselecteerd
		{
			get{return mbGeselecteerd;}
			set{mbGeselecteerd=value;}
		}

		[BaseField("Bevroren", 0)]
		public bool Bevroren
		{
			get{return mbBevroren;}
			set{mbBevroren=value;}
		}

		[BaseField("Prof", 12)]
		public string Prof // HierFuncLeid of FuncLeid of NietLeid
		{ 
			get{ return msProf; }
			set{ msProf= value; }
		}

		// READ ONLY PROPS
		[ReadOnlyField("Peri", 10)]
		public string Peri
		{ 
			get{ return msPeri; }
			set{ msPeri= value; }
		}

		[BaseField("peri_omschr", 50)]
		public string peri_omschr
		{ 
			get{
				if (msPeri_omschr != "")
					return msPeri_omschr;
				else
					return msPeri;
			}
			set{ msPeri_omschr= value; }
		}

		[ReadOnlyField("Msnr", 6)]
		public int Msnr
		{ 
			get{ return miMsnr; }
			set{ miMsnr = value; }
		}

		[ReadOnlyField("BeoNaam", 50)]
		public string BeoNaam
		{ 
			get{ return msBeoNaam; }
			set{ msBeoNaam= value; }
		}

		// voor selectie-ddl
		public string BeoID_BeoNaam
		{
			get
			{
				return this.BeoID  + " - " + this.BeoNaam;  
				}
		}


		[BaseField("BeoFunc", 45)]
		public string BeoFunc
		{ 
			get
			{
				return msBeoFunc; }
			set{ msBeoFunc= value; }
		}

		[ReadOnlyField("Afde", 45)]
		public string Afde
		{ 
			get{ return msAfde; }
			set{ msAfde= value; }
		}


		// derived prop
		public string LeidType // HierFuncLeid of FuncLeid of NietLeid
		{ 
			get
			{
				string sRet= "";
				
				sRet = cLkp.Lkp(mDal, "Omschr", "tProf", "Prof = '" + this.Prof + "'");
				return sRet;
		}

		}

		public DAL_OleDb Dal
		// ivm collecties onder cBeo
		{
			get{return mDal;}
			set {mDal = value;}
		}
		
		public cBeoElems BeoElems
		{
			get
			{
				if (mcolBeoElems == null)
				{
					// voldoende scores
					mcolBeoElems = new cBeoElems(mDal);
					mcolBeoElems = 	mcolBeoElems.GetAsList(this.BeoID, false);
					int iCompNr = 0;
					string sOld = "";
					foreach (cBeoElem oBeoElem in mcolBeoElems)
					{
						if (oBeoElem.CompOmschr != sOld)
						{
							iCompNr ++;
							sOld = oBeoElem.CompOmschr;
						}

						oBeoElem.CompNr_Omschr =  iCompNr.ToString() + ".&nbsp;" + oBeoElem.CompOmschr; 
					}
				}
				return mcolBeoElems;
			}
		}

		public cBeoElems BeoElems_onvold_scor
		{
			get
			{
				string sKey = "";

				if (mcolBeoElems_onvold == null)
				{
					// onvoldoende scores
					mcolBeoElems_onvold = new cBeoElems(mDal);
					mcolBeoElems_onvold = mcolBeoElems_onvold.GetAsList(this.BeoID, true);

					// now add CompNr_Omschr values to BeoElem elements

					cBeoElems colBeoElems = this.BeoElems; 	// collection of ALL BeoElems
					cBeoElem oBeoElem = null;               // element of collection of ALL BeoElems
					if (mcolBeoElems_onvold.Count > 0)
					{
						foreach (cBeoElem oBeoElem_onvold in mcolBeoElems_onvold)
						{
							sKey = oBeoElem_onvold.Elem;
							oBeoElem = (cBeoElem)colBeoElems[sKey];
							if (oBeoElem != null) oBeoElem_onvold.CompNr_Omschr = oBeoElem.CompNr_Omschr;
							else oBeoElem_onvold.CompNr_Omschr = oBeoElem_onvold.CompOmschr; 
						}
					}
				}
				return mcolBeoElems_onvold;
			}
		}
	}
	#endregion

}
