namespace ChatGPTPrompt.Templates
{
    public class WriteCodeTemplate : ITemplate
    {
        public string Prompt { get; set; } = "Please write C# code that does the following: \n\n{code}\n\nHere are some relevant classes that will help you in how to write this code:\n\n{related}\n\nUse explicit types rather than var.";

        public string Name => "Write Code";
    }
}
