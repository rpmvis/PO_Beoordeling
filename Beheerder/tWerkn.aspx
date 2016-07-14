<%@ Page language="c#" Codebehind="tWerkn.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.tWerkn" %>
<HTML>
	<HEAD>
		<title>tWerkn</title>
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
		<form id="tWerkn" method="post" runat="server">
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
						<td class="titelbalk" colSpan="2">Tabel tWerkn</td>
					</tr>
				</table>
			</div>
			<div id="content_beheerder_leftalign"><asp:dropdownlist id="ddlSelPeri" runat="server" AutoPostBack="True" Width="100px"></asp:dropdownlist>&nbsp;&nbsp;
				<asp:dropdownlist id="ddlSelGridItem" runat="server" AutoPostBack="True" Width="240px"></asp:dropdownlist><br>
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
						<asp:TemplateColumn SortExpression="BeoID" HeaderText="BeoID">
							<ItemTemplate>
								<ASP:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BeoID") %>' />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insBeoID" runat="server" Enabled="False" Width="0" Visible="False" Text="" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtBeoID runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "BeoID") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Peri" HeaderText="Periode">
							<ItemTemplate>
								<ASP:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Peri") %>' />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insPeri" runat="server" Width="100%" Visible="False" Text="" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtPeri runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Peri") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Msnr" HeaderText="Msnr">
							<ItemTemplate>
								<ASP:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Msnr") %>' />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insMsnr" runat="server" Width="100%" Visible="False" Text="" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtMsnr runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Msnr") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Afde" HeaderText="Afdeling">
							<ItemTemplate>
								<ASP:Label id=lblAfde runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "Afde") %>' />
								<ASP:DropDownList id=ddl_Afde CssClass = "dg" runat="server" Width="270px" Enabled="True" DataValueField="Afde" DataTextField="Afde_Omschr" DataSource="<%# Bind_ddlAfde() %>" />
							</ItemTemplate>
							<FooterTemplate>
								<!-- FooterTemplate is for inserting a record -->
								<ASP:DropDownList id=_ins_ddlAfde runat="server" Width="270px" Visible="False" Enabled="True" DataValueField="Afde" DataTextField="Afde_Omschr" DataSource="<%# Bind_ddlAfde() %>" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:Label id="_edt_lblAfde" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "Afde") %>' />
								<ASP:DropDownList id=_edt_ddlAfde CssClass = "dg" runat="server" Width="270px" DataValueField="Afde" DataTextField="Afde_Omschr" DataSource="<%# Bind_ddlAfde() %>" />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Anaam" HeaderText="Anaam">
							<ItemTemplate>
								<ASP:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Anaam") %>' />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insAnaam" runat="server" Width="100%" Visible="False" Text="" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtAnaam runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Anaam") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Aanhef" HeaderText="Aanhef">
							<ItemTemplate>
								<ASP:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Aanhef") %>' />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insAanhef" runat="server" Width="100%" Visible="False" Text="" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtAanhef runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Aanhef") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Titel" HeaderText="Titel">
							<ItemTemplate>
								<ASP:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Titel") %>' />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insTitel" runat="server" Width="100%" Visible="False" Text="" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtTitel runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Titel") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Init" HeaderText="Init">
							<ItemTemplate>
								<ASP:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Init") %>' />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insInit" runat="server" Width="100%" Visible="False" Text="" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtInit runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Init") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Tussenv" HeaderText="Tussenv">
							<ItemTemplate>
								<ASP:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Tussenv") %>' />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insTussenv" runat="server" Width="100%" Visible="False" Text="" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtTussenv runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Tussenv") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Func" HeaderText="Functie">
							<ItemTemplate>
								<ASP:Label id=lblFunc runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "Func") %>' />
								<ASP:DropDownList id=ddl_Func runat="server" Width="270px" Enabled="False" DataValueField="Func" DataTextField="Func" DataSource="<%# Bind_ddlFunc() %>" />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:DropDownList id=_ins_ddlFunc runat="server" Width="270px" Visible="False" Enabled="True" DataValueField="Func" DataTextField="Func" DataSource="<%# Bind_ddlFunc() %>" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:Label id="_edt_lblFunc" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "Func") %>' />
								<ASP:DropDownList id=_edt_ddlFunc runat="server" Width="270px" DataValueField="Func" DataTextField="Func" DataSource="<%# Bind_ddlFunc() %>" />
							</EditItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Left" CssClass="dgPager" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
		</form>
	</body>
</HTML>
