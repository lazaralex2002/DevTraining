//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            this.ResourceTasks = new HashSet<ResourceTask>();
            this.TaskPredecessors = new HashSet<TaskPredecessor>();
            this.TaskPredecessors1 = new HashSet<TaskPredecessor>();
        }
    
        public int TaskId { get; set; }

        [StringLength(30)]
        public string Name { get; set; }
        public Nullable<decimal> Duration { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> Start { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> Finish { get; set; }
        public Nullable<int> TaskMode { get; set; }
        public int ProjectId { get; set; }
    
        public virtual Project Project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResourceTask> ResourceTasks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskPredecessor> TaskPredecessors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskPredecessor> TaskPredecessors1 { get; set; }
    }
}