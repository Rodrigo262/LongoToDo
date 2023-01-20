using System;
using System.Net;
using Newtonsoft.Json;

namespace LongoToDo.Services
{
    public class ResponseService<T>
    {
        private JsonSerializerSettings jsonSerializerSettings;

        public T Model { get; set; }

        public Exception Error { get; set; }
        public bool IsSuccessStatusCode { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public ResponseService()
        {
            Model = default;
            Error = null;
            IsSuccessStatusCode = false;

            jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
        }

        public void ToModel(string json)
        {
            Model = JsonConvert.DeserializeObject<T>(json, jsonSerializerSettings);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(Model, jsonSerializerSettings);
        }
    }
}
