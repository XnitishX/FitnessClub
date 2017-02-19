using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices
{
    public interface IUserServices
    {
        UserEntity GetUserById(int userId);
        IEnumerable<UserEntity> GetAllUsers();
        int CreateUser(UserEntity userEntity);
        bool UpdateUser(int userId, UserEntity userEntity);
        bool DeleteUser(int userId);

        int Authenticate(string userName, string password);
    }
}
