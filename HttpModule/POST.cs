using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Variables;
using ClassesModule;

namespace HttpModule
{
    public class POST
    {
        public static void TimeOutUserCustom(int duration, string userId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"https://api.twitch.tv/helix/moderation/bans?broadcaster_id={ValuesProject.StreamerId}&moderator_id={ValuesProject.ActualUser.userId}"))
                {
                    string tok = ValuesProject.ActualUser.token.Replace("oauth:", "");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {tok}");
                    request.Headers.TryAddWithoutValidation("Client-Id", ValuesProject.ActualUser.clientId);


                    request.Content = new StringContent("{\"data\": {\"user_id\":\"" + userId + "\",\"duration\":" + duration + ",\"reason\":\"no reason\"}}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = httpClient.SendAsync(request).Result;
                    string end = response.Content.ReadAsStringAsync().Result;

                }
            }
        }

        public static async Task<string> StartPredicionAsync(PredicionClass predicion)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"),  $"https://api.twitch.tv/helix/predictions"))
                {
                    string tok = ValuesProject.ActualUser.token.Replace("oauth:", "");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {tok}");
                    request.Headers.TryAddWithoutValidation("Client-Id", ValuesProject.ActualUser.clientId);

                    request.Content = new StringContent("{\n  \"broadcaster_id\": \"" + ValuesProject.StreamerId + "\",\n  \"title\": \"Any leeks in the stream?\",\n  \"outcomes\": [\n    {\n      \"title\": \"Yes, give it time.\"\n    },\n    {\n      \"title\": \"Definitely not.\"\n    }\n  ],\n  \"prediction_window\": 120\n}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }
        public static async Task<string> StartPredicionAsyncDuo(PredicionClass predicion)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"https://api.twitch.tv/helix/predictions&moderator_id=12354"))
                {
                    string tok = ValuesProject.ActualUser.token.Replace("oauth:", "");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {tok}");
                    request.Headers.TryAddWithoutValidation("Client-Id", ValuesProject.ActualUser.clientId);

                    request.Content = new StringContent("{\n  \"broadcaster_id\": \"" + ValuesProject.StreamerId + "\",\n  \"title\": \"Any leeks in the stream?\",\n  \"outcomes\": [\n    {\n      \"title\": \"Yes, give it time.\"\n    },\n    {\n      \"title\": \"Definitely not.\"\n    }\n  ],\n  \"prediction_window\": 120\n}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }
    }
}
