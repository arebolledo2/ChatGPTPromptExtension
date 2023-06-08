namespace ChatGPTPrompt.Templates
{
    public class UnitTestTemplate : ITemplate
    {
        public string Name => "Unit Test";
        public string Prompt { get; set; } = "Please write unit test(s) for the following code: \n\n{code}\n\nHere are some relevant classes that will help you write this test:\n\n{related}\n\nPlease use xUnit. If we need to mock anything up, please use Moq. Use explicit types rather than var.";
    }
}
