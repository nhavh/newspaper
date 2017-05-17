<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="TMV.BackEnd.ChangePassword" %>

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
		    <div class="title">THAY ĐỔI MẬT KHẨU TÀI KHOẢN <br />WEBSITE TMV.VN</div>
			<div id="element-box" class="login">
				<div class="m">
					<h1>Thay đổi mật khẩu</h1>
				     <div id="section-box">
				        <div class=" sb-login">
					         <form id="form_login" runat="server" style="clear: both;">
						        <span class="pass input">
							        <asp:TextBox ID="txtCurrentPass" class="inputbox" runat="server" TextMode="Password" placeholder="Mật khẩu cũ"></asp:TextBox>
						        </span>
						        <span class="pass input">
							        <asp:TextBox ID="txtNewPass" class="inputbox" runat="server" TextMode="Password" placeholder="Mật khẩu mới"></asp:TextBox>
						        </span>
						        <span class="pass input">
							        <asp:TextBox ID="txtConfirmPass" class="inputbox" runat="server" TextMode="Password" placeholder="Xác nhận mật khẩu"></asp:TextBox>
						        </span>
						        <p style="clear: both;" id="form-login-lang"/>
							    																
					            <div class="button_holder">
					                <asp:LinkButton ID="cmdLogin1" runat="server" OnClick="cmdLogin_Click" 
                                        CausesValidation="False">Xác nhận</asp:LinkButton>
					            </div>
								
						        <p style="clear: both;">
						            <asp:Label id="lblResults" ForeColor="red" Font-Size="10" runat="server" Text=""></asp:Label> 
						        </p>
						        <div class="clr"></div>
					        </form>
					    </div>
						<div class="clr"></div>
					</div>
				    <div class="clr"></div>							
			    </div>				
				<div class="b">
					<div class="b">
						<div class="b"></div>
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
