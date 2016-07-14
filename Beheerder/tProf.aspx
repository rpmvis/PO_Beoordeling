<%@ Page language="c#" Codebehind="tProf.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.tProf" %>
<HTML>
	<HEAD>
		<title>tProf</title>
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
		<form id="tProf" method="post" runat="server">
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
						<td class="titelbalk" colSpan="2">Tabel tProf</td>
					</tr>
				</table>
			</div>
			<div id="content_beheerder_leftalign"><asp:dropdownlist id="ddlSelGridItem" runat="server" Width="300px" AutoPostBack="True"></asp:dropdownlist>&nbsp;&nbsp;<br>
				<br>
				<asp:datagrid id="_grid" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" BorderColor="#AEC2DD" BorderWidth="1px" CellPadding="1" ShowFooter="True" CssClass="dg">
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
						<ASP:TemplateColumn HeaderText='Prof' SortExpression='Prof'>
							<ItemTemplate>
								<ASP:Label id=lblProf Visible =True Enabled=False runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "Prof") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insProf' runat='server' Visible='False' Width='100%' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtProf' Enabled =False runat='server' Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "Prof") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='Omschrijving' SortExpression='Omschr'>
							<ItemTemplate>
								<ASP:Label id=lblOmschr Visible =True Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "Omschr") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insOmschr' runat='server' Visible='False' Width='100%' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtOmschr' runat='server' Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "Omschr") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='Opmerking' SortExpression='Opm'>
							<ItemTemplate>
								<ASP:Label id=lblOpm Visible =True Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "Opm") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insOpm' runat='server' Visible='False' Width='100%' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtOpm' runat='server' Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "Opm") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
					</Columns>
					<PagerStyle CssClass="dgPager" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
		</form>
	</body>
</HTML>
