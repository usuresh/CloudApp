<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NexusJobs.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="Styles/Design.css" />
    <title></title>
    <style type="text/css">
        .style4
        {
            width: 25%;
            height: 5px;
        }
        .style5
        {
            font-family: Tahoma;
            font-size: 9pt;
            font-style: normal;
            height: 5px;
        }
    </style>
</head>
<!--<body style="background-position: center; width:100%; height:100%; margin-left:0; margin-top:0; margin-right:0; margin-bottom:0; background-color:#024383; background-image: Url('Images/bgblue.jpg');background-repeat:repeat; " >-->
<body style="background-position: center; width: 100%; height: 100%; margin-left: 0;
    margin-top: 0; margin-right: 0; margin-bottom: 0; background-image: Url('Images/bgblue.jpg');
    background-color: #03417e; background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <div style="height: 100%">
        <table cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
            <tr style="height: 100px">
                <td style="height: 100px; width: 33%">
                </td>
                <td style="height: 100px; width: 35%">
                </td>
                <td style="height: 100px; width: 32%">
                </td>
            </tr>
            <tr style="height: 300">
                <td style="height: 300px; width: 33%">
                </td>
                <td style="height: 300px; width: 35%">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td colspan="3" style="width: 100%; height: 50px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <table cellpadding="0" cellspacing="0" width="100%" style="background-color: transparent">
                                    <tr style="border-left-width: thin; border-right-width: thin; border-top-width: thin;
                                        border-color: Black">
                                        <td colspan="2" align="left" style="height: 25px; background-color: transparent;
                                            vertical-align: middle; font-family: Tahoma; font-size: small; font-weight: bold;
                                            border-left-width: thin; border-right-width: thin; border-top-width: thin; border-color: Black">
                                            <asp:Label ID="lbPlLogin" runat="server" Text="Please Login" ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-left-width: thin; border-right-width: thin; border-color: Black">
                                        <td colspan="2" valign="middle" style="height: 40px; border-left-width: thin; border-right-width: thin;
                                            border-top-width: thin; border-color: Black; font-family: Tahoma; font-size: small;
                                            font-weight: bold;">
                                            <asp:Label ID="lbUsPwdText" runat="server" ForeColor="White" Text="Enter your username and password to continue."></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-left-width: thin; border-right-width: thin; border-color: Black;">
                                        <td valign="top" align="left" style="height: 18px;">
                                            <span class="loginFont">Username</span>
                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="FontTypes" Height="18px" Width="100%"
                                                MaxLength="15"></asp:TextBox>
                                        </td>
                                        <td style="width: 25%">
                                        </td>
                                    </tr>
                                    <tr valign="baseline">
                                        <td align="left" valign="bottom" class="style5">
                                            &nbsp;
                                        </td>
                                        <td class="style4">
                                        </td>
                                    </tr>
                                    <tr style="border-left-width: thin; border-right-width: thin; border-color: Black">
                                        <td align="left" style="height: 23px;">
                                            <span class="loginFont">Password</span>
                                            <asp:TextBox ID="txtPassword" runat="server" Width="100%" CssClass="FontTypes" Height="18px"
                                                TextMode="Password"></asp:TextBox>
                                        </td>
                                        <td style="width: 25%">
                                        </td>
                                    </tr>
                                    <tr style="border-left-width: thin; border-right-width: thin; border-bottom-width: thin;
                                        border-color: Black; width: 100%">
                                        <td align="right" valign="middle" style="height: 50px; width: 75%">
                                            <asp:ImageButton ID="imgLogin" runat="server" ImageUrl="~/Images/login.png" OnClick="imgLogin_Click"
                                                ToolTip="Login" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:ImageButton ID="imgForgotPwd" runat="server" ImageUrl="~/Images/ForgotPwds1.png"
                                                ToolTip="Forgot Password" OnClick="imgForgotPwd_Click" />
                                        </td>
                                        <td style="width: 25%">
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="height: 300px; width: 32%">
                </td>
            </tr>
            <tr style="height: 278px">
                <td colspan="3">
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
