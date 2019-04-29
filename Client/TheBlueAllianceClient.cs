using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FIRSTShares.Client
{
    public class TheBlueAllianceClient
    {
        public RestClient HttpClient;

        public TheBlueAllianceClient()
        {
            HttpClient = new RestClient("https://www.thebluealliance.com/api/v3");
        }

        public TimeTheBlueAlliance RetonarTimeTheBlueAllianceAsync(string numero)
        {
            var request = new RestRequest(string.Format("team/frc{0}", numero));
            request.AddHeader("X-TBA-Auth-Key", "xbOx30uWwOP5b2Pc0YfVoujuxHt9llWMAgAulPS4VPMOV2hhT8DLZSWLzCgMuA3G");
            request.AddHeader("Content-Type", @"application/json");

            var requestJson = HttpClient.Execute(request);
            if (requestJson.IsSuccessful)
                return JsonConvert.DeserializeObject<TimeTheBlueAlliance>(requestJson.Content);

            return null;
        }
    }

    public class TimeTheBlueAlliance
    {
        [JsonProperty(PropertyName = "nickname")]
        public string Nome { get; set; }

        [JsonProperty(PropertyName = "team_number")]
        public string Numero { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Pais { get; set; }
    }
}
