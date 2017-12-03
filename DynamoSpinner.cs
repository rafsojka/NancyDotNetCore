using System;
using System.Collections.Generic;
using System.Diagnostics;

using Amazon.Runtime;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Iwi.Slots.Events.Tests
{
    public interface IDynamoSpinner
    { }

    public class DynamoSpinner : IDisposable, IDynamoSpinner
    {
        private static Process _subProcess;

        public DynamoSpinner()
        {
            this.Start();
            this.CreateTables();
        }

        public void Dispose()
        {
            if (_subProcess != null && !_subProcess.HasExited)
            {
                _subProcess.Kill();
                _subProcess = null;
            }

            GC.SuppressFinalize(this);
        }

        public void Start()
        {
            if (_subProcess != null && !_subProcess.HasExited)
                return;

            _subProcess = new Process
            {
                StartInfo =
                {
                    FileName = "java",
                    Arguments =
                        $@"-Djava.library.path={"./DynamoDBLocal_lib"} -jar {
                            System.IO.Directory.GetCurrentDirectory() + @"\DynamoDBLocal\DynamoDBLocal.jar"} -sharedDb -inMemory true",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            _subProcess.Start();
        }

        public void CreateTables()
        {
             // get the client
            // Local DynamoDB server requires credentials to be specified,
            // but it doesn't matter what they are
            var credentials = new BasicAWSCredentials(
                accessKey: "fake-access-key",
                secretKey: "fake-secret-key");
            AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
            // Set the endpoint URL
            clientConfig.ServiceURL = "http://localhost:8000";
            var dynamoDbClient = new AmazonDynamoDBClient(credentials, clientConfig); 

            // create Movies table
            var request = new CreateTableRequest(
                tableName: "Movies",
                keySchema: new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "MovieId",
                        KeyType = KeyType.HASH
                    }
                },
                attributeDefinitions: new List<AttributeDefinition>
                {
                    new AttributeDefinition()
                    {
                        AttributeName = "MovieId",
                        AttributeType = ScalarAttributeType.S
                    }
                },
                // The provisioned throughput values are ignored locally
                provisionedThroughput: new ProvisionedThroughput
                {
                    ReadCapacityUnits = 1,
                    WriteCapacityUnits = 1
                }
            );

            var result = dynamoDbClient.CreateTableAsync(request).Result;
        }
    }
}