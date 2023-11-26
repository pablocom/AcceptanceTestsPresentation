Feature: Create Todo
	
Scenario: Creates a new todo
	Given the following data to create a Todo
		| Id                                   | Title                                            | IsCompleted |
		| 8d31d9a9-972d-4b5c-a12c-34ee8bf62a09 | "Create an example project for acceptance tests" | false       |
	When the Todo is created
	Then the created Todo should have the following data
		| Id                                   | Title                                            | IsCompleted |
		| 8d31d9a9-972d-4b5c-a12c-34ee8bf62a09 | "Create an example project for acceptance tests" | false       |