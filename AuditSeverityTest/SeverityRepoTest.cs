using AuditSeverityService.Models;
using AuditSeverityService.Providers;
using AuditSeverityService.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace AuditSeverityTest
{
    public class Tests
    {
        private Dictionary<string, int> data;
        private IBenchmarkProvider provider;
        private ISeverityRepo repo;
        private IConfiguration config;
        private readonly AuditRequest request = new AuditRequest()
        {            
                ProjectName= "project",
                ProjectManagerName= "Gautam",
                ApplicationOwnerName= "Himangshu",
                AuditDetail= new AuditDetail(){
                AuditType= AuditType.Internal,
                    AuditDate="7/8/2022",
                    AuditQuestions= new List<QAndA>(){
                        new QAndA(){Id=1,Ans="No"},
                        new QAndA(){Id=2,Ans="Yes"},
                        new QAndA(){Id=3,Ans="Yes"},
                        new QAndA(){ Id=4,Ans="Yes"},
                        new QAndA(){ Id=5,Ans="Yes"}
                    }
                }
            
        };

        [SetUp]
        public void Setup()
        {
            data = new Dictionary<string, int>();
            data.Add("Internal", 3);
        }

        [Test]
        public void GetAuditBenchmarksReturnsData()
        {            
            Mock<IBenchmarkProvider> mock = new Mock<IBenchmarkProvider>();
            mock.Setup(d => d.GetAuditBenchmarks(config)).ReturnsAsync(data);
            repo = new SeverityRepo(config, mock.Object);
            var result = repo.GetResponse(request).Result;
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAuditBenchmarksReturnsNull()
        {
            data = null;
            Mock<IBenchmarkProvider> mock = new Mock<IBenchmarkProvider>();
            mock.Setup(d => d.GetAuditBenchmarks(config)).ReturnsAsync(data);
            repo = new SeverityRepo(config, mock.Object);
            var result = repo.GetResponse(request).Result;
            Assert.IsNull(result);
        }
    }
}