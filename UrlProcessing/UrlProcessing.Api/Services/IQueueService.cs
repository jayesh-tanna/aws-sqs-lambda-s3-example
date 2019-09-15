using Amazon.SQS.Model;
using System.Net;
using System.Threading.Tasks;

namespace UrlProcessing.Api.Services
{
    public interface IQueueService
    {
        Task<HttpStatusCode> SendMessage(string message);
    }
}