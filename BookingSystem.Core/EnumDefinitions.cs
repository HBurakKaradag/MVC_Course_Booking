using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Core
{
    public class EnumDefinitions
    {
        private EnumDefinitions()
        {
        }
    }

    public enum OperationResultType
    {
        None = 0,
        Success = 1,
        Warn = 2,
        Error = 3
    }

    public enum ServiceResultCode
    {
        None = 0,
        EMailIsNotConfirmed = 5001,
        DuplicateUser = 5002
    }
}