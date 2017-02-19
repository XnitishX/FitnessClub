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
    public class TrainingServices : ITrainingServices
    {
        private readonly UnitOfWork _unitOfWork;

        public TrainingServices(UnitOfWork unitOfWork)
        {
            //_unitOfWork = new UnitOfWork();
            _unitOfWork = unitOfWork;
        }

        IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<Training, TrainingEntity>();
        }).CreateMapper();
        public TrainingEntity GetTrainingById(int trainingId)
        {

            var training = _unitOfWork.TrainingRepository.GetByID(trainingId);
            if (training != null)
            {
                var trainingModel = mapper.Map<Training, TrainingEntity>(training);
                return trainingModel;
            }
            return null;
        }

        public IEnumerable<TrainingEntity> GetAllTrainings()
        {
            var trainings = _unitOfWork.TrainingRepository.GetAll().ToList();
            if (trainings.Any())
            {
                var trainingsModel = mapper.Map<List<Training>, List<TrainingEntity>>(trainings);
                return trainingsModel;
            }
            return null;
        }

        public int CreateTraining(TrainingEntity trainingEntity)
        {
            using (var scope = new TransactionScope())
            {
                var training = new Training()
                {
                    trainingId = trainingEntity.trainingId,
                    name = trainingEntity.name,
                    description = trainingEntity.description
                };
                _unitOfWork.TrainingRepository.Insert(training);
                _unitOfWork.Save();
                scope.Complete();
                return training.trainingId;
            }
        }

        public bool UpdateTraining(int trainingId, TrainingEntity trainingEntity)
        {
            var success = false;
            if (trainingEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var training = _unitOfWork.TrainingRepository.GetByID(trainingId);
                    if (training != null)
                    {
                        training.name = trainingEntity.name;
                        training.description = trainingEntity.description;
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteTraining(int trainingId)
        {
            var success = false;
            if (trainingId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var training = _unitOfWork.TrainingRepository.GetByID(trainingId);
                    if (training != null)
                    {
                        _unitOfWork.TrainingRepository.Delete(training);
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
