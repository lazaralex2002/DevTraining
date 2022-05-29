<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%" class="table table-bordered table-hover" >
        <thead class="thead-dark " >
            <tr>
                <th scope="col">Company</th>
                <th scope="col">Contact</th>
                <th scope="col">Country</th>
            </tr>
        </thead>
        <tbody>
            <tr>
              <td>Mark</td>
              <td>Otto</td>
              <td>@mdo</td>
            </tr>
            <tr>
              <td>Jacob</td>
              <td>Thornton</td>
              <td>@fat</td>
            </tr>
            <tr>
              <td>Larry</td>
              <td>the Bird</td>
              <td>@twitter</td>
            </tr>
        </tbody>
</table>
<asp:Button ID="Button1" runat="server" Text="Call Page Methods"  OnClientClick="return fun()"/>
<div>
    <asp:Button ID="Button2"
        Text="AJAX METHOD CALL"
        OnClientClick="BindTreeView()"
        runat="server" />
    <asp:Button ID="Button3"
        Text="PAGE METHOD JS CALL"
        OnClientClick="fun()"
        runat="server" />
    <asp:Button ID="Button4"
        Text="XML FORMAT PAGE ELEMENTS"
        OnClientClick="GetRss()"
        runat="server" />
</div>
<div id="divOutput">
</div>

<script>
    function fun()
    {
        PageMethods.GetStatus(onSucceed, onError);
        return false;
    }

    function onSucceed(result)
    {
        alert(result);
    }

    function onError(result) {
        alert(result.get_message());
    }

    function BindTreeView() {
        $.ajax({
            type: "POST",
            url: "Default.aspx/test",
            data: '{ querytype: "1" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessTree,
            failure: function (response) {
                alert("failure");
            }
        });
    }
    function OnSuccessTree(response) {
        console.log(response.d)
        alert("entered")
        if (response.d.length > 0) {
            alert("works")
        }
        return false;
    }

    function GetRss() {
        InterfaceTraining.DemoService.GetRssFeed(
            "https://blogs.interfacett.com/dan-wahlins-blog/rss.xml",
            OnWSRequestComplete);
    }

    function OnWSRequestComplete(result) {
        if (document.all) //Filter for IE DOM since other browsers are limited
        {
            var items = result.selectNodes("//item");
            for (var i = 0; i < items.length; i++) {
                var title = items[i].selectSingleNode("title").text;
                var href = items[i].selectSingleNode("link").text;
                $get("divOutput").innerHTML +=
                    "<a href='" + href + "'>" + title + "</a><br/>";
            }
        }
        else {
            $get("divOutput").innerHTML = "RSS only available in IE5+";
        }
    }
</script>
</asp:Content>
