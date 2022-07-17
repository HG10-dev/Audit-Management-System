using AuditBenchmarkService.Models;
using AuditBenchmarkService.Repository;
using System;
using System.Collections.Generic;

namespace AuditBenchmarkService.Repository
{
    public class BenchmarkRepo : IBenchmarkRepo
    {
  
        private static List<AuditBenchmark> AuditBenchmarkList ()=> new List<AuditBenchmark>()
        {
        
            new AuditBenchmark
            {
                AuditType="Internal",
                BenchmarkNoAnswers=3
            },
            new AuditBenchmark
            {
                AuditType="SOX",
                BenchmarkNoAnswers=1
            }
        };
        public List<AuditBenchmark> GetNolist()
        {

            List<AuditBenchmark> listOfCriteria = new List<AuditBenchmark>();
            try
            {
                listOfCriteria = AuditBenchmarkList();
                return listOfCriteria;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
