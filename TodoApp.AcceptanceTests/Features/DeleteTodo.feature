Feature: Delete Todo

Scenario: Delete an existing todo
    Given the existing todos
      | Id                                   | Title            | IsCompleted |
      | 1111d9a9-972d-4b5c-a12c-34ee8bf61111 | "Todo to remove" | false       |
      | 2222d9a9-972d-4b5c-a12c-34ee8bf62222 | "Other todo"     | false       |
    When deleting the Todo with ID "1111d9a9-972d-4b5c-a12c-34ee8bf61111"
    Then the remaining todos should be
      | Id                                   | Title            | IsCompleted |
      | 2222d9a9-972d-4b5c-a12c-34ee8bf62222 | "Other todo"     | false       |
