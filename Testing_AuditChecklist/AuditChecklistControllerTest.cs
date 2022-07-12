using AuditChecklistModule.Controllers;
using AuditChecklistModule.Repository;
using AuditChecklistModule.Providers;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using Moq;
using AuditChecklistModule.Models;
using System.Collections.Generic;
using System.Linq;

namespace AuditChecklistModule.Testing
{
    
    public class AuditChecklistControllerTest
    {
        List<Questions> ls = new List<Questions>();
        [SetUp]
        public void setup()
        {   
            
            ls=new List<Questions>()
            {
                new Questions
                {
                    QuestionNo=1,
                    Question="Have all Change requests followed SDLC before PROD move?"
                },
                new Questions
                {
                    QuestionNo=2,
                    Question="Have all Change requests been approved by the application owner?"
                }
            };

           
        }


        [TestCase("Internal")]
        [TestCase("SOX")]
        public void CorrectTypeTestProvider(string type)
        {
            
            Mock<IChecklistRepo> mock = new Mock<IChecklistRepo>();
            mock.Setup(p => p.GetQuestions(type)).Returns(ls);
            ChecklistProvider cp = new ChecklistProvider(mock.Object);
            List<Questions> result= cp.QuestionsProvider(type);
            Assert.AreEqual(2,result.Count);
        }

        [TestCase("Internal123")]
        [TestCase("SOX123")]
        public void WrongTypeTestProvider(string type)
        {
            try
            {
                Mock<IChecklistRepo> mock = new Mock<IChecklistRepo>();
                mock.Setup(p => p.GetQuestions(type)).Returns(ls);
                ChecklistProvider cp = new ChecklistProvider(mock.Object);
                List<Questions> result = cp.QuestionsProvider(type);
                Assert.AreEqual(2, result.Count);
            }
            catch(Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.",e.Message);
            }
            
        }

        [TestCase("Internal")]
        [TestCase("SOX")]
        public void CorrectTypeController(string type)
        {
            Mock<IChecklistProvider> mock = new Mock<IChecklistProvider>();
            mock.Setup(p => p.QuestionsProvider(type)).Returns(ls);
            AuditChecklistController cp = new AuditChecklistController(mock.Object);
            OkObjectResult result = cp.GetQuestions(type) as OkObjectResult;
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestCase("Internal123")]
        [TestCase("SOX123")]
        public void WrongTypeController(string type)
        {
            try
            {
                Mock<IChecklistProvider> mock = new Mock<IChecklistProvider>();
                mock.Setup(p => p.QuestionsProvider(type)).Returns(ls);
                AuditChecklistController cp = new AuditChecklistController(mock.Object);
                OkObjectResult result = cp.GetQuestions(type) as OkObjectResult;
                Assert.AreEqual(200, result.StatusCode);
            }
            catch(Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
            
        }


    }
}