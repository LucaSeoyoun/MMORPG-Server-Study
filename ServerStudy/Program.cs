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


        static void Thread_1()
        {
            for (int i = 0; i < 100000; i++)
            {
                Interlocked.Increment(ref number);
            }
        }

        static void Thread_2()
        {
            for (int i = 0; i < 100000; i++)
            {
                Interlocked.Decrement(ref number);
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

            // temp 임시변수를 거쳐 숫자를 교체할 경우, 100000씩 ++, --를 했으나  출력값은 0이 아니다.
            // interlocked를 사용해 원자적으로 다룰 경우  예상 출력값인 0이 출력된다.
        }

    }
}