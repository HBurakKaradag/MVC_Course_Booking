using BookingSystem.Domain.Entity.BaseEntity;
using System;

namespace BookingSystem.Domain.Entity
{
    public class AuditLog : EntityBase
    {
        public string SessionId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}