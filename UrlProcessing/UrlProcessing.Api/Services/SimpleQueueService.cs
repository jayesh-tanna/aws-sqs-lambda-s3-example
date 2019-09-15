using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Net;
using System.Threading.Tasks;

namespace UrlProcessing.Api.Services
{
    public class SimpleQueueService : IQueueService
    {
        private readonly Lazy<AmazonSQSConfig> _sqsConfig;
        private readonly Lazy<AmazonSQSClient> _sqsClient;
        private string _serviceUrl;

        public AmazonSQSConfig SqsConfig
        {
            get
            {
                return _sqsConfig.Value;
            }
        }

        public AmazonSQSClient SqsClient
        {
            get
            {
                return _sqsClient.Value;
            }
        }

        public SimpleQueueService(string serviceUrl)
        {
            _serviceUrl = serviceUrl;

            _sqsConfig = new Lazy<AmazonSQSConfig>(() => new AmazonSQSConfig() { ServiceURL = serviceUrl, RegionEndpoint = RegionEndpoint.APSouth1 });

            _sqsClient = new Lazy<AmazonSQSClient>(() => new AmazonSQSClient(_sqsConfig.Value));
        }

        public async Task<HttpStatusCode> SendMessage(string message)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _serviceUrl,
                MessageBody = message
            };

            var result = await SqsClient.SendMessageAsync(sendMessageRequest);

            return result.HttpStatusCode;
        }
    }
}