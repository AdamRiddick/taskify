namespace Taskify.ArchitectureTests.Presentation;

using System.Reflection;

using Taskify.ArchitectureTests.Common;
using Taskify.Web;

public class WebTests : ClientTestsBase<WebTests>
{
    static WebTests()
    {
        var assembliesToCheck = new Assembly[]
        {
            typeof(FormFactor).Assembly, // Taskify.Web
        };

        Init("Web", assembliesToCheck);
    }
}
