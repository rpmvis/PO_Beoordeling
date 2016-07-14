<%@ Page language="c#" Codebehind="beoRapport_bespreking.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.Rapport_bespreking" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Rapport ter bespreking</title>
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
	<body class="body" id="Rapport ter bepsreking" bgColor="white" MS_POSITIONING="GridLayout">
		<form method="post" runat="server">
			<div id="top" style="Z-INDEX: 100">
				<table>
					<tr>
						<td align="right" colSpan="3"><span class="NoPrint">
								<% Response.Write(Write_Menu_beoAlgemeen());%>
							</span>
						</td>
					</tr>
					<tr>
						<td vAlign="top" rowSpan="3"><IMG class="DROpict" alt="logo Gemeente Amsterdam" src="../Pics\logo\menu_logo.gif" align="left"></td>
						<td vAlign="top" style="WIDTH: 242px"><IMG class="DROpict" alt="logo DRO" src="../Pics\headers\amsterdamdro_home_logo.gif"></td>
						<td class="appname" align="right" width="100%">Beoordelingsprogramma</td>
					</tr>
					<tr height="100%">
						<td id="list3" vAlign="bottom" colSpan="2"><span class="NoPrint">
								<% Response.Write(Write_Menu_TL());%>
							</span>
						</td>
					</tr>
					<tr>
						<td class="titelbalk" style="WIDTH: 242px">Rapport ter&nbsp;bespreking
						</td>
						<td colspan = 2 class="titelbalk" id="hdrPeriode" runat="server" align =right width = "100%">periode</td>
					</tr>
				</table>
			</div>
			<div id="content" style="Z-INDEX: 102">
				<table class="DROTable_report">
					<!-- VOORBLAD -->
					<tr vAlign="top">
						<TD colSpan="3">Naam</TD>
						<TD id="Naam" runat="server"></TD>
					</tr>
					<tr vAlign="top">
						<TD style="WIDTH: 200px" colSpan="3">Periode</TD>
						<TD id="Periode" runat="server"></TD>
					</tr>
					<tr vAlign="top">
						<TD style="WIDTH: 200px" colSpan="3">Functie</TD>
						<TD id="Functie" runat="server"></TD>
					</tr>
					<tr vAlign="top">
						<TD style="WIDTH: 200px" colSpan="3">Afdeling</TD>
						<TD id="Afdeling" runat="server"></TD>
					</tr>
					<tr style="FONT-WEIGHT: bold" vAlign="top">
						<td colSpan="4">&nbsp;&nbsp;</td>
					</tr>
					<!-- FUNCTIE-INHOUD -->
					<tr style="FONT-WEIGHT: bold" vAlign="top">
						<td colSpan="2">Functie-inhoud</td>
					</tr>
					<tr vAlign="top">
						<td colSpan="3">Leidinggevend</td>
						<td id="LeidType" runat="server"></td>
					</tr>
					<tr vAlign="top">
						<td colSpan="3">Volledige vervulling van functie?</td>
						<td id="Voll" runat="server"></td>
					</tr>
					<tr vAlign="top">
						<td colSpan="3">Toelichting indien nee</td>
						<td id="VolT" runat="server"></td>
					</tr>
					<tr vAlign="top">
						<td colSpan="3">Functievervulling op ander niveau?</td>
						<td id="Nive" runat="server"></td>
					</tr>
					<tr vAlign="top">
						<td colSpan="3">Toelichting indien ja</td>
						<td id="NivT" runat="server"></td>
					</tr>
					<tr vAlign="top">
						<td colSpan="3">Bereikte doelen/resultaten voortkomend uit de POP<BR>
							dan wel het vorig<BR>
							functionerings-/beoordelingsgesprek</td>
						<td id="Bere" runat="server"></td>
					</tr>
					<tr vAlign="top">
						<td colSpan="4">&nbsp;</td>
					</tr>
					<!-- SCORES -->
					<tr style="FONT-WEIGHT: bold" vAlign="top">
						<td colSpan="4">Beoordeling</td>
					</tr>
					<tr style="TEXT-DECORATION: underline" vAlign="top">
						<td>Competentie</td>
						<td>Element</td>
						<td>Score</td>
						<td style="WIDTH: 100%">Toelichting</td>
					</tr>
					<% Response.Write(Response_Scores(true)); %>
				</table>
				<br class="PageBreak">
				<!-- AFSPRAKEN -->
				<table class="DROTable_report">
					<tr style="FONT-WEIGHT: bold" vAlign="top">
						<td colSpan="4">Afspraken</td>
					</tr>
					<% Response.Write(Response_Afspraken()); %>
				</table>
				<table class="DROTable_report">
					<tr>
						<td><br>
							<br>
							<asp:label Font-Size="7" id="lblAfdrukdatum" runat="server" Height="10px" Width="199px">Afgedrukt: ddmmjjjj</asp:label></td>
					</tr>
				</table>
			</div>
			<div id="leftnav" style="Z-INDEX: 103"><span class="NoPrint"><% Response.Write(Write_Menu_beoRapport_bespreking());%></span></div>
			<div id="rightnav" style="Z-INDEX: 104"><IMG class="NoPrint" id="printbutton" onclick="PrintIt();" height="11" alt="Print vriendelijke versie" src="../Pics/buttons/print.gif" width="15"></div>
		</form>
	</body>
</HTML>
