using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Threading.Tasks;

namespace BlobPropertiesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //assign values
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=elxstorageacc;AccountKey=95CrgglnOsDgbtSV6BuTWduV1XajeAB/n0t6BWhuFR7OG8kgjxrh58GzqpSciteoF1Q2wmgtpS6HYQzlod2qNg==;EndpointSuffix=core.windows.net";
            string containerName = "repro";
            string blobName = "test";
            string filePath = @"C:\Customer\Sample.json";

            //upload blob
            var blob = new BlobClient(connectionString, containerName, blobName);
            blob.Upload(filePath, overwrite: true);

            //async operation in main method
            Task.Run(async () =>
            {
                // Do any async anything you need here without worry
                
                //await ReadBlobMetadataAsync(blob);
                await GetBlobPropertiesAsync(blob);
            }).GetAwaiter().GetResult();

            
            
            

        }

        //public static async Task SetBlobPropertiesAsync(BlobClient blob)
        //{
        //    Console.WriteLine("Setting blob properties...");

        //    try
        //    {
        //        // Get the existing properties
        //        BlobProperties properties = await blob.GetPropertiesAsync();

        //        BlobHttpHeaders headers = new BlobHttpHeaders
        //        {
        //            // Set the MIME ContentType every time the properties 
        //            // are updated or the field will be cleared
        //            ContentType = "text/plain",
        //            ContentLanguage = "en-us",

        //            // Populate remaining headers with 
        //            // the pre-existing properties
        //            CacheControl = properties.CacheControl,
        //            ContentDisposition = properties.ContentDisposition,
        //            ContentEncoding = properties.ContentEncoding,
        //            ContentHash = properties.ContentHash
        //        };

        //        // Set the blob's properties.
        //        await blob.SetHttpHeadersAsync(headers);
        //    }
        //    catch (RequestFailedException e)
        //    {
        //        Console.WriteLine($"HTTP error code {e.Status}: {e.ErrorCode}");
        //        Console.WriteLine(e.Message);
        //        Console.ReadLine();
        //    }
        //}

        public static async Task GetBlobPropertiesAsync(BlobClient blob)
        {
            try
            {
                // Get the blob properties
                BlobProperties properties = await blob.GetPropertiesAsync();

                // Display some of the blob's property values
                Console.WriteLine($"Copy Id: {properties.CopyId}");
                Console.WriteLine($"Copy status: {properties.CopyStatus}");
                Console.WriteLine($"Copy progress: {properties.CopyProgress}");
                Console.WriteLine($"Completion time: {properties.CopyCompletedOn}");

                Console.WriteLine($" ContentLanguage: {properties.ContentLanguage}");
                Console.WriteLine($" ContentType: {properties.ContentType}");
                Console.WriteLine($" CreatedOn: {properties.CreatedOn}");
                Console.WriteLine($" LastModified: {properties.LastModified}");
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"HTTP error code {e.Status}: {e.ErrorCode}");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }

        public static async Task ReadBlobMetadataAsync(BlobClient blob)
        {
            try
            {
                // Get the blob's properties and metadata.
                BlobProperties properties = await blob.GetPropertiesAsync();

                Console.WriteLine("Blob metadata:");

                // Enumerate the blob's metadata.
                foreach (var metadataItem in properties.Metadata)
                {
                    Console.WriteLine($"\tKey: {metadataItem.Key}");
                    Console.WriteLine($"\tValue: {metadataItem.Value}");
                }
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"HTTP error code {e.Status}: {e.ErrorCode}");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }

    }
}
