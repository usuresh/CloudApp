<%@ Page Title="" Language="C#" MasterPageFile="~/HeaderPage.Master" AutoEventWireup="true"
    CodeBehind="ManageUsers.aspx.cs" Inherits="NexusJobs.ManageUsers" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="Table1" runat="server" style="width: 100%; height: 100%; font-family: Tahoma;
        font-size: 11px;" border="1" cellspacing="10">
        
        <tr>
            <td valign="top" style="width: 50%">
                <table style="width: 95%; vertical-align: top" align="center">
                <tr><td><!--ui-->
                
                            <table style="width: 100%; font-family: Tahoma; font-size: 11px; vertical-align: top"
                                align="center">
                                <tr><td class="style1"></td><td><asp:Label 
                                        ID="lblUserAdmin" runat="server" Text="User Admin" 
                                        style="font-weight: 700;"></asp:Label>
                                    </td><td ></td></tr><tr><td></td></tr>
                                <tr>
                                    <td style="width: 40%">
                                        Access Type
                                    </td>
                                    <td style="width: 40%">
                                        <asp:DropDownList ID="ddlAccessType" runat="server" Width="51%">
                                            <asp:ListItem>Admin</asp:ListItem>
                                            <asp:ListItem>Managers</asp:ListItem>
                                            <asp:ListItem>Users</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%;">
                                        User Name
                                    </td>
                                    <td style="width: 40%">
                                        <asp:TextBox ID="txtUserName" runat="server" Width="50%" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        Password
                                    </td>
                                    <td style="width: 40%">
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="50%" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        Email
                                    </td>
                                    <td style="width: 40%">
                                        <asp:TextBox ID="txtEmail" runat="server" Width="50%" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        First Name
                                    </td>
                                    <td style="width: 40%">
                                        <asp:TextBox ID="txtFirstName" runat="server" Width="50%" MaxLength="15"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        Last Name
                                    </td>
                                    <td style="width: 40%">
                                        <asp:TextBox ID="txtLastName" runat="server" Width="50%" MaxLength="15"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        Log Days
                                    </td>
                                    <td style="width: 40%">
                                        <asp:DropDownList ID="ddlLogDays" runat="server" Width="51%">
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>60</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        Records per Grid
                                    </td>
                                    <td style="width: 40%">
                                        <asp:DropDownList ID="ddlRecordsPerGrid" runat="server" Width="51%">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr align="left">
                                    <td style="width: 40%;">
                                        
                             
                                    </td>
                                    <td style="width: 20%" >
                                             <asp:Button ID="btnAddUsers" runat="server" Text="Add Users" OnClick="btnAddUsers_Click"
                                Width="100px"></asp:Button> &nbsp
                                          <asp:Button ID="btnSave" runat="server" Text="Save All" OnClick="btnSave_Click" ValidationGroup="EmailCheck"
                                            Width="100px" style="text-align: center" /></td>
                                    <td style="width: 40%" align="left">
                                        &nbsp;</td>
                                </tr>
                            </table></td></tr>
                    <tr>
                        <td align="right">
                            <%--<asp:Button ID="btnAddUsers" runat="server" Text="Add Users" OnClick="btnAddUsers_Click"
                                Width="100px"></asp:Button>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblErrorMessage" runat="server" Text="No data to display" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                            <asp:Repeater ID="rptUsers" runat="server" OnItemCommand="rptUsers_ItemCommand">
                                <HeaderTemplate>
                                    <table id="table1" width="100%" border="1" cellspacing="0" cellpadding="2">
                                        <tr style="background-color: #5E9FD5; height: 35px">
                                            <td class="FontTypes" style="color: #FFFFFF;">
                                                <b>User</b>
                                            </td>
                                            <td class="FontTypes" style="color: #FFFFFF;">
                                                <b>Type</b>
                                            </td>
                                            <%--<td>
                                            </td>--%>
                                            <td class="FontTypes" style="color: #FFFFFF;">
                                                <b>Delete</b>
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr onmouseover="this.bgColor='#FFF8DC'" onmouseout="this.bgColor='#FFFFFF'" id="tr1"
                                        runat="server" style="border-bottom-color: Gray; border-bottom-style: solid;
                                        border-bottom-width: 1px; height: 20px;">
                                        <td class="FontTypes">
                                            <%-- <%# DataBinder.Eval(Container, "DataItem.USER")%>--%>
                                            <asp:LinkButton ID="lbUser" runat="server" CommandName="lbtnUser" Font-Underline="False"
                                                ForeColor="Black" Text='<%# DataBinder.Eval(Container, "DataItem.USER")%>'></asp:LinkButton>
                                        </td>
                                        <td class="FontTypes">
                                            <%--<%# DataBinder.Eval(Container, "DataItem.TYPE")%>--%>
                                            <asp:LinkButton ID="lbType" runat="server" CommandName="lbtnType" Font-Underline="False"
                                                ForeColor="Black" Text='<%# DataBinder.Eval(Container, "DataItem.TYPE")%>'></asp:LinkButton>
                                        </td>
                                        <%--   <td style="border-bottom-color: Gray; border-bottom-style: solid; border-bottom-width: thin;">
                                            <asp:TextBox ID="txtHiddenPkey" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PKEY") %>'
                                                Visible="false"></asp:TextBox>
                                        </td>--%>
                                        <td class="FontTypes">
                                            <asp:ImageButton ID="ibDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                ImageUrl="~/Images/Delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure about deleting ..?');" />
                                            <asp:TextBox ID="txtHiddenPkey" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PKEY") %>'
                                                Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="rptPages" runat="server" OnItemCommand="rptPages_ItemCommand">
                                <ItemTemplate>
                                    <asp:LinkButton ID="pageLinkButton" CssClass="FontTypes" CommandName="<%#Container.DataItem%>"
                                        runat="server" Text='<%#Container.DataItem%>' />
                                </ItemTemplate>
                            </asp:Repeater>
                            &nbsp;<asp:Label ID="lblPageValue" runat="server" CssClass="FontTypes"></asp:Label>
                        </td>
                        </tr>
                        <tr><td>&nbsp</td></tr>
                        <tr align="right">
                        <td>
                          <asp:TextBox ID="txtSearch" runat="server"  Width="100%"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                Width="100px" style="text-align: center; "></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%;" valign="top"  >
            
            <table style="width: 95%; font-family: Tahoma; font-size: 11px; vertical-align: top;"
                                align="center">
                                <tr><td></td><td style="font-weight: 700">
                                    <asp:Label 
                                        ID="lblErpClientAdmin" runat="server" Text="ERP & Client Admin" 
                                        style="font-weight: 700;"></asp:Label>
                                    </td><td></td></tr>
                                <tr><td></td></tr>
                                <tr>
                                    <td style="width: 40%">
                                        ERP Type
                                    </td>
                                    <td style="width: 40%">
                                        <asp:TextBox ID="txtErpType" runat="server" Width="51%"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%;">
                                        Contact 
                                    </td>
                                    <td style="width: 40%">
                                        <asp:TextBox ID="txtContactInfo" runat="server" Width="51%"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        &nbsp;</td>
                                    <td style="width: 40%">
                                        <asp:Button ID="btnAddErp" runat="server" Text="Add ERP" Width="100px" 
                                            onclick="btnAddErp_Click" /> &nbsp
                                        <asp:Button ID="btnSaveERP" runat="server" Text="Save Erp Type" Width="100px" 
                                            onclick="btnSaveERP_Click" />
                                    </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        &nbsp;</td>
                                    <td style="width: 40%">
                                        &nbsp;</td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        &nbsp;</td>
                                    <td style="width: 40%">
                                        &nbsp;</td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        &nbsp;</td>
                                    <td style="width: 40%">
                                        &nbsp;</td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        &nbsp;</td>
                                    <td style="width: 40%">
                                        &nbsp;</td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <table table style="width: 95%; font-family: Tahoma; font-size: 11px; vertical-align: top;"
                                align="center"><tr>
                             <td style="width: 40%">
                                        &nbsp;</td>
                                    <td style="width: 40%">
                                        &nbsp;</td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        ERP Name</td>
                                    <td style="width: 40%">
                                             <asp:DropDownList ID="ddlErps" runat="server" Width="51%">
                                             </asp:DropDownList>
                                          </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        Client Name</td>
                                    <td style="width: 40%">
                                        <asp:TextBox ID="txtClientName" runat="server" Width="51%"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        Client Info.</td>
                                    <td style="width: 40%">
                                        <asp:TextBox ID="txtClientInfo" runat="server" Width="51%"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        Contact Info.</td>
                                    <td style="width: 40%">
                                        <asp:TextBox ID="txtClientContact" runat="server" Width="51%"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%">
                                        Methods Type</td>
                                    <td style="width: 40%">
                                        <asp:RadioButton  ID="rbWithoutJobCost" Text="Without Job Casting" 
                                            runat="server" Checked="True" GroupName="Client" /> &nbsp
                                        <asp:RadioButton ID="rbWithJobCost" Text="With Job Casting" GroupName="Client" runat="server" />
                                    </td>
                                    <td style="width: 20%" align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr align="left">
                                    <td style="width: 40%;">
                                        
                             
                                        ERP Name</td>
                                    <td style="width: 20%" >
                                             <asp:Button ID="btnAddClient" runat="server" Text="Add Client"
                                Width="100px" onclick="btnAddClient_Click"></asp:Button> &nbsp
                                             <asp:Button ID="btnSaveClient" runat="server" Text="Save Client"
                                Width="100px" onclick="btnSaveClient_Click"></asp:Button> </td>
                                    <td style="width: 40%" align="left">
                                        &nbsp;</td>
                            </tr>
                            </table>
                            
            </td>
        </tr>
    </table>
</asp:Content>
