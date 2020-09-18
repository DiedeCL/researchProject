using System.Collections.Generic;
using IntroductionCondition = research_project_backend.Data.Domains.IntroductionCondition;

namespace research_project_backend.Services
{
    public interface IIntroductionConditionData
    {
        List<IntroductionCondition> GetIntroductionConditionById(int id);
    }
}
