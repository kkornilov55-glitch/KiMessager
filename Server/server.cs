using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    internal class server
    {
        static void Main(string[] args)
        {
            int port = 6767;
            TcpListener server = new TcpListener(IPAddress.Any, port);
            TcpClient client = new TcpClient();
            try
            {
                //1. Запуск сервера
                server.Start();
                Console.WriteLine("Сервер запущен... Ожидание подключения");

                while (true)
                {
                    //2. Ожидание подключения пользователя, программа на паузе
                    client = server.AcceptTcpClient();

                    //3. После подключения клиента выводим сообщение об этом
                    Console.WriteLine("\nКлиент подключен!");
                    Console.WriteLine("-------------------------------------");

                    //4. Открываем поток для общения с конкретным клиентом
                    NetworkStream stream = client.GetStream();

                    //5. Корзина для полученного сообщения в байтовом представлении
                    byte[] buffer = new byte[256];
                    while (true)
                    {
                        //Принятие сообщения со сбором его в "корзину"
                        int bufferCount = stream.Read(buffer, 0, buffer.Length);

                        if (bufferCount == 0)
                        {
                            Console.WriteLine("-------------------------------------");
                            Console.WriteLine("Клиент отключился!");
                            break;
                        }
                            
                        //6. Перевод сообщения из списка байтов в понятный нам текст 
                        string message = Encoding.UTF8.GetString(buffer, 0, bufferCount);
                        //Вывод полученного сообщения
                        Console.WriteLine($"Получено сообщение: {message}");
                    }
                }
            }
            catch (IOException ex) when (ex.InnerException is SocketException)
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Соединение разорвано!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
