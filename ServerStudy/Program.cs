using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        volatile static bool _stop = false;
        // volatile 옵션을 사용하면 컴파일러에서  해당 변수를 최적화하지 않게 됨

        static void ThreadMain()
        {
            Console.WriteLine("스레드 시작~~");

            while (_stop == false)
            {
                // 누군가가 stop 신호를 주기까지 대기
                // Debug모드에서와 달리, Release모드에서는 무한루프를 돈다. 어셈블리 참조. _stop 변수에 volatile 옵션 추가.
            }
            Console.WriteLine("스레드 종료.");
        }


        static void Main(string[] args)
        {
            Task t = new Task(ThreadMain);
            t.Start();

            Thread.Sleep(1000); // 1초 동안 슬립해  스레드가 충분히 실행될 시간 제공

            _stop = true;

            Console.WriteLine("stop 호출");
            Console.WriteLine("종료 대기중");

            t.Wait();

            Console.WriteLine("종료 성공");
        }
    }
}