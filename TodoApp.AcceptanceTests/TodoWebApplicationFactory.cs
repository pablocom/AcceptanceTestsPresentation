using Microsoft.AspNetCore.Mvc.Testing;

namespace TodoApp.AcceptanceTests;

public sealed class TodoWebApplicationFactory : WebApplicationFactory<WebApi.IAssemblyMarker>;