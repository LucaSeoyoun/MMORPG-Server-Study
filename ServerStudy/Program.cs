using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class ServerCore
    {

        public static void Main(string[] args)
        {
            //DNS (Domain Name System)
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 0127);

            //문지기
            Socket listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


            try
            {
                //문지기 교육
                listenSocket.Bind(endPoint);

                //영업 시작
                //Listen(backlog); backlog: 최대 대기수.
                listenSocket.Listen(10);

                while (true)
                {
                    Console.WriteLine("*** LUCA SERVER IS RUNNING ***\n\n");
                    Console.WriteLine("Listening...");

                    //손님을 입장시킨다.
                    Socket clientSocket = listenSocket.Accept();
                    //손님이 오지 않을 경우 무한 대기...


                    //받는다.
                    byte[] receiveBuff = new byte[1024];
                    int receiveBytes = clientSocket.Receive(receiveBuff);
                    string receiveData = Encoding.UTF8.GetString(receiveBuff, 0, receiveBytes);
                    Console.WriteLine($"[From Client] {receiveData}");

                    //보낸다.
                    byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to MMORPG server !!");
                    clientSocket.Send(sendBuff);

                    //쫓아낸다.
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();

                }
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

        }
    }
}