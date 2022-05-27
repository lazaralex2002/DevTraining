<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Default" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server" >

    <div>
        <asp:Menu ID="Menu" runat="server"  IncludeStyleBlock="false" Orientation="Horizontal" CssClass="navbar navbar-fixed-top" 
            StaticMenuStyle-CssClass= "nav" DynamicMenuStyle-CssClass="dropdown-menu"
            onmenuitemclick="NavigationMenu_MenuItemClick" OnMenuItemDataBound="OnMenuItemDataBound">
            <items>
                <asp:menuitem text="File" tooltip="File" >
                    <asp:menuitem text="Initialize" tooltip="Initialize"></asp:menuitem>
                    <asp:menuitem text="Save" tooltip="Save"></asp:menuitem>
                    <asp:menuitem text="Open" tooltip="Open"></asp:menuitem>
                    <asp:menuitem text="Quit" tooltip="Quit"></asp:menuitem>
                </asp:menuitem>
            </items>
        </asp:Menu>
        <asp:GridView BorderColor="black" BorderWidth="1" CellPadding="3" AutoGenerateColumns="true"
            ID="GridView1" runat="server" Height="516px" style="margin-top: 74px" Width="100%" BackColor="YellowGreen"
            class="table table-bordered table-condensed table-responsive "></asp:GridView>
          <asp:FileUpload ID="FileUpload1" runat="server" style="display:none" />
    </div>
</asp:Content>