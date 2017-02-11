using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class UserEntity
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public Nullable<int> accessLevel { get; set; }
    }
}
