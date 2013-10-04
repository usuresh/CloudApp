<%@ Page Title="" Language="C#" MasterPageFile="~/HeaderPage.Master" AutoEventWireup="true"
    CodeBehind="JobStatus.aspx.cs" Inherits="NexusJobs.JobStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <%--<tr  style="height: 15px;">
            <td colspan="2" class="Border3" style="width: 100%">
                <table class="Border3" cellpadding="0" cellspacing="0" style="width: 100%; height: 15px;">
                    <tr style="width: 100%; background-color: #3381BF">
                        <td class="Border3" align="left" style="vertical-align: bottom; height: 15px;">
                            <asp:LinkButton ID="lnkPrevious" runat="server" Text="Previous" CssClass="FontTypes"
                                ForeColor="White" onclick="lnkPrevious_Click1"></asp:LinkButton>
                        </td>
                        <td class="Border3" align="right" style="vertical-align: bottom; height: 15px;">
                            <asp:ImageButton ID="imgbtHome" runat="server" ImageUrl="~/Images/HouseButton.png"
                                CssClass="FontTypes" onclick="imgbtHome_Click1" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <tr style="width: 100%; height: 500px">
            <td>
                <table cellpadding="4" cellspacing="0" style="width: 100%; height: 500px">
                    <tr style="width: 100%">
                        <td class="FontTypes" style="width: 10%; height: 50px">
                            <b>&nbsp; &nbsp;Client Type :</b>
                        </td>
                        <td class="FontTypes" style="width: 10%; height: 50px">
                            <asp:Label ID="lbClientType" runat="server"></asp:Label>
                        </td>
                        <td class="FontTypes" style="width: 10%; height: 50px">
                            <b>Client Name :</b>
                        </td>
                        <td class="FontTypes" style="width: 10%; height: 50px">
                            <asp:Label ID="lbClientName" runat="server"></asp:Label>
                        </td>
                        <td class="FontTypes" style="width: 10%; height: 50px">
                            <b>Job Status :</b>
                        </td>
                        <td class="FontTypes" style="width: 10%; height: 50px">
                            <asp:Label ID="lbJobStatus" runat="server"></asp:Label>
                        </td>
                        <td align="right" valign="bottom" class="FontTypes" style="width: 40%; height: 50px">
                            &nbsp;<asp:LinkButton ID="lnkExportExcel" runat="server" CssClass="FontTypes" Height="22px"
                                Text="Export Excel" OnClick="lnkExportExcel_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td colspan="7" class="FontTypes" style="width: 100%; height: 400px; vertical-align: top">
                            <fieldset id="fsTable" runat="server">
                                <asp:Repeater ID="rptTable" runat="server">
                                    <HeaderTemplate>
                                        <table cellpadding="2" cellspacing="0" border="1" style="width: 100%">
                                            <tr style="background-color: #5E9FD5; height: 35px;">
                                                <td style="color: #FFFFFF">
                                                    <b>Sno</b>
                                                </td>
                                                <td class="FontTypes" style="width: 20%; color: #FFFFFF;">
                                                    <b>Method Name</b>
                                                </td>
                                                <td class="FontTypes" style="width: 20%; color: #FFFFFF;">
                                                    <b>LastRunDate</b>
                                                </td>
                                                <td class="FontTypes" style="width: 20%; color: #FFFFFF;">
                                                    <b>Comments</b>
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr onmouseover="this.bgColor='#CEE7F4'" onmouseout="this.bgColor='#FFFFFF'" style="height: 30px;">
                                            <td>
                                                <span class="FontTypes" style="margin-right: 20px;">
                                                    <%#Container.ItemIndex+1 %></span>
                                            </td>
                                            <td class="FontTypes" style="width: 20%;">
                                                <%# DataBinder.Eval(Container, "DataItem.Method Name")%>
                                            </td>
                                            <td class="FontTypes" style="width: 20%;">
                                                <%# DataBinder.Eval(Container, "DataItem.LastRunDate")%>
                                            </td>
                                            <td class="FontTypes" style="width: 60%;">
                                                <%#MakeScript(Container.ItemIndex+1) %>
                                                <%--<%# DataBinder.Eval(Container,  "DataItem.Comments")%>--%>
                                                <%# ShowContent(DataBinder.Eval(Container.DataItem, "Comments"))%>
                                                <%--<asp:LinkButton ID="lnkReadMore"  CommandName="Readmore" runat="server" Text="Read more.."></asp:LinkButton>--%>
                                                <%# MakeLink(DataBinder.Eval(Container.DataItem, "Comments"), Container.ItemIndex + 1)%>
                                            </td>
                                            <%# CreateHiddenField(DataBinder.Eval(Container.DataItem, "Comments"),Container.ItemIndex+1)%>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
