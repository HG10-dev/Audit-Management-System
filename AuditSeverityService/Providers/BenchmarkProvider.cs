using AuditSeverityService.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuditSeverityService.Providers
{
    public class BenchmarkProvider : IBenchmarkProvider
    {
        private IConfiguration config;       
        public async Task<Dictionary<string, int>> GetAuditBenchmarks(IConfiguration _config)
        {
            config = _config;
            HttpResponseMessage response = new HttpResponseMessage();
            string uriConn = config["Url:address"];

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uriConn);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    response = await client.GetAsync("/api/AuditBenchmark");
                }
                catch(Exception e)
                {
                    return null;
                }
            }

            Dictionary<string, int> benchmarks = new Dictionary<string, int>();
           
            string responseValue = response.Content.ReadAsStringAsync().Result;
            benchmarks = JsonConvert.DeserializeObject<Dictionary<string, int>>(responseValue);

            return benchmarks;
        }        
    }
}
