using System;
using Newtonsoft.Json;
using LongoToDo.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;

namespace LongoToDo.Model
{
	public class ToDoItems : BaseModel
	{
        [JsonProperty("Key")]
        public Guid Key { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("IsComplete")]
        public bool IsComplete { get; set; }

        public ToDoItems(IDependencyService dependency) : base(dependency) { }

        public ToDoItems() { }

        public async Task<IEnumerable<ToDoItems>> GetAll()
        {
            try
            {
               ResponseService<IEnumerable<ToDoItems>> response = await dependencyService.Get<IHttpClientService>().CallApi<object, IEnumerable<ToDoItems>>
                (null, string.Empty, HttpRequestType.Get, 10,false);

                if (response.IsSuccessStatusCode)
                {
                    return response.Model;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class BaseModel
    {
        public readonly IDependencyService dependencyService;

        public BaseModel(IDependencyService dependency)
        {
            dependencyService = dependency;
        }

        public BaseModel()
        {
            dependencyService = CustomDependecyService.Instance;
        }
    }
}