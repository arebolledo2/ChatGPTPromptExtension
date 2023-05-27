namespace ChatGPTPrompt.Templates
{
    public class UnitTestTemplate : ITemplate
    {
        public string Name => "Unit Test";
        public string Prompt => "Please write a unit test for the following code: \n\n{code}\n\n Here are some relevant classes that I will guide you in how to write this test\n\n{related}";
    }
}
