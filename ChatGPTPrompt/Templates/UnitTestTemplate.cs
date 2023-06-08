namespace ChatGPTPrompt.Templates
{
    public class UnitTestTemplate : ITemplate
    {
        public string Name => "Unit Test";
        public string Prompt { get; set; } = "Please write unit test(s) for the following code: \n\n{code}\n\n Here are some relevant classes that will guide you in how to write this test\n\n{related}\n\nPlease unit xUnit. If we need to mock anything up, please use Moq.";
    }
}
