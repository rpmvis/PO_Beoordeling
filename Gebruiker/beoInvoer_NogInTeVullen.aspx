<%@ Page language="c#" Codebehind="beoInvoer_NogInTeVullen.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.NogInTeVullen" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Invoer - Nog in te vullen</title>
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<META NAME="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../css/BEOstyles.css" type="text/css" rel="stylesheet" media="screen">
		<LINK href="../css/BEOstyles_print.css" type="text/css" rel="stylesheet" media="print">
		<script language="JavaScript" src="../js/General.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/RPC.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/beoValidation.js" type="text/javascript"></script>
	</HEAD>
	<body class="body">
		<form id="frmNogInTeVullen" method="post" runat='server'>
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
						<td class="titelbalk" colspan="2">
							Nog in te vullen
						</td>
					</tr>
				</table>
			</div>
			<div id="content">
				<table id="tblNogInTevullen" runat='server' class="DROTable" border="0" cellspacing="0" cellpadding="0" align="left">
					<tr>
						<td colspan="2">
							<span class="MFkopbreedWit">Nog in te vullen voor beoordeling</span>
						</td>
					</tr>
					<tr valign="top" style="FONT-WEIGHT: bold">
						<td class="TD" style="WIDTH: 178px">
							Scherm
						</td>
						<td>Nog in te vullen</td>
					</tr>
					<tr valign="top">
						<td class="TD" style="WIDTH: 178px">
							1.&nbsp;Voorblad
						</td>
						<td id="voorblad" runat='server' width="100%"></td>
					</tr>
					<tr valign="top">
						<td class="TD" style="WIDTH: 178px">
							2.&nbsp;Functie-inhoud
						</td>
						<td id="functie" runat='server'></td>
					</tr>
					<tr valign="top">
						<td class="TD" style="WIDTH: 178px">
							3.&nbsp;Scores&nbsp;en&nbsp;toelichtingen
						</td>
						<td id="scores" runat='server'></td>
					</tr>
					<tr valign="top">
						<td class="TD" style="WIDTH: 178px">
							4.&nbsp;Doelen&nbsp;en&nbsp;afspraken
						</td>
						<td id="doelen" runat='server'></td>
					</tr>
					<tr valign="top">
						<td class="TD" colspan="2">&nbsp;</td>
					</tr>
					<tr valign="top">
						<td class="TD" style="WIDTH: 178px"><STRONG>Beoordeling klaar?</STRONG></td>
						<td id="klaar" runat='server' style="FONT-WEIGHT: bold"></td>
					</tr>
				</table>
			</div>
			<div id="leftnav"><span class="NoPrint"><% Response.Write(Write_Menu_beoInvoer());%></span></div>
			<div id="rightnav"><img id="printbutton" class="NoPrint" onclick="PrintIt();" src='../Pics/buttons/print.gif' alt='Print vriendelijke versie' width="15" height="11"></div>
		</form>
	</body>
</HTML>
