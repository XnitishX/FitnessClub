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
    public class RegTrainingServices : IRegTrainingServices
    {
        private readonly UnitOfWork _unitOfWork;

        public RegTrainingServices(UnitOfWork unitOfWork)
        {
            //_unitOfWork = new UnitOfWork();
            _unitOfWork = unitOfWork;
        }

        IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<RegTraining, RegTrainingEntity>();
        }).CreateMapper();
        public RegTrainingEntity GetRegTrainingById(int regTrainingId)
        {

            var regTraining = _unitOfWork.RegTrainingRepository.GetByID(regTrainingId);
            if (regTraining != null)
            {
                var regTrainingModel = mapper.Map<RegTraining, RegTrainingEntity>(regTraining);
                return regTrainingModel;
            }
            return null;
        }

        public IEnumerable<RegTrainingEntity> GetAllRegTrainings()
        {
            var regTrainings = _unitOfWork.RegTrainingRepository.GetAll().ToList();
            if (regTrainings.Any())
            {
                var regTrainingsModel = mapper.Map<List<RegTraining>, List<RegTrainingEntity>>(regTrainings);
                return regTrainingsModel;
            }
            return null;
        }

        public int CreateRegTraining(RegTrainingEntity regTrainingEntity)
        {
            using (var scope = new TransactionScope())
            {
                var regTraining = new RegTraining()
                {
                    regId = regTrainingEntity.regId,
                    trainingId = regTrainingEntity.trainingId,
                };
                _unitOfWork.RegTrainingRepository.Insert(regTraining);
                _unitOfWork.Save();
                scope.Complete();
                return regTraining.id;
            }
        }

        public bool UpdateRegTraining(int regTrainingId, RegTrainingEntity regTrainingEntity)
        {
            var success = false;
            if (regTrainingEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var regTraining = _unitOfWork.RegTrainingRepository.GetByID(regTrainingId);
                    if (regTraining != null)
                    {
                        regTraining.regId = regTrainingEntity.regId;
                        regTraining.trainingId = regTrainingEntity.trainingId;
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteRegTraining(int regTrainingId)
        {
            var success = false;
            if (regTrainingId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var regTraining = _unitOfWork.RegTrainingRepository.GetByID(regTrainingId);
                    if (regTraining != null)
                    {
                        _unitOfWork.RegTrainingRepository.Delete(regTraining);
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
