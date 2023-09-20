using Microsoft.IO;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace AuthentikSharp.Rest
{
    public class AuthentikRestClient
    {
        internal AuthentikRestClient(AuthentikClient client, string hostUrl, string token)
        {
            if (string.IsNullOrEmpty(hostUrl))
                throw new AuthentikException("Client config ApiUrl can not be empty.");

            if (!Uri.IsWellFormedUriString(hostUrl, UriKind.Absolute))
                throw new AuthentikException("Client config ApiUrl is an invalid format.");

            if (!hostUrl.EndsWith('/'))
                hostUrl += "/";

            Client = client;
            Http = new HttpClient(new HttpClientHandler()
            {
                AllowAutoRedirect = false
            })
            {
                BaseAddress = new Uri(hostUrl + "api/v3/")
            };
            APIUrl = hostUrl + "api/v3";
            Token = "Bearer " + token;

            Http.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            Http.DefaultRequestHeaders.Add("User-Agent","AuthentikSharp");
            Http.DefaultRequestHeaders.Add("Accept", "*/*");
            Http.DefaultRequestHeaders.Add("Host", "auth.fluxpoint.dev");
            Json = new JsonSerializer();
        }

        internal HttpClient Http;
        internal AuthentikClient Client;
        internal string APIUrl;
        internal JsonSerializer Json;
        internal string Token;
        private static readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();



        public Task<TResponse?> GetAsync<TResponse>(string endpoint, IAuthentikRequest json = null, bool throwGetRequest = false) where TResponse : class
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        => InternalJsonRequest<TResponse>(HttpMethod.Get, endpoint, json, throwGetRequest);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

        public Task<HttpResponseMessage> DeleteAsync(string endpoint, IAuthentikRequest json = null)
            => InternalRequest(HttpMethod.Delete, endpoint, json);

        public Task<TResponse> DeleteAsync<TResponse>(string endpoint, IAuthentikRequest json = null) where TResponse : class
            => InternalJsonRequest<TResponse>(HttpMethod.Delete, endpoint, json);

        public Task<TResponse> PatchAsync<TResponse>(string endpoint, IAuthentikRequest json = null) where TResponse : class
            => InternalJsonRequest<TResponse>(HttpMethod.Patch, endpoint, json);

        public Task<TResponse> PutAsync<TResponse>(string endpoint, IAuthentikRequest json = null) where TResponse : class
            => InternalJsonRequest<TResponse>(HttpMethod.Put, endpoint, json);

        public Task<TResponse> PostAsync<TResponse>(string endpoint, IAuthentikRequest json = null) where TResponse : class
            => InternalJsonRequest<TResponse>(HttpMethod.Post, endpoint, json);

        public Task<HttpResponseMessage> PostAsync(string endpoint, IAuthentikRequest json = null)
            => InternalRequest(HttpMethod.Post, endpoint, json);

        internal async Task<HttpResponseMessage> InternalRequest(HttpMethod method, string endpoint, object request)
        {
            if (!endpoint.EndsWith('/'))
                endpoint += "/";

            HttpRequestMessage Mes = new HttpRequestMessage(method, new Uri(APIUrl + endpoint));
            if (request != null)
            {
                Mes.Content = new StringContent(SerializeJson(request), Encoding.UTF8, "application/json");
                if (Client.LogRequests)
                    Console.WriteLine("--- Rest REQ Json ---\n" + JsonConvert.SerializeObject(request, Formatting.Indented));
            }
            HttpResponseMessage Req = await Http.SendAsync(Mes);
            
            if (!Req.IsSuccessStatusCode)
            {
                AuthentikRestError Error = null;
                if (Req.Content.Headers.ContentLength.HasValue)
                {
                    try
                    {
                        int BufferSize = (int)Req.Content.Headers.ContentLength.Value;
                        using (MemoryStream Stream = recyclableMemoryStreamManager.GetStream("Authentik-SendRequest", BufferSize))
                        {
                            await Req.Content.CopyToAsync(Stream);
                            Stream.Position = 0;
                            Error = DeserializeJson<AuthentikRestError>(Stream);
                        }
                    }
                    catch { }
                }
                //Client.Logger.LogRestMessage(Client, Req, method, endpoint);
                if (Error != null)
                    throw new AuthentikRestException(Error.Error, (int)Req.StatusCode);
                else
                    throw new AuthentikRestException(Req.ReasonPhrase, (int)Req.StatusCode);
            }

            return Req;
        }


        internal async Task<TResponse> InternalJsonRequest<TResponse>(HttpMethod method, string endpoint, object request, bool throwGetRequest = false)
            where TResponse : class
        {
            if (!endpoint.EndsWith('/'))
                endpoint += "/";

            HttpRequestMessage Mes = new HttpRequestMessage(method, new Uri(APIUrl + endpoint));
            
            if (request != null)
            {
                Mes.Content = new StringContent(SerializeJson(request), Encoding.UTF8, "application/json");
                if (Client.LogRequests)
                    Console.WriteLine("--- Rest REQ Json ---\n" + JsonConvert.SerializeObject(request, Formatting.Indented));
            }
            HttpResponseMessage Req = await Http.SendAsync(Mes);
            
            if (!Req.IsSuccessStatusCode && (throwGetRequest || method != HttpMethod.Get))
            {
                AuthentikRestError Error = null;
                if (Req.Content.Headers.ContentLength.HasValue)
                {
                    try
                    {
                        int BufferSize = (int)Req.Content.Headers.ContentLength.Value;
                        using (MemoryStream Stream = recyclableMemoryStreamManager.GetStream("Authentik-SendRequest", BufferSize))
                        {
                            await Req.Content.CopyToAsync(Stream);
                            Stream.Position = 0;
                            Error = DeserializeJson<AuthentikRestError>(Stream);
                        }
                    }
                    catch { }
                }
                //Client.Logger.LogRestMessage(Client, Req, method, endpoint);
                if (Error != null)
                    throw new AuthentikRestException(Error.Error, (int)Req.StatusCode);
                else
                    throw new AuthentikRestException(Req.ReasonPhrase, (int)Req.StatusCode);
            }

            TResponse Response = null;
            if (Req.IsSuccessStatusCode)
            {
                int BufferSize = (int)Req.Content.Headers.ContentLength.GetValueOrDefault();
                try
                {
                    using (MemoryStream Stream = recyclableMemoryStreamManager.GetStream("AuthentikSharp-SendRequest", BufferSize))
                    {
                        await Req.Content.CopyToAsync(Stream);
                        Stream.Position = 0;
                        Response = DeserializeJson<TResponse>(Stream);
                    }
                }
                catch (Exception ex)
                {
                    //Client.InvokeLog($"Failed to parse json for {endpoint}", RevoltLogSeverity.Error);
                    throw new AuthentikRestException("Failed to parse json response: " + ex.Message, 500);
                }

                if (Response != null && Client.LogResponse)
                    Console.WriteLine("--- Rest RS Json ---\n" + JsonConvert.SerializeObject(Response, Formatting.Indented, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            }
#pragma warning disable CS8603 // Possible null reference return.
            return Response;
        }

        internal string SerializeJson(object value)
        {
            StringBuilder sb = new StringBuilder(256);
            using (TextWriter text = new StringWriter(sb, CultureInfo.InvariantCulture))
            using (JsonWriter writer = new JsonTextWriter(text))
                Json.Serialize(writer, value);
            return sb.ToString();
        }

        internal T? DeserializeJson<T>(MemoryStream jsonStream)
        {
            using (TextReader text = new StreamReader(jsonStream))
            using (JsonReader reader = new JsonTextReader(text))
                return Json.Deserialize<T>(reader);
        }
    }
}
