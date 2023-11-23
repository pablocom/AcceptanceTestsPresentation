# Acceptance Test Sample Project

## Overview

This repository hosts a sample project demonstrating the integration of SpecFlow with WebApplicationFactory (part of Microsoft.AspNetCore.Mvc.Testing) for acceptance testing of a web API. The project serves as a practical guide for configuring SpecFlow to test a comprehensive web API that interacts with a real database.

## Features

- **SpecFlow Integration**: Leveraging SpecFlow for behavior-driven development (BDD), enabling clear and comprehensive test scenarios.
- **Real Database Connection**: Tests are designed to work with a real database, ensuring a more accurate simulation of production environments.
- **Persistence Clean-Up**: Automated clean-up process between tests to maintain the integrity and isolation of each test scenario.
- **WebApplicationFactory Injection**: Flexibility to inject the `WebApplicationFactory` into any binding class, which enhances the test's capability to mimic various behaviors and scenarios.
- **Optimization Techniques**: Demonstrates how to efficiently configure the project so that `WebApplicationFactory` is built only once, similar to xUnit's `ICollectionFixture<>`.

## Getting Started

### Prerequisites

- .NET SDK (version specifics)
- SpecFlow
- An IDE (e.g., Visual Studio, Visual Studio Code)
- A SQL database (specific version or docker container)

### Installation

> Prerequisites: .NET 8 SDK and docker.

1. Clone the repository to your local machine
2. (Prerequisite, have docker installed) Run the db in docker by executing: `docker compose up -d`.
3. Run: `dotnet test`.
4. Install LivingDoc plugin: `dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI`  ([Official docs](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Installing-the-command-line-tool.html#installing-specflow-plus-livingdoc-cli))
5. Generate Acceptance Tests report as living documentation: `livingdoc test-assembly .\TodoApp.AcceptanceTests\bin\Debug\net8.0\TodoApp.AcceptanceTests.dll -t .\TodoApp.AcceptanceTests\bin\Debug\net8.0\TestExecution.json -o LivingDocOutput/TodoApp.AcceptanceTests.html`

