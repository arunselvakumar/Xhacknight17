using FrienXone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FrienXone.Services
{
    public class UtilityService
    {
        private const string uriBase = "https://southeastasia.api.cognitive.microsoft.com/face/v1.0";
        private const string subscriptionKey = "2cba0e48cb7f4f20a28d131778ba1296";

        //static byte[] GetImageAsByteArray(string imageFilePath)
        //{
        //    FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
        //    BinaryReader binaryReader = new BinaryReader(fileStream);
        //    return binaryReader.ReadBytes((int)fileStream.Length);
        //}

        public async Task<string> MakeAnalysisRequest(string imagestring)
        {
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters. A third optional parameter is "details".
            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,smile,facialHair,emotion,accessories";

            // Assemble the URI for the REST API Call.
            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = Encoding.ASCII.GetBytes(imagestring);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the JSON response.
                Console.WriteLine("\nResponse:\n");
                //Console.WriteLine(JsonPrettyPrint(contentString));
                return contentString;
            }
        }

    }
}
