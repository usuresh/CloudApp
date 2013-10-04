<%@ Page Title="" Language="C#" MasterPageFile="~/HeaderPage.Master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="NexusJobs.Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="aspx" %>
<script runat="server">


</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr style="height: 5px">
            <td colspan="3" style="width: 100%;">




            </td>
        </tr>
        <tr style="width: 100%">
            <td rowspan="2" style="width: 70%; height: 580px; vertical-align: top">
                <table cellpadding="0" cellspacing="0" style="width: 100%; vertical-align: top;">
                    <tr style="width: 100%">
                        <td style="height: 130px; width: 100%; vertical-align: top">
                            <asp:Repeater ID="rptTable" runat="server" OnItemCommand="rptTable_ItemCommand">
                                <HeaderTemplate>
                                    <table cellpadding="2" cellspacing="0" border="1" style="width: 100%;">
                                        <tr style="background-color: #5E9FD5; height: 25px;">
                                            <td class="FontTypes" style="width: 25%; color: #FFFFFF;">
                                                <b>Client Type</b>
                                            </td>
                                            <td class="FontTypes" style="width: 25%; color: #FFFFFF;">
                                                <b>Total Client Count</b>
                                            </td>
                                            <td class="FontTypes" style="width: 25%; color: #FFFFFF;">
                                                <b>Active Client</b>
                                            </td>
                                            <td class="FontTypes" style="width: 25%; color: #FFFFFF;">
                                                <b>InActive Client</b>
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr onmouseover="this.bgColor='#FFF8DC'" onmouseout="this.bgColor='#FFFFFF'" style="height: 25px;">
                                        <td class="FontTypes" style="width: 25%;">
                                            <asp:LinkButton ID="lnkClientName" CommandName="CommClientName" runat="server" class="FontTypes"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.ERPs")%>'></asp:LinkButton>
                                            <%--<%# DataBinder.Eval(Container, "DataItem.ERPs")%>--%>
                                        </td>
                                        <td class="FontTypes" style="width: 25%;">
                                            <%# DataBinder.Eval(Container, "DataItem.TOTAL_COUNT")%>
                                        </td>
                                        <td class="FontTypes" style="width: 25%;">
                                            <%# DataBinder.Eval(Container, "DataItem.ACTIVE_COUNT")%>
                                        </td>
                                        <td class="FontTypes" style="width: 25%;">
                                            <%# DataBinder.Eval(Container, "DataItem.INACTIVE_COUNT")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td align="left" class="FontTypes">
                            <%--<asp:Button ID="btnRemovejobs" runat="server" Text="Remove from the list" 
                                Height="23px" onclick="btnRemovejobs_Click" />--%>
                            <asp:ImageButton ID="imgbtnRemovejobs" runat="server" ImageUrl="~/Images/removeList.png"
                                OnClick="imgbtnRemovejobs_Click" ToolTip="Remove from list" Height="16px" Width="16px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 370px; vertical-align: top; width: 100%;" class="FontTypes">
                            <table cellpadding="0" cellspacing="0" style="border-style: inherit; width: 100%;">
                                <tr style="width: 100%">
                                    <td align="left" valign="top" style="height: 350px">
                                        <%--<%# DataBinder.Eval(Container, "DataItem.ERPs")%>--%>
                                        <asp:ScriptManager ID="scManager" runat="server" />
                                        <div>
                                            <asp:Timer ID="IntervalTimer" runat="server" Interval="15000" 
                                                ontick="IntervalTimer_Tick1" />
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Repeater ID="rptLogDeails" runat="server" OnItemCommand="rptLogDeails_ItemCommand"
                                                    OnItemDataBound="rptLogDeails_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <table cellpadding="2" cellspacing="0" border="1" style="width: 100%;">
                                                            <tr style="background-color: #5E9FD5; height: 20px">
                                                                <td class="FontTypes" style="color: #FFFFFF;">
                                                                </td>
                                                                <td class="FontTypes" style="color: #FFFFFF;">
                                                                    <b>Sno</b>
                                                                </td>
                                                                <%-- <td style=" visibility:hidden">
                                                        </td>--%>
                                                                <td class="FontTypes" style="color: #FFFFFF;">
                                                                    <b>Client Name</b>
                                                                </td>
                                                                <td style="color: #FFFFFF;">
                                                                    <b>Client Type</b>
                                                                </td>
                                                                <td style="color: #FFFFFF;">
                                                                    <b>Integration Id</b>
                                                                </td>
                                                                <td style="color: #FFFFFF;">
                                                                    <b>Job Name</b>
                                                                </td>
                                                                <td style="color: #FFFFFF;">
                                                                    <b>Job Status</b>
                                                                </td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr onmouseover="this.bgColor='#CEE7F4'" onmouseout="this.bgColor='#FFFFFF'" style="border-bottom-color: Gray;
                                                            border-bottom-style: solid; border-bottom-width: thin; height: 20px;">
                                                            <td>
                                                                <asp:CheckBox ID="chkJobsId1s" runat="server" AutoPostBack="true" />
                                                            </td>
                                                            <td>
                                                                <%# Container.ItemIndex + 1 %></span>
                                                                <asp:TextBox ID="txtGetDgPkey" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.pkey") %>'
                                                                    Visible="false" class="FontTypes"></asp:TextBox>
                                                                <asp:TextBox ID="txtGetFkeyMethod" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.fkey_methods") %>'
                                                                    Visible="false" class="FontTypes"></asp:TextBox>
                                                            </td>
                                                            <%--<td style=" visibility:hidden">
                                                        <asp:TextBox ID="txtGetDgPkey" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.pkey") %>'
                                                            Visible="false" class="FontTypes"></asp:TextBox>
                                                    </td>--%>
                                                            <td>
                                                                <asp:LinkButton ID="lnkClientName" CommandName="CommClientName" runat="server" class="FontTypes"
                                                                    Text='<%# DataBinder.Eval(Container,"DataItem.ClientName") %>'></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <%# DataBinder.Eval(Container,"DataItem.ERPs") %>
                                                            </td>
                                                            <td class="FontTypes">
                                                                <%# DataBinder.Eval(Container, "DataItem.integration_id")%>
                                                            </td>
                                                            <td class="FontTypes">
                                                                <%# DataBinder.Eval(Container, "DataItem.job_name")%>
                                                            </td>
                                                            <td class="FontTypes">
                                                                <%--<asp:LinkButton ID="lnkJobStatus" CommandName="CommJobStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Method_Status")%>'></asp:LinkButton>--%>
                                                                <asp:Label ID="lbJobStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Method_Status")%>'></asp:Label>
                                                                <%--<%# DataBinder.Eval(Container, "DataItem.Method_Status")%>--%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                        <tr style="width: 100%">
                                                            <td class="FontTypes" style="height: 10px; vertical-align: top">
                                                            </td>
                                                        </tr>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                </script>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
            <td class="FontTypes" style="width: 30%; height: 580px;" valign="top" rowspan="2">
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%; border-left-style: solid;
                    border-left-color: Gray; border-left-width: thin;">
                    <tr style="width: 100%">
                        <td class="FontTypes" style="height: 250px; vertical-align: bottom;">
                            <asp:Chart ID="ClientChart" class="FontTypes" runat="server" Width="262px" BorderlineWidth="0"
                                Height="190px">
                                <Series>
                                    <asp:Series ChartArea="ChartArea1" ChartType="Pie" Legend="Default" Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend Alignment="Near" Docking="Right" IsTextAutoFit="true" Name="Default"
                                        LegendStyle="Column" AutoFitMinFontSize="6" BorderWidth="0" Font="Verdana, 6.75pt"
                                        ItemColumnSpacing="20" TextWrapThreshold="100" />
                                </Legends>
                            </asp:Chart>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td class="FontTypes" style="height: 250px; vertical-align: bottom;">
                            &nbsp;
                            <asp:Chart ID="StackedChart" runat="server" Width="262px" Height="203px" Palette="SemiTransparent">
                                <Series>
                                    <asp:Series ChartType="Column" Name="Total" XValueMember="ERPs" YValueMembers="TOTAL_COUNT">
                                    </asp:Series>
                                    <asp:Series ChartType="Column" Name="Active" XValueMember="ERPs" YValueMembers="ACTIVE_COUNT">
                                    </asp:Series>
                                    <asp:Series ChartType="Column" Name="InActive" XValueMember="ERPs" YValueMembers="INACTIVE_COUNT">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend TitleFont="Verdana, 8pt, style=Bold" BackColor="Transparent" LegendStyle="Row"
                                        Docking="Bottom" Font="verdana, 8.25pt, style=Bold" IsTextAutoFit="true" Enabled="False"
                                        Name="Default">
                                    </asp:Legend>
                                </Legends>
                            </asp:Chart>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
