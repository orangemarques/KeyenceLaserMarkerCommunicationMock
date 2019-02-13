using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketServerLaserMock
{
    class Program
    {
        private static NetworkStream socketStream;
        private static TcpListener servidor = new TcpListener(50002);
        private static BinaryWriter escreve;
        private static BinaryReader ler;
        private static Thread threadSocket;

        static void Main(string[] args)
        {
            threadSocket = new Thread(new ThreadStart(RunSocketServer));
            threadSocket.Start();
        }

        public static void RunSocketServer()
        {
            servidor.Start();

            while(true)
            {
                Socket conexao = servidor.AcceptSocket();
                socketStream = new NetworkStream(conexao);
                escreve = new BinaryWriter(socketStream);
                ler = new BinaryReader(socketStream);

                StringBuilder stringBuilder = new StringBuilder();

                while (true)
                {
                    try
                    {
                        byte[] message = ler.ReadBytes(1);
                        Console.Write($"{System.Text.Encoding.Default.GetString(message)}");
                        stringBuilder.Append(System.Text.Encoding.Default.GetString(message));

                        if (BitConverter.ToString(message) == "0A")
                        {
                            string msg = stringBuilder.ToString();
                            stringBuilder.Clear();
                            Console.Write($"msg = {msg}");

                            if(msg.StartsWith("WX,ProgramNo="))
                            {
                                System.Threading.Thread.Sleep(500);
                                byte[] bytes = Encoding.ASCII.GetBytes("WX,OK\r");
                                escreve.Write(bytes);
                            }
                            else if(msg.StartsWith("WX,PRG="))
                            {
                                System.Threading.Thread.Sleep(500);
                                byte[] bytes = Encoding.ASCII.GetBytes("WX,OK\r");
                                escreve.Write(bytes);
                            }
                            else if (msg.StartsWith("WX,StartMarking"))
                            {
                                System.Threading.Thread.Sleep(3000);
                                byte[] bytes = Encoding.ASCII.GetBytes("WX,OK\r");
                                escreve.Write(bytes);
                            }
                            else if (msg.StartsWith("WX,ErrorClear"))
                            {
                                System.Threading.Thread.Sleep(1000);
                                byte[] bytes = Encoding.ASCII.GetBytes("WX,OK\r");
                                escreve.Write(bytes);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ex.Message = {ex.Message}");
                        //break;
                    }
                    //System.Threading.Thread.Sleep(100);
                }
            }
        }
    }
}
