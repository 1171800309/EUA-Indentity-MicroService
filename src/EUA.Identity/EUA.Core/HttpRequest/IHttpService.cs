using System.Threading.Tasks;

namespace EUA.Core.HttpRequest
{
    public interface IHttpService
    {
        Task<T> Get<T>(string url, string token = "");
        Task<T> Delete<T>(string url, string token = "");
        Task<T> Post<T>(string url, string param, string token = "");
        Task<T> Put<T>(string url, string param, string token = "");
    }
}
