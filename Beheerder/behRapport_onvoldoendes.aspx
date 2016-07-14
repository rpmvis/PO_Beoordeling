<%@ Page language="c#" Codebehind="behRapport_onvoldoendes.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.behRapport_onvoldoendes" %>
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK media="screen" href="../css/BEOstyles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../css/BEOstyles_print.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../js/General.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/RPC.js" type="text/javascript"></script>
		<script language="javascript">
			function init(){
				MaxWindow();
			}
		</script>
	</HEAD>
	<body class="body" onload="init();">
		<form id="tWerkn" method="post" runat="server">
			<div id="top_leftalign">
				<table>
					<tr>
						<td align="right" colSpan="3"><span class="NoPrint"></span></td>
					</tr>
					<tr>
						<td vAlign="top" rowSpan="3"><IMG class="DROpict" alt="logo Gemeente Amsterdam" src="../Pics\logo\menu_logo.gif" align="left"></td>
						<td vAlign="top" colSpan="2"><IMG class="DROpict" alt="logo DRO" src="../Pics\headers\amsterdamdro_home_logo.gif"><span class="appname">Beoordelingsprogramma</span></td>
					</tr>
					<tr height="100%">
						<td width="100%"><% Response.Write(Write_Menu_beheer());%></td>
						<td><IMG class="NoPrint" id="printbutton" onclick="PrintIt();" alt="Print vriendelijke versie" src="../Pics/buttons/print.gif">
						</td>
					</tr>
					<tr>
						<td class="titelbalk" colSpan="2">Rapport onvoldoendes</td>
					</tr>
				</table>
			</div>
			<div id="content_beheerder_leftalign" style="BACKGROUND-COLOR: white">
				<DIV style="DISPLAY: inline; WIDTH: 33px; HEIGHT: 19px" ms_positioning="FlowLayout">Jaar:&nbsp;</DIV>
				<asp:dropdownlist id="ddlSelPeri" runat="server" Width="88px"></asp:dropdownlist>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="NoPrint">
					<asp:Button Visible="true" id="btnMakeReport" runat="server" Text="Maak rapport" Width="107px"></asp:Button></span>
				<p></p>
				<div id="divReport" runat="server"></div>
			</div>
		</form>
	</body>
</HTML>
