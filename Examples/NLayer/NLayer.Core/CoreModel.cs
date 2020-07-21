using NLayer.Core.Abstractions;
using System;

namespace NLayer.Core
{
    public class CoreModel : IBussinessModel
    {
        readonly IDataProvider dataProvider;

        public CoreModel(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        public void Show(IPresenter presenter)
        {
            foreach (object item in dataProvider.GetSomeData())
            {
                presenter.Present(item.ToString());
            }
        }
    }
}
