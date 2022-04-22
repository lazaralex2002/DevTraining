using System;
using System.IO;

namespace Phase2
{
    class Program
    {
        static void Main(string[] args)
        {
            Project p = new Project();
            p.Initialize("input.txt");
            Console.WriteLine(p);
        }
    }
}
