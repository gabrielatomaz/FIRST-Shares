using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.Client
{
    public class ImgurClient
    {
        public RestClient HttpClient;

        public ImgurClient()
        {
            HttpClient = new RestClient("https://api.imgur.com/3");
        }

        public string Upload(string image)
        {

            var request = new RestRequest("/upload", Method.POST);
            request.AddHeader("Authorization", "Client-ID ae049d96b66b5a5");
            request.AddHeader("Content-Type", @"application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { image });

            var requestJson = HttpClient.Execute(request);
            if (requestJson.IsSuccessful)
                return JsonConvert.DeserializeObject<ImgurPhoto>(requestJson.Content).Data.Url;

            return null;
        }
    }
    public class ImgurPhoto
    {
        [JsonProperty(PropertyName = "data")]
        public Data Data { get; set; }
    }

    public class Data {
        [JsonProperty(PropertyName = "link")]
        public string Url { get; set; }
    }
}
