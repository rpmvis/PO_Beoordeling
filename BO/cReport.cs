using System;
using System.Text; 

namespace BO
{
	/// <summary>
	/// Summary description for cReport.
	/// </summary>
	public class cReport
	{

		public cReport()
		{
		}

		public string Response_Scores(bool bMetIndicatoren, 
			                            cBeo oBeo, 
			                            ref string sErrMsg)
		{
			if(oBeo.Prof == "")
			{
				sErrMsg = "Geef eerst het type 'Leidinggevend' op in blad 2 alvorens scores in te vullen.";
				return "";
			}

			cBeoElems oBEs = oBeo.BeoElems;

			string sBE_Row =
				"<tr id='rowLeid0' vAlign='top'>" +
				" <td style='HEIGHT: 10px'><b>#compdescr#</b>&nbsp;</td>" +
				"	<td style='HEIGHT: 10px'><b>#elemomschr#</b></td>" +
				" <td style='HEIGHT: 10px'>#scor#</td>" +
				" <td style='HEIGHT: 10px'>#toel#</td></tr>";
			string sIndi_Row =
				"<tr vAlign='top'>" +
				" <td>&nbsp;</td>" +
				" <td colSpan='3'>#indi#</td></tr>";

			string sOldComp = "";
			StringBuilder sb = new StringBuilder();
			string sRow1 = ""; // scor + toel
			string sRow2 = ""; // indi

			foreach (cBeoElem oBE in oBEs)
			{
				sRow1 = sBE_Row;
				if (oBE.CompNr_Omschr != sOldComp)
				{
					sRow1 = sRow1.Replace("#compdescr#", oBE.CompNr_Omschr); 
					sOldComp = oBE.CompNr_Omschr; 
				}
				else
				{
					sRow1 = sRow1.Replace("#compdescr#", "");   
				}
				
				sRow1 = sRow1.Replace("#elemomschr#", oBE.ElemDescr);   
				sRow1 = sRow1.Replace("#scor#", oBE.Scor); 
				sRow1 = sRow1.Replace("#toel#", oBE.Toel); 
				sb.Append(sRow1); 
				sb.Append("\n"); 

				// in rapport ter bespreking wel indicatoren, in eindrapport niet
				if(bMetIndicatoren)
				{
					sRow2 = sIndi_Row;
					sRow2 = sRow2.Replace(">#indi#", oBE.Indi);   
					sb.Append(sRow2); 
					sb.Append("\n"); 
				}
			}
			return sb.ToString(); 
		}

		public string Response_Afspraken(cBeo oBeo)
		{
			cBeoElems oBEs_onvold = oBeo.BeoElems_onvold_scor; 
			StringBuilder sb = new StringBuilder();

			string sEmpty_row = "<tr vAlign='top'><td colSpan='4'>&nbsp;</td></tr>\n";

			if (oBEs_onvold.Count == 0) // afspraken nvt
			{
				string sNvt_row = "<tr vAlign='top'><td colSpan='4'>Niet van toepassing</td></tr>\n";
				sb.Append(sNvt_row);
				sb.Append(sEmpty_row);
				return sb.ToString();
			}

			string s0_row =
				"<tr style='TEXT-DECORATION: underline' vAlign='top'>" +
				"<td colSpan='4'>Afspraak #nr#</td></tr>\n";
			string s1_row = "<tr vAlign='top'><td>Competentie</td>" +
				"<td width='100%' colSpan='3'><b>#compdescr#</b></td></tr>\n";
			string s2_row = "<tr vAlign='top'><td>Element</td><td colSpan='3'><b>#elemomschr#</b></td></tr>\n";
			string s3_row = "<tr vAlign='top'><td>Doel</td><td colSpan='3'>#doel#</td></tr>\n";
			string s4_row = "<tr vAlign='top'><td>Afspraak</td><td colSpan='3'>#afsp#</td></tr>\n";
			string sRow0 = "", sRow1 = "", sRow2 = "", sRow3 = "", sRow4 = ""; 
			int iAfspNr = 0;

			// 1 of meer afspraken 
			foreach (cBeoElem oBE in oBEs_onvold)
			{
				sRow0 = s0_row;
				sRow1 = s1_row;
				sRow2 = s2_row;
				sRow3 = s3_row;
				sRow4 = s4_row;

				iAfspNr ++;

				// afspraaknr
				sRow0 = sRow0.Replace("#nr#", iAfspNr.ToString()); 

				// competentie
				StringBuilder sb2 = new StringBuilder(); 
				sb2.Append(oBE.CompNr_Omschr);  
				sRow1 = sRow1.Replace("#compdescr#", sb2.ToString()); 

				// beoord. element
				sRow2 = sRow2.Replace("#elemomschr#", oBE.ElemDescr);   
				
				// doel
				sRow3 = sRow3.Replace("#doel#", oBE.Doel); 

				// afpsraak
				sRow4 = sRow4.Replace("#afsp#", oBE.Afsp); 

				sb.Append(sRow0); 
				sb.Append(sRow1);
				sb.Append(sRow2);
				sb.Append(sRow3);
				sb.Append(sRow4);
			}
			return sb.ToString(); 
		}


	}
}
