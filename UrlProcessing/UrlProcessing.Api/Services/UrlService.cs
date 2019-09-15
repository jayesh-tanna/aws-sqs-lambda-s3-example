using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using UrlProcessing.Models;

namespace UrlProcessing.Api.Services
{
    public class UrlService : IUrlService
    {
        private readonly IQueueService _queueService;

        public UrlService(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public async Task<HttpStatusCode> PostUrl(UrlModel model)
        {
            try
            {
                model.Id = Guid.NewGuid().ToString();

                var message = JsonConvert.SerializeObject(model);

                var status = await _queueService.SendMessage(message);

                return status;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}