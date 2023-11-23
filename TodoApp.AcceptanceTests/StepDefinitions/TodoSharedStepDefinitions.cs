namespace TodoApp.AcceptanceTests.StepDefinitions;

[Binding]
public class TodoSharedStepDefinitions
{
    [Then(@"the Todo ""(.*)"" should have the following data")]
    public void ThenTheTodoShouldHaveTheFollowingData(string p0, Table table)
    {
        ScenarioContext.StepIsPending();
    }
}