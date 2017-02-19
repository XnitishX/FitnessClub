using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices
{
    public interface ITimeSlotServices
    {
        TimeSlotEntity GetTimeSlotById(int timeSlotId);
        IEnumerable<TimeSlotEntity> GetAllTimeSlots();
        int CreateTimeSlot(TimeSlotEntity timeSlotEntity);
        bool UpdateTimeSlot(int timeSlotId, TimeSlotEntity timeSlotEntity);
        bool DeleteTimeSlot(int timeSlotId);
    }
}
