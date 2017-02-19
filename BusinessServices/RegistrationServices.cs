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
    public class RegistrationServices : IRegistrationServices
    {
        private readonly UnitOfWork _unitOfWork;

        public RegistrationServices(UnitOfWork unitOfWork)
        {
            //_unitOfWork = new UnitOfWork();
            _unitOfWork = unitOfWork;
        }

        IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<Registration, RegistrationEntity>();
        }).CreateMapper();
        public RegistrationEntity GetRegistrationById(int registrationId)
        {

            var registration = _unitOfWork.RegistrationRepository.GetByID(registrationId);
            if (registration != null)
            {
                var registrationModel = mapper.Map<Registration, RegistrationEntity>(registration);
                return registrationModel;
            }
            return null;
        }

        public IEnumerable<RegistrationEntity> GetAllRegistrations()
        {
            var registrations = _unitOfWork.RegistrationRepository.GetAll().ToList();
            if (registrations.Any())
            {
                var registrationsModel = mapper.Map<List<Registration>, List<RegistrationEntity>>(registrations);
                return registrationsModel;
            }
            return null;
        }

        public int CreateRegistration(RegistrationEntity registrationEntity)
        {
            using (var scope = new TransactionScope())
            {
                var registration = new Registration()
                {
                    fname=registrationEntity.fname,
                    lname=registrationEntity.lname,
                    gender=registrationEntity.gender,
                    dob=registrationEntity.dob,
                    contactNo=registrationEntity.contactNo,
                    email=registrationEntity.email,
                    address=registrationEntity.address,
                    trainings=registrationEntity.trainings,
                    goals=registrationEntity.goals,
                    timeSlot=registrationEntity.timeSlot,
                    adMedia=registrationEntity.adMedia,
                    imageId=registrationEntity.imageId,
                    repId=registrationEntity.repId,
                    registeredOn=registrationEntity.registeredOn,
                    lastChangedOn=registrationEntity.lastChangedOn,
                    status=registrationEntity.status    
                };
                _unitOfWork.RegistrationRepository.Insert(registration);
                _unitOfWork.Save();
                scope.Complete();
                return registration.regId;
            }
        }

        public bool UpdateRegistration(int registrationId, RegistrationEntity registrationEntity)
        {
            var success = false;
            if (registrationEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var registration = _unitOfWork.RegistrationRepository.GetByID(registrationId);
                    if (registration != null)
                    {
                        registration.fname = registrationEntity.fname;
                        registration.lname = registrationEntity.lname;
                        registration.gender = registrationEntity.gender;
                        registration.dob = registrationEntity.dob;
                        registration.contactNo = registrationEntity.contactNo;
                        registration.email = registrationEntity.email;
                        registration.address = registrationEntity.address;
                        registration.trainings = registrationEntity.trainings;
                        registration.goals = registrationEntity.goals;
                        registration.timeSlot = registrationEntity.timeSlot;
                        registration.adMedia = registrationEntity.adMedia;
                        registration.imageId = registrationEntity.imageId;
                        registration.repId = registrationEntity.repId;
                        registration.registeredOn = registrationEntity.registeredOn;
                        registration.lastChangedOn = registrationEntity.lastChangedOn;
                        registration.status = registrationEntity.status;
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteRegistration(int registrationId)
        {
            var success = false;
            if (registrationId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var registration = _unitOfWork.RegistrationRepository.GetByID(registrationId);
                    if (registration != null)
                    {
                        _unitOfWork.RegistrationRepository.Delete(registration);
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
