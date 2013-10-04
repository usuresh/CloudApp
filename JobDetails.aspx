<%@ Page Title="" Language="C#" MasterPageFile="~/HeaderPage.Master" AutoEventWireup="true"
    CodeBehind="JobDetails.aspx.cs" Inherits="NexusJobs.JobDetails" %>

<script runat="server">

    
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr style="width: 100%; height: 100px; border=0;">
            <td>
                <fieldset id="fsFilter" runat="server">
                    <legend id="lgFilter" runat="server" class="FontTypes">Filters </legend>
                    <table cellpadding="0" cellspacing="0" style="width: 100%; height: 80px;">
                        <tr style="width: 100%; height: 5px">
                            <td colspan="9" style="width: 100%; height: 5px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td class="FontTypes" style="width: 10%">
                                <asp:Label ID="lbClientType" runat="server" Text="Client Type" CssClass="FontTypes"></asp:Label>
                            </td>
                            <td class="FontTypes" style="width: 15%">
                                <asp:DropDownList ID="ddlClientType" runat="server" CssClass="FontTypes" Width="150px"
                                    Height="22px" OnSelectedIndexChanged="ClientType_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 5%">
                                &nbsp;
                            </td>
                            <td class="FontTypes" style="width: 10%">
                                <asp:Label ID="lbClientName" runat="server" Text="Client Name" CssClass="FontTypes"></asp:Label>
                            </td>
                            <td class="FontTypes" style="width: 15%">
                                <asp:DropDownList ID="ddlClientName" runat="server" CssClass="FontTypes" Width="150px"
                                    Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ClientName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 5%">
                                &nbsp;
                            </td>
                            <td class="FontTypes" style="width: 10%">
                                <asp:Label ID="lbMethods" runat="server" Text="Methods" CssClass="FontTypes"></asp:Label>
                            </td>
                            <td class="FontTypes" style="width: 15%">
                                <asp:DropDownList ID="ddlMethods" runat="server" CssClass="FontTypes" Width="150px"
                                    Height="22px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 13%">
                                &nbsp
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td class="FontTypes" style="width: 10%">
                                <asp:Label ID="lbFromDate" runat="server" Text="From Date" CssClass="FontTypes"></asp:Label>
                            </td>
                            <td class="FontTypes" style="width: 15%">
                                <input name='txtCalendar' id='idCalendar' class='inputBoxStyle' style="width: 120px;
                                    font-family: Verdana; font-size: 8pt" runat="server" readonly="readonly" />
                                <img src="Calander/calander4.jpg" align='top' onmouseover="fnInitCalendar(this,'ctl00_ContentPlaceHolder1_idCalendar','close=true')" />
                            </td>
                            <td style="width: 5%">
                                &nbsp;
                            </td>
                            <td class="FontTypes" style="width: 10%">
                                <asp:Label ID="lbToDate" runat="server" Text="To Date" CssClass="FontTypes"></asp:Label>
                            </td>
                            <td class="FontTypes" style="width: 15%">
                                <input name='txtCalendar1' id='idCalendar1' style="width: 120px; font-family: Verdana;
                                    font-size: 8pt" class='linksButton_' runat="server" readonly="readonly" />
                                <img src="Calander/calander4.jpg" align='top' onmouseover="fnInitCalendar(this,'ctl00_ContentPlaceHolder1_idCalendar1','close=true')" />
                            </td>
                            <td style="width: 5%">
                                &nbsp
                            </td>
                            <td style="width: 10%">
                                <asp:Label ID="lbStatus" runat="server" Text="Status" CssClass="FontTypes"></asp:Label>
                            </td>
                            <td style="width: 13%">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="FontTypes" Width="150px"
                                    Height="22px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td class="FontTypes" style="width: 10%">
                                <asp:Label ID="lbIntergrationId" runat="server" Text="Intergration Id" CssClass="FontTypes"></asp:Label>
                            </td>
                            <td class="FontTypes" style="width: 15%">
                                <asp:DropDownList ID="ddlIntergrationId" runat="server" CssClass="FontTypes" Width="150px"
                                    Height="25px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 5%">
                                &nbsp;
                            </td>
                            <td class="FontTypes" style="width: 10%">
                                &nbsp;
                            </td>
                            <td class="FontTypes" style="width: 15%">
                                &nbsp;
                            </td>
                            <td style="width: 5%">
                                &nbsp;
                            </td>
                            <td class="FontTypes" style="width: 10%">
                            </td>
                            <td style="width: 10%">
                                &nbsp
                            </td>
                            <td style="width: 13%">
                                &nbsp
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr style="width: 100%; height: 5px;">
            <td style="width: 100%; height: 5px;">
            </td>
        </tr>
        <tr>
            <td align="center" style="width: 100%; height: 10px; vertical-align: bottom">
                <asp:Button ID="btShowDetails" runat="server" OnClick="btShowDetails_Click" CssClass="FontTypes"
                    Text="Show Details" Width="90px" Height="22px" />
                &nbsp;
                <asp:Button ID="btClear" runat="server" Text="Clear" CssClass="FontTypes" Height="22px"
                    OnClick="btClear_Click" />
            </td>
        </tr>
        <tr style="width: 100%; height: 5px;">
            <td style="width: 100%; height: 5px;">
                <table cellpadding="2" cellspacing="0" width="100%">
                    <tr>
                        <td align="left" style="vertical-align: bottom">
                            &nbsp;
                            <asp:Button ID="btnAddToDashboard" runat="server" CssClass="FontTypes" Text="Add to Dashboard"
                                Height="22px" OnClick="btnAddToDashboard_Click" />
                        </td>
                        <td align="right" style="vertical-align: bottom">
                            <asp:LinkButton ID="lnkExportExcel" runat="server" CssClass="FontTypes" Height="22px"
                                Text="Export Excel" OnClick="lnkExportExcel_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="width: 100%; height: 400px">
        
            <td style="vertical-align: top">
            <asp:ScriptManager ID="scManager" runat="server" />
                                        <div>
                                            <asp:Timer ID="IntervalTimer" runat="server" Interval="10000" 
                                                ontick="IntervalTimer_Tick" />
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                <fieldset id="fsTable" runat="server">
                    <asp:Repeater ID="dgRptTable" runat="server" OnItemCommand="dgRptTable_ItemCommand"
                        OnItemDataBound="dgRptTable_ItemDataBound">
                        <HeaderTemplate>
                            <table width="100%" class="Border2" border="1" cellspacing="0" cellpadding="2">
                                <tr class="Border2" style="background-color: #5E9FD5; height: 35px">
                                    <td class="FontTypes">
                                    </td>
                                    <td class="FontTypes" style="color: #FFFFFF;">
                                        <b>Sno</b>
                                    </td>
                                    <%--<td>
                                        </td>--%>
                                    <td class="FontTypes" style="color: #FFFFFF;">
                                        <b>Client Name</b>
                                    </td>
                                    <%--<td>
                                        </td>--%>
                                    <td class="FontTypes" style="color: #FFFFFF;">
                                        <b>Client Type</b>
                                    </td>
                                    <td class="FontTypes" style="color: #FFFFFF;">
                                        <b>Integration Id</b>
                                    </td>
                                    <%--<td>
                                        </td>--%>
                                    <td class="FontTypes" style="color: #FFFFFF;">
                                        <b>Job Name</b>
                                    </td>
                                    <td class="FontTypes" style="color: #FFFFFF;">
                                        <b>Job Status</b>
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
                            <tr onmouseover="this.bgColor='#CEE7F4'" onmouseout="this.bgColor='#FFFFFF'" style="border-bottom-color: Gray;
                                border-bottom-style: solid; border-bottom-width: thin; height: 30px;">
                                <td>
                                    <asp:CheckBox ID="chkJobids" runat="server" AutoPostBack="true"  />
                                </td>
                                <td>
                                    <span class="FontTypes" style="margin-right: 20px;">
                                        <!--<%# Container.ItemIndex + 1 %>-->
                                        <asp:Label ID="lblSerial" runat="server" Text=""></asp:Label>
                                    </span>
                                </td>
                                <%-- <td >
                                        <asp:TextBox ID="txtGetDgPkey" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.pkey") %>'
                                            Visible="false" class="FontTypes"></asp:TextBox>
                                    </td>--%>
                                <td>
                                    <asp:LinkButton ID="lnkClientName" CommandName="CommClientName" runat="server" class="FontTypes"
                                        Text='<%# DataBinder.Eval(Container,"DataItem.ClientName") %>'></asp:LinkButton>
                                    <asp:TextBox ID="txtGetDgPkey" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.pkey") %>'
                                        Visible="false" class="FontTypes"></asp:TextBox>
                                    <asp:TextBox ID="txtGetFkeyMethods" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Fkey_methods") %>'
                                        Visible="false" class="FontTypes"></asp:TextBox>
                                    <%-- </td>
                                    <td >--%>
                                </td>
                                <td class="FontTypes">
                                    <%# DataBinder.Eval(Container,"DataItem.ERPs") %>
                                    <asp:TextBox ID="txtERPType" runat="server" class="FontTypes" Text='<%# DataBinder.Eval(Container,"DataItem.ERPs") %>'
                                        Visible="false"></asp:TextBox>
                                </td>
                                <td class="FontTypes">
                                    <%# DataBinder.Eval(Container, "DataItem.integration_id")%>
                                </td>
                                <%--<td class="FontTypes" >
                                        <asp:TextBox ID="txtDGJobName" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container,"DataItem.job_name") %>'>'></asp:TextBox>
                                    </td>--%>
                                <td class="FontTypes">
                                    <%# DataBinder.Eval(Container, "DataItem.job_name")%>
                                </td>
                                <td class="FontTypes">
                                    <asp:LinkButton ID="lnkJobStatus" CommandName="CommJobStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Method_Status")%>'></asp:LinkButton>
                                </td>
                                <td class="FontTypes">
                                    <%# DataBinder.Eval(Container, "DataItem.LastRunDate")%>
                                </td>
                                <td class="FontTypes">
                                    <%# DataBinder.Eval(Container, "DataItem.NextRunDate")%>
                                </td>
                                <td class="FontTypes">
                                    <%--<%# DataBinder.Eval(Container,  "DataItem.Comments")%>--%>
                                    <%#MakeScript(Convert.ToInt16(DataBinder.Eval(Container, "DataItem.pkey")))%>
                                    <%# ShowContent(DataBinder.Eval(Container.DataItem, "Comments"))%>
                                    <%# MakeLink(DataBinder.Eval(Container.DataItem, "Comments"), Convert.ToInt16(DataBinder.Eval(Container,"DataItem.pkey")))%>
                                </td>
                                <%# CreateHiddenField(DataBinder.Eval(Container.DataItem, "Comments"), Convert.ToInt16(DataBinder.Eval(Container, "DataItem.pkey")))%>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    </script>
                    <asp:Repeater ID="PgTable" runat="server" OnItemCommand="PgTable_OnItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton ID="pageLinkButton" CssClass="FontTypes" CommandName="<%#Container.DataItem%>"
                                runat="server" Text='<%#Container.DataItem%>' />
                        </ItemTemplate>
                    </asp:Repeater>
                    <!--start-->
                    <!--end-->
                    &nbsp;<asp:Label ID="Pagevaluelbl" runat="server" CssClass="FontTypes"></asp:Label>
                </fieldset>
                </ContentTemplate></asp:UpdatePanel>
            </td>
        
        </tr>
    </table>
</asp:Content>
