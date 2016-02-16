<%@ Page Language="C#" AutoEventWireup="True" Codebehind="register_third.aspx.cs" Inherits="OAuth2Client.WebFile.passport._register_third" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>完善资料</title>
</head>
<body>
这里是成功登陆后完善其他信息或绑定已有用户的界面，由于每个系统都不一样，业务代码您自己写吧。
<br /><br />下面是oauth2登录后获得的用户信息，请自行整合到自己的业务系统吧
<br /><br />第三方类型：<%=OAuthCode%>
<br /><br />唯一标识：<%=OpenID %>
<br /><br />昵称：<%=NickName %>
<br /><br />头像地址：<img src="<%=HeadUrl %>" />
<br /><br />[<a href="Logout.aspx">退出</a>]
<br /><br />如果到了这一步，您还不会的话，对不起了，只能购买我们的有偿技术支持咯。
</body>
</html>
