using AuditSeverityService.Models;
using AuditSeverityService.Providers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuditSeverityService.Repositories
{
    public class SeverityRepo: ISeverityRepo
    {
        private IBenchmarkProvider provider;
        private IConfiguration config;

        public SeverityRepo(IConfiguration _config, IBenchmarkProvider _provider)
        {
            provider = _provider;  
            config = _config;  
        }

        public async Task<AuditResponse> GetResponse(AuditRequest request)
        {
            Dictionary<string, int> benchmarks = await provider.GetAuditBenchmarks(config);
            if(benchmarks == null)
            {
                return null;    
            }
            
            AuditType type = request.AuditDetail.AuditType;
            //Dictionary<int, string> auditQuestions = request.AuditDetail.AuditQuestions.ToDictionary();
            List<QAndA> auditQuestions = request.AuditDetail.AuditQuestions;
            int counter = 0;
            //foreach(KeyValuePair<int,string> question in auditQuestions)
            foreach(QAndA question in auditQuestions)
            {
                if (question.Ans == "No")
                    counter++;
            }

            AuditResponse auditResponse = new AuditResponse();
            Random r = new Random();
            auditResponse.AuditId = r.Next(100, 200);

            if (counter <= benchmarks[type.ToString()])
                auditResponse.ProjectExecutionStatus = Status.Green;
            else
                auditResponse.ProjectExecutionStatus = Status.Red;
            
           if(auditResponse.ProjectExecutionStatus != Status.Green)
            {
                auditResponse.RemedialActionDuration = type == AuditType.Internal ? 2 : 1;
            }
            

            return auditResponse;
        }
    }
}
