using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Phase2
{
	class Project
	{
		protected HashSet<Task> tasks;

		public Project()
		{
			tasks = new HashSet<Task>();
		}
		
		public void Initialize(String fileName)
		{
			String[] lines = null;
			String fullName =  Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + '\\' + fileName ;
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
				foreach (String line in lines)
				{
					String l  = line.Trim();
					if (l.StartsWith("SetTaskField"))
					{
						l = l.Substring(l.IndexOf("SetTaskField") + "SetTaskField".Length);
						l = l.Trim();
						String[] fields = l.Split(", ");
						Dictionary<String, String> properties = new Dictionary<String, String>();
						int taskID = 0;
						for (int i = 0; i < fields.Length; ++i )
						{
							int c;
							if (fields[i].StartsWith("Field")) c = 0;
							else
							{
								if (fields[i].StartsWith("TaskID")) c = 1;
								else c = 2;
							}
							switch (c)
							{
								case 0:
									String propertyName = fields[i].Substring(fields[i].IndexOf('=') + 1).Trim('"');
									String value = fields[i + 1].Substring(fields[i].IndexOf('=') + 1).Trim('"');
									properties.Add(propertyName, value);
									i++;
									//Console.WriteLine(propertyName + ' ' + value);
									break;
								case 1:
									taskID = int.Parse(fields[i].Substring(fields[i].IndexOf('=') + 1).Trim('"'));
									//Console.WriteLine(taskID);
									break;
								default:
									break;
							}
						}
						Task currentTask = new Task(taskID, System.DateTime.Now, System.DateTime.Now);
						if (tasks.Contains(currentTask))
                        {
							foreach ( Task t in tasks )
                            {
								if ( t.Equals(currentTask))
                                {
									currentTask = t;
									tasks.Remove(t);
									break;
                                }
                            }
                        }

						foreach (String key in properties.Keys)
						{
							switch ( key )
                            {
								case "Name":
									currentTask.name = properties[key];
									break;
								case "Duration":
									currentTask.duration = Double.Parse(properties[key]);
									break;
								case "Start":
									currentTask.start = DateTime.Parse(properties[key]);
									break;
								case "Finish":
									currentTask.finish = DateTime.Parse(properties[key]);
									break;
								case "TaskMode":
									if ( properties[key].Equals("Yes") )
                                    {
										currentTask.taskMode = TASK_MODE.Yes;
                                    }
									else
                                    {
										currentTask.taskMode = TASK_MODE.No;
									}
									break;
								case "Resourse Names":
									currentTask.setResourceNames(properties[key]);
									break;
								default:
									break;
                            }
						}
						tasks.Add(currentTask);
					}
				}
			}

		}
		public override string ToString()
		{
			String msg = "";
			foreach(Task t in tasks )
            {
				msg += t.ToString();
            }
			return msg;
		}
	}
}
