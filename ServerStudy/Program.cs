using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Lock
    {
        AutoResetEvent _available = new AutoResetEvent(true);
        // AutoResetEvent는 문이 열려 점유한 뒤 문이 자동으로 닫히도록 되어 있음.
        //ManualResetEvent _available = new ManualResetEvent(true);

        public void Acquire()
        {
            _available.WaitOne(); // 입장 시도
            //_available.Reset(); // Reset()은 문을 닫는 작업으로써 이미 AutoResetEvent의 작업과 중복됨.
        }

        public void Release()
        {
            _available.Set();
        }
    }


    class Program
    {
        static Lock _locked = new Lock();
        static int _num = 0;
        //static Mutex _lock = new Mutex();
        // Mutex는 커널에서 지원해주는 것이기에 속도가 굉장히 느림.

        static void Thread_1()
        {
            for (int i = 0; i < 100000; i++)
            {
                _locked.Acquire();
                //_lock.WaitOne();
                _num++;
                _locked.Release();
               // _lock.ReleaseMutex();
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