using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        //TLS
        static object _lock = new object();
        static ThreadLocal<string> ThreadName = new ThreadLocal<string>();

        //static String ThreadName으로 선언할 경우, ThreadName.Value는 모두 같은 값이 나옴.
        //ThreadLocal<>: Thread마다 고유한 공간을 생성.
        //한 뭉텅이씩 일을 가져와서 하는 것. 이 경우 Lock을 하지 않아도 부담 없음.
        //전역에 접근하는 횟수를 줄이는 등의 이점이 있어 부하를 줄이는 데 도움이 됨.

        static void WhoAmI()
        {
            ThreadName.Value = ($"{Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(1000); //1초 대기

            Console.WriteLine($"Thread num.{ThreadName.Value}" +
                $"the order is {Thread.CurrentThread.Priority}");
        }

        public static void Main(string[] args)
        {
            lock (_lock)
            {
                Thread.CurrentThread.Priority = ThreadPriority.Lowest;
                ThreadPool.QueueUserWorkItem(obj => { });
                Console.WriteLine($"Thread num.{Thread.CurrentThread.ManagedThreadId} " +
                    $"the order is {Thread.CurrentThread.Priority}");
            }

            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Parallel.Invoke(WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI);
        }
    }
}