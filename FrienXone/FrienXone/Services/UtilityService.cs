using FrienXone.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

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

        public async Task<string> MakeAnalysisRequest(string imageFilePath)
        {

            var restClient = new RestClient();
            var request = new RestRequest();
            request.Method = Method.POST;

            request.AddParameter("returnFaceId", "true");
            request.AddParameter("returnFaceLandmarks", "false");
            request.AddParameter("returnFaceAttributes", "age,gender,smile,facialHair,emotion,accessories");

            request.AddHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
            request.AddHeader("Content-Type", "application/json");

            var imagePath = new ImagePath();
            imagePath.Url = imageFilePath;

            request.AddBody(JsonConvert.SerializeObject(imagePath));

            restClient.BaseUrl = new Uri(uriBase);


            var response = restClient.Execute(request);

            return null;
        }

        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

    }
}
