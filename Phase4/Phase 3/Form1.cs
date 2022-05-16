using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LazarAlexandruConstantin
{
    public partial class Form1 : Form
    {
        private DateTimePicker dateTimePicker1 = new DateTimePicker();
        private Project project;

        public Form1()
        {
            project = new Project();
            InitializeComponent();
            SetDataGridProperties();
            ResizeDataGridView();
        }

        private Task GetTask(int taskID)
        {
            return project.tasks.GetTask(taskID);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            // Ensure the Form remains square (Height = Width).
            if (control.Size.Height >= Screen.FromControl(this).Bounds.Height / 2 && control.Size.Width >= Screen.FromControl(this).Bounds.Width / 2)
            {
                control.Size = new Size(control.Size.Width, control.Size.Height);
            }
            else
            {
                control.Size = new Size(Screen.FromControl(this).Bounds.Width / 2, Screen.FromControl(this).Bounds.Height / 2);
            }
            ResizeDataGridView();
            ResizeDataGridViewColumns();
        }

        private void ResizeDataGridView()
        {
            dataGridView1.Width = this.Width / 10 * 9;
            dataGridView1.Height = this.Height / 10 * 8; ;
        }

        private void ResizeDataGridViewColumns()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                this.dataGridView1.Columns[Columns.TaskID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView1.Columns[Columns.TaskName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView1.Columns[Columns.Duration].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView1.Columns[Columns.Start].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView1.Columns[Columns.Finish].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView1.Columns[Columns.Predecessors].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView1.Columns[Columns.ResourceNames].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                this.dataGridView1.Columns[Columns.TaskMode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dateTimePicker1.Dispose();
                if (e.ColumnIndex == Columns.Start || e.ColumnIndex == Columns.Finish)
                {
                    dateTimePicker1 = new DateTimePicker();
                    dataGridView1.Controls.Add(dateTimePicker1);
                    dateTimePicker1.Format = DateTimePickerFormat.Custom;
                    dateTimePicker1.CustomFormat = "M/dd/yyyy hh:mm:ss tt";
                    Rectangle oRectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    dateTimePicker1.Size = new Size(oRectangle.Width, oRectangle.Height);
                    dateTimePicker1.Location = new Point(oRectangle.X, oRectangle.Y);
                    dateTimePicker1.TextChanged += new EventHandler(DateTimePickerChange);
                    dateTimePicker1.CloseUp += new EventHandler(DateTimePickerClose);
                }
            }
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int rowTaskID = int.Parse(dataGridView1[0, e.RowIndex].Value.ToString().Trim());
                var currentCellValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                switch (e.ColumnIndex)
                {
                    case Columns.TaskName:
                        GetTask(rowTaskID).Name = currentCellValue.ToString();
                        break;
                    case Columns.Duration:
                        if (currentCellValue.ToString().IsInteger())
                        {
                            GetTask(rowTaskID).Duration = int.Parse(currentCellValue.ToString());
                        }
                        else
                        {
                            MessageBox.Show("The value must be an integer");
                            dataGridView1[e.ColumnIndex, e.RowIndex].Value = GetTask(rowTaskID).Duration;
                        }
                        break;
                    case Columns.Start:
                        break;
                    default:
                        break;
                }
            }
        }

        private void DateTimePickerChange(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = dateTimePicker1.Text.ToString();
            int taskID = (int)dataGridView1[Columns.TaskID, dataGridView1.CurrentCell.RowIndex].Value;

            var selectedDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month,
                    dateTimePicker1.Value.Day, dateTimePicker1.Value.Hour, dateTimePicker1.Value.Minute,
                    dateTimePicker1.Value.Second);

            if (dataGridView1.CurrentCell.ColumnIndex == Columns.Start)
            {
                GetTask(taskID).Start = selectedDate;
            }
            else
            {
                GetTask(taskID).Finish = selectedDate;
            }
        }

        private void DateTimePickerClose(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = false;
            dateTimePicker1.Hide();
        }

        private void SetDataGridProperties()
        {
            dataGridView1.ColumnCount = 8;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            dataGridView1.Columns[Columns.TaskID].Name = "Task ID";
            dataGridView1.Columns[Columns.TaskName].Name = "Task Name";
            dataGridView1.Columns[Columns.Duration].Name = "Duration";
            dataGridView1.Columns[Columns.Start].Name = "Start";
            dataGridView1.Columns[Columns.Finish].Name = "Finish";
            dataGridView1.Columns[Columns.Predecessors].Name = "Predecessors";
            dataGridView1.Columns[Columns.ResourceNames].Name = "ResourceNames";
            dataGridView1.Columns[Columns.TaskMode].Name = "TaskMode";
            dataGridView1.Columns[Columns.TaskID].ReadOnly = true;
            dataGridView1.Columns[Columns.Start].ReadOnly = true;
            dataGridView1.Columns[Columns.Finish].ReadOnly = true;
            dataGridView1.Columns[Columns.ResourceNames].ReadOnly = true;
            dataGridView1.Columns[Columns.Predecessors].ReadOnly = true;
            dataGridView1.Columns[Columns.TaskMode].ReadOnly = true;
        }

        private void LoadDataGrid()
        {

            dataGridView1.Rows.Clear();
            int i = 1;
            foreach (var task in project.tasks)
            {
                dataGridView1.Rows.Add(task.TaskID, task.Name, task.Duration, task.Start, task.Finish, task.Predecessors.ContentToString(),
                task.ResourceNames.ContentToString(), task.TaskMode);
                i++;
            }
            ResizeDataGridViewColumns();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit this application?",
                                "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFD.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFD.Title = "Select XML file";
            openFD.Filter = "XML|*.xml|All Files|*.*";
            if (openFD.ShowDialog() != DialogResult.Cancel)
            {
                string chosenFile = openFD.FileName;
                project = project.Deserialize(chosenFile);
                if (project == null)
                {
                    MessageBox.Show("The provided XML files contains an empty object!");
                }
                else
                {
                    LoadDataGrid();
                }
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string savedFile;
            saveFD.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFD.Title = "Save XML file";
            saveFD.Filter = "XML|*.xml|All Files|*.*";
            saveFD.FileName = "program";
            if (saveFD.ShowDialog() != DialogResult.Cancel)
            {
                savedFile = saveFD.FileName;
                project.Serialize(savedFile);
            }
        }

        private void InitializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            project.Initialize("input.txt");
            LoadDataGrid();
        }

        private static class Columns
        {
            public const int TaskID = 0;
            public const int TaskName = 1;
            public const int Duration = 2;
            public const int Start = 3;
            public const int Finish = 4;
            public const int Predecessors = 5;
            public const int ResourceNames = 6;
            public const int TaskMode = 7;
        }

    }
}
