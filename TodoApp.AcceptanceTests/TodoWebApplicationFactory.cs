using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using TodoApp.AcceptanceTests.TestOutputLogging;

namespace TodoApp.AcceptanceTests;

public sealed class TodoWebApplicationFactory : WebApplicationFactory<WebApi.IAssemblyMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(x => x.AddProvider(new TestOutputLoggerProvider()));
    }
}