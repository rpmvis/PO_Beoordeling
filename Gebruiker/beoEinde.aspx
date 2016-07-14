<%@ Page language="c#" Codebehind="beoEinde.aspx.cs" EnableSessionState="true" AutoEventWireup="false" Inherits="PO_Beoordeling.Einde_beheerder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title class="wit">Einde</title>
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
	<body class="body" language="javascript">
		<FORM id="Einde" method="post" runat="server">
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
							<% Response.Write(Write_Menu_TL());%>
						</td>
					</tr>
					<tr>
						<td class="titelbalk" colspan="2">Einde</td>
					</tr>
				</table>
			</div>
			<div id="content">
				<table class="DROtable" id="Table2" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; POSITION: relative; BORDER-BOTTOM-STYLE: none">
					<tr>
						<td><span class="MFkopbreedWit">Einde Beoordelingsprogramma DRO</span>
						</td>
					</tr>
					<tr>
						<td class="TD"><% Response.Write(GetEinde()); %>
						</td>
					</tr>
					<tr>
						<td align="right">
							&nbsp;
						</td>
					</tr>
					<tr>
						<td class="TD">
							<INPUT id="btnToSiteAdam" style="WIDTH: 107px; CURSOR: hand; HEIGHT: 24px" onclick="CloseApp();" type="button" value="Website DRO" name="btnToSiteAdam">
						</td>
					</tr>
					<tr>
						<td><br>
						</td>
					</tr>
				</table>
			</div>
			<!---->
			<div id="leftnav"><span class="NoPrint"></span></div>
			<div id="rightnav"><img id="printbutton" class="NoPrint" onclick="PrintIt();" src='../Pics/buttons/print.gif' alt='Print vriendelijke versie'></div>
		</FORM>
	</body>
</HTML>
