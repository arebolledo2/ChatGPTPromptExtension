namespace ChatGPTPrompt.Templates
{
    public class DefaultTemplate : ITemplate
    {
        public string Prompt { get; set; } = string.Empty;

        public string Name => "Default";
    }
}
