using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;

namespace BusinessServices
{
    public class GoalServices : IGoalServices
    {
        private readonly UnitOfWork _unitOfWork;

        public GoalServices(UnitOfWork unitOfWork)
        {
            //_unitOfWork = new UnitOfWork();
            _unitOfWork = unitOfWork;
        }

        IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<Goal, GoalEntity>();
        }).CreateMapper();
        public GoalEntity GetGoalById(int goalId)
        {

            var goal = _unitOfWork.GoalRepository.GetByID(goalId);
            if (goal != null)
            {
                var goalModel = mapper.Map<Goal, GoalEntity>(goal);
                return goalModel;
            }
            return null;
        }

        public IEnumerable<GoalEntity> GetAllGoals()
        {
            var goals = _unitOfWork.GoalRepository.GetAll().ToList();
            if (goals.Any())
            {
                var goalsModel = mapper.Map<List<Goal>, List<GoalEntity>>(goals);
                return goalsModel;
            }
            return null;
        }

        public int CreateGoal(GoalEntity goalEntity)
        {
            using (var scope = new TransactionScope())
            {
                var goal = new Goal()
                {
                    goalId=goalEntity.goalId,
                    name=goalEntity.name,
                    description=goalEntity.description
                };
                _unitOfWork.GoalRepository.Insert(goal);
                _unitOfWork.Save();
                scope.Complete();
                return goal.goalId;
            }
        }

        public bool UpdateGoal(int goalId, GoalEntity goalEntity)
        {
            var success = false;
            if (goalEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var goal = _unitOfWork.GoalRepository.GetByID(goalId);
                    if (goal != null)
                    {
                        goal.goalId = goalEntity.goalId;
                        goal.name = goalEntity.name;
                        goal.description = goalEntity.description;
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteGoal(int goalId)
        {
            var success = false;
            if (goalId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var goal = _unitOfWork.GoalRepository.GetByID(goalId);
                    if (goal != null)
                    {
                        _unitOfWork.GoalRepository.Delete(goal);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
