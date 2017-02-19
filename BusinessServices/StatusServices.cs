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
    public class StatusServices : IStatusServices
    {
        private readonly UnitOfWork _unitOfWork;

        public StatusServices(UnitOfWork unitOfWork)
        {
            //_unitOfWork = new UnitOfWork();
            _unitOfWork = unitOfWork;
        }

        IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<Status, StatusEntity>();
        }).CreateMapper();
        public StatusEntity GetStatusById(int statusId)
        {

            var status = _unitOfWork.StatusRepository.GetByID(statusId);
            if (status != null)
            {
                var statusModel = mapper.Map<Status, StatusEntity>(status);
                return statusModel;
            }
            return null;
        }

        public IEnumerable<StatusEntity> GetAllStatus()
        {
            var status = _unitOfWork.StatusRepository.GetAll().ToList();
            if (status.Any())
            {
                var statusModel = mapper.Map<List<Status>, List<StatusEntity>>(status);
                return statusModel;
            }
            return null;
        }

        public int CreateStatus(StatusEntity statusEntity)
        {
            using (var scope = new TransactionScope())
            {
                var status = new Status()
                {
                    id = statusEntity.id,
                    name = statusEntity.name,
                    description = statusEntity.description
                };
                _unitOfWork.StatusRepository.Insert(status);
                _unitOfWork.Save();
                scope.Complete();
                return status.id;
            }
        }

        public bool UpdateStatus(int statusId, StatusEntity statusEntity)
        {
            var success = false;
            if (statusEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var status = _unitOfWork.StatusRepository.GetByID(statusId);
                    if (status != null)
                    {
                        status.id = statusEntity.id;
                        status.name = statusEntity.name;
                        status.description = statusEntity.description;
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteStatus(int statusId)
        {
            var success = false;
            if (statusId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var status = _unitOfWork.StatusRepository.GetByID(statusId);
                    if (status != null)
                    {
                        _unitOfWork.StatusRepository.Delete(status);
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
