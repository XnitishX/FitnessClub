using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;


namespace BusinessServices
{
    public interface IRegistrationServices
    {
        RegistrationEntity GetRegistrationById(int regId);
        IEnumerable<RegistrationEntity> GetAllRegistrations();
        int CreateRegistration(RegistrationEntity registrationEntity);
        bool UpdateRegistration(int regId, RegistrationEntity registrationEntity);
        bool DeleteRegistration(int regId);
    }
}
