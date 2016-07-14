<%@ Page language="c#" Codebehind="beoInvoer_Voorblad.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.Voorblad" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Invoer voorblad</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/BEOstyles.css" type="text/css" rel="stylesheet" media="screen">
		<LINK href="../css/BEOstyles_print.css" type="text/css" rel="stylesheet" media="print">
		<script language="JavaScript" src="../js/General.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/RPC.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/beoValidation.js" type="text/javascript"></script>
	</HEAD>
	<body class="body" MS_POSITIONING="GridLayout">
		<form id="frmVoorblad" method="post" runat='server'>
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
						<td class="titelbalk" colspan="2">Voorblad</td>
					</tr>
				</table>
			</div>
			<div id="content">
				<table class="DROTable" cellSpacing="0" cellPadding="0">
					<tr>
						<td><span class="MFkopbreedWit">Voorblad beoordeling (1/4)</span>
						</td>
					</tr>
					<tr>
						<td>
							<table class="DROTable_relativewidth" cellSpacing="0" cellPadding="0">
								<tr>
									<td class="TD_kop_blauw" colSpan="2">Personalia:
									</td>
								<tr>
									<td class="TD_rijkop">Persoon</td>
									<td><input id="Persoon" runat="server" style="WIDTH: 196px; HEIGHT: 22px" type="text" size="27" Height="22px" Width="219px" disabled readOnly></td>
								</tr>
								<tr>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td class="TD_rijkop">Periode omschrijving</td>
									<td><input id="peri_omschr" runat="server" style="WIDTH: 196px; HEIGHT: 22px" type="text" size="27" Height="22px" Width="219px"></td>
								</tr>
								<tr>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td class="TD_rijkop"><span style="COLOR: red">*&nbsp;</span>Functie</td>
									<td>
										<INPUT id="Functie" runat="server" style="WIDTH: 196px; HEIGHT: 22px" type="text" size="27" name="Text1" Height="22px" Width="219px"></td>
								</tr>
								<tr>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td class="TD_rijkop">Afdeling</td>
									<td><input id="Afdeling" runat="server" style="WIDTH: 196px; HEIGHT: 22px" disabled readOnly size="27" Height="22px" Width="219px"></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td>
							<table class="DROTable_relativewidth" cellSpacing="0" cellPadding="0">
								<tr>
									<td class="TD_kop_blauw" colSpan="2">Beoordeling opgemaakt door:
									</td>
								<tr>
									<td class="TD_rijkop"><span style="COLOR: red">*&nbsp;</span>1e beoordelaar</td>
									<td><input id="Beoordelaar1" runat="server" style="WIDTH: 321px; HEIGHT: 22px" type="text" Height="22px" Width="219px" size="48"></td>
								</tr>
								<tr>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td class="TD_rijkop">2e beoordelaar</td>
									<td><input class="inputON" runat="server" id="Beoordelaar2" style="WIDTH: 321px" type="text" Height="22px" Width="219px">
									</td>
								</tr>
								<tr>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td class="TD_rijkop">P&nbsp;&amp;&nbsp;O&nbsp;adviseur
									</td>
									<td><input class="inputON" id="PenO_adviseur" runat="server" style="WIDTH: 321px" type="text" Height="22px" Width="219px">
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td class="TD_rijkop" colSpan="3"><span style="COLOR: red">*</span>
						= verplichte invoer
						<td></td>
					</tr>
				</table>
			</div>
			<div id="leftnav"><span class="NoPrint"><% Response.Write(Write_Menu_beoInvoer());%></span></div>
			<div id="rightnav"><img id="printbutton" class="NoPrint" onclick="PrintIt();" src='../Pics/buttons/print.gif' alt='Print vriendelijke versie' width="15" height="11"></div>
		</form>
	</body>
</HTML>
