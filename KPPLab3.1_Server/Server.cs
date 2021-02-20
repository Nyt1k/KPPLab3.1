using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace KPPLab3._1
{
    public class Server
    {
        public static string ChangeString(string data)
        {
            string ndata = "";

            int n = data.Length / 2;
            if (data.Length % 2 == 0)
            {

                ndata = data.Remove(n - 1, 2);

            }
            else
            {
                ndata = data.Remove(n, 1);
            }



            return ndata;
        }
        public static void Main(string[] args)
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Any, 8080);
                listener.Start();

                Console.WriteLine(DateTime.Now.ToShortTimeString() + " - Server isn now ready to work");
                Console.WriteLine(DateTime.Now.ToShortTimeString() + " - Waiting...");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    StreamReader reader = new StreamReader(client.GetStream());
                    StreamWriter writer = new StreamWriter(client.GetStream());

                    byte[] buffer = new byte[256];
                    stream.Read(buffer, 0, buffer.Length);
                    int recv = 0;
                    foreach(byte b in buffer)
                    {
                        if(b!=0)
                        {
                            recv++;
                        }
                    }
                    string str = Encoding.UTF8.GetString(buffer,0,recv);
                    
                    Console.WriteLine(DateTime.Now.ToShortTimeString() +  " - Received data: " + str);
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + " - Working...");

                    string nstr = ChangeString(str);
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + " - Result: " + nstr);
                    buffer = Encoding.UTF8.GetBytes(nstr);

                    writer.WriteLine(nstr);
                    writer.Flush();
                }

            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }

            System.Threading.Thread.Sleep(10000);
        }
    }
}
