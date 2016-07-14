<%@ Page language="c#" Codebehind="beoBeoordelingen.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.Beoordelingen" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Beoordelingen</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/BEOstyles.css" type="text/css" rel="stylesheet" media="screen">
		<LINK href="../css/BEOstyles_print.css" type="text/css" rel="stylesheet" media="print">
		<script language="JavaScript" src="../js/General.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/RPC.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/beoValidation.js" type="text/javascript"></script>
		<script language="javascript" id="clientEventHandlersJS">
			var msLBnum = '';
		
			function Bevriezen()
			{
				var i = getEl("ListBox1").selectedIndex; 
				if (i==-1){
					alert("Selecteer eerst een persoon uit de lijst 'Invoer'!");
					return;
				}
				
				if (getVal("txtInvoerKlaar") !== 'ja'){
					alert("U bent nog niet klaar met het invullen van de beoordeling!");
					return;
				}

				var sText = getEl("ListBox1").options[i].text;

				var	sQuestion = "Wilt u de beoordeling van '" + sText + "' nu bevriezen?\n\n" +
											  "Ontdooien is slechts bij uitzondering mogelijk via de technisch contactpersoon,\nzie menu-item 'Contact'.\n\n";
  			if (confirm(sQuestion)== true){
					getEl("btnAdd2").click();
				}
			}

			function NextPage(elId, sNextPage){
				var sListBoxName = "";
				var sLijst = "";
				switch (elId){
					case "btnInvoer":
						sListBoxName = "ListBox1";
						sLijst = "In beoordeling";
						break;
					case "btnRapport_bespreking":
						sListBoxName = "ListBox1";
						sLijst = "In beoordeling";
						break;
					case "btnRapport":
						sListBoxName = "ListBox2";
						sLijst = "Bevroren"	;
						break;
				}
				
				var i = getEl(sListBoxName).selectedIndex; 
				if (i==-1){
					alert("Selecteer eerst een persoon uit de lijst '" + sLijst + "'!");
					return;
				}
				sBeoId = getEl(sListBoxName).options[i].value;

				// opslaan BeoId van geselecteerd persoon uit een van de lijsten
				var arrFV = new Array();
				arrFV[0] = ['beoid', sBeoId];
				FVs_ToServer_Plus_GoTo(arrFV, sNextPage);
				return true;
			}

		</script>
	</HEAD>
	<body class="body" MS_POSITIONING="GridLayout">
		<form id="Beoordelingen" method="post" runat="server">
			<div id="top">
				<table>
					<tr>
						<td align="right" class="NoPrint" colSpan="3">
							<span class="NoPrint">
								<% Response.Write(Write_Menu_beoAlgemeen());%>
							</span>
						</td>
					</tr>
					<tr>
						<td vAlign="top" rowSpan="3"><IMG class="DROpict" alt="logo Gemeente Amsterdam" src="../Pics\logo\menu_logo.gif" align="left"></td>
						<td vAlign="top"><IMG class="DROpict" alt="logo DRO" src="../Pics\headers\amsterdamdro_home_logo.gif"></td>
						<td class="appname" align="right" width="100%">Beoordelingsprogramma</td>
					</tr>
					<tr height="100%">
						<td id="list3" vAlign="bottom" colSpan="2">
							<span class="NoPrint">
								<% Response.Write(Write_Menu_TL());%>
							</span>
						</td>
					</tr>
					<tr>
						<td class="titelbalk" colSpan="2">Beoordelingen</td>
					</tr>
				</table>
			</div>
			<div id="content">
				<table width="100%">
					<tr>
						<td vAlign="top"><span class="MFkopbreedWit">Overzicht&nbsp;beoordelingen</span>
						</td>
						<td align="right" width="100%">
							<table class="DROTable_relativewidth" cellSpacing="0" cellPadding="0">
								<tr>
									<td class="TD_kop">Periode</td>
									<td id="txtPeri" runat="server"></td>
								</tr>
								<tr>
									<td class="TD_kop">Teamleider</td>
									<td id="txtNaam" runat="server"></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<asp:textbox id="txtInvoerKlaar" style="Z-INDEX: 102; LEFT: 456px; POSITION: absolute; TOP: 60px" runat="server" Width="0px" Height="0px"></asp:textbox><br>
				<table class="DROTable">
					<tr vAlign="top">
						<td class="TD_kop"><STRONG>Te beoordelen</STRONG></td>
						<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
						<td class="TD2_kop"><STRONG>In beoordeling</STRONG></td>
						<td></td>
						<td class="TD2_kop"><STRONG>Bevroren</STRONG></td>
					</tr>
					<tr vAlign="top">
						<td class="TD"><asp:listbox id="ListBox0" runat="server" Width="166px" Height="256px"></asp:listbox></td>
						<td class="TD2"><asp:button id="btnAdd1" runat="server" Width="28px" ToolTip="Selecteer persoon voor Invoer beoordeling" Text=">"></asp:button><BR>
							<BR>
							<asp:button id="btnRemove1" runat="server" Width="28px" ToolTip="De-selecteer persoon voor Invoer beoordeling" Text="<"></asp:button></td>
						<td class="TD2"><asp:listbox id="ListBox1" runat="server" Width="166px" Height="256px" AutoPostBack="True"></asp:listbox></td>
						<td class="TD2"><INPUT id="btnBevriezen" style="WIDTH: 101px; HEIGHT: 27px" onclick="Bevriezen();" type="button" value="> Bevriezen" name="btnBevriezen"><BR>
							<BR>
						</td>
						<asp:button id="btnAdd2" runat="server" Width="0px" Height="0px" ToolTip="Bevries Invoer beoordeling" Text="> bevriezen" BackColor="Transparent" ForeColor="Transparent" BorderStyle="None"></asp:button>
						<td class="TD2"><asp:listbox id="ListBox2" runat="server" Width="166px" Height="256px"></asp:listbox></td>
					</tr>
					<tr>
						<td class="TD">
						<td></td>
						<td vAlign="top" align="left"><INPUT id="btnInvoer" style="WIDTH: 86px; HEIGHT: 27px" onclick="NextPage(this.id, 'Gebruiker/beoInvoer_Voorblad.aspx');" type="button" value="Invoer" name="btnInvoer"><br>
							<br>
							<INPUT id="btnRapport_bespreking" style="WIDTH: 156px; HEIGHT: 27px" onclick="NextPage(this.id, 'Gebruiker/beoRapport_bespreking.aspx');" type="button" value="Rapport ter bespreking" name="btnRapport_bespreking"></td>
						<td></td>
						<td vAlign="top"><INPUT id="btnRapport" style="WIDTH: 86px; HEIGHT: 27px" onclick="NextPage(this.id, 'Gebruiker/beoRapport.aspx');" type="button" value="Rapport" name="btnRapport"></td>
					</tr>
				</table>
			</div>
			<div id="leftnav"><span class="NoPrint"></span></div>
			<div id="rightnav"><img id="printbutton" class="NoPrint" onclick="PrintIt();" alt="Print vriendelijke versie" src="../Pics/buttons/print.gif"></div>
		</form>
	</body>
</HTML>
