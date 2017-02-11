using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class RegTrainingEntity
    {
        public int id { get; set; }
        public Nullable<int> regId { get; set; }
        public Nullable<int> trainingId { get; set; }
    }
}
