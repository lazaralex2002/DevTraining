using System;
using System.Collections.Generic;
using System.Text;

namespace Phase2
{
    class Task
    {
        protected int taskID;
        public String name { get; set; }
        public double duration { get; set; }
        public DateTime start { get; set; }
        public DateTime finish { get; set; }
        protected HashSet<Task> predecessors;
        protected String resourceNames;
        public TASK_MODE taskMode;

        public Task(int taskId, DateTime start, DateTime finish)
        {
            this.taskID = taskId;
            this.duration = 1;
            this.start = start;
            this.finish = finish;
            taskMode = TASK_MODE.No;
        }
        
      

        public void setPredecessors ( HashSet<Task> predecessors)
        {
            foreach( Task i in predecessors)
            {
                this.predecessors.Add(i);
            }
        }

        public void setResourceNames ( String resourceNames )
        {
            this.resourceNames = resourceNames;
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
            String msg = "";
            msg += "taskID: " + taskID + '\n';
            msg += "name: " + name + '\n';
            msg += "duration: " + duration + '\n';
            msg += "start: " + start + '\n';
            msg += "finish: " + finish + '\n';
            msg += "resource names: " + resourceNames + '\n';
            msg += "task mode: " + taskMode + '\n';
            msg +=  '\n';
            return msg;
        }
        public override int GetHashCode()
        {
            return taskID;
        }
    }
    public enum TASK_MODE
    {
        Yes,
        No
    };
}
