using System;
using System.Data;
using System.Text;  

namespace BO
{
	/// <summary>
	/// Summary description for cImport2.
	/// </summary>
	public class cImport2
	{
		String[] _aTitel = new string[]{"dr.","drs.","ing.","mr.","ir.","prof.", 
										   "dr ","drs ","ing ","mr ","ir ","prof "}; 
		String[] _aTussenv = new string[]{"van den ","van der ","van de ","van ", "de "
											 ,"ten ", "ter ","te ", "op ten ", "el ", "le "};


		private struct structPers
		{
			public string volnaam;
			public string aanhef;
			public string titel;
			public string init;
			public string tussenv;
			public string anaam;
		}

		private DAL_OleDb oDal_ = new DAL_OleDb();  
		
		public cImport2()
		{
		}

		public void Import2(string sFilePath 
			, ref DataTable dtSource
			, ref string sMessage)
		{
			string sBeoID = "";
			string sWhere = "";
			string sMsg = "";
			DataTable dtDest_werkn = null; // voor werknemers
			DataTable dtDest_beo = null;   // voor beoordelingen
			DataTable dtDest_beoscore = null; // voor test of er al op beoordeeeld is
			DataColumn dc = null;
			int iWerkn_new = 0, iWerkn_upd = 0, iWerkn_TL_upd=0, iBeo = 0; // counts
			string sSQL = "";
			string sSQL_new_1 = ""; // SQL string voor nieuwe werknemer, versie 1
			string sSQL_new_2 = ""; // SQL string voor nieuwe werknemer, eindversie 2
			string sSQL_upd = "";   // SQL string voor bestaande werknemer
			string sVal = ""; 
			StringBuilder sb;
			string sCol_out = "";
			int i = 0;
			DataRow dr2 = null;
			string sAfde = "";
			string sFilter = "";

			// selection of rows to be added to the database
			DataRow[] aRows = null; 

			// if no data then exit
			if (dtSource == null)
			{
				sMsg = "Er zijn geen data aangetroffen in het  bestand\n"  + sFilePath + "!";
				throw new Exception(sMsg);    
			}

			// check of alle kolommen er zijn:
			// "lv_jaar","lv_periode","lv_msnr","afdeling", "kostenplaats","kpl_oms", "volnaam","beroep"
			string[] aReqCols = new string[]{"lv_jaar","lv_periode",
												"lv_msnr","afdeling",
												"kostenplaats","kpl_oms",
												"volnaam","beroep"};
			CheckCols(sFilePath, ref dtSource, ref aReqCols);

			// laad alle afdelingen
			DataTable dtAfde = new DataTable("tAfde");
			dtAfde.Columns.Add("afdeling", System.Type.GetType("System.String"));

			foreach(DataRow dr in dtSource.Rows)
			{
				sAfde = dr["afdeling"].ToString(); 
				if (sAfde == null) sAfde = ""; 
				sFilter = "afdeling = '" + sAfde + "'";
				if (dtAfde.Select(sFilter).Length == 0)
				{
					dr2 = dtAfde.NewRow();
					dr2["afdeling"] = sAfde;
					dtAfde.Rows.Add(dr2);  
				}
			}

			// checken of afdeling reeds bestaat; 
			// indien niet: toevoegen aan tabel tAfde
			CheckAfde(ref dtAfde); 

			// laad jaar
			dr2 = dtSource.Rows[0]; 
			string sYear = dr2["lv_jaar"].ToString();

			// toevoegen kolom BeoId aan alle data
			// BeoId is in jaar_msnr format
			SetBeoId(ref dtSource);

			// import Werknemers
			string[] aCols_in = new string[]{"BeoId","lv_jaar", "lv_msnr","afdeling", 
												"Anaam","Aanhef","Titel","Init","Tussenv","beroep"};
			string[] aCols_out = new String[]{"[BeoID]", "[peri]", // "[peri_omschr]",
												 "[Msnr]", "[Afde]", 
												 "[Anaam]", "[Aanhef]",
												 "[Titel]", "[Init]",
												 "[Tussenv]", "[Func]"};
			// make string like
			// "INSERT INTO [tWerkn] 
			// ([BeoID], [peri], [peri_omschr], [Msnr], [Afde], [Anaam], [Aanhef], [Titel], [Init], [Tussenv], [Func]"
			sb = new StringBuilder();
			sb.Append("INSERT INTO [tWerkn] \n(");

			string sCol = "";
			for (int j = 0; j < aCols_out.Length; j++)
			{
				sCol = aCols_out[j];
				sb.Append(sCol + ", "); 
			}
			sb.Remove(sb.Length -2, 2);
			sb.Append(")\n"); 
			sSQL_new_1  = sb.ToString(); 
			
			// laad werknermers uit database
			sSQL = "SELECT * FROM [tWerkn]";
			oDal_.ExecQuery_DataTable(sSQL, ref dtDest_werkn);

			// laad beoordelingen uit database
			sSQL = "SELECT * FROM [tblBeo]";
			oDal_.ExecQuery_DataTable(sSQL, ref dtDest_beo);			

			// laad beoordeling-scores uit database
			sSQL = "SELECT * FROM [tblBeoScore]";
			oDal_.ExecQuery_DataTable(sSQL, ref dtDest_beoscore);			

			// voorbereiden ontleden VolNaam
			// extra kolommen worden toegevoegd aan de tabel zoals geïmporteerd
			Type typStr = System.Type.GetType("System.String");  

			dtSource.Columns.Add("Aanhef", typStr);  
			dtSource.Columns.Add("Titel", typStr);  
			dtSource.Columns.Add("Init", typStr);  
			dtSource.Columns.Add("Tussenv", typStr);  
			dtSource.Columns.Add("Anaam", typStr);  

			foreach (DataRow row in dtSource.Rows)
			{
				
				// aanmaken Where clause voor controle op bestaan werknemer
				sBeoID = row["BeoId"].ToString(); 
				sWhere = "[BeoId] = '" + sBeoID + "'";

				Parse_volnaam(row);

				// alleen werknemer toevoegen als die nog niet bestaat
				aRows = dtDest_werkn.Select(sWhere);

				// A value of -1 indicates that the array contains no elements
				// dus: werknemer met dit BeoId bestaat nog niet
				if(aRows.GetUpperBound(0) == -1) 
				{
					sb = new StringBuilder();
					sb.Append("VALUES(");

					for (int j = 0; j < aCols_in.Length; j++)
					{
						sCol = aCols_in[j];
						dc = dtSource.Columns[sCol];
						sVal = row[sCol].ToString();

						if (dc.DataType.ToString() == "System.String")
						{
							sVal = "'" + sVal.Replace("'", "''")  + "'";
						}

						// do NOT insert more into a table field than it's maximum length 
						if (dc.MaxLength > 0)
						{
							sVal = row[sCol].ToString().Substring(0,dc.MaxLength);
						}
						sb.Append(sVal); 
						sb.Append(",");  
					}

					sb.Remove(sb.Length -1, 1);
					sb.Append(")"); 

					sSQL_new_2  = sSQL_new_1 + "\n" + sb.ToString(); 
					oDal_.Exec_ActionQuery(sSQL_new_2);
					iWerkn_new ++;
				}
				else
				{
					// alleen werknemer wijzigen als er nog geen beoordeling-scores op zijn
					aRows = dtDest_beoscore.Select(sWhere);
				
					// A value of -1 indicates that the array contains no elements
					// dus: werknemer heeft nog geen scores
					if(aRows.GetUpperBound(0) == -1) 
					{
						/* make update query like:
						UPDATE [tWerkn]
						SET [peri]=<peri,varchar(10),>, [peri_omschr]=	 <peri_omschr
						WHERE [BeoID]=<BeoID,varchar(20)
						*/
						sb = new StringBuilder();
						sb.Append("UPDATE [tWerkn] \nSET ");
						i = -1;
						foreach (string sCol_in in aCols_in)
						{
							i++;
							switch (sCol_in)
							{
								case "BeoId":
									// skip because already in Where clause
									break;
								default:
									dc = dtSource.Columns[sCol_in];
									sVal = row[sCol_in].ToString();

									if (dc.DataType.ToString() == "System.String")
									{
										sVal = "'" + sVal.Replace("'", "''")  + "'";
									}

									// do NOT insert more into a table field than it's maximum length 
									if (dc.MaxLength > 0)
									{
										sVal = row[sCol_in].ToString().Substring(0,dc.MaxLength);
									}

									sCol_out = aCols_out[i];
									sb.Append(sCol_out);
									sb.Append(" = ");
									sb.Append(sVal); 
									sb.Append(",");  
									break;
							}
						}

						sb.Remove(sb.Length -1, 1);
						sb.Append("\nWHERE ");
						sb.Append(sWhere); 
						sSQL_upd = sb.ToString();
						oDal_.Exec_ActionQuery(sSQL_upd);
						iWerkn_upd++; 
					}
				}

				// alleen beoordeling toevoegen als die nog niet bestaat
				aRows = dtDest_beo.Select(sWhere);

				// A value of -1 indicates that the array contains no elements
				// dus: beoordleing met dit BeoId bestaat nog niet
				if(aRows.GetUpperBound(0) == -1) 
				{
					sSQL_new_2 = 
						"INSERT INTO dbo.tblBeo" +
						" (BeoID, peri_omschr, BeoFunc, geselecteerd, bevroren)" +
						" SELECT BeoID, peri, Func, 0 AS geselecteerd, 0 AS bevroren" +
						" FROM dbo.tWerkn" +
						" WHERE " + sWhere + ";";
					oDal_.Exec_ActionQuery(sSQL_new_2);
					iBeo ++;
				}
			}

			// now update afdeling to 100000 (DIRECTIE) for all Werknemers that are Teamleiders
			string sAfdeCodeDirectie = cLkp.Lkp(
				oDal_ 
				, "Afde"
				, "tAfde"
				, "LEFT(Omschr, 8) = 'DIRECTIE'"); 

			if(sAfdeCodeDirectie =="")
			{
				throw new Exception("Import2: geen Afdelingcode voor 'DIRECTIE' gevonden in tabel 'tAfde'!"); 			}

			sSQL = "UPDATE [tWerkn]" +
				" SET [Afde]='" + sAfdeCodeDirectie + "'" +
				" WHERE [BeoId] IN" +
				" (SELECT dbo.tWerkn.BeoId FROM dbo.tWerkn INNER JOIN dbo.tTL_Afde_Peri" +
				" ON dbo.tWerkn.peri = dbo.tTL_Afde_Peri.peri" +
				" AND dbo.tWerkn.Msnr = dbo.tTL_Afde_Peri.TLMsnr" +
				" WHERE dbo.tWerkn.peri = '" + sYear +"')";

			iWerkn_TL_upd = oDal_.Exec_ActionQuery(sSQL);

			sMessage = "Er zijn ingevoerd:\n";
			sMessage += iWerkn_new.ToString() + " werknemers\n";
			sMessage += iBeo.ToString() + " beoordelingen\n\n";
			sMessage += "Gewijzigd zijn:\n";
			sMessage += iWerkn_upd.ToString() + " nog niet beoordeelde werknemers\n\n";
			sMessage += "De afdeling is ingesteld op " + sAfdeCodeDirectie + " (DIRECTIE) voor:\n";
			sMessage += iWerkn_TL_upd.ToString() + " teamleiders\n\n";

			sMessage += "Einde Import uit bestand \n" + sFilePath;

			// Check of er een Teamleider is voor elke afdeling/jaar combinatie
			Check_Tl_Afde(ref dtAfde, sYear, ref sMessage);
		}

		private void CheckCols(string sFilePath, ref DataTable dt, ref string[] aReqCols) 
		{
			// controle op verplichte kolommnen
			string sReqCol = "";
			DataColumn dc = null;
			string sMsg = "";

			for (int i = 0;i < aReqCols.Length; i++)
			{
				sReqCol = aReqCols[i];
				dc = dt.Columns[sReqCol]; 
				if (dc==null)
				{
					sMsg = sMsg + sReqCol + "\n";
				}
			}

			if (sMsg !="")
			{
				sMsg = "De volgende veld(en) zijn niet aangetroffen in het betand:\n" +
					sFilePath + "\n\n" +
					sMsg +  "\nHerstel dit svp!";
				throw new Exception(sMsg);    
			}
		}

		private void CheckAfde(ref DataTable dtAfde)
		{
			// checken of afdeling reeds bestaat
			// indien niet: toevoegen aan tabel tAfde
			string sAfde= "", sAfde2 = "", sSQL = "";
			cAfde oAfde = null;
			string sMsg = "";
			foreach(DataRow row in dtAfde.Rows)
			{
				sAfde = row["Afdeling"].ToString();
				oAfde = new cAfde();
				
				sSQL = "SELECT Afde" +
					" FROM tAfde" +
					" WHERE Afde = '" + sAfde.Replace("'", "''")  + "'";

				sAfde2 = Convert.ToString(oDal_.ExecQuery_Scalar(sSQL));  

				if (sAfde2 == "")
				{
					sMsg = sMsg + sAfde + "\n";
				}
			}

			if(sMsg != "")
			{
				sMsg = "De volgende afdelingen bestaan nog niet in tabel 'tAfde'\n" +
					sMsg + "\n" +
					"Maak deze aan via de beheer web pagina.\n" +
					"Werk daar ook tabel 'tTL_Afde_Peri' bij!";
				throw new Exception(sMsg);  
			}
		}

		private void Check_Tl_Afde(ref DataTable dtAfde
			, string sYear
			, ref string sMessage)
		{
			// checken of Teamleider reeds bestaat voor afdeling + periode
			string sAfde= "", sSQL = "";
			int iTLMsnr;
			string sTemp = "";
			foreach(DataRow row in dtAfde.Rows)
			{
				sAfde = row["Afdeling"].ToString();

				sSQL = "SELECT TLMsnr" +
					" FROM tTL_Afde_Peri" +
					" WHERE Afde = '" + sAfde.Replace("'", "''")  + "'" +
					" AND peri = '" + sYear + "';";
				iTLMsnr = Convert.ToInt32(oDal_.ExecQuery_Scalar(sSQL)); 

				if (iTLMsnr == 0)
				{
					sTemp = sTemp + sAfde + "\n";
				}
			}

			if(sTemp != "")
			{
				sTemp = "\n\nDe volgende afdelingen hebben in jaar " + sYear + " nog geen teamleider:\n\n" +
					sTemp + "\n" +
					"Maak deze aan via de beheer web pagina in tabel 'tTL_Afde_Peri'";
				sMessage += sTemp;
			}
		}


		private void SetBeoId(ref DataTable dt)
		{
			string sBeoId = "";
			Type typStr = System.Type.GetType("System.String");  
 
			dt.Columns.Add("BeoId", typStr); 
			foreach (DataRow row in dt.Rows)
			{
				row.BeginEdit();
				sBeoId = row["lv_jaar"].ToString() + "_" + row["lv_msnr"].ToString();
				row["BeoId"] = sBeoId;
				row.EndEdit();
			}
			dt.AcceptChanges(); 
		}

		private void Parse_volnaam(DataRow row)
		{
			// volnaam ontleden voor huidige rij in DataTable
			DataColumn dc = null;
			structPers pers;

			String sRest = "";

			int iPos = 0;

			// START PARSING 'volnaam'
			pers = new structPers(); 
			pers.aanhef = "";
			pers.titel = "";
			pers.init = "";
			pers.tussenv = "";
			pers.anaam = "";

			dc = row.Table.Columns["volnaam"];
			pers.volnaam = row[dc].ToString();

			sRest = pers.volnaam;
			
			// 1) aanhef
			if (sRest.ToLower().StartsWith("dhr."))
			{
				pers.aanhef = "Dhr.";
				sRest = sRest.Substring(4).TrimStart(); 
			}
			else
			{
				if (sRest.ToLower().StartsWith("mw."))
				{
					pers.aanhef = "Mw.";
					sRest = sRest.Substring(3).TrimStart(); 	
				}
			}
			
			// 2 Titel
			for (int i = 0; i < _aTitel.Length;i++)
			{
				while (true)
				{
					if (sRest.ToLower().StartsWith(_aTitel[i])) 
					{
						pers.titel = pers.titel  + _aTitel[i];
						// corrigeer voor titel die NIET met een . maar een spatie wordt gesloten
						if (!pers.titel.EndsWith(".")) pers.titel = pers.titel.TrimEnd() + ".";
						pers.titel = pers.titel  + " ";
						sRest = sRest.Substring(_aTitel[i].Length).TrimStart();   
					}
					else break;
				}
			}
			pers.titel = pers.titel.TrimEnd();  

			// 3 Init
			// zoek eerste spatie opnemen als 'Init' als er id substring een . zit
			iPos = sRest.IndexOf(" ");
			if (iPos > -1)
			{
				pers.init = sRest.Substring(0,iPos);  

				// defensief: check of 'pers.init' een punt bevat
				if (pers.init.IndexOf(".") > -1)
				{
					sRest = sRest.Substring(iPos +1).TrimStart();
				}
				else
				{
					// reset to empty
					pers.init = "";
				}
			}

			// 4) tusssenvoegsel
			for (int i = 0; i < _aTussenv.Length;i++)
			{
				if (sRest.ToLower().StartsWith(_aTussenv[i])) 
				{
					pers.tussenv = sRest.Substring(0, _aTussenv[i].Length).TrimEnd(); 
					sRest = sRest.Substring(_aTussenv[i].Length).TrimStart();   
					break;
				}
			}

			pers.anaam = sRest;

			row.BeginEdit();
			row["Aanhef"] = pers.aanhef; 
			row["Titel"] = pers.titel;
			row["Init"] = pers.init;
			row["Tussenv"] = pers.tussenv;
			row["Anaam"] = pers.anaam; 
			row.EndEdit();

			// END PARSING 'volnaam'
		}
	}
}
