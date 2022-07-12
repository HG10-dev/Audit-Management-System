using System;
using System.Collections.Generic;
using AuditBenchmarkService.Models;
using AuditBenchmarkService.Repository;

namespace AuditBenchmarkService.Provider
{
    public class BenchmarkProvider : IBenchmarkProvider
    {
        private readonly IBenchmarkRepo objBenchmarkRepo;

        public BenchmarkProvider(IBenchmarkRepo _objBenchmarkRepo)
        {
            objBenchmarkRepo = _objBenchmarkRepo;
        }

        public Dictionary<string, int> GetBenchmark()
        {
            List<AuditBenchmark> listOfRepository = new List<AuditBenchmark>();
            try
            {
                listOfRepository = objBenchmarkRepo.GetNolist();
                Dictionary<string, int> benchmarkList = new Dictionary<string, int>();
                foreach (AuditBenchmark benchmark in listOfRepository)
                {
                    benchmarkList.Add(benchmark.AuditType, benchmark.BenchmarkNoAnswers);
                }
                return benchmarkList;
            }
            catch (Exception e)
            {
                return null;
            }

        }

    }
}
