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
	/// Summary description for Invoer_Toelichting.
	/// </summary>
	public class Invoer_Doel : frmBase
	{
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
				else this.Redirect_ErrorPage(); 
			}
		}

		public string Response_DataRows()
		{
			string sNvt_row = "<tr id='row0_nvt'><td class='TD_kop' colSpan='5'>" +
				"niet van toepassing</td></tr>";
			
			if(moBeo.Prof == "")
			{
				string sMsg = "Geef eerst het type 'Leidinggevend' op in blad 2 alvorens doelen/afspraken in te vullen.";
				this.Message  = sMsg;
				return sNvt_row;
			}

			StringBuilder sb = new StringBuilder();
				if (moBeo.BeoElems.Count == 0) return sNvt_row;

				int i = -1;

				string sComp_Row = "<tr id='#compid#'><td class='TD_kop_blauw'" +
					" colSpan='4'>#value#</td></tr>";

				string sBE_Row = "<tr onmousemove=\"Show_Indi('#compid#','#i#');\" id='row#i#' style='POSITION: relative' runat='server'\n>" +
					"<td class='TD_kop' id='elemomschr#i#'>#elemomschr#</td>\n" +
					"<td><input id='doel#i#' value = '#doel#' style='WIDTH: 200; HEIGHT: 22px' type='text' maxLength='255' onchange=\"ch_doel('#i#')\" size='46'></td>\n" +
					"<td><input id='afsp#i#' value = '#afsp#' style='WIDTH: 200; HEIGHT: 22px' type='text' maxLength='255' onchange=\"ch_afsp('#i#')\" size='46'></td>\n" +
					"<td style='WIDTH: 43px'><INPUT class='DRObtnDialog' id='btnDlg#i#' onclick=\"Zoom_onclick('#i#')\" type='button'></td>\n</tr>";

				int iCompNr = 0;

				string sCompId = "";
				string sOldComp = ""; bool bCompChange = false;

				string sRow = "";

				foreach (cBeoElem oBE in moBeo.BeoElems)
				{
					if (oBE.Comp != sOldComp)
					{
						iCompNr ++;
						bCompChange = true;
						sOldComp = oBE.Comp;
					}

					// competentie ALLEEN toevoegen als er onvoldoende is gescoord
					if (oBE.Scor == "O")
					{
						// eerst competentie rij
						if (bCompChange)
						{
							sCompId = "comp"+ (iCompNr-1).ToString();

							StringBuilder sb2 = new StringBuilder(); 
							sb2.Append(iCompNr.ToString()); 
							sb2.Append(". ");
							sb2.Append(oBE.CompOmschr);  

							sRow = sComp_Row.Replace("#compid#", sCompId);
							sRow = sRow.Replace("#value#", sb2.ToString());   
								
							sb.Append(sRow); 
							sb.Append("\n"); 
						}

						
						// dan doel / afspraak rij
						i++;
						sRow = sBE_Row;
						sRow = sRow.Replace("#compid#", sCompId); // voor verticale hoogte indicatoren
						sRow = sRow.Replace("#i#", i.ToString()); 
						sRow = sRow.Replace("#elemomschr#", oBE.ElemDescr);   

						// doel + afsp
						sRow = sRow.Replace("#doel#", oBE.Doel); 
						sRow = sRow.Replace("#afsp#", oBE.Afsp); 
						sb.Append(sRow); 
						sb.Append("\n"); 
					}
			}
			return sb.ToString(); 
		}


		public string Response_LookupTable()
		{
			// tabel  met opzoekwaarden geheel opbouwen
			StringBuilder sb = new StringBuilder();
			int i = -1;

			sb.Append("<table style='LEFT: 0px; VISIBILITY: hidden; WIDTH: 0px; POSITION: absolute; TOP: 0px; HEIGHT: 0px'>\n"); 

			foreach (cBeoElem oBE in moBeo.BeoElems)
			{
				if(oBE.Scor== "O")
				{
					i++;
					sb.Append("<tr>");
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

					sb.Append("<td id='lkp_scor");
					sb.Append(i.ToString());
					sb.Append("' noWrap>");
					sb.Append(oBE.Scor);
					sb.Append("</td>"); 
					sb.Append("</tr>\n");
				}
			}

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
