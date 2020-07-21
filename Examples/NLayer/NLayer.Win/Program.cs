using NLayer.CompositionRoot;
using NLayer.Core.Abstractions;
using System;
using Unity;

namespace NLayer.Win
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            UnityConfig.RegisterTypes(container);

            container
                .Resolve<IApplicationRunner>()
                .Run();

            Console.ReadKey();
        }
    }
}
