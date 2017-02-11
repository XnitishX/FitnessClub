using System.ComponentModel.Composition;
using DataModel;
using DataModel.UnitOfWork;
using System.Data.Entity;
using Resolver;

namespace DataModel
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            //registerComponent.RegisterType<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
