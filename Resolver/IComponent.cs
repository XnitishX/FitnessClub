using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver
{
    public interface IComponent
    {
        /// <summary>
        /// Register underlying types with unity.
        /// </summary>
        void SetUp(IRegisterComponent registerComponent);
    }
}
