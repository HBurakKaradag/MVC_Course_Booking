using BookingSystem.Core.CustomAttribute;
using System;

namespace BookingSystem.Domain.WebUI.AuditLog
{
    public class AuditLogVM : IModel
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        [MapIgnore]
        public DateTime CreateDateTime { get; set; }

        [MapIgnore]
        public string UserDataJson { get; set; }

        [MapIgnore]
        public string LoginName { get; set; }
    }
}