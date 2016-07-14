<%@ Page language="c#" AutoEventWireUp="false" Codebehind="behImport Excel.aspx.cs" Inherits="PO_Beoordeling.behImport_Excel" %>
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
					<td class="titelbalk" colSpan="2">Import vanuit Excel bestand</td>
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
						<form id="Form1" method="post" runat="server" enctype="multipart/form-data">
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
									<TD colSpan="2" height="52" style="HEIGHT: 52px"></TD>
									<TD style="HEIGHT: 52px">
										<asp:label id="lblFile" runat="server" Font-Bold="True">Excel bestand</asp:label></TD>
									<TD colSpan="2" style="HEIGHT: 52px">
										<input id="inpFile" type="file" runat="server" size="85" style="WIDTH: 625px; HEIGHT: 22px"></TD>
								</TR>
								<TR vAlign="top">
									<TD colSpan="3" height="25"></TD>
									<TD>
										<asp:Button id="btnLoad" runat="server" Text="Data laden in database"></asp:Button></TD>
								</TR>
								<TR>
									<TD colSpan="4">&nbsp;</TD>
								</TR>
								<TR vAlign="top">
									<TD colSpan="3" height="25"></TD>
									<TD>
										<asp:Label id="lbStatus" runat="server" Width="110px" ForeColor="Green" Visible="False" Height="16px">Importeren ...</asp:Label></TD>
								</TR>
							</TABLE>
						</form>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD width="0"></TD>
					<TD></TD>
					</TD>
				</TR>
			</TABLE>
		</div>
	</body>
</HTML>
