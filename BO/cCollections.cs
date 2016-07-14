using System;
using System.Collections; 
using FUNC;
using System.Text; 
using System.Data; 


namespace BO
{

	public class cKeyValues: CollectionBase
	{
		Hashtable mhtNV = new Hashtable();  
		
		public cKeyValues()
		{
		}

		public void Add(string sKey, string sValue)
		{
			mhtNV.Add(sKey, sValue); 
		}
	}

	public class cSessions: cColl_base
	{
		public cSessions(DAL_OleDb oDal) : base(oDal, typeof(cSession))
		{
		}

		public cSessions GetAsList()
		{
			string sSQL = "SELECT * FROM tSession ORDER BY Session;";
			return (cSessions)this.GetAs_IList(sSQL);  
		}
	}

	public class cSettings: cColl_base
	{
		public cSettings(DAL_OleDb oDal) : base(oDal, typeof(cSetting))
		{
		}
	}

	/// <summary>
	/// Summary description for cWerkns.
	/// </summary>
	public class cWerkns: cColl_base
	{

		public cWerkns(DAL_OleDb oDal): base(oDal, typeof(cWerkn))
		{
		}

		public cWerkns GetAsList()
		{
			// alle werknemers
			string sSQL = "SELECT * FROM tWerkn ORDER BY Msnr;";
			return (cWerkns)this.GetAs_IList(sSQL);  
		}
		
		public cWerkns GetAsList(string sPeri)
		{
			// werknemers per periode
			string sSQL = "SELECT * FROM tWerkn" +
				" WHERE Peri = '" + sPeri + 
				"' ORDER BY Msnr;";
			return (cWerkns)this.GetAs_IList(sSQL);  
		}
	}
	
	
	public class cTLs: cColl_base
	{
		public cTLs(DAL_OleDb oDal) : base(oDal, typeof(cTL))
		{
		}

		public cTLs GetAsList()
		{
			StringBuilder sb = new StringBuilder();

			// selecteer Naam TL met LEFT JOIN / laatste periode uit tWerk 
			sb.Append("SELECT tl.TLMsnr, tl.TLGebruikersNaam, tl.TLWachtwoord, s1.TLNaam");
			sb.Append(" FROM dbo.tTL tl LEFT OUTER JOIN"); 
			sb.Append(" (SELECT w.Msnr, w.Anaam + ', ' + w.Aanhef + ' ' + w.Titel + ' ' + w.Init + w.Tussenv AS TLNaam"); 
			sb.Append(" FROM dbo.tWerkn w"); 
			sb.Append(" WHERE (w.peri = (SELECT MAX([peri]) FROM [tWerkn]))) s1"); 
			sb.Append(" ON tl.TLMsnr = s1.Msnr"); 
			sb.Append(" ORDER BY tl.TLMsnr"); 

			string sSQL =  sb.ToString();
			return (cTLs)this.GetAs_IList(sSQL);  
		}
	}

	public class cTL_Afde_Peris: cColl_base
	{
		private string sSQL_base = "";
		private DataTable _dt_ddlPeri = null, _dt_ddlAfdOmschr = null;

		public cTL_Afde_Peris(DAL_OleDb oDal) : base(oDal, typeof(cTL_Afde_Peri))
		{
			sSQL_base = "SELECT tap.ID, tap.peri, tap.Afde, ISNULL(w.Anaam, '') + ', ' + ISNULL(w.Aanhef + ' ', '') +" +
			            " ISNULL(w.Titel + ' ','') + ISNULL(w.Init + ' ', '') +" +
			            " ISNULL(w.Tussenv + ' ', '') AS TLNaam, tap.TLMsnr" + 
			            " FROM tTL_Afde_Peri tap LEFT JOIN tWerkn w" + 
			            " ON tap.TLMsnr = w.Msnr" + 
				          " AND tap.Peri = w.Peri";
		}

		public cTL_Afde_Peris GetAsList()
		{
			string sSQL = sSQL_base +
				            " ORDER BY tap.Peri DESC, tap.Afde ASC, tap.TLMsnr ASC;";
			return (cTL_Afde_Peris)this.GetAs_IList(sSQL);  
		}

		public cTL_Afde_Peris GetAsList(string sWhere)
		{
			if (sWhere != "") sWhere = " WHERE " + sWhere;
			string sSQL = sSQL_base +
				sWhere + 
				" ORDER BY tap.Peri DESC, tap.Afde ASC, tap.TLMsnr ASC;";
			return (cTL_Afde_Peris)this.GetAs_IList(sSQL);  
		}

		public cTL_Afde_Peris GetAsList_afde()
		{
			// alle afdelingen uit TL_Afde_Peri
			string sSQL = "SELECT Afde" + 
                    " FROM dbo.tTL_Afde_Peri tap" +
                    " GROUP BY Afde";
			return (cTL_Afde_Peris)this.GetAs_IList(sSQL);  
		}

		public cTL_Afde_Peris GetAsList_afde_omschr()
		{
			// alle afdeling-omschrijvingen uit TL_Afde_Peri
			string sSQL = "SELECT tap.Afde, tAfde.Omschr, " + 
				" FROM dbo.tTL_Afde_Peri tap" +
				" LEFT OUTER JOIN dbo.tAfde" +
				" ON tap.Afde = dbo.tAfde.Afde" +
				" GROUP BY dbo.tAfde.Omschr, tap.Afde ";
			return (cTL_Afde_Peris)this.GetAs_IList(sSQL);  
		}

		// load combo voor selectie dg-item 
		public DataTable dt_ddlPeri
		{
			get
			{
				if (_dt_ddlPeri == null)
				{
					_dt_ddlPeri = new DataTable();
					string sSQL = 
							"SELECT '<ALLE>' AS Peri" +
              " FROM tTL_Afde_Peri" +
              " UNION" +
              " SELECT Peri" +
              " FROM tTL_Afde_Peri" +
              " GROUP BY peri" +
						  " ORDER BY peri ASC;";  

					this.Dal.ExecQuery_DataTable(sSQL, ref _dt_ddlPeri);
				}
				return _dt_ddlPeri;
			}
		}

		// load combo voor selectie dg-item 
		public DataTable dt_ddlAfdOmschr
		{
			get
			{
				if (_dt_ddlAfdOmschr == null)
				{
					_dt_ddlAfdOmschr = new DataTable();
					string sSQL = "SELECT '<ALLE>' AS Afde, '<ALLE>' AS Afde_Omschr" +
						            " FROM tTL_Afde_Peri" +
						            " UNION" +
                        " SELECT tap.Afde, tap.Afde + ' ' + tAfde.Omschr AS Afde_Omschr"  +
												" FROM dbo.tTL_Afde_Peri tap" +
												" LEFT OUTER JOIN tAfde" +
												" ON tap.Afde = tAfde.Afde" +
												" GROUP BY tap.Afde, tap.Afde + ' ' + tAfde.Omschr";
			
					this.Dal.ExecQuery_DataTable(sSQL, ref _dt_ddlAfdOmschr);
				}
				return _dt_ddlAfdOmschr;
			}
		}
	}

	public class cAfdeen: cColl_base
	{
		private DataTable _dt_ddlAfdOmschr = null;

		public cAfdeen(DAL_OleDb oDal) : base(oDal, typeof(cAfde))
		{
		}

//		public cAfdeen GetAsList_omschr()
//		{
//			string sSQL = "SELECT * FROM tAfde ORDER BY Omschr;";
//			return (cAfdeen)this.GetAs_IList(sSQL);  
//		}

		public cAfdeen GetAsList_afde()
		{
			string sSQL = "SELECT ID, Afde, Omschr FROM tAfde ORDER BY Afde;";
			return (cAfdeen)this.GetAs_IList(sSQL);  
		}

		// load combo voor selectie dg-item 
		public DataTable dt_ddlAfdOmschr
		{
			get
			{
				if (_dt_ddlAfdOmschr == null)
				{
					_dt_ddlAfdOmschr = new DataTable();
					string sSQL = "SELECT ID, Afde + ' ' + Omschr AS Afde_Omschr"  +
						            " FROM tAfde" + 
						            " ORDER BY Afde;";
			
					this.Dal.ExecQuery_DataTable(sSQL, ref _dt_ddlAfdOmschr);
				}
				return _dt_ddlAfdOmschr;
			}
			
		}
	}

	public class cFuncs: cColl_base
	{
		public cFuncs(DAL_OleDb oDal) : base(oDal, typeof(cFunc))
		{
		}

		public cFuncs GetAsList()
		{
			string sSQL = "SELECT * FROM tFunc ORDER BY Func;";
			return (cFuncs)this.GetAs_IList(sSQL);  
		}
	}

	/// <summary>
	/// Summary description for cElems.
	/// </summary>
	public class cElems: cColl_base
	{
		public cElems(DAL_OleDb oDal) : base(oDal, typeof(cElem))
		{
		}

		public cElems GetAsList()
		{
			string sSQL = "SELECT e.* FROM tElem e INNER JOIN tComp c ON e.Comp = c.Comp ORDER BY c.UI_Index, e.UI_Index;";
			return (cElems)this.GetAs_IList(sSQL);  
		}

		public cElems GetAsList(string sComp)
		{
			string sSQL = "SELECT * FROM tElem" +
				" WHERE Comp = '" + sComp + "'" +
				" ORDER BY UI_Index";
			return (cElems)this.GetAs_IList(sSQL);  
		}
	}

	public class cPeris: cColl_base
	{
		public cPeris(DAL_OleDb oDal) : base(oDal, typeof(cPeri))
		{
		}

		public cPeris GetAsList_werkn()
		{
			string sSQL = "SELECT peri" +
				" FROM tWerkn" +
				" GROUP BY peri" +
				" ORDER BY peri DESC";
			return (cPeris)this.GetAs_IList(sSQL);  
		}

		public cPeris GetAsList_TL()
		{
			string sSQL = "SELECT peri" +
				" FROM tTL_Afde_Peri" +
				" GROUP BY peri" +
				" ORDER BY peri DESC";
			return (cPeris)this.GetAs_IList(sSQL);  
		}
	}

	/// <summary>
	/// Summary description for cPenOs.
	/// </summary>
	public class cPenOs : cColl_base
	{
		public cPenOs(DAL_OleDb oDal) : base(oDal, typeof(cPenO))
		{
		}

		public cPenOs GetAsList()
		{
			string sSQL = "SELECT * FROM tPenO;";
			return (cPenOs)this.GetAs_IList(sSQL);  
		}

		public cPenO Hoofd_PenO
		{
			get
			{
				foreach (cPenO oPenO in this)
				{
					if (oPenO.IsHoofd)
					{
						return oPenO;
					}
				}
				return null; // geen hoofd P&O gevonden
			}
		}

		public cPenO TechContact_PenO
		{
			get
			{
				foreach (cPenO oPenO in this)
				{
					if (oPenO.IsTechContact)
					{
						return oPenO;
					}
				}
				return null; // geen hoofd P&O gevonden
			}
		}
	}

	/// <summary>
	/// Summary description for cProfs.
	/// </summary>
	public class cProfs : cColl_base
	{
		public cProfs(DAL_OleDb oDal) : base(oDal, typeof(cProf))
		{
		}

		public cProfs GetAsList()
		{
			string sSQL = "SELECT * FROM tProf;";
			return (cProfs)this.GetAs_IList(sSQL);  
		}
	}

	/// <summary>
	/// Summary description for cProf_Comps.
	/// </summary>
	public class cProf_Comps : cColl_base
	{
		public cProf_Comps(DAL_OleDb oDal) : base(oDal, typeof(cProf_Comp))
		{
		}

		public cProf_Comps GetAsList()
		{
			string sSQL = "SELECT * FROM tProf_Comp ORDER BY Prof, UI_Index;";
			return (cProf_Comps)this.GetAs_IList(sSQL);  
		}
	}

	/// <summary>
	/// Summary description for cScores.
	/// </summary>
	public class cScores: cColl_base
	{
		private string msSQL_base = "";

		public cScores(DAL_OleDb oDal) : base(oDal, typeof(cScor))
		{
			StringBuilder sb = new StringBuilder();
 
			sb.Append("SELECT ISNULL(w.Anaam, '') + ', ' + ISNULL(w.Aanhef + ' ', '') +");
			sb.Append(" ISNULL(w.Titel + ' ','') + ISNULL(w.Init + ' ', '') +");
			sb.Append(" ISNULL(w.Tussenv + ' ', '') AS BeoNaam,"); 
			sb.Append(" w.peri AS Peri, w.Msnr AS Msnr, s.*"); 
			sb.Append(" FROM tblBeoScore s INNER JOIN tWerkn w"); 
			sb.Append(" ON s.BeoID = w.BeoID"); 

			msSQL_base = sb.ToString(); 
		}

		public cScores GetAsList()
		{
			string sSQL = msSQL_base + " ORDER BY w.Peri DESC, w.Msnr ASC;";
			return (cScores)this.GetAs_IList(sSQL); 
		}

		public cScores GetAsList(string sPeri)
		{
			// beoordelingen per periode
			string sSQL = msSQL_base + 
				" WHERE w.Peri = '" + sPeri + "' ORDER BY w.Peri DESC, w.Msnr ASC;";
			return (cScores)this.GetAs_IList(sSQL);  
		}
	}

	public class cMenu_MenuItems: cColl_base
	{
		// koppelt menu met menu-item
		public cMenu_MenuItems(DAL_OleDb oDal) : base(oDal, typeof(cMenu_MenuItem))
		{
		}

		public cMenu_MenuItems GetAsList(string sMenuId)
		{
			string sSQL = "SELECT m_mi.MenuId, m_mi.MenuItemId, m_mi.UI_index, m_mi.LinkText, mi.Map" +
				" FROM tMenu_MenuItem m_mi INNER JOIN tMenuItem mi" +
				" ON m_mi.MenuItemId = mi.MenuItemId" +
				" WHERE (m_mi.MenuId = '" + sMenuId + "')" +
				" ORDER BY m_mi.UI_index;";

			return (cMenu_MenuItems)this.GetAs_IList(sSQL); 
		}
	}

	public class cMenuObject
	{
		private cMenu_MenuItems mcolMenu_MenuItems = null;
		private DAL_OleDb mDal;
		private string msMenuId = "";

		// koppelt menu met menu-item
		public cMenuObject(DAL_OleDb oDal, string sMenuId)
		{
			mDal = oDal;
			msMenuId = sMenuId;
		}

		public cMenu_MenuItems MenuItems
		{
			get
			{
				if (mcolMenu_MenuItems == null)
				{
					mcolMenu_MenuItems = new cMenu_MenuItems(mDal);
					mcolMenu_MenuItems.GetAsList(msMenuId);
				}
				return mcolMenu_MenuItems;
			}
		}
	}


	/// <summary>
	/// Summary description for cBeoElems.
	/// </summary>
	public class cBeoElems: cColl_base
	{
		private string msBeoId;
  	private Hashtable mhtElems;

		public cBeoElems(DAL_OleDb oDal) : base(oDal, typeof(cBeoElem))
		{
		}

		public cBeoElems GetAsList(string sBeoId, bool bOnvoldoendeSCores)
		{
			msBeoId = sBeoId;

			string spName = ""; 
			if(bOnvoldoendeSCores)
				spName = "sp_BeoElems_onvold_scor";
			else
				spName = "sp_BeoElems";

			this.Dal.ClearParameters();  
			this.Dal.AddInputParameter("@BeoId", sBeoId);
			int iRows = this.Dal.RetrieveChildObjects(spName, SourceType.sp, this, this.ChildType);  

			// voor Indexer op elementnaam
			if (iRows > 0)
			{
				mhtElems = new Hashtable(); 
				for(int i = 0; i < this.Count; i++) 
				{
					cBeoElem oBeoElem = (cBeoElem)this[i];
					mhtElems.Add(oBeoElem.Elem, i);  
				}
			}
			return this;
		}
		
		// indexer property, overloaded
		public cBeoElem this[string sElem]
		{
			get 
			{
				if (mhtElems.ContainsKey(sElem))
				{
					int iPos =(int)mhtElems[sElem];
					return (cBeoElem)this[iPos];
				}
				else return null;
			}
		}

		public cBeo Beo(string sBeoId)
		{
			return (cBeo)this.GetObject_ByKey(sBeoId);   
		}

		public string Elems_toString(bool AlleenMetOnvoldoendeScore)
			// AlleenMetOnvoldoendeScore == true voor Doelen/Afspraken
		{
			string s = "";
			bool bAdd = true;
			
			foreach (cBeoElem oBE in this)
			{
				if(AlleenMetOnvoldoendeScore) bAdd =(oBE.Scor== "O");
				
				if (bAdd)
				{
					s = cString.AddToResponse(s, "beoid", oBE.BeoID); // key1
					s = cString.AddToResponse(s, "elem", oBE.Elem); // key2
					s = cString.AddToResponse(s, "compomschr", oBE.CompOmschr);
					s = cString.AddToResponse(s, "ElemDescr", oBE.ElemDescr);
					s = cString.AddToResponse(s, "scor", oBE.Scor);
					s = cString.AddToResponse(s, "toel", oBE.Toel);
					s = cString.AddToResponse(s, "doel", oBE.Doel);
					s = cString.AddToResponse(s, "afsp", oBE.Afsp);
					s = cString.AddToResponse(s, "indi", oBE.Indi_with_ElemDescr);
					s = s + ";";
				}
			}
			s = cString.Strip(s, ";"); 

			return s;
		}
	}
}
