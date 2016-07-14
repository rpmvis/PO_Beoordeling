<%@ Page language="c#" Codebehind="beoInvoer_Doelen.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.Invoer_Doel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Invoer doelstelling & afspraak</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK media="screen" href="../css/BEOstyles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../css/BEOstyles_print.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../js/General.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/RPC.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/beoValidation.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			var bBevroren = <% Response.Write(this.GetBevroren); %>;

			function ch_doel(sIdx){
				var beoid = getInHTML("lkp_beoid" + sIdx);
				var elem = getInHTML("lkp_elem" + sIdx);
				var doel = getVal("doel" + sIdx);

				var arrFV = new Array();
				arrFV[0] = ['action', "savedoel"];
				arrFV[1] = ['beoid', beoid];
				arrFV[2] = ['elem', elem];
				arrFV[3] = ['doel', doel];
				// alert(arrFV.toString());
				FVs_ToServer_Plus_GoTo(arrFV, "");
			}

			function ch_afsp(sIdx){
				var beoid = getInHTML("lkp_beoid" + sIdx);
				var elem = getInHTML("lkp_elem" + sIdx);
				var afsp = getVal("afsp" + sIdx);

				var arrFV = new Array();
				arrFV[0] = ['action', "saveafsp"];
				arrFV[1] = ['beoid', beoid];
				arrFV[2] = ['elem', elem];
				arrFV[3] = ['afsp', afsp];
				
				FVs_ToServer_Plus_GoTo(arrFV, "");				
			}

			function Zoom_onclick(sIdx){
				var scor = getVal("lkp_scor" + sIdx);
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
				
				var doel = getVal("doel" + sIdx);
				var afsp = getVal("afsp" + sIdx);

   			var sheader1Out = getInHTML("elemomschr" + sIdx);

				var sheader2Out = getInHTML("lkp_indi" + sIdx);
									
   			sheader1Out = "Doelstelling en afspraak m.b.t. '" + sheader1Out + "'";

				var sInputTitleOut1 = scor;
				var sInputTitleOut2;
				var sInputTitleOut3;
				
				if (bBevroren){
					sInputTitleOut2 = 'Doelstelling:';
					sInputTitleOut3 = 'Afspraak:';	
				}
				else{
					sInputTitleOut2 = 'Vul s.v.p. doelstelling in:';
					sInputTitleOut3 = 'Vul s.v.p. afspraak in:';
				}

				var doel = getVal("doel" + sIdx);
				var afsp = getVal("afsp" + sIdx);

				var ArgsOut = new Array(sheader1Out, sheader2Out, sInputTitleOut1, sInputTitleOut2, sInputTitleOut3, doel, afsp, bBevroren);
				
				var WinSettings = "center:yes;resizable:no;dialogHeight:550px;dialogWidth:629px;status=no;";
				// var WinSettings = "center:yes;resizable:no;status=yes;title=hehe";
				
				var ArgsIn = window.showModalDialog("beoDlgDoelAfsp.htm", 
														ArgsOut, WinSettings);

				if (ArgsIn == null){
					// No changes returned
				}
				else{
					var value = ArgsIn[0].toString();	
					if (value == "") {value = "-";}
					setVal("doel" + sIdx, value);

					var value = ArgsIn[1].toString();	
					if (value == "") {value = "-";}
					setVal("afsp" + sIdx, value);
					
					// gelijk wegschrijven
					ch_doel(sIdx);
					ch_afsp(sIdx);
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
	<body class="body" MS_POSITIONING="GridLayout">
		<form id="frmDoelen" method="post" runat="server">
			<div id="top">
				<table>
					<tr>
						<td align="right" colSpan="3"><span class="NoPrint">
								<% Response.Write(Write_Menu_beoAlgemeen());%>
							</span>
						</td>
					</tr>
					<tr>
						<td vAlign="top" rowSpan="3"><IMG class="DROpict" alt="logo Gemeente Amsterdam" src="../Pics\logo\menu_logo.gif" align="left"></td>
						<td vAlign="top"><IMG class="DROpict" alt="logo DRO" src="../Pics\headers\amsterdamdro_home_logo.gif"></td>
						<td class="appname" align="right" width="100%">Beoordelingsprogramma</td>
					</tr>
					<tr height="100%">
						<td id="list3" vAlign="bottom" colSpan="2"><span class="NoPrint">
								<% Response.Write(Write_Menu_TL());%>
							</span>
						</td>
					</tr>
					<tr>
						<td class="titelbalk" colSpan="2">Doelen en afspraken</td>
					</tr>
				</table>
			</div>
			<div id="content">
				<table class="DROTable" cellSpacing="0" cellPadding="0">
					<tr>
						<td colSpan="2"><span class="MFkopbreedWit">Doelen en afspraken (4/4)</span>
						</td>
					</tr>
					<tr>
						<td vAlign="top">
							<table cellSpacing="0" cellPadding="0">
								<tr>
									<td>&nbsp;
									</td>
									<td class="TD2_kop"><b>Doel</b>
									</td>
									<td class="TD2_kop">
										<b>Afspraak</b>
									<td></td>
									<% Response.Write(Response_DataRows()); %>
								</tr>
							</table>
						</td>
						<td vAlign="top">
							<table class="DROTable_relativewidth" cellSpacing="0" cellPadding="0" width="220">
								<tr id="showindioffset" height="0">
									<td>&nbsp;
									</td>
								</tr>
								<tr>
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
