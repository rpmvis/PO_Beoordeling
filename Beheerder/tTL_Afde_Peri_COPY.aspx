<%@ Page language="c#" Codebehind="tTL_Afde_Peri_COPY.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.tTL_Afde_Peri_COPY" %>
<HTML>
	<HEAD>
		<title>tTL_Afde_Peri</title>
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
		<form id="tTL_Afde_Peri" method="post" runat="server">
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
						<td class="titelbalk" colSpan="2">Tabel tTL_Afde_Peri</td>
					</tr>
				</table>
			</div>
			<div id="content_beheerder_leftalign"><asp:dropdownlist id="ddlSelPeri" runat="server" Width="100px"></asp:dropdownlist>&nbsp;&nbsp;
				<asp:dropdownlist id="ddlSelGridItem" runat="server" Width="400px"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:button id="btnSelect" runat="server" Width="93px" Text="Select"></asp:button>&nbsp;&nbsp;<br>
				<br>
				<asp:datagrid id="_grid" runat="server" CssClass="dg" ShowFooter="True" CellPadding="1" BorderWidth="1px" BorderColor="#AEC2DD" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False">
					<AlternatingItemStyle CssClass="dgAltItem"></AlternatingItemStyle>
					<ItemStyle CssClass="dgItem"></ItemStyle>
					<HeaderStyle CssClass="dgHeader"></HeaderStyle>
					<FooterStyle CssClass="dgFooter"></FooterStyle>
					<Columns>
						<ASP:TemplateColumn FooterStyle-Width="50px" ItemStyle-Width="50px" ItemStyle-VerticalAlign="Middle" FooterStyle-VerticalAlign="Middle">
							<HeaderStyle Width="30px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderTemplate>
								<IMG height="1" src="../Pics/Buttons/n.gif" width="50px">
							</HeaderTemplate>
							<ItemTemplate>
								<ASP:ImageButton id="_edtButton" runat="server" AlternateText="Wijzigen" ImageUrl="../Pics/Buttons/Edit.gif" CommandName="Edit" ImageAlign="Left" />
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Center"></FooterStyle>
							<FooterTemplate>
								<ASP:ImageButton id="_insButton" runat="server" AlternateText="Toevoegen" ImageUrl="../Pics/Buttons/Insert.gif" CommandName="_ins" ImageAlign="Left" />
								<ASP:ImageButton id="_ins_updateButton" runat="server" Visible="False" AlternateText="OK" ImageUrl="../Pics/Buttons/Update.gif" CommandName="_insUpdate" ImageAlign="Left" />
								<ASP:ImageButton id="_ins_cancelButton" runat="server" Visible="False" AlternateText="Annuleren" ImageUrl="../Pics/Buttons/Cancel.gif" CommandName="_insCancel" ImageAlign="Left" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:ImageButton id="_edt_updateButton" runat="server" AlternateText="OK" ImageUrl="../Pics/Buttons/Update.gif" CommandName="_edtUpdate" ImageAlign="Left" />
								<ASP:ImageButton id="_edt_cancelButton" runat="server" AlternateText="Annuleren" ImageUrl="../Pics/Buttons/Cancel.gif" CommandName="_edtCancel" ImageAlign="Left" />
								<br>
								<br>
								<ASP:ImageButton id="_delButton" runat="server" AlternateText="Verwijderen" ImageUrl="../Pics/Buttons/Delete.gif" CommandName="_del" ImageAlign="Left" />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='' HeaderStyle-Width="0" SortExpression='Id'>
							<ItemTemplate>
								<ASP:Label id=lblId Visible =False Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insId' runat='server' Visible='False' Width="0" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtId' runat='server' Width=0 Text='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TEMPLATECOLUMN SortExpression="TLNaam" HeaderText="TLNaam">
							<ITEMTEMPLATE>
								<ASP:LABEL id="Label1" runat="server" Enabled =False Visible="True" Text='<%# DataBinder.Eval(Container.DataItem, "TLNaam") %>' /></ASP:LABEL>
							</ITEMTEMPLATE>
							<FOOTERTEMPLATE>
								<ASP:TEXTBOX id="Textbox1" runat="server" Width="0" Visible="False" />
							</FOOTERTEMPLATE>
							<EDITITEMTEMPLATE>
								<ASP:TEXTBOX id="Textbox2" runat="server" Enabled = false Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "TLNaam") %>' />
							</EDITITEMTEMPLATE>
						</ASP:TEMPLATECOLUMN>
						<ASP:TemplateColumn HeaderText='TLMsnr' SortExpression='TLMsnr'>
							<ItemTemplate>
								<ASP:Label id=lblTLMsnr Visible =True Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "TLMsnr") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insTLMsnr' runat='server' Visible='False' Width='100%' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtTLMsnr' runat='server' Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "TLMsnr") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText="Afdeling" SortExpression="Afde" ItemStyle-Wrap="True">
							<ItemTemplate>
								<ASP:Label id=lblAfde runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "Afde") %>' />
								<ASP:DropDownList id=ddl_Afde CssClass = "dg" runat="server" Width="270px" Enabled="False" DataValueField="Afde" DataTextField="Omschr" DataSource="<%# Bind_ddlAfde() %>" />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:DropDownList id=_ins_ddlAfde runat="server" Width="270px" Visible="False" Enabled="True" DataValueField="Afde" DataTextField="Omschr" DataSource="<%# Bind_ddlAfde() %>" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:DropDownList id=_edt_ddlAfde CssClass = "dg" runat="server" Width="270px" DataValueField="Afde" DataTextField="Omschr" DataSource="<%# Bind_ddlAfde() %>" />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='Periode' SortExpression='peri'>
							<ItemTemplate>
								<ASP:Label id=lblperi Visible =True Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "peri") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insperi' runat='server' Visible='False' Width='100%' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtperi' runat='server' Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "peri") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
					</Columns>
					<PagerStyle CssClass="dgPager" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
		</form>
	</body>
</HTML>
