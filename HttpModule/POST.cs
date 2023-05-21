using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Variables;

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
    }
}
