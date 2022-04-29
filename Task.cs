using System;
using System.Collections.Generic;
using System.Text;

namespace Phase2
{
    class Task
    {
        private int taskID;
        public string Name { get; set; }
        public double Duration { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public HashSet<string> Predecessors ;
        public HashSet<string> ResourceNames;
        public Task_Mode TaskMode;

        public Task(int taskId)
        {
            this.taskID = taskId;
            this.Duration = 1;
            this.Start = System.DateTime.Today;
            this.Finish = System.DateTime.Today;
            TaskMode = Task_Mode.No;
            Predecessors = new HashSet<string>();
            ResourceNames = new HashSet<string>();
        }

        public void SetPredecessors(string predecessors)
        {
            this.Predecessors.Add(predecessors);
        }

        public void SetResourceNames ( string resourceNames )
        {
            this.ResourceNames.Add(resourceNames);
        }

        public void SetResourceNames(HashSet<string> resourceNames)
        {
            ResourceNames = new HashSet<string>();
            foreach (String i in resourceNames)
            {
                this.ResourceNames.Add(i);
            }
        }
        public override bool Equals(object obj)
        {
            if ( obj == null || !this.GetType().Equals(obj.GetType()) )
            {
                return false;
            }
            Task t = (Task)obj;
            return this.taskID == t.taskID;
        }


        public override string ToString()
        {
            string msg = "";
            msg += "taskID: " + taskID + '\n';
            msg += "name: " + Name + '\n';
            msg += "duration: " + Duration + '\n';
            msg += "start: " + Start + '\n';
            msg += "finish: " + Finish + '\n';
            msg += "resource names: ";
            foreach(var resouce in ResourceNames )
            {
                msg += resouce + " ";
            }
            msg += '\n';
            msg += "predecessors: ";
            foreach (var pred in Predecessors)
            {
                msg += pred + " ";
            }
            msg += '\n';
            msg += "task mode: " + TaskMode + '\n';
            msg +=  '\n';
            return msg;
        }
        public override int GetHashCode()
        {
            return taskID;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public enum Task_Mode
    {
        Yes,
        No
    };
}
