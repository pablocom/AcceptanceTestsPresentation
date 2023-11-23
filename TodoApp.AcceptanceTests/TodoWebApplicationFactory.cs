using Microsoft.AspNetCore.Mvc.Testing;

namespace TodoApp.AcceptanceTests;

public class TodoWebApplicationFactory : WebApplicationFactory<WebApi.IAssemblyMarker>
{
}