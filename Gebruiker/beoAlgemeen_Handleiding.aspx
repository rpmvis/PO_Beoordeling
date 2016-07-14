<%@ Page language="c#" Codebehind="beoAlgemeen_Handleiding.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.Handleiding" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Handleiding</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK media="screen" href="../css/BEOstyles.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../css/BEOstyles_print.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../js/General.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/RPC.js" type="text/javascript"></script>
		<script language="JavaScript" src="../js/beoValidation.js" type="text/javascript"></script>
		<style> .scherm { color: blue; }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Handleiding" method="post" runat="server">
			<div id="top">
				<table>
					<tr>
						<td align="right" colSpan="3"><span class="NoPrint">
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
						<td vAlign="bottom" colSpan="2"><span class="NoPrint">
								<% Response.Write(Write_Menu_horizontaal_hist());%>
							</span>
						</td>
					</tr>
					<tr>
						<td class="titelbalk" colSpan="2">Handleiding</td>
					</tr>
				</table>
			</div>
			<div id="content">
				<TABLE class="DROTable_report">
					<TR>
						<TD class="TD" colSpan="2">
							<P><br>
								<b>Handleiding Beoordelingsprogramma DRO
									<BR>
								</b>
								<BR>
								Dit is een handleiding voor&nbsp;teamleiders van DRO 
								die&nbsp;hun&nbsp;teamleden met&nbsp;het Beoordelingsprogramma&nbsp;DRO 
								beoordelen.</P>
							Het is de bedoeling dat u de beoordeling zo volledig mogelijk invult.<BR>
							Na invulling van de&nbsp;beoordeling kunt u:
							<ul>
								<li>
								een Concept Beoordelingsrapport printen. A.d.h.v. dit concept kunt u uw 
								beoordeling met derden bespreken.
								<li>
									een Beoordelingsrapport printen. Dit rapport dient ondertekend te worden door 
									de betrokkenen.
								</li>
							</ul>
							Het Beoordelingsprogramma DRO&nbsp;is alleen via het internet te bereiken en in 
							te vullen. Wanneer u een beoordeling niet in één keer kunt of wilt invullen, 
							kunt u deze later weer opvragen.<BR>
							<br>
							Deze handleiding beschrijft stap-voor-stap het maken van een beoordeling.
						</TD>
					</TR>
					<tr>
						<td class="TD" colspan="2"><hr>
						</td>
					</tr>
					<TR>
						<TD class="TD" style="WIDTH: 69px" vAlign="top"><STRONG>Stap&nbsp;1</STRONG></TD>
						<TD width="100%">
							<P>Na het inloggen komt het <span class="scherm">Welkom</span> scherm.<BR>
								- Klik hier de periode aan waarover u wilt beoordelen<BR>
								- Klik nu op de link <A href="#">Beoordelingen</A></P>
						</TD>
					</TR>
					<tr>
						<td class="TD" colspan="2"><hr>
						</td>
					</tr>
					<TR>
						<TD class="TD" style="WIDTH: 69px" vAlign="top"><STRONG>Stap&nbsp;2</STRONG></TD>
						<TD>
							<P>U bent nu in het scherm <span class="scherm">Beoordelingen</span>.<BR>
								De eerste twee kolommen bevatten de lijsten 'Te beoordelen' en 'In 
								beoordeling'.<BR>
								- Klik op de knop <input style="WIDTH: 28px; TEXT-ALIGN: center" value=">"> (Selecteer 
								persoon) om een persoon uit de lijst 'Te beoordelen' over te zetten naar de 
								lijst 'In beoordeling'. Door op de knop <input style="WIDTH: 28px; TEXT-ALIGN: center" value="<">
								(Deselecteer persoon) te klikken, zet u een persoon weer terug in de lijst 'Te beoordelen'; eventueel ingevulde beoordelingsgegevens blijven hierbij bewaard.</P>
						</TD>
					</TR>
					<tr>
						<td class="TD" colspan="2"><hr>
						</td>
					</tr>
					<TR>
						<TD class="TD" style="WIDTH: 69px" vAlign="top"><STRONG>stap 3</STRONG></TD>
						<TD>- Selecteer (eventueel) de gewenste persoon uit de lijst 'In beoordeling'<BR>
							- klik op de knop <INPUT id="btnInvoer" style="WIDTH: 86px; HEIGHT: 27px" type="button" value="Invoer" name="btnInvoer">. 
							Er verschijnt vervolgens een verticaal&nbsp;menu links op het scherm&nbsp;:<BR>
							<ul class="links_vert" id="Menu_Invoer_vert">
								<li>
									<A class="current" id="MenuItem9" style="COLOR: red" href="#">1. Voorblad</A>
								<li>
									<A href="#">2. Functie-inhoud</A>
								<li>
									<A href="#">3. Scores en toelichtingen</A>
								<li>
									<A href="#">4. Doelen en afspraken</A>
								<li>
									<A href="#">Nog in te vullen</A>
								<li>
									<A href="#">Terug</A></li></ul>
						</TD>
					</TR>
					<tr>
						<td class="TD" colspan="2"><hr>
						</td>
					</tr>
					<TR>
						<TD class="TD" style="WIDTH: 69px" vAlign="top"><STRONG>stap 4</STRONG></TD>
						<TD>
							<P>U bent nu op scherm <span class="scherm">Voorblad (1/4)</span><BR>
								- Vul de&nbsp;items op dit scherm in.<BR>
								<BR>
								Opmerkingen:
							</P>
							<UL>
								<li>
								enkele items ('Persoon' en 'Afdeling') zijn reeds ingevuld en niet meer te 
								wijzigen. Deze items zijn herkenbaar aan de grijze letterkleur.
								<li>
									items voorafgegaan door een rode asterisk <SPAN style="COLOR: red">*</SPAN> zijn 
									verplicht. Zonder deze&nbsp;items kunt u geen (concept) beoordelingsrapport 
									maken.</li></UL>
							<P>- klik op menu-item Functie-inhoud.&nbsp;&nbsp;
							</P>
						</TD>
					</TR>
					<tr>
						<td class="TD" colspan="2"><hr>
						</td>
					</tr>
					<TR>
						<TD class="TD" style="WIDTH: 69px" vAlign="top"><STRONG>stap 5</STRONG></TD>
						<TD>U bent nu op scherm <span class="scherm">Functie-inhoud 2/4</span><BR>
							- Vul de&nbsp;items op dit scherm in (Zie ook laatste opmerking bij stap 4).
						</TD>
					</TR>
					<tr>
						<td class="TD" colspan="2"><hr>
						</td>
					</tr>
					<TR>
						<TD class="TD" style="WIDTH: 69px" vAlign="top"><STRONG>stap 6</STRONG></TD>
						<TD>U bent nu op scherm <span class="scherm">Scores en toelichtingen 3/4</span><BR>
							<P>- klik op alle items van beoordeling een <INPUT type="radio" value="V" name="opt0">&nbsp;<b>V</b>&nbsp;&nbsp;<INPUT id="opt01" type="radio" value="O" name="opt0">&nbsp;<b>O</b>
								een V (Voldoende) of O (Onvoldoende) aan.
								<BR>
								- vul de Toelichting in als u een item als Onvoldoende hebt 
								beoordeeld.&nbsp;&nbsp;<BR>
								-&nbsp;als u op <INPUT class="DRObtnDialog" type="button"> klikt, opent een 
								dialoogscherm waarin u meer ruimte hebt om de toelichting in te vullen.
							</P>
							<P>Opmerking:
							</P>
							<P></P>
							<UL>
								<LI>
									mocht u besluiten om een Onvoldoende te veranderen in een Voldoende en hebt u 
									reeds een toelichting gegeven, dan blijft de toelichting bewaard. Toelichtingen 
									bij voldoendes worden echter <U>niet</U>&nbsp;opgenomen in een (concept) 
									beoordelingsrapport.</LI></UL>
						</TD>
					</TR>
					<tr>
						<td class="TD" colspan="2"><hr>
						</td>
					</tr>
					<TR>
						<TD class="TD" style="WIDTH: 69px" vAlign="top"><STRONG>stap 7</STRONG></TD>
						<TD>U bent nu op scherm <span class="scherm">Doelen en afspraken 4/4</span><BR>
							<P>Als u alleen voldoendes in scherm Scores en toelichtingen 3/4 hebt gegeven, 
								hoeft u hier niets in te vullen. Anders:<BR>
								- vul voor elk als onvoldoende beoordeelde item een Doel en een Afspraak in.<BR>
								-&nbsp;als u op <INPUT class="DRObtnDialog" type="button"> klikt, 
								opent&nbsp;een dialoogscherm waarin u meer ruimte hebt om Doel en Afspraak in 
								te vullen.</P>
						</TD>
					</TR>
					<tr>
						<td class="TD" colspan="2"><hr>
						</td>
					</tr>
					<TR>
						<TD class="TD" style="WIDTH: 69px" vAlign="top"><STRONG>stap 8</STRONG></TD>
						<TD>Controleer met scherm <span class="scherm">Nog in te vullen</span> of u alles 
							ingevuld heeft.<BR>
							Indien u items vergeten heeft,&nbsp;geeft dit scherm hiervan een 
							overzicht.&nbsp;</TD>
					</TR>
					<tr>
						<td class="TD" colspan="2"><hr>
						</td>
					</tr>
					<TR>
						<TD class="TD" style="WIDTH: 69px" vAlign="top"><STRONG>stap 9</STRONG></TD>
						<TD>- klik op <A href="#">Terug</A>om terug te gaan naar het scherm <span class="scherm">
								Beoordelingen</span></TD>
					</TR>
					<tr>
						<td class="TD" colspan="2"><hr>
						</td>
					</tr>
					<TR>
						<TD class="TD" style="WIDTH: 69px" vAlign="top"><STRONG>stap&nbsp;10</STRONG></TD>
						<TD>
							<P>Als u alles hebt ingevuld, kunt u een Concept Beoordelingsrapport opvragen.<BR>
								Klik op&nbsp;<INPUT style="WIDTH: 156px; HEIGHT: 27px" type="button" value="Rapport ter bespreking"><BR>
								U krijgt een melding als de beoordeling nog niet volledig is.<BR>
								Anders krijgt u op scherm de inhoud van het Concept Beoordelingsrapport te 
								zien.<BR>
								<BR>
								- gebruik de knop <IMG alt="Print vriendelijke versie" src="../Pics/buttons/print.gif">
								voor een Print vriendelijke versie van het rapport.
								<BR>
							</P>
						</TD>
					</TR>
					<tr>
						<td class="TD" colspan="2"><hr>
						</td>
					</tr>
					<TR>
						<TD class="TD" style="WIDTH: 69px" vAlign="top"><STRONG>stap 11</STRONG></TD>
						<TD>Als u alles hebt ingevuld, kunt u een de beoordeling bevriezen.<BR>
							- klik op <INPUT style="WIDTH: 101px; HEIGHT: 27px" type="button" value="> Bevriezen">
							<P>Mocht de beoordeling niet volledig zijn dan krijgt u hiervan een melding.<BR>
								Anders krijgt U de volgende vraag (verkort):<BR>
								"Hebt u de 1e beoordelaar (uzelf) en beoordeelde de schriftelijke beoordeling 
								laten tekenen?<BR>
								en wilt u de beoordeling nu bevriezen? Ontdooien is slechts bij uitzondering 
								mogelijk via de technisch contactpersoon".<BR>
								<BR>
								Als u deze vraag met "Ja" beantwoordt, wordt de beoordeling bevroren en kunt u 
								deze niet meer wijzigen.
							</P>
						</TD>
					</TR>
					<tr>
						<td class="TD" colspan="2"><hr>
						</td>
					</tr>
					<TR>
						<TD class="TD" style="WIDTH: 69px" vAlign="top"><STRONG>stap 12</STRONG></TD>
						<TD>- Selecteer een persoon&nbsp;uit de lijst 'Bevroren'<BR>
							- klik op de knop <INPUT style="WIDTH: 86px; HEIGHT: 27px" type="button" value="Rapport"><BR>
							Op scherm ziet u de inhoud van het Beoordelingsrapport.<BR>
							<br>
							- gebruik de knop <IMG alt="Print vriendelijke versie" src="../Pics/buttons/print.gif">
							voor een Print vriendelijke versie van het rapport. Dit rapport kunt u 
							aanbieden aan betrokkenen voor ondertekening.&nbsp;
						</TD>
					</TR>
				</TABLE>
			</div>
			<div id="leftnav"><span class="NoPrint"><% Response.Write(Write_Menu_verticaal_hist());%></span></div>
			<div id="rightnav"><IMG class="NoPrint" id="printbutton" onclick="PrintIt();" height="11" alt="Print vriendelijke versie" src="../Pics/buttons/print.gif" width="15"></div>
		</form>
	</body>
</HTML>
