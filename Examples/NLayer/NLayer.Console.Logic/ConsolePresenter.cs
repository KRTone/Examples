using NLayer.Core.Abstractions;
using System;

namespace NLayer.Console.Logic
{
    public class ConsolePresenter : IPresenter
    {
        readonly Random rnd = new Random();

        public void Present(string str)
        {
            System.Console.ForegroundColor = (ConsoleColor)rnd.Next(1, 15);
            System.Console.WriteLine(str);
        }
    }
}
