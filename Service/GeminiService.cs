using System.Net.Http;
using System.Text;
using System.Text.Json;

public class GeminiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "AIzaSyD_eN7_h5CeM4JWQPixiopFOc0kUbCBa8w";

    public GeminiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> AnalyzeResume(string resumeText, string jobDescription)
    {
        var prompt = $@"
Analyze the resume based on the job description.

Resume:
{resumeText}

Job Description:
{jobDescription}

Return:
SkillMatchScore: %
CareerBreak: Yes/No
FrequentSwitch: Yes/No
FinalScore: %
";

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_apiKey}";

        var response = await _httpClient.PostAsync(url, content);

        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}