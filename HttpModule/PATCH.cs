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
    public class PATCH
    {
        public static void EndPrediction(PredicionClass predicion)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), "https://api.twitch.tv/helix/predictions"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer cfabdegwdoklmawdzdo98xt2fo512y");
                    request.Headers.TryAddWithoutValidation("Client-Id", "uo6dggojyb8d6soh92zknwmi5ej1q2");

                    request.Content = new StringContent("{\n  \"broadcaster_id\": \"141981764\",\n  \"id\": \"bc637af0-7766-4525-9308-4112f4cbf178\",\n  \"status\": \"RESOLVED\",\n  \"winning_outcome_id\": \"73085848-a94d-4040-9d21-2cb7a89374b7\"\n}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                   // var response = await httpClient.SendAsync(request);
                }
            }
        }
    }
}
