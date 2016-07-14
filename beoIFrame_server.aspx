<%@ Page language="c#" Codebehind="beoIFrame_server.aspx.cs" AutoEventWireup="false" Inherits="PO_Beoordeling.IFrame_server" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>IFrame_server</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script type="text/javascript">
		function init(){
			if (window.parent.handleResponse)
			{
				var sReqType = document.getElementById('lblReqType').innerHTML;
				var sSessionId = document.getElementById('lblSessionId').innerHTML;
				var sWebPage = document.getElementById('lblWebPage').innerHTML;
				// alert('WebPage: ' + sWebPage);
				window.parent.handleResponse(sReqType, sWebPage, sSessionId);
			}
			else{
				// alert('Moederformulier kan response van server niet afhandelen!');
				return;
			}
		}
		</script>
	</HEAD>
	<BODY class='body' onload="init();" MS_POSITIONING="GridLayout">
		<form id="IFrame_server" method="post" runat="server">
			<asp:Label id="lblReqType" style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server" Height="19" Width="154">""</asp:Label>
			<asp:Label id="lblWebPage" style="Z-INDEX: 102; LEFT: 12px; POSITION: absolute; TOP: 81px" runat="server" Width="159" Height="19">""</asp:Label>
			<asp:Label id="lblSessionId" style="Z-INDEX: 100; LEFT: 10px; POSITION: absolute; TOP: 43px" runat="server" Width="154" Height="19">""</asp:Label>
		</form>
	</BODY>
</HTML>
