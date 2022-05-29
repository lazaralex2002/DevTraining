<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
      <div class="dropdown">
      <div class="dropbtn">File</div>
      <div class="dropdown-content">
        <a onclick="InitializeButtonClicked()">Initialize</a>
        <a href="#">Save</a>
        <a href="#">Open</a>
      </div>
    </div>

    <table id="table" style="width:100%" class="table table-bordered table-hover" >
        <thead class="thead-dark " >
            <tr>
                <th scope="col">TaskID</th>
                <th scope="col">TaskName</th>
                <th scope="col">Duration</th>
                <th scope="col">Start</th>
                <th scope="col">Finish</th>
                <th scope="col">Predecessors</th>
                <th scope="col">ResourceNames</th>
                <th scope="col">TaskMode</th>
            </tr>
        </thead>
        <tbody id ="tableBody">
            
        </tbody>
</table>

<script>
    function ProjectInitialized(result)
    {
        replaceBody();
        var tbodyRef = document.getElementById('table').getElementsByTagName('tbody')[0];
        var nrOfRows = parseInt(result);

        for (let i = 0; i < nrOfRows; i++)
        {
            var newRow = tbodyRef.insertRow();
            insertRow(i , newRow);
        }
    }

    function replaceBody() {
        const old_tbody = document.getElementById("tableBody")
        const new_tbody = document.createElement('tbody');
        old_tbody.parentNode.replaceChild(new_tbody, old_tbody)
    }

    function onError(result) {
        alert(result.get_message());
    }

    function InitializeButtonClicked()
    {
        PageMethods.Initialize(ProjectInitialized, onError);
    }

    function insertRow(index, row) {
        for (let i = 0; i < 8; ++i)
        {
            var newText;
            PageMethods.GetCell( index, i,
                function (response) {
                    var newCell = row.insertCell();
                    newText = document.createTextNode(response.toString());
                    newCell.appendChild(newText);
                } 
                , onError);

        }
    }

    function insertCell(result) {
        $("table").find('tbody').append("<td>" + result + "</td>");
    }


</script>
</asp:Content>
