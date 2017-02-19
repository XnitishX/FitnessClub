using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;


namespace BusinessServices
{
    public interface IImageDetailServices
    {
        ImageDetailEntity GetImageDetailById(int imageId);
        IEnumerable<ImageDetailEntity> GetAllImageDetail();
        int CreateImageDetail(ImageDetailEntity imageEntity);
        bool UpdateImageDetail(int imageId, ImageDetailEntity imageEntity);
        bool DeleteImageDetail(int imageId);
    }
}

