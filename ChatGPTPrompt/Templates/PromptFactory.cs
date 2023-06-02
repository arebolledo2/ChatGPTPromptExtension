namespace ChatGPTPrompt.Templates
{
    public class PromptFactory
    {
        public string Create(string template, string code, string related)
        {
            return template.Replace("{code}", code).Replace("{related}", related);
        }
    }
}
