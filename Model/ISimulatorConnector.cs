using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimolatorDesktopApp_1.Model
{
    /*
     * Interface ISimulatorConnector - holds functions that we need for communicate with simulator
     * flight app.
     */
    public interface ISimulatorConnector : INotifyPropertyChanged
    {
        void Connect(string ip, int port);
        void Write(string command);
        string Read();
        void Disconnect();
    }
}
