using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TypeLibrary.Core.Controllers.V1;

namespace TypeLibrary.Core.Helper
{
    public class ExeptionLogger
    {
        private readonly ILogger<LibraryAttributeController> _logger;

        public void LoggExeption(Exception e)
        {
            _logger.LogError("Internal server error", e.Message, e.StackTrace, e.InnerException, e.Data, e.Source);

            return;
        }

    }
}
