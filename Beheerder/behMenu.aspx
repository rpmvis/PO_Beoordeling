<%@ Page language="c#" Codebehind="behMenu.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.behMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Inloggen</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/BEOstyles.css" type="text/css" rel="stylesheet" media="screen">
		<LINK href="../css/BEOstyles_print.css" type="text/css" rel="stylesheet" media="print">
		<script language="JavaScript" src="../js/General.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/RPC.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			function init(){
				MaxWindow();
			}

			function GotoPage(sPage)
			{
				// 1) (on)zichtbaar maken van knop "refmenu" in frame "frameHeader"
				var el = getFrEl("frameHeader", "refMenu", true);
				if (el !== null){
					el.style.visibility = "visible";
				}
				
				// 2) navigeer naar pagina
				if(sPage !== null){
				 	document.location = sPage;
				}
			}
		</script>
	</HEAD>
	<BODY class='body' onload="init();" MS_POSITIONING="FlowLayout">
		<form method="post" runat='server'>
			<div id="top_leftalign">
				<table>
					<tr>
						<td colspan="3" align="right">
							<span class="NoPrint"></span>
						</td>
					</tr>
					<tr>
						<td rowspan="3" valign="top"><IMG class="DROpict" alt="logo Gemeente Amsterdam" src="../Pics\logo\menu_logo.gif" align="left"></td>
						<td colspan="2" valign="top"><IMG class="DROpict" alt="logo DRO" src="../Pics\headers\amsterdamdro_home_logo.gif"><span class="appname">Beoordelingsprogramma</span></td>
					</tr>
					<tr height="100%">
						<td width="100%"><% Response.Write(Write_Menu_beheer());%></td>
						<td><IMG class="NoPrint" id="printbutton" onclick="PrintIt();" alt="Print vriendelijke versie" src="../Pics/buttons/print.gif">
						</td>
					</tr>
					<tr>
						<td class="titelbalk" colspan="2">Menu&nbsp;beheerder</td>
					</tr>
				</table>
			</div>
			<div id="content_beheerder_leftalign">
				<table class="DROtable" cellSpacing="0" cellPadding="2" border="0">
					<TR>
						<TD colSpan="3"></TD>
					</TR>
					<tr>
						<td colSpan="3"><b>Tabellen</b></td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/Table.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/tblBeo.aspx');">tblBeo</A></td>
						<td>Beoordelingen; met deze&nbsp;tabel <FONT color="darkviolet">ontdooien</FONT></td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/Table.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/tblBeoScore.aspx');">tblBeoScore</A></td>
						<td>Scores per beoordeling</td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/Table.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/tWerkn.aspx');">tWerkn</A></td>
						<td>Werknemers; <FONT color="darkviolet">nieuwe werknemer</FONT> invoeren</td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/Table.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/tTL.aspx');">tTL</A></td>
						<td>Teamleiders, gebruikersnamen en wachtwoorden</td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/Table.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/tTL_Afde_Peri.aspx');">tTL_Afde_Peri</A></td>
						<td>Teamleiders per afdeling en periode</td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/Table.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/tAfde.aspx');">tAfde</A></td>
						<td>Afdelingen</td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/Table.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/tFunc.aspx');">tFunc</A></td>
						<td>Functies</td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/Table.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/tProf.aspx');">tProf</A></td>
						<td>Profielen voor beoordeling</td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/Table.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/tProf_Comp.aspx');">tProf_Comp</A></td>
						<td>Competenties per profiel</td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/Table.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/tComp.aspx');">tComp</A></td>
						<td>Competenties</td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/Table.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/tElem.aspx');">tElem</A></td>
						<td>Beoordelingselementen</td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/Table.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/tPenO.aspx');">tPenO</A></td>
						<td>Personen van afdeling P&amp;O, belast met functioneel en 
							technisch&nbsp;beheer&nbsp;</td>
					</tr>
					<tr>
						<td colSpan="3"><b></b></td>
					</tr>
					<tr>
						<td colSpan="3"><b>Rapporten</b></td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/Report.jpg"></td>
						<td><A href="Javascript: GoTo('./Beheerder/behRapport_onvoldoendes.aspx');">Rapport 
								onvold.</A></td>
						<td>Rapport onvoldoende beoordelingen</td>
					</tr>
					<tr>
						<td colSpan="3"><b></b></td>
					</tr>
					<tr>
						<td colSpan="3"><b>Import</b></td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/TableImport.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/behImport XML.aspx');">Importeer 
								XML&nbsp;bestand</A></td>
						<td>
							<P>Import vanuit&nbsp;XML bestand</P>
						</td>
					</tr>
					<tr>
						<td><IMG src="../Pics/Buttons/TableImport.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/behImport TXT.aspx');">Importeer 
								Tekst&nbsp;bestand</A></td>
						<td>
							<P>Import vanuit&nbsp;tekst bestand</P>
						</td>
					</tr>
					<!--
					<tr>
						<td></td>
						<td><A href="Javascript: GoTo('./Beheerder/Test2.aspx');">Test</A></td>
						<td>
							<P>Doe een test</P>
						</td>
					</tr>
					-->
					<!--
					<tr>
						<td><IMG src="../Pics/Buttons/TableImport.gif"></td>
						<td><A href="Javascript: GoTo('./Beheerder/behImport Excel.aspx');">Excel import</A></td>
						<td>Import vanuit Excel bestand</td>
					</tr>
					-->
				</table>
			</div>
		</form>
	</BODY>
</HTML>
