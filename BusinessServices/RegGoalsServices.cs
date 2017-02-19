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
    public class RegGoalsServices : IRegGoalsServices
    {
        private readonly UnitOfWork _unitOfWork;

        public RegGoalsServices(UnitOfWork unitOfWork)
        {
            //_unitOfWork = new UnitOfWork();
            _unitOfWork = unitOfWork;
        }

        IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<RegGoal, RegGoalsEntity>();
        }).CreateMapper();
        public RegGoalsEntity GetRegGoalsById(int regGoalsId)
        {

            var regGoals = _unitOfWork.RegGoalRepository.GetByID(regGoalsId);
            if (regGoals != null)
            {
                var regGoalsModel = mapper.Map<RegGoal, RegGoalsEntity>(regGoals);
                return regGoalsModel;
            }
            return null;
        }

        public IEnumerable<RegGoalsEntity> GetAllRegGoals()
        {
            var regGoals = _unitOfWork.RegGoalRepository.GetAll().ToList();
            if (regGoals.Any())
            {
                var regGoalsModel = mapper.Map<List<RegGoal>, List<RegGoalsEntity>>(regGoals);
                return regGoalsModel;
            }
            return null;
        }

        public int CreateRegGoals(RegGoalsEntity regGoalsEntity)
        {
            using (var scope = new TransactionScope())
            {
                var regGoals = new RegGoal()
                {
                    regId=regGoalsEntity.regId,
                    goalId=regGoalsEntity.goalId
                };
                _unitOfWork.RegGoalRepository.Insert(regGoals);
                _unitOfWork.Save();
                scope.Complete();
                return regGoals.id;
            }
        }

        public bool UpdateRegGoals(int regGoalsId, RegGoalsEntity regGoalsEntity)
        {
            var success = false;
            if (regGoalsEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var regGoals = _unitOfWork.RegGoalRepository.GetByID(regGoalsId);
                    if (regGoals != null)
                    {
                        regGoals.regId = regGoalsEntity.regId;
                        regGoals.goalId = regGoalsEntity.goalId;
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteRegGoals(int regGoalsId)
        {
            var success = false;
            if (regGoalsId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var regGoals = _unitOfWork.RegGoalRepository.GetByID(regGoalsId);
                    if (regGoals != null)
                    {
                        _unitOfWork.RegGoalRepository.Delete(regGoals);
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
