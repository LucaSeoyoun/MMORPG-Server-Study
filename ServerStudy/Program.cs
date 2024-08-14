using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        static volatile int number = 0;
        static object _obj = new object();

        static void Thread_1()
        {
            for (int i = 0; i < 100000; i++)
            {
                Monitor.Enter(_obj);
                // 문 잠금. 밖에서 접근 불가.
                // Mutual Exclusive 상호배제. 범위를 지정해 싱글스레드의 형태로 활용할 수 있음.
                number++;
                Monitor.Exit(_obj); // 잠금해제
            }
        }

        static void Thread_2()
        {
            for (int i = 0; i < 100000; i++)
            {
                lock (_obj) // 다른 잠금 방식.
                {
                    number--;
                }
            }
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);

            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(number);
        }
    }
}