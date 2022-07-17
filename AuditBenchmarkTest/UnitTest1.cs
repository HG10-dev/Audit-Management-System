using AuditBenchmarkService.Models;
using AuditBenchmarkService.Provider;
using AuditBenchmarkService.Repository;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace AuditBenchmarkTest
{
    public class Tests
    {
        private IBenchmarkProvider provider;
        private IBenchmarkRepo repo;
        List<AuditBenchmark> list;
        [SetUp]
        public void Setup()
        {
            list = new List<AuditBenchmark>()
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
            
        }

        [Test]
        public void GetNoListReturnsList()
        {
            repo = new BenchmarkRepo();
            var list = repo.GetNolist();
            Assert.IsNotNull(list);
        }

        [Test]
        public void GetBenchmarkReturnsData()
        {
            repo = new BenchmarkRepo();
            provider = new BenchmarkProvider(repo);
            Mock<IBenchmarkRepo> mock = new Mock<IBenchmarkRepo>();
            mock.Setup(c => c.GetNolist()).Returns(list);
            var data = provider.GetBenchmark();

            Assert.IsNotNull(data);
        }

        [Test]
        public void GetBenchmarkReturnsNull()
        {
            list = null;
            provider = new BenchmarkProvider(repo);
            Mock<IBenchmarkRepo> mock = new Mock<IBenchmarkRepo>();
            mock.Setup(c => c.GetNolist()).Returns(list);
            
            var data = provider.GetBenchmark();

            Assert.IsNull(data);
        }
        [TearDown]
        public void testTearDown()
        {
            repo = null;
        }

    }
}