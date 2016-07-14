<%@ Page language="c#" Codebehind="beoInvoer_Functie.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.Invoer_Functie" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Invoer functie</title> 
		<!--Dit is een tekst van 255 karakters lang. Dit is een tekst van 255 karakters lang. Dit is een tekst van 255 karakters lang. Dit is een tekst van 255 karakters lang. Dit is een tekst van 255 karakters lang. Dit is een tekst van 255 karakters lang. Dit is ee-->
<meta content="Microsoft Visual Studio 7.0" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK media=screen href="../css/BEOstyles.css" type=text/css rel=stylesheet ><LINK media=print href="../css/BEOstyles_print.css" type=text/css rel=stylesheet >
<script language=JavaScript src="../js/General.js" type=text/javascript></script>

<script language=JavaScript src="../js/RPC.js" type=text/javascript></script>

<script language=JavaScript src="../js/beoValidation.js" type=text/javascript></script>

<script language=javascript type=text/javascript>
			function init()
			{
				document.getElementById("lstVoll").click(); // triggert 'enable_VolT'
				document.getElementById("lstNive").click(); // triggert 'enable_NivT'
			}

			function enable_VolT(value)
			{
				var bEna = (value !== "ja");
				if (bEna) ChangeElClass("VolT", "inputON");
				else ChangeElClass("VolT", "inputOFF");
				enable("VolT", bEna);
			}

			function enable_NivT(value)
			{
				var bEna = (value === "ja");
				if (bEna) ChangeElClass("NivT", "inputON");
				else ChangeElClass("NivT", "inputOFF");
				enable("NivT", bEna);
			}
		</script>
</HEAD>
<body class=body onload=init(); 
MS_POSITIONING="GridLayout">
<form id=frmFunctie method=post runat="server">
<div id=top>
<table>
  <tr>
    <td class=NoPrint align=right colSpan=3><span 
      class=NoPrint>
								<% Response.Write(Write_Menu_beoAlgemeen());%>
							</SPAN></TD></TR>
  <tr>
    <td vAlign=top rowSpan=3><IMG class=DROpict alt="logo Gemeente Amsterdam" src="../Pics\logo\menu_logo.gif" align=left ></TD>
    <td vAlign=top><IMG class=DROpict alt="logo DRO" src="../Pics\headers\amsterdamdro_home_logo.gif" ></TD>
    <td class=appname align=right width="100%" 
      >Beoordelingsprogramma</TD></TR>
  <tr height="100%">
    <td id=list3 vAlign=bottom colSpan=2><span 
      class=NoPrint>
								<% Response.Write(Write_Menu_TL());%>
							</SPAN></TD></TR>
  <tr>
    <td class=titelbalk colSpan=2 
  >Functie-inhoud</TD></TR></TABLE></DIV>
<div id=content>
<table class=DROTable cellSpacing=0 cellPadding=0>
  <tr>
    <td><span class=MFkopbreedWit 
      >Functie-inhoud (2/4)</SPAN> </TD></TR>
  <tr>
    <td>
      <table class=DROTable_relativewidth cellSpacing=0 cellPadding=0 
      >
        <tr>
          <td class=TD_kop_blauw colSpan=2>Algemeen: </TD>
        <tr vAlign=top>
          <td class=TD_rijkop><span style="COLOR: red" 
            >*&nbsp;</SPAN>Functie</TD>
          <td><input id=Functie 
            style="WIDTH: 149px; HEIGHT: 22px" type=text size=19 
             runat="server" Height="22px" 
        Width="219px"></TD></TR>
        <tr>
          <td>&nbsp;</TD></TR>
        <tr vAlign=top>
          <td class=TD_rijkop vAlign=top><span 
            style="COLOR: red" 
          >*&nbsp;</SPAN>Leidinggevend</TD>
          <td class=TD2_rijkop><asp:radiobuttonlist id=lstProf runat="server" Height="34px" Width="137px" CssClass="RadioButtonList">
											<asp:ListItem Value="NietLeid">niet&nbsp;van&nbsp;toepassing</asp:ListItem>
											<asp:ListItem Value="FuncLeid">alleen&nbsp;functioneel</asp:ListItem>
											<asp:ListItem Value="HierFuncLeid">hiërarchisch&nbsp;en &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;functioneel</asp:ListItem>
										</asp:radiobuttonlist></TD></TR>
        <tr>
          <td>&nbsp;</TD></TR>
        <tr>
          <td class=TD_rijkop><span style="COLOR: red" 
            >*&nbsp;</SPAN>Was er sprake van een<BR 
            >volledige vervulling<BR 
            >van de functie?</TD>
          <td><asp:radiobuttonlist id=lstVoll runat="server" CssClass="RadioButtonList" AutoPostBack="False">
											<asp:ListItem Value="ja">ja</asp:ListItem>
											<asp:ListItem Value="nee">nee</asp:ListItem>
										</asp:radiobuttonlist></TD>
          <td class=TD2_rijkop style="WIDTH: 114px">Zo 
            nee,<BR>welke<BR 
            >werkzaamheden<br>werden 
            verricht?</TD>
          <td><TEXTAREA class=inputOFF id=VolT onkeyup="maxLen('VolT', 255);" rows=3 cols=24 runat="server"></TEXTAREA> 
          </TD></TR>
        <tr>
          <td>&nbsp;</TD></TR>
        <tr>
          <td class=TD_rijkop><span style="COLOR: red" 
            >*&nbsp;</SPAN>Was er sprake van een<BR 
            >functievervulling<BR>op 
            een ander niveau<br>dan de typering 
aangeeft?</TD>
          <td><asp:radiobuttonlist id=lstNive runat="server" CssClass="RadioButtonList" AutoPostBack="False">
											<asp:ListItem Value="ja">ja</asp:ListItem>
											<asp:ListItem Value="nee">nee</asp:ListItem>
										</asp:radiobuttonlist></TD>
          <td class=TD2_rijkop style="WIDTH: 114px">Zo 
            ja, welke<BR>werkzaamheden betrof dit?<br 
            >Geef ook het aandeel<BR>in 
            de tijd aan.</TD>
          <td><TEXTAREA class=inputOFF id=NivT onkeyup="maxLen('VolT', 255);" name=Textarea1 rows=3 cols=24 runat="server"></TEXTAREA></TD></TR></TABLE></TD></TR>
  <tr>
    <td></TD></TR>
  <tr>
    <td>
      <table class=DROTable_relativewidth cellSpacing=0 cellPadding=0 
      >
        <tr>
          <td class=TD_kop_blauw style="WIDTH: 564px" colSpan=2 
          >Bereikte doelen uit het vorig functionerings- / 
            beoordelingsgesprek </TD>
        <tr>
									<!-- <td class="TD_rijkop" style="WIDTH: 120px; HEIGHT: 45px" vAlign="top">&nbsp;
									</td> -->
          <td style="WIDTH: 564px; HEIGHT: 45px" colSpan=2><TEXTAREA class=inputON id=Bere onkeyup="maxLen('VolT', 255);" style="WIDTH: 580px; HEIGHT: 60px" name=Textarea1 rows=3 cols=70 runat="server"></TEXTAREA> 
          </TD></TR></TABLE></TD></TR>
  <tr>
    <td>&nbsp;</TD></TR>
  <tr>
    <td class=TD_rijkop colSpan=3><span 
      style="COLOR: red">*</SPAN> = verplichte invoer 
    <td></TD></TR></TABLE></DIV>
<div id=leftnav><span class=NoPrint><% Response.Write(Write_Menu_beoInvoer());%></SPAN></DIV>
<div id=rightnav><IMG class=NoPrint id=printbutton onclick=PrintIt(); height=11 alt="Print vriendelijke versie" src="../Pics/buttons/print.gif" width=15 ></DIV></FORM>
	</body>
</HTML>
