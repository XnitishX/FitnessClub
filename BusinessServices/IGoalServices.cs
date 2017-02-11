using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;


namespace BusinessServices
{
    public interface IGoalServices
    {
        GoalEntity GetGoalById(int goalId);
        IEnumerable<GoalEntity> GetAllGoals();
        int CreateGoal(GoalEntity goalEntity);
        bool UpdateGoal(int goalId, GoalEntity goalEntity);
        bool DeleteGoal(int goalId);
    }
}

