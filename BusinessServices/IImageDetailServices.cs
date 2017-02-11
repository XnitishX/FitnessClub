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
        ImageDetailEntity GetImageById(int imageId);
        IEnumerable<ImageDetailEntity> GetAllImages();
        int CreateImage(ImageDetailEntity imageEntity);
        bool UpdateImage(int imageId, ImageDetailEntity imageEntity);
        bool DeleteImage(int imageId);
    }
}

