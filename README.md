# aws-sqs-lambda-s3-example

Problem:
We want to save html content of entered url. E.g. Google.com then html content of this page should be saved. Solution should be scalable. Should handle huge traffic.

Solution projects / components:
1. Api: It will receive request from consumer. 
2. Sqs: Api will send JSON seriallized message to AWS sqs. 
3. Lambda (.net core): Lambda function will be executed as soon as new message arrives in sqs.
4. Tester: It will make call to Api.

Api will send message to sqs. Lambda wil be triggered as soon as message arrives in sqs. Lambda function will deseriallize message and download the content from the entered url. Same lambda function will upload the upload the content to S3 bucket. Logs are written in CloudWatch from lambda function. 

Prerequisite: 
VS 2017/19, AWS toolkit, AWS account

Notes:
Need to create AWS account. Proper IAM user along with enough role to execute SQS, Lambda and S3 service.

AWS services:
Lambda function,
S3 bucket,
SQS,
Cloudwatch,
IAM roles


Nuget packages:
AWSSDK.SQS,
Newtonsoft.Json,
AWSSDK.S3,
Microsoft.AspNet.WebApi.Client
