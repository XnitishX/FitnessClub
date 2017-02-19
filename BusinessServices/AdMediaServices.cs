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
    public class AdMediaServices : IAdMediaServices
    {
        private readonly UnitOfWork _unitOfWork;

        public AdMediaServices(UnitOfWork unitOfWork)
        {
            //_unitOfWork = new UnitOfWork();
            _unitOfWork = unitOfWork;
        }

        IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<AdMedia, AdMediaEntity>();
        }).CreateMapper();
        public AdMediaEntity GetAdMediaById(int adMediaId)
        {

            var adMedia = _unitOfWork.AdMediaRepository.GetByID(adMediaId);
            if (adMedia != null)
            {
                var adMediaModel = mapper.Map<AdMedia, AdMediaEntity>(adMedia);
                return adMediaModel;
            }
            return null;
        }

        public IEnumerable<AdMediaEntity> GetAllAdMedia()
        {
            var adMedias = _unitOfWork.AdMediaRepository.GetAll().ToList();
            if (adMedias.Any())
            {
                var adMediasModel = mapper.Map<List<AdMedia>, List<AdMediaEntity>>(adMedias);
                return adMediasModel;
            }
            return null;
        }

        public int CreateAdMedia(AdMediaEntity adMediaEntity)
        {
            using (var scope = new TransactionScope())
            {
                var adMedia = new AdMedia()
                {
                    id=adMediaEntity.id,
                    name=adMediaEntity.name,
                    description=adMediaEntity.description
                };
                _unitOfWork.AdMediaRepository.Insert(adMedia);
                _unitOfWork.Save();
                scope.Complete();
                return adMedia.id;
            }
        }

        public bool UpdateAdMedia(int adMediaId, AdMediaEntity adMediaEntity)
        {
            var success = false;
            if (adMediaEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var adMedia = _unitOfWork.AdMediaRepository.GetByID(adMediaId);
                    if (adMedia != null)
                    {
                        adMedia.id = adMediaEntity.id;
                        adMedia.name = adMediaEntity.name;
                        adMedia.description = adMediaEntity.description;
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteAdMedia(int adMediaId)
        {
            var success = false;
            if (adMediaId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var adMedia = _unitOfWork.AdMediaRepository.GetByID(adMediaId);
                    if (adMedia != null)
                    {
                        _unitOfWork.AdMediaRepository.Delete(adMedia);
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
