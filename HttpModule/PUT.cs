using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Variables;
using ClassesModule;

namespace HttpModule
{
    public class PUT
    {
        // Смена цвета ника
        public static async Task HttpColorChatAsync(string color)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PUT"), $"https://api.twitch.tv/helix/chat/color?user_id={ValuesProject.ActualUser.userId}&color={color}"))
                {
                    string tok = ValuesProject.ActualUser.token.Replace("oauth:", "");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {tok}");
                    request.Headers.TryAddWithoutValidation("Client-Id", ValuesProject.ActualUser.clientId);
                    //request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    string end = response.Content.ReadAsStringAsync().Result;
                }
            }
        }
    }
}
