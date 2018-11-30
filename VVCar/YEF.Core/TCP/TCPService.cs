using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YEF.Core.TCP
{
    /// <summary>
    /// TCP服务
    /// </summary>
    public class TCPService
    {
        static SocketListener listener;

        public static void Run()
        {
            try
            {
                ////实例化Timer类，设置间隔时间为60000毫秒；
                //System.Timers.Timer t = new System.Timers.Timer(60000);
                //t.Elapsed += new System.Timers.ElapsedEventHandler(CheckListen);
                ////到达时间的时候执行事件； 
                //t.AutoReset = true;
                //t.Start();

                listener = new SocketListener();
                listener.ReceiveTextEvent += new SocketListener.ReceiveTextHandler(ShowText);
                listener.StartListen();
            }
            catch (Exception e)
            {
                AppContext.Logger.Error("TCP服务运行异常：" + e.Message);
            }
        }

        private static void ShowText(string text)
        {
            AppContext.Logger.Info(text);
        }

        private static void CheckListen(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (listener != null && listener.Connection != null)
            {
                AppContext.Logger.Info($"TCP连接数：{listener.Connection.Count.ToString()}");
            }
        }
    }

    public class Connection
    {
        Socket _connection;

        public Connection(Socket socket)
        {
            _connection = socket;
        }

        public void WaitForSendData(object connection)
        {
            try
            {
                while (true)
                {
                    byte[] bytes = new byte[1024];
                    string data = "";

                    //等待接收消息
                    int bytesRec = this._connection.Receive(bytes);

                    if (bytesRec == 0)
                    {
                        ReceiveText("客户端[" + _connection.RemoteEndPoint.ToString() + "]连接关闭...");
                        break;
                    }

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    ReceiveText("TCP服务端收到消息：" + data);

                    string sendStr = "服务端已收到信息！";
                    byte[] bs = Encoding.UTF8.GetBytes(sendStr);
                    _connection.Send(bs, bs.Length, 0);
                }
            }
            catch (Exception)
            {
                ReceiveText("客户端[" + _connection.RemoteEndPoint.ToString() + "]连接已断开...");
                Hashtable hConnection = connection as Hashtable;
                if (hConnection.Contains(_connection.RemoteEndPoint.ToString()))
                {
                    hConnection.Remove(_connection.RemoteEndPoint.ToString());
                }
            }
        }

        public delegate void ReceiveTextHandler(string text);
        public event ReceiveTextHandler ReceiveTextEvent;

        private void ReceiveText(string text)
        {
            ReceiveTextEvent?.Invoke(text);
        }
    }

    public class SocketListener
    {
        public Hashtable Connection = new Hashtable();

        private Socket startSocket;

        public void StartListen()
        {
            Agine:
            try
            {
                //端口号、IP地址
                //int port = 8889;
                //string host = "127.0.0.1";
                //IPAddress ip = IPAddress.Parse(host);
                //IPEndPoint ipe = new IPEndPoint(ip, port);
                string ip = string.Empty;
                System.Net.IPHostEntry IpEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                for (int i = 0; i != IpEntry.AddressList.Length; i++)
                {
                    if (!IpEntry.AddressList[i].IsIPv6LinkLocal)
                    {
                        ip = IpEntry.AddressList[i].ToString();
                    }
                }
                IPEndPoint ipend = new IPEndPoint(IPAddress.Parse(ip), 6000);
                //创建一个Socket类
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Bind(ipend);//绑定6000端口
                s.Listen(0);//开始监听

                ReceiveText("启动Socket监听...");

                startSocket = s;

                Thread acceptConnectThread = new Thread(AcceptConnect);
                acceptConnectThread.Start();

            }
            catch (ArgumentNullException ex)
            {
                ReceiveText("ArgumentNullException:" + ex);
            }
            catch (SocketException ex)
            {
                ReceiveText("SocketException:" + ex);
            }

            goto Agine;
        }

        public delegate void ReceiveTextHandler(string text);
        public event ReceiveTextHandler ReceiveTextEvent;

        private void ReceiveText(string text)
        {
            ReceiveTextEvent?.Invoke(text);
        }

        private void AcceptConnect()
        {
            while (true)
            {
                try
                {
                    Socket connectionSocket = startSocket.Accept();//为新建连接创建新的Socket

                    ReceiveText("客户端[" + connectionSocket.RemoteEndPoint.ToString() + "]连接已建立...");

                    Connection gpsCn = new Connection(connectionSocket);
                    gpsCn.ReceiveTextEvent += new Connection.ReceiveTextHandler(ReceiveText);

                    Connection.Add(connectionSocket.RemoteEndPoint.ToString(), gpsCn);

                    //在新线程中启动新的socket连接，每个socket等待，并保持连接
                    Thread thread = new Thread(gpsCn.WaitForSendData);
                    thread.Name = connectionSocket.RemoteEndPoint.ToString();
                    thread.Start(Connection);
                }
                catch (Exception e)
                {
                    startSocket.Close();
                    AppContext.Logger.Error(e.Message);
                    break;
                }
            }
        }

    }
}
