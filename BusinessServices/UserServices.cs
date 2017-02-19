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
    public class UserServices : IUserServices
    {
        private readonly UnitOfWork _unitOfWork;

        public UserServices(UnitOfWork unitOfWork)
        {
            //_unitOfWork = new UnitOfWork();
            _unitOfWork = unitOfWork;
        }

        IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<User, UserEntity>();
        }).CreateMapper();

        public int Authenticate(string userName, string password)
        {
            var user = _unitOfWork.UserRepository.Get(u=>u.userName==userName&&u.password==password);
            if(user!=null && user.userId>0)
            {
                return user.userId;
            }
            return 0;
        }
        public UserEntity GetUserById(int userId)
        {
            
            var user = _unitOfWork.UserRepository.GetByID(userId);
            if (user != null)
            {
                var userModel=mapper.Map<User, UserEntity>(user);
                return userModel;
            }
            return null;
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            var users = _unitOfWork.UserRepository.GetAll().ToList();
            if (users.Any())
            {
                var usersModel = mapper.Map<List<User>, List<UserEntity>>(users);
                return usersModel;
            }
            return null;
        }

        public int CreateUser(UserEntity userEntity)
        {
            using (var scope = new TransactionScope())
            {
                var user = new User()
                {
                    userId = userEntity.userId,
                    userName = userEntity.userName,
                    password= userEntity.password,
                    name= userEntity.name
                };
                _unitOfWork.UserRepository.Insert(user);
                _unitOfWork.Save();
                scope.Complete();
                return user.userId;
            }
        }

        public bool UpdateUser(int userId,UserEntity userEntity)
        {
            var success = false;
            if(userEntity!=null)
            {
                using (var scope = new TransactionScope())
                {
                    var user = _unitOfWork.UserRepository.GetByID(userId);
                    if(user!=null)
                    {
                        user.userName = userEntity.userName;
                        user.password = userEntity.password;
                        user.name = userEntity.name;
                        user.accessLevel = userEntity.accessLevel;
                        _unitOfWork.Save();
                        scope.Complete();
                        success=true;
                    }
                }
            }
            return success;
        }

        public bool DeleteUser(int userId)
        {
            var success = false;
            if(userId>0)
            {
                using (var scope=new TransactionScope())
                {
                    var user = _unitOfWork.UserRepository.GetByID(userId);
                    if(user!=null)
                    {
                        _unitOfWork.UserRepository.Delete(user);
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
