<%@ Page language="c#" Codebehind="beoAlgemeen_Contact.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.Contact" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Contact</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/BEOstyles.css" type="text/css" rel="stylesheet" media="screen">
		<LINK href="../css/BEOstyles_print.css" type="text/css" rel="stylesheet" media="print">
		<script language="JavaScript" src="../js/General.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/RPC.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/beoValidation.js" type="text/javascript"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Contact" method="post" runat='server'>
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
						<td vAlign="bottom" colSpan="2"><span class="NoPrint">
								<% Response.Write(Write_Menu_horizontaal_hist());%>
							</span>
						</td>
					</tr>
					<tr>
						<td class="titelbalk" colspan="2">Contact</td>
					</tr>
				</table>
			</div>
			<div id="content">
				<table class="DROTable" ID="Table3">
					<tr>
						<!-- <td>&nbsp;</td> -->
						<td>
							<span class="MFkopbreedWit">Contact</span>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td class="TD">
							Voor vragen of opmerkingen<br>
							kunt u contact opnemen met:
						</td>
					</tr>
					<tr>
						<td class="TD">
							<br>
							<P>
								<% Response.Write(Reponse_Functioneel_contactpersoon());%>
							</P>
							<br>
							<P>
								<% Response.Write(Reponse_Technisch_contactpersoon());%>
							</P>
							<br>
						</td>
					</tr>
				</table>
			</div>
			<div id="leftnav"><span class="NoPrint"><% Response.Write(Write_Menu_verticaal_hist());%></span></div>
			<div id="rightnav"><img id="printbutton" class="NoPrint" onclick="PrintIt();" src='../Pics/buttons/print.gif' alt='Print vriendelijke versie' width="15" height="11"></div>
		</form>
	</body>
</HTML>
