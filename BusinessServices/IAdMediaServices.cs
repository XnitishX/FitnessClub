using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DataModel;

namespace BusinessServices
{
    public interface IAdMediaServices
    {
        AdMediaEntity GetAdMediaById(int adMediaId);
        IEnumerable<AdMediaEntity> GetAllAdMedia();
        int CreateAdMedia(AdMediaEntity adMediaEntity);
        bool UpdateAdMedia(int userId, AdMediaEntity adMediaEntity);
        bool DeleteAdMedia(int adMediaId);
    }
}

