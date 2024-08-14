using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    //메모리 배리어
    // A) 코드 재배치 억제
    // B) 가시성

    // 1) Full Memory Barrier
    // 2) Store Memory Barrier: Store만 막는다
    // 3) Load Memory Barrier: Load만 막는다

    class Program
    {
        static volatile int x = 0;
        static volatile int y = 0;
        static volatile int r1 = 0;
        static volatile int r2 = 0;

        static void Thread_1()
        {
            y = 1; // Store y
            // ---------------넘지마시오--------
            //Thread.MemoryBarrier(); 
            // 메모리 배리어의 모습. 최신 정보 판별을 위해 코드의 맨앞, 맨뒤에 두어 확실히 하기도 한다.
            r1 = x; // Load x
        }

        static void Thread_2()
        {
            x = 1;  // Store x
            // ---------------넘지마시오--------
            //Thread.MemoryBarrier();
            r2 = y; // Load y
        }

        static void Main(string[] args)
        {
            int count = 0;
            while (true)
            {
                count++;
                x = y = r1 = r2 = 0;

                Task t1 = new Task(Thread_1);
                Task t2 = new Task(Thread_2);

                t1.Start();
                t2.Start();

                Task.WaitAll(t1, t2);

                if (r1 ==  0 & r2 == 0) { break; }
            }

            Console.WriteLine($"{count}번 만에 빠져나옴!");
        }

    }

}