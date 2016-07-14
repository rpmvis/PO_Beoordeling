<%@ Page language="c#" Codebehind="tblBeo.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.tblBeo" %>
<HTML>
	<HEAD>
		<title>tblBeo</title>
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
		<form id="tblBeo" method="post" runat="server">
			<div id="top_leftalign">
				<table>
					<tr>
						<td style="HEIGHT: 1px" align="right" colSpan="3"><span class="NoPrint"></span></td>
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
						<td class="titelbalk" colSpan="2">Tabel tblBeo</td>
					</tr>
				</table>
			</div>
			<div id="content_beheerder_leftalign"><asp:dropdownlist id="ddlSelPeri" runat="server" Width="100px" AutoPostBack="True"></asp:dropdownlist>&nbsp;&nbsp;
				<asp:dropdownlist id="ddlSelGridItem" runat="server" Width="300px" AutoPostBack="True"></asp:dropdownlist>&nbsp;&nbsp;<br>
				<br>
				<asp:datagrid id="_grid" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" BorderColor="Transparent" BorderWidth="1px" CellPadding="1" ShowFooter="True">
					<AlternatingItemStyle CssClass="dgAltItem"></AlternatingItemStyle>
					<ItemStyle CssClass="dgItem"></ItemStyle>
					<HeaderStyle CssClass="dgHeader"></HeaderStyle>
					<FooterStyle CssClass="dgFooter"></FooterStyle>
					<Columns>
						<asp:TemplateColumn>
							<ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Middle"></ItemStyle>
							<HeaderTemplate>
								<IMG height="1" src="../Pics/Buttons/n.gif" width="50">
							</HeaderTemplate>
							<ItemTemplate>
								<ASP:ImageButton id="_edtButton" runat="server" ImageAlign="Left" CommandName="Edit" ImageUrl="../Pics/Buttons/Edit.gif" AlternateText="Wijzigen" />
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></FooterStyle>
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
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="BeoID" HeaderText="BeoID">
							<ItemTemplate>
								<ASP:Label id=lblBeoID runat="server" Visible="True" Text='<%# DataBinder.Eval(Container.DataItem, "BeoID") %>' Enabled="True" /></ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insBeoID" runat="server" Width="100%" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtBeoID runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "BeoID") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="BeoNaam" HeaderText="BeoNaam">
							<ItemTemplate>
								<ASP:Label id="Label1" runat="server" Enabled =False Visible="True" Text='<%# DataBinder.Eval(Container.DataItem, "BeoNaam") %>' /></ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="Textbox1" runat="server" Width="0" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id="Textbox2" runat="server" Enabled = false Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "BeoNaam") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Bevroren" HeaderText="Bevroren">
							<ItemTemplate>
								<ASP:CheckBox id=chkBevroren runat="server" Visible="True" Enabled="False" Checked='<%# DataBinder.Eval(Container.DataItem, "Bevroren") %>' /></ASP:CheckBox>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:CheckBox id="_insBevroren" runat="server" Width="100%" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:CheckBox id=_edtBevroren runat="server" Width="100%" Checked='<%# DataBinder.Eval(Container.DataItem, "Bevroren") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="BeoFunc" HeaderText="BeoFunc">
							<ItemTemplate>
								<ASP:Label id=lblBeoFunc runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "BeoFunc") %>' />
								<ASP:DropDownList id=ddl_BeoFunc runat="server" Width="270px" Enabled="False" DataValueField="Func" DataTextField="Func" DataSource="<%# Bind_ddlBeoFunc() %>" />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:DropDownList id=_ins_ddlBeoFunc runat="server" Width="270px" Visible="False" Enabled="True" DataValueField="Func" DataTextField="Func" DataSource="<%# Bind_ddlBeoFunc() %>" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:Label id="_edt_lblBeoFunc" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "BeoFunc") %>' />
								<ASP:DropDownList id=_edt_ddlBeoFunc runat="server" Width="270px" DataValueField="Func" DataTextField="Func" DataSource="<%# Bind_ddlBeoFunc() %>" />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Prof" HeaderText="Prof">
							<ItemTemplate>
								<ASP:Label id="lblProf" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "Prof") %>' />
								<ASP:DropDownList id="ddl_Prof" runat="server" Width="100px" Enabled="False" DataValueField="Prof" DataTextField="Prof" DataSource="<%# Bind_ddlProf() %>" />
							</ItemTemplate>
							<FooterTemplate>
								<ASP:DropDownList id="_ins_ddlProf" runat="server" Width="100px" Visible="False" Enabled="True" DataValueField="Prof" DataTextField="Prof" DataSource="<%# Bind_ddlProf() %>" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:Label id="_edt_lblProf" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "Prof") %>' />
								<ASP:DropDownList id="_edt_ddlProf" runat="server" Width="100px" DataValueField="Prof" DataTextField="Prof" DataSource="<%# Bind_ddlProf() %>" />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Geselecteerd" HeaderText="Geselecteerd">
							<ItemTemplate>
								<ASP:CheckBox id=chkGeselecteerd runat="server" Visible="True" Enabled="False" Checked='<%# DataBinder.Eval(Container.DataItem, "Geselecteerd") %>' /></ASP:CheckBox>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:CheckBox id="_insGeselecteerd" runat="server" Width="100%" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:CheckBox id=_edtGeselecteerd runat="server" Width="100%" Checked='<%# DataBinder.Eval(Container.DataItem, "Geselecteerd") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Voll" HeaderText="Volledige functie?">
							<ItemTemplate>
								<ASP:Label id=lblVoll runat="server" Visible="True" Text='<%# DataBinder.Eval(Container.DataItem, "Voll") %>' Enabled="True" /></ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insVoll" runat="server" Width="100%" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtVoll runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Voll") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="VolT" HeaderText="Volledig toelichting">
							<ItemTemplate>
								<ASP:Label id=lblVolT runat="server" Visible="True" Text='<%# DataBinder.Eval(Container.DataItem, "VolT") %>' Enabled="True" /></ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insVolT" runat="server" Width="100%" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtVolT runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "VolT") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Nive" HeaderText="Ander Niveau">
							<ItemTemplate>
								<ASP:Label id=lblNive runat="server" Visible="True" Text='<%# DataBinder.Eval(Container.DataItem, "Nive") %>' Enabled="True" /></ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insNive" runat="server" Width="100%" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtNive runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Nive") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="NivT" HeaderText="Niveau toelichting">
							<ItemTemplate>
								<ASP:Label id=lblNivT runat="server" Visible="True" Text='<%# DataBinder.Eval(Container.DataItem, "NivT") %>' Enabled="True" /></ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insNivT" runat="server" Width="100%" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtNivT runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "NivT") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Bere" HeaderText="Bereikt">
							<ItemTemplate>
								<ASP:Label id=lblBere runat="server" Visible="True" Text='<%# DataBinder.Eval(Container.DataItem, "Bere") %>' Enabled="True" /></ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insBere" runat="server" Width="100%" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtBere runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Bere") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Beo1" HeaderText="Beoordelaar 1">
							<ItemTemplate>
								<ASP:Label id=lblBeo1 runat="server" Visible="True" Text='<%# DataBinder.Eval(Container.DataItem, "Beo1") %>' Enabled="True" /></ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insBeo1" runat="server" Width="100%" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtBeo1 runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Beo1") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Beo2" HeaderText="Beoordelaar 2">
							<ItemTemplate>
								<ASP:Label id=lblBeo2 runat="server" Visible="True" Text='<%# DataBinder.Eval(Container.DataItem, "Beo2") %>' Enabled="True" /></ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insBeo2" runat="server" Width="100%" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtBeo2 runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Beo2") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Poad" HeaderText="P&amp;O adviseur">
							<ItemTemplate>
								<ASP:Label id=lblPoad runat="server" Visible="True" Text='<%# DataBinder.Eval(Container.DataItem, "Poad") %>' Enabled="True" /></ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="_insPoad" runat="server" Width="100%" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id=_edtPoad runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Poad") %>' />
							</EditItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Left" CssClass="dgPager" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
		</form>
	</body>
</HTML>
