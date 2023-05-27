namespace ChatGPTPrompt.Templates
{
    public interface ITemplate
    {
        string Prompt { get; }
        string Name { get; }
    }
}
