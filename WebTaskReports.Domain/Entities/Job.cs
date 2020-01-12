using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using WebTaskReports.Domain.Entities.Base;
using WebTaskReports.Domain.Entities.Base.Interfaces;
using WebTaskReports.Domain.Entities;

namespace WebTaskReports.Domain.Entities
{
    public class Job : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int? UserId { get; set; }
        public int? ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual ICollection<Category> Categories { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }

    }
}
