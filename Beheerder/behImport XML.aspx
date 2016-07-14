<%@ Page language="c#" AutoEventWireUp="false" Codebehind="behImport XML.aspx.cs" Inherits="PO_Beoordeling.behImport_XML" %>
<HTML>
	<HEAD>
		<title>Excel import</title>
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
			
			function Doorgaan_met_Import()
			{

				var sQuestion = "Voordat u importeert is het aan te bevelen tabel 'tTL_Afde_Peri' (Teamleiders per afdeling en periode) gevuld te hebben  voor de importperiode.\n\n" +
				                "Dit omdat dan voor de ingevoerde werknemer-teamleiders de afdeling DIRECTIE ingesteld kan worden.\n\n" +
				                "Wilt u doorgaan met importeren?"

  				if (confirm(sQuestion)== true){
						getEl("btnLoad2").click();
					}
			}
			
		</script>
	</HEAD>
	<body class="body" onload="init();">
		<div id="top_leftalign">
			<table>
				<tr>
					<td style="HEIGHT: 1px" align="right" colSpan="3"><span class="NoPrint"></span></td>
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
					<td class="titelbalk" colSpan="2">
						<P>Import vanuit XML bestand</P>
					</td>
				</tr>
			</table>
		</div>
		<div id="content_beheerder_leftalign">
			<TABLE cellSpacing="0" cellPadding="0" width="138" border="0" ms_2d_layout="TRUE">
				<TR>
					<TD width="0" height="0"></TD>
					<TD width="10" height="0"></TD>
					<TD width="128" height="0"></TD>
				</TR>
				<TR vAlign="top">
					<TD width="0" height="15"></TD>
					<TD colSpan="2" rowSpan="2">
						<form id="Form1" method="post" encType="multipart/form-data" runat="server">
							<TABLE height="121" cellSpacing="0" cellPadding="0" width="755" border="0" ms_2d_layout="TRUE">
								<TR vAlign="top">
									<TD width="10" height="15"></TD>
									<TD width="14"></TD>
									<TD width="112"></TD>
									<TD width="32"></TD>
									<TD width="587"></TD>
								</TR>
								<TR vAlign="top">
									<TD height="9"></TD>
									<TD colSpan="4">
										<p></p>
									</TD>
								</TR>
								<TR vAlign="top">
									<TD style="HEIGHT: 52px" colSpan="2" height="52"></TD>
									<TD style="HEIGHT: 52px"><asp:label id="lblFile" runat="server" Font-Bold="True">XML bestand (*.xml)</asp:label></TD>
									<TD style="HEIGHT: 52px" colSpan="2">
										<input id="inpFile" style="WIDTH: 625px; HEIGHT: 22px" type="file" size="85" runat="server"></TD>
								</TR>
								<TR vAlign="top">
									<TD colSpan="3" height="25"></TD>
									<TD><INPUT id="btnLoad" style="WIDTH: 200px; HEIGHT: 27px" onclick="Doorgaan_met_Import();" type="button" value="Data laden in database" name="btnLoad">
										<asp:button id="btnLoad2" runat="server" Width="0px" Height="0px" Text="Data laden in database"></asp:button></TD>
								</TR>
								<TR>
									<TD colSpan="4">
									</TD>
								</TR>
								<TR vAlign="top">
									<TD colSpan="3" height="25"></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</form>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD width="0"></TD>
					<TD></TD>
					</TD></TR>
			</TABLE>
		</div>
	</body>
</HTML>
