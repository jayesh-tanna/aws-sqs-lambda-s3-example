using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.S3;
using Amazon.S3.Model;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using UrlProcessing.Models;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace UrlProcessing.UrlWorker
{
    public class Function
    {
        public Function()
        {
        }

        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            foreach (var message in evnt.Records)
            {
                await ProcessMessageAsync(message, context);
            }
        }

        private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
        {
            var model = JsonConvert.DeserializeObject<UrlModel>(message.Body);

            context.Logger.LogLine(model.Url);

            string htmlCode = string.Empty;

            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(model.Url);
            }

            var fileKey = model.Id.Replace("-", string.Empty) + ".html";

            await PutS3Object(fileKey, htmlCode, context);

            context.Logger.LogLine("Uploaded");

            await Task.CompletedTask;
        }

        public static async Task<bool> PutS3Object(string key, string content, ILambdaContext context)
        {
            try
            {
                using (var client = new AmazonS3Client(Amazon.RegionEndpoint.APSouth1))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = Environment.GetEnvironmentVariable("s3bucketname"),
                        Key = key,
                        ContentBody = content
                    };
                    var response = await client.PutObjectAsync(request);
                }
                return true;
            }
            catch (Exception ex)
            {
                context.Logger.LogLine(ex.Message.ToString());

                return false;
            }
        }
    }
}