using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

// 

namespace ServerCore
{
    class Program
    {
        static void MainThread(object state)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("스레드 작동");
            }
        }

        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(5, 5);


            for (int i = 0; i < 5; i++)
            {
                Task t = new Task(() => { while (true) { } }, TaskCreationOptions.LongRunning);
                // 오래 걸리는 작업임을 명시하는 옵션. 별도의 스레드로 관리됨.
                // LongRunning 옵션을 제외할 경우, MainThread 함수는 작동하지 않는다.
                t.Start();
            }

            //for (int i = 0; i < 5; i++)
            //{
            //    ThreadPool.QueueUserWorkItem((obj) => { while (true) { } });
            //}

            ThreadPool.QueueUserWorkItem(MainThread);

            //Thread t = new Thread(MainThread);
            //t.IsBackground = true;
            //t.Start();
            //Console.WriteLine("스레드 조인 대기중");
            //t.Join();
            //Console.WriteLine("스레드 조인 완료");

            while (true) { }
        }
    }
}