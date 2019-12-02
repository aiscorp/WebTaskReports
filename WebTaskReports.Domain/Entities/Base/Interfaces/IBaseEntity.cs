using System;
using System.Collections.Generic;
using System.Text;

namespace WebTaskReports.Domain.Entities.Base.Interfaces
{
    /// <summary>Сущность</summary>
    public interface IBaseEntity
    {
        /// <summary>Идентификатор</summary>
        int Id { get; set; }
    }
}
