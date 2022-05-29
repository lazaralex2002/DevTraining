using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using LazarAlexandruConstantin;

namespace WebForms
{
    public partial class _Default : Page
    {
        private static Project project;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string GetStatus()
        {
            return "Welcome PageMethods";
        }

        [System.Web.Services.WebMethod]
        public static string Initialize()
        {
            initizeProject();
            return project.Tasks.Count().ToString();
        }

        private static void initizeProject()
        {
            project = new Project();
            string fileName=  "input.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            Debug.WriteLine(filePath);
            try
            {
                project.Initialize(filePath);
            }
            catch(Exception e )
            {
                throw e;
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetCell(int rowIndex, int columnIndex)
        {
            rowIndex += 1;
            switch(columnIndex)
            {
                case Columns.TaskID:
                    return project.Tasks.GetTask(rowIndex).TaskID.ToString();
                case Columns.TaskName:
                    return project.Tasks.GetTask(rowIndex).Name;
                case Columns.Duration:
                    return project.Tasks.GetTask(rowIndex).Duration.ToString();
                case Columns.Start:
                    return project.Tasks.GetTask(rowIndex).Start.ToString();
                case Columns.Finish:
                    return project.Tasks.GetTask(rowIndex).Finish.ToString();
                case Columns.Predecessors:
                    return project.Tasks.GetTask(rowIndex).Predecessors.ContentToString();
                case Columns.ResourceNames:
                    return project.Tasks.GetTask(rowIndex).ResourceNames.ContentToString();
                case Columns.TaskMode:
                    return project.Tasks.GetTask(rowIndex).TaskMode.ToString();
                default:
                    return "undefined";
            }
        }
    }
}