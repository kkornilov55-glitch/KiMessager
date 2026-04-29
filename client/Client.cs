using System.Net.Sockets;
using System.Text;

namespace client
{
    internal class Client
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 6767;

            try
            {
                TcpClient client = new TcpClient();
                Console.WriteLine("Попытка подключения: {0}:{1}", ip, port);
                client.Connect(ip, port);
                Console.WriteLine("Подключено!");

                while (true)
                {
                    Console.Write("\nВведите сообщение: ");
                    string message = Console.ReadLine();
                    if (string.IsNullOrEmpty(message))
                    {
                        Console.Write("Разрыв соединения...");
                        break;
                    }

                    byte[] buff = new byte[256];
                    buff = Encoding.UTF8.GetBytes(message);

                    Console.WriteLine("Отправка сообщения...");
                    NetworkStream stream = client.GetStream();
                    stream.Write(buff, 0, buff.Length);
                    Console.WriteLine("Отправлено!");
                }  
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка подключения: " + ex.Message);
            }
        }
    }
}
