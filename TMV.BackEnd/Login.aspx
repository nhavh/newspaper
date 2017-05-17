<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TMV.BackEnd.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login Page</title>
     <link href="css/login.css" rel="stylesheet" type="text/css" />
     <link type="text/css" href="/css/rounded.css" rel="stylesheet" />
    
</head>
<body>
	<div class="login-new">
		<div class="padding">
		    <div class="title">HỆ THỐNG QUẢN TRỊ WEBSITE <br />THẨM MỸ VIỆN HOÀNG ANH</div>
			<div id="element-box" class="login">
				<div class="m">
					<h1>Đăng nhập</h1>
					<div id="section-box">
						<div class="sb-login">
							 <form id="form_login" runat="server" style="clear: both;">
								<span class="user input"><asp:TextBox ID="txtUsername" class="inputbox" placeholder="Tên đăng nhập" runat="server" autocomplete="off"></asp:TextBox></span>
								<span class="pass input"><asp:TextBox ID="txtPassword" class="inputbox" placeholder="Mật khẩu" runat="server" TextMode="Password"></asp:TextBox></span>
								<span class="check-box">
								    <input type="checkbox" id="chkRemember" class="" runat="server" />
								    <label for="chkRemember"><span></span>Nhớ đăng nhập</label>
								</span>
								<p style="clear: both;" id="form-login-lang"/>							
							    <div class="button_holder">
							        <asp:LinkButton ID="cmdLogin1" runat="server" OnClick="cmdLogin_Click">Đăng nhập</asp:LinkButton>
							    </div>
								<p style="clear: both;">
								    <asp:Label id="lblResults" ForeColor="red" Font-Size="10" runat="server" Text=""></asp:Label> 
								</p>
								<div class="clr"></div>
							</form>
						</div>	
						<div class="clr"></div>
					</div>				
			    </div>				
	        </div>
	    </div>
		<noscript>
			'WARNJAVASCRIPT'
		</noscript>
		<div class="clr"></div>
	</div>
</body>
</html>
