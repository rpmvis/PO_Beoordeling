<%@ Page language="c#" Codebehind="tblBeoScore.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.tblBeoScore" %>
<HTML>
	<HEAD>
		<title>tblBeoScore</title>
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
		<form id="tblBeoScore" method="post" runat="server">
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
						<td class="titelbalk" colSpan="2">Tabel tblBeoScore</td>
					</tr>
				</table>
			</div>
			<div id="content_beheerder_leftalign">
				<asp:dropdownlist id="ddlSelPeri" runat="server" Width="100px" AutoPostBack="True"></asp:dropdownlist>&nbsp;&nbsp;
				<asp:dropdownlist id="ddlSelGridItem" runat="server" Width="350px" AutoPostBack="True"></asp:dropdownlist>&nbsp;&nbsp;<br>
				<br>
				<asp:datagrid id="_grid" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" BorderColor="#AEC2DD" BorderWidth="1px" CellPadding="1" ShowFooter="True" CssClass="dg">
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
						<ASP:TemplateColumn HeaderText='' HeaderStyle-Width="0" SortExpression='BeoScoreId'>
							<ItemTemplate>
								<ASP:Label id=lblBeoScoreId Visible =False Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "BeoScoreId") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insBeoScoreId' runat='server' Visible='False' Width="0" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtBeoScoreId' runat='server' Width=0 Text='<%# DataBinder.Eval(Container.DataItem, "BeoScoreId") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='BeoID' SortExpression='BeoID'>
							<ItemTemplate>
								<ASP:Label id=lblBeoID Visible =True Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "BeoID") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insBeoID' runat='server' Visible='False' Width='100%' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtBeoID' runat='server' Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "BeoID") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn SortExpression="BeoNaam" HeaderText="BeoNaam">
							<ItemTemplate>
								<ASP:Label id="Label1" runat="server" Enabled =False Visible="True" Text='<%# DataBinder.Eval(Container.DataItem, "BeoNaam") %>' /></ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id="Textbox1" runat="server" Width="0" Visible="False" />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id="Textbox2" runat="server" Enabled = false Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "BeoNaam") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='Element' SortExpression='Elem'>
							<ItemTemplate>
								<ASP:Label id=lblElem Visible =True Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "Elem") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insElem' runat='server' Visible='False' Width='100%' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtElem' runat='server' Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "Elem") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='Score' SortExpression='Scor'>
							<ItemTemplate>
								<ASP:Label id=lblScor Visible =True Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "Scor") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insScor' runat='server' Visible='False' Width='100%' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtScor' runat='server' Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "Scor") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='Toelichting' SortExpression='Toel'>
							<ItemTemplate>
								<ASP:Label id=lblToel Visible =True Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "Toel") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insToel' runat='server' Visible='False' Width='100%' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtToel' runat='server' Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "Toel") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='Doel' SortExpression='Doel'>
							<ItemTemplate>
								<ASP:Label id=lblDoel Visible =True Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "Doel") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insDoel' runat='server' Visible='False' Width='100%' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtDoel' runat='server' Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "Doel") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
						<ASP:TemplateColumn HeaderText='Afspraak' SortExpression='Afsp'>
							<ItemTemplate>
								<ASP:Label id=lblAfsp Visible =True Enabled=True runat='server' Text='<%# DataBinder.Eval(Container.DataItem, "Afsp") %>' />
								</ASP:Label>
							</ItemTemplate>
							<FooterTemplate>
								<ASP:TextBox id='_insAfsp' runat='server' Visible='False' Width='100%' />
							</FooterTemplate>
							<EditItemTemplate>
								<ASP:TextBox id='_edtAfsp' runat='server' Width='100%' Text='<%# DataBinder.Eval(Container.DataItem, "Afsp") %>' />
							</EditItemTemplate>
						</ASP:TemplateColumn>
					</Columns>
					<PagerStyle CssClass="dgPager" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
		</form>
	</body>
</HTML>
