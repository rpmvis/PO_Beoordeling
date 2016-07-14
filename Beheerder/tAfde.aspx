<%@ Page language="c#" Codebehind="tAfde.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.tAfde" %>
<HTML>
	<HEAD>
		<title>tAfde</title>
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
		<form id="tAfde" method="post" runat="server">
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
						<td class="titelbalk" colSpan="2">Tabel tAfde</td>
					</tr>
				</table>
			</div>
			<div id="content_beheerder_leftalign"><asp:dropdownlist id="ddlSelGridItem" runat="server" AutoPostBack="True" Width="400px"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
				<br>
				<asp:datagrid id="_grid" runat="server" CssClass="dg" ShowFooter="True" CellPadding="1" BorderWidth="1px" BorderColor="#AEC2DD" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False">
					<AlternatingItemStyle CssClass="dgAltItem"></AlternatingItemStyle>
					<ItemStyle CssClass="dgItem"></ItemStyle>
					<HeaderStyle CssClass="dgHeader"></HeaderStyle>
					<FooterStyle CssClass="dgFooter"></FooterStyle>
					<Columns>
						<asp:TemplateColumn>
							<HeaderStyle Width="30px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></ItemStyle>
							<HeaderTemplate>
								<IMG height="1" src="../Pics/Buttons/n.gif" width="50px">
							</HeaderTemplate>
							<ItemTemplate>
								<ASP:ImageButton id="_edtButton" runat="server" AlternateText="Wijzigen" ImageUrl="../Pics/Buttons/Edit.gif" CommandName="Edit" ImageAlign="Left" />
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></FooterStyle>
							<FooterTemplate>
								<ASP:ImageButton id="_insButton" runat="server" AlternateText="Toevoegen" ImageUrl="../Pics/Buttons/Insert.gif" CommandName="_ins" ImageAlign="Left" />
								<ASP:ImageButton id="_ins_updateButton" runat="server" Visible="False" AlternateText="OK" ImageUrl="../Pics/Buttons/Update.gif" CommandName="_insUpdate" ImageAlign="Left" />
								<br>
								<br>
								<ASP:ImageButton id="_ins_cancelButton" runat="server" Visible="False" AlternateText="Annuleren" ImageUrl="../Pics/Buttons/Cancel.gif" CommandName="_insCancel" ImageAlign="Left" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:ImageButton id="_edt_updateButton" runat="server" AlternateText="OK" ImageUrl="../Pics/Buttons/Update.gif" CommandName="_edtUpdate" ImageAlign="Left" />
								<ASP:ImageButton id="_edt_cancelButton" runat="server" AlternateText="Annuleren" ImageUrl="../Pics/Buttons/Cancel.gif" CommandName="_edtCancel" ImageAlign="Left" />
								<br>
								<br>
								<ASP:ImageButton id="_delButton" runat="server" AlternateText="Verwijderen" ImageUrl="../Pics/Buttons/Delete.gif" CommandName="_del" ImageAlign="Left" />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
						<asp:TemplateColumn SortExpression="Afde" HeaderText="Afdeling">
							<ItemTemplate>
								<ASP:Label runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "Afde") %>' />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insAfde" runat="server" Width="100%" Visible="False" Text="" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtAfde runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Afde") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Omschr" HeaderText="Omschrijving">
							<ItemTemplate>
								<ASP:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Omschr") %>' />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insOmschr" runat="server" Width="100%" Visible="False" Text="" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtOmschr runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Omschr") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Left" CssClass="dgPager" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
		</form>
	</body>
</HTML>
