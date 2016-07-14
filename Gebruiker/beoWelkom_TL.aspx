<%@ Page language="c#" Codebehind="beoWelkom_TL.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.frmWelkom" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title class="wit">Welkom Teamleider</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK media="screen" href="../css/BEOstyles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../css/BEOstyles_print.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../js/General.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/RPC.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/beoValidation.js" type="text/javascript"></script>
	</HEAD>
	<BODY class='body' language="javascript">
		<FORM id="frmWelkom" method="post" runat="server">
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
						<td id="list3" colspan="2" valign="bottom">
							<span class="NoPrint">
								<% Response.Write(Write_Menu_TL());%>
							</span>
						</td>
					</tr>
					<tr>
						<td class="titelbalk" colspan="2">Welkom</td>
					</tr>
				</table>
			</div>
			<div id="content">
				<table class="DROtable" id="tblWelkom">
					<tr>
						<td><span class="MFkopbreedWit">Welkom bij het beoordelingsprogramma</span>
						</td>
					</tr>
					<tr>
						<td class="TD">U bent aangelogd als leidinggevende.<BR>
							<BR>
							Nadat u een beoordelingsperiode hebt opgegeven, krijgt u een overzicht van de 
							leden van uw team. Voor elk teamlid kunt u:<br>
							<br>
							<ul>
								<li>
								een beoordeling invoeren
								<li>
									een beoordelingsrapport aanmaken.
								</li>
							</ul>
						</td>
					</tr>
				</table>
				<table>
					<TBODY>
						<tr>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td class="TD">
								<table>
									<tr vAlign="top">
										<td><span class="MFbalksmalDblauw" id="lblFeedback" style="WIDTH: 153px; HEIGHT: 24px">Beoordelingsperiode:</span>
										</td>
										<td><asp:listbox id="lstPeri" runat="server" Font-Names="verdana,arial" Height="201px" Width="105px"></asp:listbox></td>
									</tr>
								</table>
							</td>
						</tr>
					</TBODY>
				</table>
			</div>
			<!---->
			<div id="leftnav"><span class="NoPrint"></span></div>
			<div id="rightnav"><img id="printbutton" class="NoPrint" onclick="PrintIt();" src='../Pics/buttons/print.gif' alt='Print vriendelijke versie'></div>
		</FORM>
	</BODY>
</HTML>
