using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices
{
    public interface IStatusServices
    {
        StatusEntity GetStatusById(int statusId);
        IEnumerable<StatusEntity> GetAllStatus();
        int CreateStatus(StatusEntity statusEntity);
        bool UpdateStatus(int statusId, StatusEntity statusEntity);
        bool DeleteStatus(int statusId);
    }
}
