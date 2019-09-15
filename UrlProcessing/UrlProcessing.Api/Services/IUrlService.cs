using System.Net;
using System.Threading.Tasks;
using UrlProcessing.Models;

namespace UrlProcessing.Api.Services
{
    public interface IUrlService
    {
        Task<HttpStatusCode> PostUrl(UrlModel model);
    }
}