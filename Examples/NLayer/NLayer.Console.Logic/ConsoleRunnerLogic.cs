using NLayer.Core.Abstractions;
using System;
namespace NLayer.Console.Logic
{
    public class ConsoleRunnerLogic : IApplicationRunner
    {
        readonly IBussinessModel model;
        readonly IPresenter presenter;

        public ConsoleRunnerLogic(IBussinessModel model, IPresenter presenter)
        {
            this.model = model ?? throw new ArgumentNullException(nameof(model));
            this.presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        public void Run()
        {
            model.Show(presenter);
        }
    }
}
