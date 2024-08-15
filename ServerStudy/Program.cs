using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

// SpinLock: 락을 획득할 때까지 무한루프를 도는 과정.

namespace ServerCore
{
    class SpinLock
    {
        volatile int _locked = 0;

        public void Acquire()
        {
            while (true)
            {
                // 1: Interlocked.Exchange
                //int original = Interlocked.Exchange(ref _locked, 1);
                // _locked에 1을 넣어주는 것을 한번에 처리하도록 락함.
                //if (original == 0)
                //{
                //    break;
                //}


                // 2: Interlocked.CompareExchange. Compare-And-Swap(CAS)
                int expected = 0;
                int desired = 1;
                if (Interlocked.CompareExchange(ref _locked, desired, expected) == expected)
                        break;
                // _locked가 expected이면 desired로 바꾸는 처리를 한번에.

            }

        }

        public void Release()
        {
            _locked = 0;
        }
    }


    class Program
    {
        static SpinLock _locked = new SpinLock();

        static int _num = 0;

        static void Thread_1()
        {
            for (int i = 0; i < 100000; i++)
            {
                _locked.Acquire();
                _num++;
                _locked.Release();
            }
        }

        static void Thread_2()
        {
            for (int i = 0; i < 100000; i++)
            {
                _locked.Acquire();
                _num--;
                _locked.Release();
            }
        }

        static void Main(string[] args) 
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);

            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);
            Console.WriteLine(_num);
        }
    }
}