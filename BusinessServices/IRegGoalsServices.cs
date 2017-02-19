using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices
{
    public interface IRegGoalsServices
    {
        RegGoalsEntity GetRegGoalsById(int regGoalsId);
        IEnumerable<RegGoalsEntity> GetAllRegGoals();
        int CreateRegGoals(RegGoalsEntity regGoalsEntity);
        bool UpdateRegGoals(int regGoalsId, RegGoalsEntity regGoalsEntity);
        bool DeleteRegGoals(int regGoalsId);
    }
}
