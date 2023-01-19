using System;
using System.Threading.Tasks;

namespace LongoToDo.Services
{
	public interface IHttpClientService
	{
        Task<ResponseService<TOut>> CallApi<TIn, TOut>(TIn root, string uri, HttpRequestType httpRequestType, int timeout = 10, bool auth = true);
        Task<bool> Ping();
    }
}

