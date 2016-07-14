using System;
using System.Text; 

namespace BO
{
	/// <summary>
	/// Summary description for cComps.
	/// </summary>
	public class cComps: cColl_base
	{
		public cComps(DAL_OleDb oDal) : base(oDal, typeof(cComp))
		{
		}
		
		public cComps GetAsList()
		{
			string sSQL = "SELECT * FROM tComp ORDER BY UI_Index;";
			return (cComps)this.GetAs_IList(sSQL); 
		}
		
		public string Competentielijst(string sClassName)
		{
			StringBuilder sb = new StringBuilder();
			string sThisComp = "", sLastComp = ""; 

			// 3e kolom met indicatoren 100% breed
			sb.Append("<table class = '"); 
			sb.Append(sClassName);
			sb.Append("'>");
			sb.Append("<tr>");

			sb.Append("<td class = 'TD' width='1'>"); 
			sb.Append("<b>Competentie</b>"); 
			sb.Append("</td>"); 
			sb.Append("<td width='100%'>"); 
			sb.Append("<b>Beoordelingselement met indicatoren</b>"); 
			sb.Append("</td>"); 
			sb.Append("</tr>"); 
			foreach(cComp oComp in this)
			{
				cElems colElems = oComp.Elems(this.Dal);
				foreach(cElem oElem in colElems)
				{
					sThisComp = oComp.Descr;
					sb.Append("<tr vAlign = 'Top'>");
					// 1) competentie

					// sb.Append("<td class = 'tdRij_kop'>");
					sb.Append("<td class = 'TD'>");
					if (sThisComp != sLastComp)
					{
						sb.Append("<b>");
						sb.Append(sThisComp);
						sb.Append("</b>");
						sLastComp = sThisComp;
					}
					sb.Append("</td>");

//						// 2) element
//						sb.Append("<td>");
//						sb.Append(oElem.Descr_break); 
//						sb.Append("</td>");
//
					// 2) element + indicatoren, staan reeds in HTML formaat
					sb.Append("<td>");
					sb.Append(oElem.Indi_with_ElemDescr); 
					sb.Append("</td>");

					sb.Append("</tr>");
				}
			}

			sb.Append("</table>");

			string s = sb.ToString();
			return s;
		}
	}
}
