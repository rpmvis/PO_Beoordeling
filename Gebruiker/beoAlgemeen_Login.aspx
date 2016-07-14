<%@ Page language="c#" Codebehind="beoAlgemeen_Login.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.Default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Inloggen</title>
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
			function init(){
				if (!Js_IE5_Ok()) return;
				MaxWindow();
			}
			
			function Js_IE5_Ok(){
				getEl("trNoJs").style.visibility = "hidden";
				getEl("trNoJs").style.height = 0;
				
				if (browser.isIE5up){
					getEl("trNoIE5up").style.visibility = "hidden";
					getEl("trNoIE5up").style.height = 0;
					getEl("tblLogin").style.visibility = "visible";
					focusFirst();
					return true;
				}
				else{
					getEl("trNoIE5up").style.visibility = "visible";
					getEl("tblLogin").style.visibility = "hidden";
					return false;
				}
 			}
		</script>
	</HEAD>
	<BODY class='body' onload="init();" MS_POSITIONING="FlowLayout">
		<form method="post" runat='server'>
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
						<td width="100%" class="appname">Beoordelingsprogramma</td>
					</tr>
					<tr height="100%">
						<td vAlign="bottom" colSpan="2"><span class="NoPrint">
								<% Response.Write(Write_Menu_horizontaal_hist());%>
							</span>
						</td>
					</tr>
					<tr>
						<td class="titelbalk" colspan="2">Inloggen</td>
					</tr>
				</table>
			</div>
			<div id="content">
				<table class="DROTable" id="tblLogin" style="VISIBILITY: hidden; HEIGHT: 180px">
					<TR>
						<td colSpan="2"><span class="MFkopbreedWit">Inloggen</span>
						</td>
					<tr>
						<td class="TD" style="WIDTH: 84px" vAlign="center">Gebruikersnaam</td>
						<td class="TD2" style="HEIGHT: 19px" align="left"><INPUT id="Gebruikersnaam" style="WIDTH: 170px; HEIGHT: 22px" type="text" size="23"></td>
					</tr>
					<tr>
						<td class="TD" style="WIDTH: 84px" vAlign="center">Wachtwoord</td>
						<td class="TD2" align="left"><INPUT id="Wachtwoord" style="WIDTH: 170px; HEIGHT: 22px" type="password" size="23"></td>
					</tr>
					<tr>
						<td class="TD" style="WIDTH: 84px" align="left"></td>
						<td class="TD2"><INPUT id="btnLogin" style="WIDTH: 78px; HEIGHT: 24px" onclick="GoTo('Gebruiker/beoWelkom_TL.aspx');" type="button" value="Log in">
							<a target="_parent"><INPUT id="btnCancel" style="MARGIN-LEFT: 10px; WIDTH: 78px; HEIGHT: 24px" onclick="CloseApp();" type="button" value="Annuleer"></a>
						</td>
					</tr>
					<tr>
						<td class="TD"><br>
						</td>
						<td></td>
					</tr>
				</table>
				<table class="DROTable" id="Table2">
					<tr id="trNoJs" style="VISIBILITY: visible">
						<td class="TD">Uw browser ondersteunt geen Javascript.<BR>
							<br>
							Zet 'scripting' aan in uw browser of open deze website met een browser die 
							Javascript ondersteunt.
						</td>
					</tr>
					<tr id="trNoIE5up" style="VISIBILITY: hidden">
						<td class="TD">Deze website is alleen toegankelijk met Internet Explorer als 
							browser en in versie 5.00 of hoger.
						</td>
					</tr>
				</table>
			</div>
			<div id="leftnav"><span class="NoPrint"><% Response.Write(Write_Menu_verticaal_hist());%></span></div>
			<div id="rightnav"><img id="printbutton" class="NoPrint" onclick="PrintIt();" src='../Pics/buttons/print.gif' alt='Print vriendelijke versie'></div>
		</form>
	</BODY>
</HTML>
