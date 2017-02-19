using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices
{
    public interface ITrainingServices
    {
        TrainingEntity GetTrainingById(int trainingId);
        IEnumerable<TrainingEntity> GetAllTrainings();
        int CreateTraining(TrainingEntity trainingEntity);
        bool UpdateTraining(int trainingId, TrainingEntity trainingEntity);
        bool DeleteTraining(int trainingId);
    }
}
