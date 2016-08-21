using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JitInterface.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            JitHook.Hook();
            Console.WriteLine("Hook set!");

            Test1();

            Console.ReadKey();

            JitHook.Unhook();
            Console.WriteLine("Hook unset.");

            Test3();

            Console.ReadKey();
        }

        static void Test1()
        {
            Console.WriteLine("Test1");
            Test2();
        }

        static void Test2()
        {
            Console.WriteLine("Test2.");
        }

        static void Test3()
        {
            Console.WriteLine("Test3.");
        }
    }
}
