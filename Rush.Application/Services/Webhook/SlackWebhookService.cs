using System.Text.Json;

namespace Rush.Application.Services.Webhook
{
    public class SlackWebhookService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task SendMessageAsync(string webhookUrl, string message)
        {
            if (string.IsNullOrWhiteSpace(webhookUrl) || string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("El webhookUrl o el mensaje no pueden estar vacíos.");
            }

            var payload = new
            {
                text = message
            };

            var jsonPayload = JsonSerializer.Serialize(payload);

            using var request = new HttpRequestMessage(HttpMethod.Post, webhookUrl)
            {
                Content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                
            }
        }
    }
}