using WebTo.Models;

namespace WebTo.Interfaces
{
    public interface IAppInterfaceService
    {
        object? RestApiController(RestApiType restApiType, string url, object input, string JWT = null);

        object? ResApiFileContent(RestApiType restApiType, string url, object input, string JWT = null);
    }
}
