using System;
using System.Collections.Generic;
using System.Text;
using WebTaskReports.Domain.Entities.Base;
using WebTaskReports.Domain.Entities.Base.Interfaces;

namespace WebTaskReports.Domain.Entities
{

    public class Project : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int? UserId { get; set; }
    }
}
