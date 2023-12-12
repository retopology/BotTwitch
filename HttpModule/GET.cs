using ClassesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Variables;

namespace HttpModule
{
    public class GET
    {
        // 105430491 - мой 
        public static async Task<string> GetUserDateFollow(string idUser)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://api.twitch.tv/helix/channels/followers?user_id={idUser}&broadcaster_id={ValuesProject.StreamerId}"))
                {
                    string tok = ValuesProject.ActualUser.token.Replace("oauth:", "");

                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {tok}");
                    request.Headers.TryAddWithoutValidation("Client-Id", ValuesProject.ActualUser.clientId);
                    var response = await httpClient.SendAsync(request);
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }
        public static async Task<string> GetTitleAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://api.twitch.tv/helix/channels?broadcaster_id={ValuesProject.StreamerId}"))
                {
                    string tok = ValuesProject.ActualUser.token.Replace("oauth:", "");

                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {tok}");
                    request.Headers.TryAddWithoutValidation("Client-Id", ValuesProject.ActualUser.clientId);
                    var response = await httpClient.SendAsync(request);
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }
        public static async Task<string> GetPredicion(PredicionClass predicion)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://api.twitch.tv/helix/channels?broadcaster_id={ValuesProject.StreamerId}"))
                {
                    string tok = ValuesProject.ActualUser.token.Replace("oauth:", "");

                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {tok}");
                    request.Headers.TryAddWithoutValidation("Client-Id", ValuesProject.ActualUser.clientId);
                    var response = await httpClient.SendAsync(request);
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }
    }
}
