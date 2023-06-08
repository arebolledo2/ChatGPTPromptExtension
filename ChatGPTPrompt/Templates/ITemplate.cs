namespace ChatGPTPrompt.Templates
{
    public interface ITemplate
    {
        string Prompt { get; set; }
        string Name { get; }
    }
}
