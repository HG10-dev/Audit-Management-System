using AuditChecklistModule.Models;
using AuditChecklistModule.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditChecklistModule.Providers
{
    public class ChecklistProvider:IChecklistProvider
    {
        private readonly IChecklistRepo repo;
        public ChecklistProvider(IChecklistRepo _repo)
        {
            repo = _repo;
        }
        List<Questions> listOfQuestions = new List<Questions>();

        public List<Questions> QuestionsProvider(string type)
        {
                     
            try
            {
                listOfQuestions = repo.GetQuestions(type);
                return listOfQuestions;
            }
            catch(Exception e)
            {
                return null;
            }
            
            
        }
    }
}
