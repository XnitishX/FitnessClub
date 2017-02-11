using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class TokenEntity
    {
        public int tokenId { get; set; }
        public Nullable<int> userId { get; set; }
        public string authToken { get; set; }
        public Nullable<System.DateTime> issuedOn { get; set; }
        public Nullable<System.DateTime> expiresOn { get; set; }
    }
}
