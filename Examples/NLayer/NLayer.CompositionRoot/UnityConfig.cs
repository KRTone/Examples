using NLayer.Console.Logic;
using NLayer.Core;
using NLayer.Core.Abstractions;
using NLayer.Dal;
using Unity;

namespace NLayer.CompositionRoot
{
    public class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDataProvider, DataProvider>();
            container.RegisterType<IBussinessModel, CoreModel>();
            container.RegisterType<IPresenter, ConsolePresenter>();
            container.RegisterType<IApplicationRunner, ConsoleRunnerLogic>();
        }
    }
}
