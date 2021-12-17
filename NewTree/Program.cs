using System;

namespace NewTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinTree<int> tree = new BinTree<int>();

            tree.Notify += GetMessage;
            tree.Add(1);
            tree.Add(-1);
            tree.Add(2);
            tree.Add(3);
            tree.Add(3);
            tree.Add(6);
            tree.Add(5);
            tree.Add(4);
            tree.Add(0);
            tree.Add(-7);
            tree.Add(3);
            tree.Add(-4);
            tree.Add(-2);
            tree.Add(-3);
            tree.Add(-5);
            tree.Add(-6);
            tree.Add(-8);



            tree.PrintTree();

            tree.Remove(-2);
            tree.Remove(-2);
            tree.PrintTree();


            Console.ReadKey();
        }

        private static void GetMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
