using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using research_project_backend.Data;
using research_project_backend.Data.Domains;
using IntroductionCondition = research_project_backend.Data.Domains.IntroductionCondition;

namespace research_project_backend.Services
{
    public class IntroductionConditionData : IIntroductionConditionData
    {
        private readonly MijnStagesDbContext _mijnStageDbContext;

        public IntroductionConditionData(MijnStagesDbContext mijnStagesDbContext)
        {
            _mijnStageDbContext = mijnStagesDbContext;
        }

        public List<IntroductionCondition> GetIntroductionConditionById(int id)
        {
            return _mijnStageDbContext.IntroductionCondition.ToList()
                .Where(i => i.InternshipAssignmentId == id).ToList();
        }
    }
}
