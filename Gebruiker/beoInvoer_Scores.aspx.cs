using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BO;
using FUNC;
using System.Text; 
using PO_Beoordeling;


namespace PO_Beoordeling
{
	/// <summary>
	/// Summary description for Invoer_Score.
	/// </summary>
	public class Scores: frmBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable tblLookup;
		protected System.Web.UI.HtmlControls.HtmlTableRow KopToel;
		protected System.Web.UI.WebControls.TextBox txtBevroren;
	
		private cBeo moBeo = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			using(DAL_OleDb oDal = new DAL_OleDb())
			{
				cSession oS = new cSession();
				if (oS.Load(oDal, this.SessionId(oDal)))
				{
					moBeo = new cBeo();
					moBeo.Load(oDal, oS.BeoId);
				}
				this.Redirect_ErrorPage(); 
			}
		}
		
		public int GetListIndex(string sVal, string[] arrOptions)
		{
			// return index of value in arrOptions
			// if absent return -1
			int idx = -1;
			if (sVal == null) return idx;

			for(int i=0; i < arrOptions.Length; i++)
			{
				if (arrOptions[i].ToLower() == sVal.ToLower())
				{
					idx = i;
					break;
				}
			}
			return idx;
		}

		public string Response_DataRows()
		{
			if (moBeo == null) return "";

			if(moBeo.Prof == "" || moBeo.Prof == "undefined")
			{
				string sMsg = "Geef eerst het type 'Leidinggevend' op in blad 2 alvorens scores in te vullen.";
				this.Message  = sMsg;
				return "";
			}

			int i = -1;
			string[] arrOptions = new string[]{"V","O"};		

			string sComp_Row = "<tr id='#compid#'><td class='TD_kop_blauw'" +
				" style='WIDTH: 468px' colSpan='4'>#compdescr#</td></tr>";
			string sBE_Row = "<tr onmousemove=\"Show_Indi('#compid#','#i#');\" id='row#i#' style='POSITION: relative' runat='server'\n>" +
				"<td class='TD_kop' id='elemomschr#i#'>#elemomschr#</td>\n" +
				"<td><INPUT id='opt#i#0' #checked0# onclick=\"ch_scor('#i#', this.value);\" type='radio' value='V' name='opt#i#'>&nbsp;<b>V</b>&nbsp;&nbsp;" +
				"<INPUT id='opt#i#1' #checked1# onclick=\"ch_scor('#i#', this.value);\" type='radio' value='O' name='opt#i#'>&nbsp;<b>O</b></td>\n" +
				"<td><input id='toel#i#' value = '#toel#' style='WIDTH: 295px; HEIGHT: 20px' type='text' maxLength='255' onchange=\"ch_toel('#i#')\" size='41'></td>\n" +
				"<td style='WIDTH: 40px'><INPUT class='DRObtnDialog' id='btnDlg#i#' onclick=\"Zoom_onclick('#i#')\" type='button'></td>\n</tr>";

			string sOldComp = "";
			int iCompNr = 0;
			StringBuilder sb = new StringBuilder();
			string sCompId = "";

			string sRow = "";

			foreach (cBeoElem oBE in moBeo.BeoElems)
			{
				if (oBE.Comp != sOldComp)
				{
					sCompId = "comp"+ iCompNr.ToString();
					iCompNr ++;
					
					StringBuilder sb2 = new StringBuilder(); 
					sb2.Append(iCompNr.ToString()); 
					sb2.Append(". ");
					sb2.Append(oBE.CompOmschr);  

					sRow = sComp_Row.Replace("#compid#", sCompId);
					sRow = sRow.Replace("#compdescr#", sb2.ToString());   
					
					sb.Append(sRow);
					sb.Append("\n"); 
					sOldComp = oBE.Comp;
				}
				
				i++;
				sRow = sBE_Row;
				sRow = sRow.Replace("#compid#", sCompId); // voor verticale hoogte indicatoren
				sRow = sRow.Replace("#i#", i.ToString()); 
				sRow = sRow.Replace("#elemomschr#", oBE.ElemDescr);   

				int idx = GetListIndex(oBE.Scor, arrOptions);
				switch (idx)
				{
					case -1:
						sRow = sRow.Replace("#checked0#", ""); 
						sRow = sRow.Replace("#checked1#", ""); 
						break;
					case 0:
						sRow = sRow.Replace("#checked0#", "checked"); 
						sRow = sRow.Replace("#checked1#", ""); 
						break;
					case 1:
						sRow = sRow.Replace("#checked0#", ""); 
						sRow = sRow.Replace("#checked1#", "checked"); 
						break;
				}
				// toevoegen toelichting
				sRow = sRow.Replace("#toel#", oBE.Toel); 
				sb.Append(sRow); 
				sb.Append("\n"); 
			}
			return sb.ToString(); 
		}

		public string Response_LookupTable()
		{
			if (moBeo == null) return "";

			// tabel  met opzoekwaarden geheel opbouwen
			StringBuilder sb = new StringBuilder();
			int i = -1;

			sb.Append("<table id='tblLookup' runat='server' style='LEFT: 0px; VISIBILITY: hidden; WIDTH: 0px; POSITION: absolute; TOP: 0px; HEIGHT: 0px'>\n"); 
			sb.Append("<tr>"); 

			foreach (cBeoElem oBE in moBeo.BeoElems)
			{
				i++;

				sb.Append("<td id='lkp_beoid"); // key1
				sb.Append(i.ToString());
				sb.Append("' noWrap>");
				sb.Append(oBE.BeoID);
				sb.Append("</td>"); 

				sb.Append("<td id='lkp_elem"); // key2
				sb.Append(i.ToString());
				sb.Append("' noWrap>");
				sb.Append(oBE.Elem);
				sb.Append("</td>"); 

				sb.Append("<td id='lkp_indi");
				sb.Append(i.ToString());
				sb.Append("' noWrap>");
				sb.Append(oBE.Indi_with_ElemDescr);
				sb.Append("</td>"); 
			}

			sb.Append("</tr>\n");
		
			sb.Append("</table>"); 
			string s = sb.ToString();
			return s;
		}

		public string GetBevroren
		{
			get
			{
				if (moBeo == null) return "false";
				else return moBeo.Bevroren.ToString().ToLower();
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
