<%@ Page Title="" Language="C#" MasterPageFile="~/HeaderPage.Master" AutoEventWireup="true"
    CodeBehind="ClientJobs.aspx.cs" Inherits="NexusJobs.ClientJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr style="width: 100%; height: 500px">
            <td style="vertical-align: top; width: 100%;">
                <table cellpadding="4" cellspacing="0" style="width: 100%; height: 100%">
                    <tr style="width: 100%">
                        <td align="center" class="FontTypes" style="width: 10%;">
                            <b>Client Type :</b>
                        </td>
                        <td class="FontTypes" style="width: 10%;">
                            <asp:Label ID="lbClientType" runat="server"></asp:Label>
                        </td>
                        <td align="center" class="FontTypes" style="width: 10%;">
                            <b>Client Name :</b>
                        </td>
                        <td class="FontTypes" style="width: 10%;">
                            <asp:Label ID="lbClientName" runat="server"></asp:Label>
                        </td>
                        <td align="right" valign="bottom" class="FontTypes" style="width: 60%;">
                            <asp:LinkButton ID="lnkExportExcel" runat="server" CssClass="FontTypes" Height="22px"
                                Text="Export Excel" OnClick="lnkExportExcel_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td colspan="5" style="width: 100%; vertical-align: top">
                            <fieldset id="fsTable" runat="server">
                                <asp:Repeater ID="rptTable" runat="server" OnItemCommand="rptTable_ItemCommand" OnItemDataBound="rptTable_ItemDataBound">
                                    <HeaderTemplate>
                                        <table cellpadding="2" cellspacing="0" border="1" style="width: 100%">
                                            <tr class="Border2" style="background-color: #5E9FD5; height: 35px;">
                                                <td class="FontTypes" style="color: #FFFFFF;">
                                                    <b>Sno</b>
                                                </td>
                                                <%--<td class="FontTypes">
                                                    </td>--%>
                                                <td class="FontTypes" style="color: #FFFFFF;">
                                                    <b>Method Name</b>
                                                </td>
                                                <%-- <td><b>Integration Id</b></td>--%>
                                                <td class="FontTypes" style="color: #FFFFFF;">
                                                    <b>Status</b>
                                                </td>
                                                <td class="FontTypes" style="color: #FFFFFF;">
                                                    <b>LastRunDate</b>
                                                </td>
                                                <td class="FontTypes" style="color: #FFFFFF;">
                                                    <b>NextRunDate</b>
                                                </td>
                                                <td class="FontTypes" style="color: #FFFFFF;">
                                                    <b>Comments</b>
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr onmouseover="this.bgColor='#CEE7F4'" onmouseout="this.bgColor='#FFFFFF'" style="height: 30px;">
                                            <td class="FontTypes">
                                                <span style="margin-right: 20px;">
                                                    <%# Container.ItemIndex + 1 %></span>
                                            </td>
                                            <%--<td >
                                                    
                                                </td>--%>
                                            <td class="FontTypes">
                                                <%# DataBinder.Eval(Container, "DataItem.MethodName")%>
                                                <asp:TextBox ID="txtGetDgPkey" CssClass="FontTypes" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Fkey") %>'
                                                    Visible="false"></asp:TextBox>
                                            </td>
                                            <%--<td><%# DataBinder.Eval(Container, "DataItem.integration_id")%></td> --%>
                                            <td class="FontTypes">
                                                <asp:LinkButton ID="lnkJobStatus" CommandName="commJobStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status")%>'></asp:LinkButton>
                                            </td>
                                            <td class="FontTypes">
                                                <%# DataBinder.Eval(Container, "DataItem.LastRunDate")%>
                                            </td>
                                            <td class="FontTypes">
                                                <%# DataBinder.Eval(Container, "DataItem.NextRunDate")%>
                                            </td>
                                            <td class="FontTypes">
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
