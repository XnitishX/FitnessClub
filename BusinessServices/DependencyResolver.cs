using System.ComponentModel.Composition;
using DataModel;
using DataModel.UnitOfWork;
using Resolver;
namespace BusinessServices
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUserServices, UserServices>();
            registerComponent.RegisterType<ITokenServices, TokenServices>();
            registerComponent.RegisterType<IAdMediaServices, AdMediaServices>();
            registerComponent.RegisterType<ITimeSlotServices, TimeSlotServices>();
            registerComponent.RegisterType<IImageDetailServices, ImageDetailServices>();
            registerComponent.RegisterType<IRegistrationServices, RegistrationServices>();
            registerComponent.RegisterType<IStatusServices, StatusServices>();
            registerComponent.RegisterType<IGoalServices, GoalServices>();
            registerComponent.RegisterType<ITrainingServices, TrainingServices>();
            registerComponent.RegisterType<IRegTrainingServices, RegTrainingServices>();
            registerComponent.RegisterType<IRegGoalsServices, RegGoalsServices>();
        }
    }
}
