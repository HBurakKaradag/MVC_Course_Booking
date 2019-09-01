using BookingSystem.Domain.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Entity
{
    public class AuditLog : EntityBase
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}