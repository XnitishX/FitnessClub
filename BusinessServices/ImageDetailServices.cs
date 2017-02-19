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
    public class ImageDetailServices : IImageDetailServices
    {
        private readonly UnitOfWork _unitOfWork;

        public ImageDetailServices(UnitOfWork unitOfWork)
        {
            //_unitOfWork = new UnitOfWork();
            _unitOfWork = unitOfWork;
        }

        IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<ImageDetail, ImageDetailEntity>();
        }).CreateMapper();
        public ImageDetailEntity GetImageDetailById(int imageDetailId)
        {

            var imageDetail = _unitOfWork.ImageDetailRepository.GetByID(imageDetailId);
            if (imageDetail != null)
            {
                var imageDetailModel = mapper.Map<ImageDetail, ImageDetailEntity>(imageDetail);
                return imageDetailModel;
            }
            return null;
        }

        public IEnumerable<ImageDetailEntity> GetAllImageDetail()
        {
            var imageDetails = _unitOfWork.ImageDetailRepository.GetAll().ToList();
            if (imageDetails.Any())
            {
                var imageDetailsModel = mapper.Map<List<ImageDetail>, List<ImageDetailEntity>>(imageDetails);
                return imageDetailsModel;
            }
            return null;
        }

        public int CreateImageDetail(ImageDetailEntity imageDetailEntity)
        {
            using (var scope = new TransactionScope())
            {
                var imageDetail = new ImageDetail()
                {
                    id=imageDetailEntity.id,
                    path=imageDetailEntity.path,
                    description=imageDetailEntity.description

                };
                _unitOfWork.ImageDetailRepository.Insert(imageDetail);
                _unitOfWork.Save();
                scope.Complete();
                return imageDetail.id;
            }
        }

        public bool UpdateImageDetail(int imageDetailId, ImageDetailEntity imageDetailEntity)
        {
            var success = false;
            if (imageDetailEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var imageDetail = _unitOfWork.ImageDetailRepository.GetByID(imageDetailId);
                    if (imageDetail != null)
                    {
                        imageDetail.path = imageDetailEntity.path;
                        imageDetail.description = imageDetailEntity.description;
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteImageDetail(int imageDetailId)
        {
            var success = false;
            if (imageDetailId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var imageDetail = _unitOfWork.ImageDetailRepository.GetByID(imageDetailId);
                    if (imageDetail != null)
                    {
                        _unitOfWork.ImageDetailRepository.Delete(imageDetail);
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
