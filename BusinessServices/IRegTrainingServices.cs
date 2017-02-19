using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices
{
    public interface IRegTrainingServices
    {
        RegTrainingEntity GetRegTrainingById(int regTrainingId);
        IEnumerable<RegTrainingEntity> GetAllRegTrainings();
        int CreateRegTraining(RegTrainingEntity regTrainingEntity);
        bool UpdateRegTraining(int regTrainingId, RegTrainingEntity regTrainingEntity);
        bool DeleteRegTraining(int regTrainingId);
    }
}
