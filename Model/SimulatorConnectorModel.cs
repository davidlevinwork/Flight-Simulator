using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SimolatorDesktopApp_1.Model
{
    public class SimulatorConnectorModel : ISimulatorConnector
    {
        TcpClient aClient;
        NetworkStream stream;
        private bool isConnected = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
                INotifyPropertyChanged("IsConnected");
            }
        }

        public void INotifyPropertyChanged(string propName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void Connect(string ip, int port)
        {
            try {
                XDocument doc2 = XDocument.Load("C:/Program Files/FlightGear 2020.3.6/data/Protocol/playback_small.xml");
                string xmlFile = doc2.ToString();
                string[] words = xmlFile.Split(' ');
                Dictionary<int, string> featuresMap = new Dictionary<int, string>();
                int location = 0;
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Contains("<output>"))
                    {
                        while (!words[i].Contains("</output>"))
                        {
                            if (words[i].StartsWith("<name>"))
                            {
                                string sub = words[i].Split('>')[1];
                                sub = sub.Split('<')[0];
                                featuresMap.Add(location++, sub);
                            }
                            i++;
                        }
                        break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("need to upload xml file");
            }
            aClient = new TcpClient(ip, port);
            stream = aClient.GetStream();
            IsConnected = true; // set property connect
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                stream.Close();
                aClient.Close();
                IsConnected = false;
            }
        }

        public string Read()
        {
            return "";
        }

        public void Write(string command)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(command + "\n");

            try
            {
                NetworkStream stream = this.aClient.GetStream();
                stream.Flush();
                stream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                Console.WriteLine("Failed in write command!");
            }
        }
    }
}
