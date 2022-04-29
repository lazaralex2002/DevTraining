using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Phase2
{
	class Project
	{
		private HashSet<Task> tasks;

		public Project()
		{
			tasks = new HashSet<Task>();
		}
		
		public void Initialize(string fileName)
		{
			string[] lines = null;
			string fullName =  Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + '\\' + fileName ;
			try 
			{
				lines = System.IO.File.ReadAllLines(fullName);
			}
			catch ( Exception e )
			{
				Console.WriteLine(e);
			}
			if ( lines != null )
			{

				AddTasks(lines);
			}

		}

		private void AddTasks( string[] lines)
        {
			foreach (var l in lines)
			{
				string line = l.Trim();
				if (line.StartsWith("SetTaskField"))
				{
					line = line.Substring(line.IndexOf("SetTaskField") + "SetTaskField".Length);
					line = line.Trim();
					string[] fields = line.Split(", ");
					var properties = new Dictionary<string, string>();
					int taskID = 0;
					for (int i = 0; i < fields.Length; ++i)
					{
						int case1 = -1;
						if (fields[i].StartsWith("Field"))
						{
							case1 = 0;
						}
						else if (fields[i].StartsWith("TaskID"))
						{
							case1 = 1;
						}
						else
						{
							case1 = 2;
						}
						switch (case1)
						{
							case 0:
								string propertyName = fields[i].Substring(fields[i].IndexOf('=') + 1).Trim('"');
								string value = fields[i + 1].Substring(fields[i].IndexOf('=') + 1).Trim('"');
								properties.Add(propertyName, value);
								i++;
								break;
							case 1:
								taskID = int.Parse(fields[i].Substring(fields[i].IndexOf('=') + 1).Trim('"'));
								break;
							default:
								break;
						}
					}
					var currentTask = new Task(taskID);
					if (!Exists(currentTask))
					{
						AddTaskProperties(currentTask, properties);
						tasks.Add(currentTask);
					}
					else
                    {
						foreach (var t in tasks)
						{
							if (t.Equals(currentTask))
							{
								AddTaskProperties(t, properties);
							}
						}
					}
				}
			}
		}

		private void UpdateTask(Task currentTask)
        {
			foreach (Task t in tasks)
			{
				if (t.Equals(currentTask))
				{
					currentTask = t;
					break;
				}
			}
		}

		private bool Exists ( Task currentTask )
        {
			if (tasks.Contains(currentTask))
			{
				return true;
			}
			return false;
		}

		private void AddTaskProperties( Task currentTask, Dictionary<string, string> properties)
        {
			foreach (string key1 in properties.Keys)
			{
				string key = key1.Trim();
				switch (key)
				{
					case "Name":
						currentTask.Name = properties[key];
						break;
					case "Duration":
						currentTask.Duration = Double.Parse(properties[key]);
						break;
					case "Start":
						currentTask.Start = DateTime.Parse(properties[key]);
						break;
					case "Finish":
						currentTask.Finish = DateTime.Parse(properties[key]);
						break;
					case "Task Mode":
						if ( properties[key].Trim().Equals("Yes"))
                        {
							currentTask.TaskMode = Task_Mode.Yes;
						}
						else if (properties[key].Trim().Equals("No"))
						{
							currentTask.TaskMode = Task_Mode.No;
                        }
						break;
					case "Resource Names":
						currentTask.SetResourceNames(properties[key]);
						break;
					case "Predecessors":
						currentTask.SetPredecessors(properties[key]);
						break;
					default:
						break;
				}
			}
		}

		public override string ToString()
		{
			string msg = "";
			foreach(var t in tasks )
            {
				msg += t.ToString();
            }
			return msg;
		}
	}
}
