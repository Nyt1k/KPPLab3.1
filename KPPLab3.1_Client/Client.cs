using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace KPPLab3._1_Client
{
    public class Client
    {
      
        public static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    TcpClient client = new TcpClient("127.0.0.1", 8080);

                    
                    Console.Write("Enter line: ");
                    var message = Console.ReadLine();

                    int byteCount = Encoding.ASCII.GetByteCount(message + 1);
                    byte[] data = Encoding.ASCII.GetBytes(message);

                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);

                    StreamReader reader = new StreamReader(stream);
                    string answer = reader.ReadLine();
                    Console.WriteLine(answer);

                    stream.Close();
                    client.Close();
                    Console.ReadLine();
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


