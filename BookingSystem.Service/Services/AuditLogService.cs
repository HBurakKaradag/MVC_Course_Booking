﻿using BookingSystem.Data.Context;
using BookingSystem.Domain.Entity;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.AuditLog;
using BookingSystem.Service.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem.Service.Services
{
    public class AuditLogService : ServiceBase
    {
        #region AuditLog

        public ServiceResultModel<AuditLogVM> SaveAuditLog(AuditLogVM auditLog)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                context.AuditLogs.Add(auditLog.MapToEntityModel<AuditLog>());
                context.SaveChanges();
            }

            return ServiceResultModel<AuditLogVM>.OK(auditLog);
        }

        public ServiceResultModel<List<AuditLogVM>> GetAuditLogs()
        {
            List<AuditLogVM> resultList = new List<AuditLogVM>();
            using (EFBookingContext context = new EFBookingContext())
            {
                var auditLogs = context.AuditLogs.ToList();
                resultList.AddRange(auditLogs.Select(p => p.MapToViewModel<AuditLogVM>()));
            }

            return ServiceResultModel<List<AuditLogVM>>.OK(resultList);
        }

        #endregion AuditLog
    }
}