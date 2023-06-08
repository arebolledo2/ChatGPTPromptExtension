using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class OpenAIQuery
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<string> GetOpenAIResponseAsync(string prompt)
    {
        var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

        var uri = new Uri("https://api.openai.com/v1/chat/completions");

        var body = new
        {
            messages = new[] { new { role = "user", content = prompt } },
            model = "gpt-3.5-turbo"
        };

        string jsonString = JsonConvert.SerializeObject(body);

        using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            request.Content = new StringContent(jsonString);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    return responseObject.choices[0].message.content;
                }
                else
                {
                    throw new Exception($"Request failed with status code: {response.StatusCode}");
                }
            }
        }
    }
}
