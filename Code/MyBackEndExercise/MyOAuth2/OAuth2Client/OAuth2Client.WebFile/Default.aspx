<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OAuth2Client.WebFile.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta property="qc:admins" content="4611253517154026654" />
    <meta property="wb:webmaster" content="6c6a0cf69423fd9f" />
    <link href="/static/css/common.css" type="text/css" rel="stylesheet" />
    <title>OAuth2Client.NET_新浪微博OAUTH2.0，腾讯OAUTH2.0，微信OAUTH2.0，人人OAUTH2.0，百度OAUTH2.0，淘宝OAUTH2.0
    </title>
    <meta name="keywords" content="新浪微博OAUTH2.0，腾讯OAUTH2.0，微信OAUTH2.0，人人OAUTH2.0，百度OAUTH2.0，淘宝OAUTH2.0，OAuth2.NET，开源，免费" />
    <meta name="description" content="OAuth2Client.NET是一个完全免费开源的NET版OAuth2.0规范的通用控件，默认集成了新浪微博，腾讯，微信，人人，百度，淘宝" />
</head>
<body>
    <div class="wrapper">
        <a href="/" title="OAuth2Client.NET">
            <img src="/static/images/logo.jpg" alt="OAuth2Client.NET" /></a>
        <div>
            <p>
                OAuth2.0几乎成了当今第三方平台的一个标准中的标准（我不知道几年后会出3.0），那既然是一个标准，为什么就不能用一个相对标准的类库或项目来实现呢？翻遍整个china的开源项目，就别说是C#了，连java、php都没有这样的现成项目，那老朽就卖一把老，继续为各位献上一点微薄之力吧。
            </p>
            <p>
                由于时间关系，第一版我就做了6个接口的对接，其中微信的回调地址是在公众平台设置的，不支持传参，注意哦
            </p>
            <p>
                整个项目只有register_third.aspx是需要跟您的会员系统对接，其他都不需要修改~~哦，忘了说，appkey和appsecret啥的是需要改的，您懂的。<br />
            </p>
            <p>
                由于懒惰，我也不做太多的页面美化工作了，何况我美化了您也用不上啊~~~~~还不如直接下载呢！<a href="javascript:;" onclick="alert('请发送邮件至791104444@qq.com索取')">OAuth2ClientV1.1.15.723</a>
            </p>
            <p>
                现在体验：<a class="qq" href="/OAuth2.0/Go2AuthorizeURL.aspx?oauth_code=qq"></a><a class="weixin"
                    href="/OAuth2.0/Go2AuthorizeURL.aspx?oauth_code=weixin"></a><a class="weibo" href="/OAuth2.0/Go2AuthorizeURL.aspx?oauth_code=weibo"></a><a
                        class="taobao" href="/OAuth2.0/Go2AuthorizeURL.aspx?oauth_code=taobao"></a><a class="baidu"
                            href="/OAuth2.0/Go2AuthorizeURL.aspx?oauth_code=baidu"></a><a class="renren" href="/OAuth2.0/Go2AuthorizeURL.aspx?oauth_code=renren"></a>
                            <a class="jd" href="/OAuth2.0/Go2AuthorizeURL.aspx?oauth_code=jd"></a>
                            <a class="jumbot" href="/OAuth2.0/Go2AuthorizeURL.aspx?oauth_code=jumbot"></a>
            </p>
        </div>
    </div>
</body>
</html>
