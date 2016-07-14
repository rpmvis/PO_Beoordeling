<%@ Page language="c#" Codebehind="beoAlgemeen_CompetentieLijst.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.CompetentieLijst" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CompetentieLijst</title>
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
		<form id="CompetentieLijst" method="post" runat='server'>
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
						<td class="titelbalk" colspan="2">CompetentieLijst</td>
					</tr>
				</table>
			</div>
			<div id="content">
				<% Response.Write(GetLijst()); %>
			</div>
			<div id="leftnav"><span class="NoPrint"><% Response.Write(Write_Menu_verticaal_hist());%></span></div>
			<div id="rightnav"><img id="printbutton" class="NoPrint" onclick="PrintIt();" alt="Print vriendelijke versie" src="../Pics/buttons/print.gif"></div>
		</form>
	</body>
</HTML>
