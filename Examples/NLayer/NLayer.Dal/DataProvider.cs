using NLayer.Core.Abstractions;
using System;
using System.Linq;

namespace NLayer.Dal
{
    public class DataProvider : IDataProvider
    {
        readonly Lazy<MyObject[]> objContainer;

        public DataProvider()
        {
            objContainer = new Lazy<MyObject[]>(InitializeObjects);
        }

        public object[] GetSomeData()
        {
            return objContainer.Value.Cast<object>().ToArray();
        }

        private MyObject[] InitializeObjects()
        {
            int count = 10;
            Random rnd = new Random();
            MyObject[] myObjects = new MyObject[count];
            for (int i = 0; i < count; i++)
            {
                myObjects[i] = new MyObject(rnd.Next(i, count), rnd.Next(rnd.Next(i, i * count)));
            }
            return myObjects;
        }
    }
}
