using AuditBenchmarkService.Models;
using System.Collections.Generic;

namespace AuditBenchmarkService.Provider
{
    public interface IBenchmarkProvider
    {
        public Dictionary<string, int> GetBenchmark();
    }
}
