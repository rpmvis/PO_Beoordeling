<%@ Page language="c#" Codebehind="tTL.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.tTL" %>
<HTML>
	<HEAD>
		<title>tTL</title>
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
		<form id="tTL" method="post" runat="server">
			<div id="top_leftalign">
				<table>
					<tr>
						<td align="right" colSpan="3"><span class="NoPrint"></span></td>
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
						<td class="titelbalk" colSpan="2">Tabel tTL</td>
					</tr>
				</table>
			</div>
			<div id="content_beheerder_leftalign"><asp:dropdownlist id="ddlSelGridItem" runat="server" AutoPostBack="True" Width="300px"></asp:dropdownlist>&nbsp;&nbsp;<br>
				<br>
				<asp:datagrid id="_grid" runat="server" CssClass="dg" ShowFooter="True" CellPadding="1" BorderWidth="1px" BorderColor="#AEC2DD" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False">
					<AlternatingItemStyle CssClass="dgAltItem"></AlternatingItemStyle>
					<ItemStyle CssClass="dgItem"></ItemStyle>
					<HeaderStyle CssClass="dgHeader"></HeaderStyle>
					<FooterStyle CssClass="dgFooter"></FooterStyle>
					<Columns>
						<ASP:TemplateColumn FooterStyle-VerticalAlign="Middle" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="50px" FooterStyle-Width="50px">
							<ItemStyle Width="30px"></ItemStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderTemplate>
								<IMG height="1" src="../Pics/Buttons/n.gif" width="50">
							</HeaderTemplate>
							<ItemTemplate>
								<ASP:ImageButton id="_edtButton" runat="server" ImageAlign="Left" CommandName="Edit" ImageUrl="../Pics/Buttons/Edit.gif" AlternateText="Wijzigen" />
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Center"></FooterStyle>
							<FooterTemplate>
								<ASP:ImageButton id="_insButton" runat="server" ImageAlign="Left" CommandName="_ins" ImageUrl="../Pics/Buttons/Insert.gif" AlternateText="Toevoegen" />
								<ASP:ImageButton id="_ins_updateButton" runat="server" ImageAlign="Left" CommandName="_insUpdate" ImageUrl="../Pics/Buttons/Update.gif" AlternateText="OK" Visible="False" />
								<ASP:ImageButton id="_ins_cancelButton" runat="server" ImageAlign="Left" CommandName="_insCancel" ImageUrl="../Pics/Buttons/Cancel.gif" AlternateText="Annuleren" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:ImageButton id="_edt_updateButton" runat="server" ImageAlign="Left" CommandName="_edtUpdate" ImageUrl="../Pics/Buttons/Update.gif" AlternateText="OK" />
								<ASP:ImageButton id="_edt_cancelButton" runat="server" ImageAlign="Left" CommandName="_edtCancel" ImageUrl="../Pics/Buttons/Cancel.gif" AlternateText="Annuleren" /><BR>
								<BR>
								<ASP:ImageButton id="_delButton" runat="server" ImageAlign="Left" CommandName="_del" ImageUrl="../Pics/Buttons/Delete.gif" AlternateText="Verwijderen" />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='Msnr' HeaderStyle-Width="0" SortExpression='TLMsnr'>
							<ItemTemplate>
								<ASP:Label id=lblTLMsnr Visible =True Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "TLMsnr") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insTLMsnr' runat='server' Visible='False' Width="100%" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtTLMsnr' runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "TLMsnr") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='TLNaam' SortExpression='TLNaam'>
							<ItemTemplate>
								<ASP:Label id="Label1" Visible =True Enabled=False runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "TLNaam") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="Textbox1" runat='server' Width="0" Enabled="False" Visible='False' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id="Textbox2" runat='server' Enabled =False Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "TLNaam") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='TLGebruikersNaam' SortExpression='TLGebruikersNaam'>
							<ItemTemplate>
								<ASP:Label id=lblTLGebruikersNaam Visible =True Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "TLGebruikersNaam") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insTLGebruikersNaam' runat='server' Visible='False' Width='100%' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtTLGebruikersNaam' runat='server' Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "TLGebruikersNaam") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='TLWachtwoord' SortExpression='TLWachtwoord'>
							<ItemTemplate>
								<ASP:Label id=lblTLWachtwoord Visible =True Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "TLWachtwoord") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insTLWachtwoord' runat='server' Visible='False' Width='100%' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtTLWachtwoord' runat='server' Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "TLWachtwoord") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
					</Columns>
					<PagerStyle CssClass="dgPager" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
		</form>
	</body>
</HTML>
