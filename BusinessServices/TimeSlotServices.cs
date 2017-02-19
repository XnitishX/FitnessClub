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
    public class TimeSlotServices : ITimeSlotServices
    {
        private readonly UnitOfWork _unitOfWork;

        public TimeSlotServices(UnitOfWork unitOfWork)
        {
            //_unitOfWork = new UnitOfWork();
            _unitOfWork = unitOfWork;
        }

        IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<TimeSlot, TimeSlotEntity>();
        }).CreateMapper();
        public TimeSlotEntity GetTimeSlotById(int timeSlotId)
        {

            var timeSlot = _unitOfWork.TimeSlotRepository.GetByID(timeSlotId);
            if (timeSlot != null)
            {
                var timeSlotModel = mapper.Map<TimeSlot, TimeSlotEntity>(timeSlot);
                return timeSlotModel;
            }
            return null;
        }

        public IEnumerable<TimeSlotEntity> GetAllTimeSlots()
        {
            var timeSlots = _unitOfWork.TimeSlotRepository.GetAll().ToList();
            if (timeSlots.Any())
            {
                var timeSlotsModel = mapper.Map<List<TimeSlot>, List<TimeSlotEntity>>(timeSlots);
                return timeSlotsModel;
            }
            return null;
        }

        public int CreateTimeSlot(TimeSlotEntity timeSlotEntity)
        {
            using (var scope = new TransactionScope())
            {
                var timeSlot = new TimeSlot()
                {
                    id = timeSlotEntity.id,
                    name = timeSlotEntity.name,
                    description = timeSlotEntity.description,
                };
                _unitOfWork.TimeSlotRepository.Insert(timeSlot);
                _unitOfWork.Save();
                scope.Complete();
                return timeSlot.id;
            }
        }

        public bool UpdateTimeSlot(int timeSlotId, TimeSlotEntity timeSlotEntity)
        {
            var success = false;
            if (timeSlotEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var timeSlot = _unitOfWork.TimeSlotRepository.GetByID(timeSlotId);
                    if (timeSlot != null)
                    {
                        timeSlot.name = timeSlotEntity.name;
                        timeSlot.description = timeSlotEntity.description;
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteTimeSlot(int timeSlotId)
        {
            var success = false;
            if (timeSlotId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var timeSlot = _unitOfWork.TimeSlotRepository.GetByID(timeSlotId);
                    if (timeSlot != null)
                    {
                        _unitOfWork.TimeSlotRepository.Delete(timeSlot);
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
