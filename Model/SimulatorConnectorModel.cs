using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimolatorDesktopApp_1.Model
{
    public class SimulatorConnectorModel : ISimulatorConnector
    {
        TcpClient aClient;
        NetworkStream stream;
        //private static readonly Mutex mut = new Mutex();
        // public int conectionAttempts;
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
            aClient = new TcpClient(ip, port);
            stream = aClient.GetStream();
            IsConnected = true; // set property connect
           // SimulatorModel simulatorModel = new SimulatorModel(this);
           // simulatorModel.startSimulator();
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
            return "need to finish Read function";
        }

        public void Write(string command)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(command + "\n");

            try
            {
                NetworkStream stream = this.aClient.GetStream();
                stream.Flush();
                stream.Write(buffer, 0, buffer.Length);
                // Console.WriteLine("enter write scope");
            }
            catch
            {

            }
        }

        public string WriteCommand(string word)
        {
            return "need to finish WriteCommand function";
        }
    }
}
