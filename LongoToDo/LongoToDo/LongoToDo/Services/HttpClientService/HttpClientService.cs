using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LongoToDo.Services;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpClientService))]
namespace LongoToDo.Services
{
	public class HttpClientService : IHttpClientService
	{
        private readonly HttpClient httpClient;
        private CancellationTokenSource cts;

        public static string BaseAddress =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8080" : "http://localhost:8080";
        public static string TodoItemsUrl = $"{BaseAddress}/api/todo";

        public JsonSerializerSettings JsonSerializerSettings;

        public string Token { get; set; } = string.Empty;

        public HttpClientService()
        {
            //HttpClientHandler insecureHandler = GetInsecureHandler();

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            //HttpClient client = new HttpClient(clientHandler);

            httpClient = new HttpClient(clientHandler) { BaseAddress = new Uri(TodoItemsUrl) };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            JsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
        }
        private HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public async Task<ResponseService<TOut>> CallApi<TIn, TOut>(TIn root, string uri, HttpRequestType httpRequestType, int timeout = 10, bool auth = true)
        {
            //if (!await Ping()) throw new Exception();

            ResponseService<TIn> inObject = new ResponseService<TIn>();
            ResponseService<TOut> outObject = new ResponseService<TOut>();

            try
            {
                AddHeaders(TimeSpan.FromSeconds(timeout), auth);
                HttpResponseMessage response = null;
                StringContent content;

                switch (httpRequestType)
                {
                    case HttpRequestType.Get:
                        response = await httpClient.GetAsync(uri).ConfigureAwait(false);
                        break;

                    case HttpRequestType.Post:
                        if (root != null)
                        {
                            inObject.Model = root;
                            content = new StringContent(inObject.ToString(), Encoding.UTF8, "application/json");
                            response = await httpClient.PostAsync(uri, content, cts.Token).ConfigureAwait(false);
                        }
                        break;

                    case HttpRequestType.Put:
                        if (root != null)
                        {
                            inObject.Model = root;
                            content = new StringContent(inObject.ToString(), Encoding.UTF8, "application/json");
                            response = await httpClient.PutAsync(uri, content, cts.Token).ConfigureAwait(false);
                        }
                        break;

                    case HttpRequestType.Delete:
                        if (root != null)
                        {
                            inObject.Model = root;
                            content = new StringContent(inObject.ToString(), Encoding.UTF8, "application/json");
                            string url = httpClient.BaseAddress + uri;
                            HttpRequestMessage request = new HttpRequestMessage()
                            {
                                Content = content,
                                Method = HttpMethod.Delete,
                                RequestUri = new Uri(url)
                            };
                            response = await httpClient.SendAsync(request, cts.Token).ConfigureAwait(false);
                        }
                        break;
                }

                if (response != null)
                    await ManageResponse(outObject, response);
            }
            catch (OperationCanceledException operationCanceledException)
            {
                cts.Cancel();
                throw operationCanceledException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                cts.Dispose();
            }

            return outObject;
        }

        public async Task<bool> Ping()
        {
            try
            {
                cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(5));

                HttpResponseMessage response = await httpClient.GetAsync(string.Empty, cts.Token).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound) return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return false;
        }

        private void AddHeaders(TimeSpan timeout, bool auth)
        {
            if (auth)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }

            cts = new CancellationTokenSource();
            cts.CancelAfter(timeout);
        }

        private static async Task ManageResponse<Tout>(ResponseService<Tout> outObject, HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                outObject.IsSuccessStatusCode = true;
                outObject.StatusCode = response.StatusCode;
                outObject.ToModel(data);
            }
            else
            {
                string contentString = await response.Content.ReadAsStringAsync();
                throw new Exception(contentString);
                //Debug.WriteLine("Exception-> " + contentString);
            }
        }
    }
}