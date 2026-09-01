using System;
using System.Threading.Tasks;

namespace wowmenfashions.Services;

public interface IErrorLoggerService
{
    Task LogErrorAsync(Exception exception, string? userContext = null);
}
