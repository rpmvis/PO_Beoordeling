<%@ Page language="c#" Codebehind="beoInvoer_Scores.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.Scores" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Invoer scores</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/BEOstyles.css" type="text/css" rel="stylesheet" media="screen">
		<LINK href="../css/BEOstyles_print.css" type="text/css" rel="stylesheet" media="print">
		<script language="JavaScript" src="../js/General.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/RPC.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/beoValidation.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			var bBevroren = <% Response.Write(this.GetBevroren); %>;

			function ch_scor(sIdx, scor){
				// alert("scor: " + scor);
				var beoid = getInHTML("lkp_beoid" + sIdx);
				var elem = getInHTML("lkp_elem" + sIdx);
				
				var arrFV = new Array();
				arrFV[0] = ['action', "savescor"];
				arrFV[1] = ['beoid', beoid];
				arrFV[2] = ['elem', elem];
				arrFV[3] = ['scor', scor];
				// FVs_ToServer(arrFV);
				FVs_ToServer_Plus_GoTo(arrFV, "");
				
				enable_toel(sIdx, scor);				
			}

			function ch_toel(sIdx){
				var beoid = getInHTML("lkp_beoid" + sIdx);
				var elem = getInHTML("lkp_elem" + sIdx);
				var toel = getVal("toel" + sIdx);

				var arrFV = new Array();
				arrFV[0] = ['action', "savetoel"];
				arrFV[1] = ['beoid', beoid];
				arrFV[2] = ['elem', elem];
				arrFV[3] = ['toel', toel];
				// FVs_ToServer(arrFV);
				FVs_ToServer_Plus_GoTo(arrFV, "");
			}

			function enable_toel(sIdx, scor){
				// alert("sIdx, scor" + sIdx + " , " + scor);
				var bEna = (scor === "O");

				enable_ctl("toel" + sIdx, bEna);
				enable_ctl("btnDlg" + sIdx, bEna);
			}

		function enable_ctl(elId, bEnable){
			var el = getEl(elId);
			if(el == null) return;

			var sClass;

			if (bEnable){
				if(elId.substring(0,3).toLowerCase() === "btn") sClass = "DRObtnDialog";
				else sClass = "inputON";
			}
			else{
				if(elId.substring(0,3).toLowerCase() === "btn") sClass = "DRObtnDialog_disabled";
				else sClass = "inputOFF";
			}
			
			el.disabled = !bEnable; 
			
			// alert("el.id + enable " + el.id + " + " + bEnable);
			ChangeElClass(elId, sClass);
		}

			function Zoom_onclick(sIdx){
				var scor = GetRadioValue("opt" + sIdx);	
				switch(scor){
					case "O":
						scor = "onvoldoende";
						break;
					case "V":
						scor = "voldoende";
						break;
					default:
						scor = "?";
						break;
				}

   			var sheader1Out = getInHTML("elemomschr" + sIdx);

				var sheader2Out = getInHTML("lkp_indi" + sIdx); 
									
   			sheader1Out = "Toelichting op '" + sheader1Out + "'";

				var sInputTitleOut1 = scor;
				
				if (bBevroren){
					var sInputTitleOut2 = 'Uw toelichting:';}
				else{
					var sInputTitleOut2 = 'Geef s.v.p. uw toelichting:';}

				var txtTl = getEl("toel" + sIdx); 
				var sTextOut = txtTl.value;
			
				var ArgsOut = new Array(sheader1Out, sheader2Out, sInputTitleOut1, sInputTitleOut2, sTextOut, bBevroren);
				
				var WinSettings = "center:yes;resizable:no;dialogHeight:550px;dialogWidth:629px;status=no;";
				// var WinSettings = "center:yes;resizable:no;status=yes;title=hehe";
				
				var ArgsIn = window.showModalDialog("beoDlgToel.htm", 
														ArgsOut, WinSettings);

				if (ArgsIn == null){
					// No changes returned
				}
				else{
					var value = ArgsIn[0].toString();	
					if (value == "") {value = "-";}
					setVal("toel" + sIdx, value);
					
					// gelijk wegschrijven
					ch_toel(sIdx);
				}
			}
			
			var sOldIndi="";
			
  		function Show_Indi(sCompName, sIdx) {

				var sText = getInHTML("lkp_indi" + sIdx);
				if (sText!==sOldIndi){
					setInHTML("showindi", sText);
				
					// verticaal positioneren
					var el = getEl(sCompName);
					var iOffSet = el.offsetTop + el.offsetHeight;

					getEl("showindioffset").style.top = 0;
					getEl("showindioffset").style.height = iOffSet;
     
					sOldIndi = sText;
				}
			}
		</script>
	</HEAD>
	<body class="body" MS_POSITIONING="FlowLayout">
		<form id="frmScores" method="post" runat="server">
			<div id="top">
				<table>
					<tr>
						<td colspan="3" align="right">
							<span class="NoPrint">
								<% Response.Write(Write_Menu_beoAlgemeen());%>
							</span>
						</td>
					</tr>
					<tr>
						<td rowspan="3" valign="top"><IMG class="DROpict" alt="logo Gemeente Amsterdam" src="../Pics\logo\menu_logo.gif" align="left"></td>
						<td valign="top"><IMG class="DROpict" alt="logo DRO" src="../Pics\headers\amsterdamdro_home_logo.gif"></td>
						<td width="100%" align="right" class="appname">Beoordelingsprogramma</td>
					</tr>
					<tr height="100%">
						<td id="list3" vAlign="bottom" colSpan="2">
							<span class="NoPrint">
								<% Response.Write(Write_Menu_TL());%>
							</span>
						</td>
					</tr>
					<tr>
						<td class="titelbalk" colspan="2">Scores en toelichtingen</td>
					</tr>
				</table>
			</div>
			<div id="content">
				<table class="DROTable" style="POSITION: relative" cellSpacing="0" cellPadding="0">
					<tr>
						<td colspan="2"><span class="MFkopbreedWit">Scores en toelichtingen (3/4)</span>
						</td>
					</tr>
					<tr>
						<td style="WIDTH: 520px" vAlign="top">
							<table class="DROTable_relativewidth" cellSpacing="0" cellPadding="0">
								<tr>
									<td></td>
									<td colSpan="3">
										<table class="DROTable_relativewidth">
											<tr>
												<td><b>V</b>&nbsp;=&nbsp;Voldoende<BR>
													<b>O</b>&nbsp;=&nbsp;Onvoldoende
												</td>
												<td align="right" width="100%"></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr id="KopToel" style="POSITION: relative" runat="server">
									<td class="TD_kop"></td>
									<td></td>
									<td colSpan="2"><b>Toelichting</b>
									</td>
								</tr>
								<% Response.Write(Response_DataRows()); %>
								<TR>
									<TD class="TD_kop"></TD>
									<TD>&nbsp;</TD>
									<TD></TD>
									<TD style="WIDTH: 40px"></TD>
								</TR>
							</table>
						</td>
						<td vAlign="top">
							<table class="DROTable_relativewidth" cellSpacing="0" cellPadding="0" width="220">
								<tr id="showindioffset" vAlign="top" height="0">
									<td>&nbsp;
									</td>
								</tr>
								<tr vAlign="top">
									<td id="showindi" style="BORDER-RIGHT: gray thin solid; BORDER-TOP: gray thin solid; DISPLAY: block; PADDING-LEFT: 10px; VISIBILITY: visible; BORDER-LEFT: gray thin solid; BORDER-BOTTOM: gray thin solid; POSITION: relative; BACKGROUND-COLOR: white" vAlign="top"></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
			<div id="leftnav"><span class="NoPrint"><% Response.Write(Write_Menu_beoInvoer());%></span></div>
			<div id="rightnav"><img id="printbutton" class="NoPrint" onclick="PrintIt();" src='../Pics/buttons/print.gif' alt='Print vriendelijke versie' width="15" height="11"></div>
			<!-- opzoekwaarden -->
			<% Response.Write(Response_LookupTable()); %>
		</form>
	</body>
</HTML>
