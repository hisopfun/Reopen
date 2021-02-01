using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Net.Configuration;
using System.Reflection;
using System.IO;

namespace 覆盤
{
    class SOCKET
    {
        public Thread t1;
        public Socket Sclient;
        public string firstMsg = "";
        public string date = "";
        //DateTime connect_time = new DateTime();
        public string datas = "";
        public string localIp = "";
        public string loaclPrivateIP = "";
        public string serverIP = "";
        public int serverPort = 0;
        public Queue<string> ticks = new Queue<string>();
        public object Lock = new object();
        public bool realTime = false;
        public TickEncoder TE;
        private string GetPublicIpAddress()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://ifconfig.me");

            request.UserAgent = "curl"; // this will tell the server to return the information as if the request was made by the linux "curl" command

            string publicIPAddress;

            request.Method = "GET";
            using (WebResponse response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    publicIPAddress = reader.ReadToEnd();
                }
            }

            return publicIPAddress.Replace("\n", "");
        }
        static string GetIPAddress()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }
        public SOCKET(string nDate, string sIP, int sPort, bool sRealTime) {
            localIp = GetPublicIpAddress();
            loaclPrivateIP = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address).ToString();
            date = nDate;

            serverIP = sIP;
            serverPort = sPort;
            realTime = sRealTime;
        }
        public void _Load()
        {
            t1 = new Thread(StartClient);
            t1.Start();
            //StartClient();
        }

        // State object for receiving data from remote device.  
        public class StateObject
        {
            // Client socket.  
            public Socket workSocket = null;
            // Size of receive buffer.  
            public const int BufferSize = 1048576;
            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];
            // Received data string.  
            public StringBuilder sb = new StringBuilder();
        }


        // The port number for the remote device.  
        private const int port = 12002;

        // ManualResetEvent instances signal completion.  
        private ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        // The response from the remote device.  
        private String response = String.Empty;

        public string IP() {
            string externalip = new WebClient().DownloadString("http://icanhazip.com");


            return externalip.Replace("\n", "");// + "," + SvrIP;
            /* Environment.MachineName + ',' + System.Security.Principal.WindowsIdentity.GetCurrent().Name
                             + ',' + SvrIP + ',' + externalip + ",64bit:" + Environment.Is64BitOperatingSystem;
            */
        }

        private void StartClient()
        {
            // Connect to a remote device.  
            try
            {
                connectDone = new ManualResetEvent(false);
                sendDone = new ManualResetEvent(false);
                receiveDone = new ManualResetEvent(false);
                // Establish the remote endpoint for the socket.  
                // The name of the
                // remote device is "host.contoso.com".  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(serverIP);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, serverPort);

                // Create a TCP/IP socket.  
                Sclient = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                Sclient.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), Sclient);
                connectDone.WaitOne();

                // Send test data to the remote device.  
                Send(Sclient, "client," + date + "," + localIp + "," + Environment.MachineName + ',' + System.Security.Principal.WindowsIdentity.GetCurrent().Name
                            + ',' + loaclPrivateIP + ',' + localIp + ",64bit:" + Environment.Is64BitOperatingSystem);
                sendDone.WaitOne();

                // Receive the response from the remote device.  
                Receive(Sclient);
                receiveDone.WaitOne();

                // Write the response to the console.  
                Console.WriteLine("Response received : {0}", response);

                // Release the socket.  
                Sclient.Shutdown(SocketShutdown.Both);
                Sclient.Close();

            }
            catch (Exception ex)
            {
                MethodBase m = MethodBase.GetCurrentMethod();
                t1.Abort();
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                //api.brk@yuanta.com
                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();

            }
            catch (Exception ex)
            {
                MethodBase m = MethodBase.GetCurrentMethod();
                if (ex.Message.Contains("無法連線，因為目標電腦拒絕連線")) {
                    datas = "無法連線，因為目標電腦拒絕連線";
                    MessageBox.Show("無法連線，因為目標電腦拒絕連線");
                }
                t1.Abort();
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                MethodBase m = MethodBase.GetCurrentMethod();
            }
        }
        private void ReciveReOpenData(string msg){
            if (msg.Contains("NO DATA"))
            {
                datas = "NO DATA";
                Sclient.Shutdown(SocketShutdown.Both);
                Sclient.Close();
                t1.Abort();
            }
            if (msg.Contains("DONE"))
            {
                Sclient.Shutdown(SocketShutdown.Both);
                Sclient.Close();
                t1.Abort();
            }
            datas += msg;
            if (datas.Contains('\n'))
            {
                string[] words = datas.Split('\n');
                int i;
                for (i = 0; i < words.Length; i++)
                {
                    if (i < words.Length - 1)
                        lock (Lock)
                            ticks.Enqueue(words[i]);
                    else
                        datas = words[i];
                }
            }
        }

        private void FirstMsg(string msg) {
            if (firstMsg == "")
            {
                if (msg.Contains("\n"))
                {
                    firstMsg = msg.Split('\n')[0];
                    datas += msg.Split('\n')[1];
                }
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);
                string msg = "";
                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    msg = Encoding.UTF8.GetString(state.buffer, 0, bytesRead);

                    if (!realTime)
                    {
                        //First Msg
                        FirstMsg(msg);

                        //history ticks data
                        ReciveReOpenData(msg);
                    }
                    else {
                        //ticks.Enqueue(msg);
                        TE.Encode(msg);
                    }

                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                            new AsyncCallback(ReceiveCallback), state);
                }
            }
            catch (Exception ex)
            {
                MethodBase m = MethodBase.GetCurrentMethod();
            }
        }

        private void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception ex)
            {
                MethodBase m = MethodBase.GetCurrentMethod();
            }
        }
    }
}
