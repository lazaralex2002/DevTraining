using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LazarAlexandruConstantin;

namespace WebForm
{
    public partial class Default : Page
    {
        Project project;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"Scripts\myJs.js"));
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "hideFileLoader()", true);
        }

        public void NavigationMenu_MenuItemClick(Object sender, MenuEventArgs e)
        {
            // Display the text of the menu item selected by the user.
            switch (e.Item.Text.Trim())
            {
                case "Initialize":
                    initialize();
                    break;
                case "Save":
                    save(sender, e);
                    break;
                case "Open":
                    break;
                case "Quit":
                    quit();
                    break;
                default:
                    break;
            }
        }

        private void initialize()
        {
            project = new Project();
            try
            {
                project.Initialize("input.txt");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            GridView1.DataSource = CreateDataSource();
            GridView1.DataBind();
        }

        private void quit()
        {
            GridView1.DataSource = new DataView();
            GridView1.DataBind();
        }

        private void save(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "x", "saveFileDialog()", true);
        }

        DataView CreateDataSource()
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow;

            dataTable.Columns.Add(new DataColumn("TaskID", typeof(Int32)));
            dataTable.Columns.Add(new DataColumn("Task Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Duration", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Start", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Finish", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Predecessors", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Rersource Names", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Task Mode", typeof(double)));

            foreach (var task in project.Tasks)
            {
                dataRow = dataTable.NewRow();
                dataRow[Columns.TaskID] = task.TaskID;
                dataRow[Columns.TaskName] = task.Name;
                dataRow[Columns.Duration] = task.Duration;
                dataRow[Columns.Finish] = task.Finish;
                dataRow[Columns.Start] = task.Start;
                dataRow[Columns.TaskMode] = task.TaskMode;
                dataRow[Columns.Predecessors] = task.Predecessors.ContentToString();
                dataRow[Columns.RersourceNames] = task.ResourceNames.ContentToString();

                dataTable.Rows.Add(dataRow);
            }

            DataView dv = new DataView(dataTable);
            return dv;
        }

        protected void OnMenuItemDataBound(object sender, MenuEventArgs e)
        {
            if (SiteMap.CurrentNode != null)
            {
                if (e.Item.Text == SiteMap.CurrentNode.Title)
                {
                    if (e.Item.Parent != null)
                    {
                        e.Item.Parent.Selected = true;
                    }
                    else
                    {
                        e.Item.Selected = true;
                    }
                }
            }
        }

        private static class Columns
        {
            public static int TaskID = 0;
            public static int TaskName = 1;
            public static int Duration = 2;
            public static int Start = 3;
            public static int Finish = 4;
            public static int Predecessors = 5;
            public static int RersourceNames = 6;
            public static int TaskMode = 7;
        }
    }
}