using System;
using System.Configuration; 
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using BO; 

namespace PO_Beoordeling      
{
	/// <summary>
	/// Summary description for frmBase.
	/// </summary>
	public class frmBase : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblErrMsg;
		private cURL moURL = null;
		private string msSessionId = "";
		private string msMsg ="";

		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		public string Message
		{
			get
			{
				return msMsg;
			}
			set
			{
				msMsg = value;

				// tevens ervoor zorgen dat popup verschijnt bij client via js
				if(msMsg != "")
				{
					this.RegisterHiddenField("lblErrMsg", msMsg);

					StringBuilder sb = new StringBuilder();
					sb.Append("<script language='javascript'>"); 
					sb.Append("\n"); 
					sb.Append("var sErrMsg = document.getElementById('lblErrMsg').value;");
					sb.Append("\n"); 
					sb.Append("if(sErrMsg !== ''){");
					sb.Append("\n"); 
					sb.Append("  alert(sErrMsg);");
					sb.Append("\n");
					sb.Append("}"); 
					sb.Append("</script>"); 

					this.RegisterStartupScript("keyErrMsg", sb.ToString());
				}
			}
		}

		protected bool SetCell(Control ctlParent, string cellName, string sValue)
		{
			Control ctl = null;
			
			if (!this.FindCtl(ctlParent, cellName, ref ctl)) return false;
			
			try
			{
				HtmlTableCell cel = (HtmlTableCell)ctl;
				cel.InnerHtml = sValue;
				return true;
			}
			catch
			{
				this.Message = "Fout bij het vullen van cel '" +  cellName + "' met waarde '" + sValue + "'!"; 
				return false;
			}
		}

		protected bool SetInputControl(Control ctlParent, string ctlName, string sValue)
		{
			Control ctl = FindCtl(ctlParent, ctlName);
			if (ctl == null) return false;

			try
			{
				HtmlInputControl input = (HtmlInputControl)ctl;
				input.Value = sValue;
				return true;
			}
			catch
			{
				this.Message = "Fout bij het vullen van control '" +  ctlName + "' met waarde '" + sValue + "'!"; 
				return false;
			}    
		}

		protected Control FindCtl(Control ctlParent, string ctlName)
		{
			Control ctl = ctlParent.FindControl(ctlName);
			if (ctl==null)
			{
				this.Message = "Control " +  ctlName + " bestaat niet!";
			}
			return ctl;
		}

		protected bool FindCtl(Control ctlParent, string ctlName, ref Control ctl)
		{
			ctl = ctlParent.FindControl(ctlName);
			if (ctl==null)
			{
				this.Message = "Control " +  ctlName + " bestaat niet!";
				return false;
			}
			return true;
		}
		
		protected string SessionId(DAL_OleDb oDal)
		{
			cURL oURL = this.URL();
			msSessionId = oURL.GetValue("sessionid"); 
			return msSessionId;
		}

		protected void CtlVis(Control ctlParent, string ctlName, bool bVisible)
		{
			Control ctl;
			ctl = this.FindCtl(ctlParent, ctlName);
			if (ctl != null) ctl.Visible = bVisible;
		}

		protected void CtlVis(string ctlParentName, string ctlName, bool bVisible)
		{
			Control ctlParent;

			ctlParent = FindCtl(this, ctlParentName); // werkt 'this' hier goed? ivm overerving
			if (ctlParent !=null)
			{
				CtlVis(ctlParent, ctlName, bVisible);
			}
		}

		public string Write_Menu_beoAlgemeen()
		{
			string sMenu = "";
			using(BO.DAL_OleDb oDal = new DAL_OleDb())
			{
				// geen beveiliging met Sessie record
				cMenu oMenu = new cMenu();
				oMenu.Load(oDal, "beoAlgemeen");
				string sActivePage = this.MyFileName;
				sMenu = oMenu.GetMenu(sActivePage);
 
				// leegmaken menu's in sessie-record als het geen algemeen formulier is
				if (this.MyFileName.ToLower().IndexOf("algemeen") == -1)
				{
					cSession oS = new cSession();
					if (oS.Load(oDal, this.SessionId(oDal)))
					{
						oS.Menu_horizontal = "";
						oS.Menu_vertical = "";
						oS.Update(oDal); 
					}
				}
			}
			return sMenu;
		}

		public string Write_Menu_behAlgemeen()
		{
			string sMenu = "";
			using(BO.DAL_OleDb oDal = new DAL_OleDb())
			{
				// geen beveiliging met Sessie record
				cMenu oMenu = new cMenu();
				oMenu.Load(oDal, "behAlgemeen");
				string sActivePage = this.MyFileName;
				sMenu = oMenu.GetMenu(sActivePage);
 
				// leegmaken menu's in sessie-record als het geen algemeen formulier is
				if (this.MyFileName.ToLower().IndexOf("algemeen") == -1)
				{
					cSession oS = new cSession();
					if (oS.Load(oDal, this.SessionId(oDal)))
					{
						oS.Menu_horizontal = "";
						oS.Menu_vertical = "";
						oS.Update(oDal); 
					}
				}
			}
			return sMenu;
		}


		public string Write_Menu_beheer()
		{
			string sMenu = "";
			using(BO.DAL_OleDb oDal = new DAL_OleDb())
			{
				cSession oS = new cSession();
				if (oS.Load(oDal, this.SessionId(oDal)))
				{
					cMenu oMenu = new cMenu();
					oMenu.Load(oDal, "behMenu");
					string sActivePage = this.MyFileName;
					sMenu = oMenu.GetMenu(sActivePage);

					WriteMenu_to_Session(oDal, oS, oMenu.IsHorizontal, sMenu);
				}
				else
				{
				}
			}
			return sMenu;
		}

		public string Write_Menu_Beheer_tabel()
		{
			string sMenu = "";
				using(BO.DAL_OleDb oDal = new DAL_OleDb())
				{
					cSession oS = new cSession();
					if (oS.Load(oDal, this.SessionId(oDal)))
					{
						cMenu oMenu = new cMenu();
						oMenu.Load(oDal, "behTabel");
						string sActivePage = this.MyFileName;
						sMenu = oMenu.GetMenu(sActivePage);

						WriteMenu_to_Session(oDal, oS, oMenu.IsHorizontal, sMenu);
					}
				}
			return sMenu;
		}

		public string Write_Menu_TL()
		{
			string sMenu = "";
			using(BO.DAL_OleDb oDal = new DAL_OleDb())
			{
				cSession oS = new cSession();
				if (oS.Load(oDal, this.SessionId(oDal)))
				{
					cMenu oMenu = new cMenu();
					oMenu.Load(oDal, "beoTL");
					string sActivePage = this.MyFileName;
					sMenu = oMenu.GetMenu(sActivePage);
 
					WriteMenu_to_Session(oDal, oS, oMenu.IsHorizontal, sMenu);
				}
			}
			return sMenu;
		}

		public string Write_Menu_beoInvoer()
		{
			string sMenu = "";
			using(BO.DAL_OleDb oDal = new DAL_OleDb())
			{
				cSession oS = new cSession();
				if (oS.Load(oDal, this.SessionId(oDal)))
				{
					cMenu oMenu = new cMenu();
					oMenu.Load(oDal, "beoInvoer");
					string sActivePage = this.MyFileName;
					sMenu = oMenu.GetMenu(sActivePage);
 
					WriteMenu_to_Session(oDal, oS, oMenu.IsHorizontal, sMenu);
				}
			}
			return sMenu;
		}

		public string Write_Menu_beoRapport_bespreking()
		{
			string sMenu = "";
				using(BO.DAL_OleDb oDal = new DAL_OleDb())
				{
					cSession oS = new cSession();
					if (oS.Load(oDal, this.SessionId(oDal)))
					{		
						cMenu oMenu = new cMenu();
						oMenu.Load(oDal, "beoRapport_bespreking");
						string sActivePage = this.MyFileName;
						sMenu = oMenu.GetMenu(sActivePage);
 
						WriteMenu_to_Session(oDal, oS, oMenu.IsHorizontal, sMenu);
					}
			}
			return sMenu;
 		}

		public string Write_Menu_beoRapport()
		{
			string sMenu = "";
				using(BO.DAL_OleDb oDal = new DAL_OleDb())
				{
					cSession oS = new cSession();
					if (oS.Load(oDal, this.SessionId(oDal)))
					{
						cMenu oMenu = new cMenu();
						oMenu.Load(oDal, "beoRapport");
						string sActivePage = this.MyFileName;
						sMenu = oMenu.GetMenu(sActivePage);

						WriteMenu_to_Session(oDal, oS, oMenu.IsHorizontal, sMenu);
					}
				}
			return sMenu;
		}

		private void WriteMenu_to_Session(DAL_OleDb oDal, cSession oS, bool bHorizontal, string sMenu)
		{
			// "class = current" en "style='COLOR: red'" moeten UIT de menu-string
			sMenu = sMenu.Replace("class='current'", "");
			sMenu = sMenu.Replace("style='COLOR: red'", "");
			
			if(bHorizontal)
			{
				oS.Menu_horizontal = sMenu;
			}
			else
			{
				oS.Menu_vertical = sMenu;
			}
			oS.Update(oDal); 
		}

		public string Write_Menu_horizontaal_hist()
		{
			string sMenu = "";
			using(BO.DAL_OleDb oDal = new DAL_OleDb())
			{
				cSession oS = new cSession();
				if (oS.Load(oDal, this.SessionId(oDal)))
				{
					sMenu = oS.Menu_horizontal; 
				}
			}
			return sMenu;
		}

		public string Write_Menu_verticaal_hist()
		{
			string sMenu = "";
			using(BO.DAL_OleDb oDal = new DAL_OleDb())
			{
				cSession oS = new cSession();
				if (oS.Load(oDal, this.SessionId(oDal)))
				{
					sMenu = oS.Menu_vertical; 
				}
			}
			return sMenu;
		}

		public string MyFileName
		{
			// extract filename (like 'Foo.aspx') from 'this' object
			get
			{
				string sExpr = this.ToString(); 
				char[] sep =".".ToCharArray(); // mbv spatie wordt ook de tijd string gesplitst
				string[] sSplit = sExpr.Split(sep);
				string sRet = sSplit[sSplit.GetUpperBound(0)]; 
				sRet = sRet.Replace("_aspx", ".aspx"); 

				return sRet;
			}
		}
		
		public cURL URL()
		{
			if (this.Request != null  )
			{
				if (moURL==null)
				{
					moURL = new cURL(this.Request.RawUrl);
				}
			}
			return moURL;
		}

		public void Redirect_ErrorPage()
		{
			// als web-applicatie in productie is: een redirect naar fout-pagina
			if (System.Configuration.ConfigurationSettings.AppSettings["InProduction"] == "false") 
				return;

			// redirect naar error page
			string sFrmName = this.MyFileName;
 			
			string [] sValues = new String[2]; 
			sValues[0] = "Kan formulier '" + sFrmName + "' niet laden zonder een geldig sessie-id!";
			// IsBeheerder = 'true' als filename begint met 'beh' of 't'
			sValues[1] = (sFrmName.Substring(0, 3).ToLower()  == "beh"
				|| sFrmName.Substring(0, 1).ToLower()  == "t").ToString().ToLower();  

			// waarden encoderen voor transport in query-string
			for(int i = 0; i <= sValues.GetUpperBound(0);i++)
			{
				sValues[i] = Microsoft.JScript.GlobalObject.escape(sValues[i]); 
				sValues[i] = HttpUtility.UrlEncode(sValues[i]); 
			}

			this.Response.Redirect("../frmError.htm?errmsg=" + sValues[0] +
				"&isbeheerder=" + sValues[1]); 
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
