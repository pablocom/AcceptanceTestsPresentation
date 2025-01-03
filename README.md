# Acceptance Test Sample Project

## Overview

This repository hosts a sample project demonstrating the integration of Reqnroll with WebApplicationFactory (part of Microsoft.AspNetCore.Mvc.Testing) for acceptance testing of a web API. The project serves as a practical guide for configuring Reqnroll to test a comprehensive web API that interacts with a real database.

## Features

- **Reqnroll Integration**: Leveraging Reqnroll for behavior-driven development (BDD), enabling clear and comprehensive test scenarios.
- **Real Database Connection**: Tests are designed to work with a real database, ensuring a more accurate simulation of production environments.
- **Persistence Clean-Up**: Automated clean-up process between tests to maintain the integrity and isolation of each test scenario.
- **WebApplicationFactory Injection**: Flexibility to inject the `WebApplicationFactory` into any binding class, which enhances the test's capability to mimic various behaviors and scenarios.
- **Optimization Techniques**: Demonstrates how to efficiently configure the project so that `WebApplicationFactory` is built only once, similar to xUnit's `ICollectionFixture<>`.

## Getting Started

### Prerequisites

- .NET 9 SDK
- Preferably: An IDE (e.g., Visual Studio, Visual Studio Code) with the Reqnroll plugin
- Docker 

### Installation guide

1. Clone the repository
2. Run the db in a docker container: `docker compose up -d`.
3. Install .NET CLI tools: `dotnet tool restore` 
4. Execute db migrations: `dotnet ef database update -p ./TodoApp.WebApi/TodoApp.WebApi.csproj`
5. Run: `dotnet test`.