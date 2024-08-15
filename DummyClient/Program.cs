using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DummyClient
{
    class DummyClient
    {

        static void Main(string[] args)
        {
            //서버와 동일한 부분임.
            //DNS (Domain Name System)
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 0127);

            //휴대폰 설정
            Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


            try
            {
                //문지기한테 입장 문의
                socket.Connect(endPoint); //endPoint(상대방 주소)와 연락 가능한지 묻기
                Console.WriteLine($"Connected to {socket.RemoteEndPoint.ToString()}");

                //보낸다
                byte[] sendBuff = Encoding.UTF8.GetBytes("Hello world!");
                int sentBytes = socket.Send(sendBuff);

                //받는다
                byte[] receiveBuff = new byte[1024];
                int receiveBytes = socket.Receive(receiveBuff);
                string receiveData = Encoding.UTF8.GetString(receiveBuff, 0, receiveBytes);
                Console.WriteLine($"[From Server] {receiveData}");

                //나간다
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

        }
    }
}