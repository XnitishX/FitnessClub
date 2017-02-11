using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class RegistrationEntity
    {
        public int regId { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public Nullable<bool> gender { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public string contactNo { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public Nullable<int> trainings { get; set; }
        public Nullable<int> goals { get; set; }
        public Nullable<int> timeSlot { get; set; }
        public Nullable<int> adMedia { get; set; }
        public Nullable<int> imageId { get; set; }
        public Nullable<int> repId { get; set; }
        public Nullable<System.DateTime> registeredOn { get; set; }
        public Nullable<System.DateTime> lastChangedOn { get; set; }
        public Nullable<int> status { get; set; }
    }
}
