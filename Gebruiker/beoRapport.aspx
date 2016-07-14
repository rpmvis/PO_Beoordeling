<%@ Page language="c#" Codebehind="beoRapport.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.Beoordelingsrapport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Beoordelingsrapport</title>
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
	<body class="body" bgColor="white" MS_POSITIONING="GridLayout">
		<form id="Beoordelingsrapport" method="post" runat="server">
			<div id="top">
				<table>
					<tr>
						<td align="right" colSpan="3"><span class="NoPrint"><% Response.Write(Write_Menu_beoAlgemeen());%></span>
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
						<td class="titelbalk" style="WIDTH: 242px">Beoordelingsrapport&nbsp;
						</td>
						<td class="titelbalk" id="hdrPeriode" runat="server" align="right">periode
						</td>
					</tr>
				</table>
			</div>
			<div id="content">
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
					<% Response.Write(Response_Scores(false)); %>
				</table>
				<br>
				<!-- AFSPRAKEN -->
				<table class="DROTable_report">
					<tr style="FONT-WEIGHT: bold" vAlign="top">
						<td colSpan="4">Afspraken</td>
					</tr>
					<% Response.Write(Response_Afspraken()); %>
				</table>
				<BR class="PageBreak">
				<TABLE class="DROTable_report" style="HEIGHT: 89px" borderColor="gray" border="1">
					<tr style="FONT-WEIGHT: bold" vAlign="top">
						<TD style="WIDTH: 117px">Opgemaakt door</TD>
						<TD>Naam</TD>
						<TD>Datum</TD>
						<TD>Handtekening</TD>
					</tr>
					<tr vAlign="top">
						<TD style="WIDTH: 117px">1e beoordelaar<BR>
							&nbsp;
						</TD>
						<TD id="Beoordelaar1" runat="server">&nbsp;</TD>
						<TD width="60">&nbsp;</TD>
						<TD width="300">&nbsp;</TD>
					</tr>
					<tr vAlign="top">
						<TD style="WIDTH: 117px">2e beoordelaar<BR>
							&nbsp;
						</TD>
						<TD id="Beoordelaar2" runat="server">&nbsp;</TD>
						<TD>&nbsp;</TD>
						<TD>&nbsp;</TD>
					</tr>
					<tr vAlign="top">
						<TD style="WIDTH: 117px">P&amp;O adviseur<BR>
							&nbsp;
						</TD>
						<TD id="PenOadviseur" runat="server">&nbsp;</TD>
						<TD>&nbsp;</TD>
						<TD>&nbsp;</TD>
					</tr>
					<tr vAlign="top">
						<TD colSpan="2">Gezien beoordeelde<BR>
							&nbsp;
						</TD>
						<TD>&nbsp;</TD>
						<TD>&nbsp;</TD>
					</tr>
				</TABLE>
				<br>
				<TABLE class="DROTable_report" borderColor="gray" border="1">
					<tr style="FONT-WEIGHT: bold" vAlign="top" borderColor="white">
						<TD>Commentaar medewerk(st)er</TD>
					</tr>
					<tr vAlign="top" borderColor="gray">
						<TD style="HEIGHT: 36pt">&nbsp;</TD>
					</tr>
				</TABLE>
				<br>
				<TABLE class="DROTable_report" borderColor="gray" border="1">
					<tr style="FONT-WEIGHT: bold" vAlign="top" borderColor="white">
						<TD>Advies P&amp;O</TD>
					</tr>
					<tr vAlign="top" borderColor="gray">
						<TD style="HEIGHT: 36pt">&nbsp;</TD>
					</tr>
				</TABLE>
				<br>
				<TABLE class="DROTable_report" borderColor="gray" border="1">
					<tr style="FONT-WEIGHT: bold" vAlign="top" borderColor="white">
						<TD colSpan="2">Directeur DRO</TD>
					</tr>
					<tr vAlign="top" borderColor="gray">
						<TD style="HEIGHT: 36pt">Aldus</TD>
						<td><INPUT id="optGewijzigd" type="radio" value="">&nbsp;gewijzigd<br>
							<INPUT id="optOngewijzigd" type="radio" value="">&nbsp;ongewijzigd
						</td>
						<td width="100%">&nbsp;&nbsp;vastgesteld door de directeur DRO</td>
					</tr>
					<tr vAlign="top" borderColor="gray">
						<TD style="HEIGHT: 36pt">Opmerking</TD>
						<td width="100%" colSpan="2">&nbsp;</td>
					</tr>
					<tr vAlign="top" borderColor="gray">
						<TD style="HEIGHT: 36pt">Datum</TD>
						<td width="100%" colSpan="2">&nbsp;</td>
					</tr>
					<tr vAlign="top" borderColor="gray">
						<TD style="HEIGHT: 36pt">Handtekening</TD>
						<td width="100%" colSpan="2">&nbsp;</td>
					</tr>
				</TABLE>
				<table class="DROTable_report">
					<tr>
						<td><br>
							<br>
							<asp:label Font-Size="7" id="lblAfdrukdatum" runat="server" Height="10px" Width="199px">Afgedrukt: ddmmjjjj</asp:label></td>
					</tr>
				</table>
			</div>
			<div id="leftnav"><span class="NoPrint"><% Response.Write(Write_Menu_beoRapport());%></span></div>
			<div id="rightnav"><IMG class="NoPrint" id="printbutton" onclick="PrintIt();" height="11" alt="Print vriendelijke versie" src="../Pics/buttons/print.gif" width="15"></div>
		</form>
	</body>
</HTML>
