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
        AdMedia GetAdMediaById(int adMediaId);
        IEnumerable<AdMedia> GetAllAdMedia();
        int CreateAdMedia(AdMediaEntity adMediaEntity);
        bool UpdateAdMediar(int userId, AdMediaEntity adMediaEntity);
        bool DeleteAdMedia(int adMediaId);
    }
}

