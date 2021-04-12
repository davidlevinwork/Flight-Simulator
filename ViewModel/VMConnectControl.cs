using SimolatorDesktopApp_1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimolatorDesktopApp_1.ViewModel
{
    class VMConnectControl : INotifyPropertyChanged
    {
        SimulatorConnectorModel _simulatorConnectorModel;
        public event PropertyChangedEventHandler PropertyChanged;

        public VMConnectControl(SimulatorConnectorModel simulatorConnectorModel)
        {
            _simulatorConnectorModel = simulatorConnectorModel;
            _simulatorConnectorModel.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    INotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }


        public void VMConnect(string ip, int port)
        {
            _simulatorConnectorModel.Connect(ip, port);
        }

        public void VMDisconnect()
        {
            _simulatorConnectorModel.Disconnect();
        }

        public bool VM_IsConnected
        {
            get { return _simulatorConnectorModel.IsConnected; }
        }

        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
