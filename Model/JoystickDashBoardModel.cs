using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimolatorDesktopApp_1.Model
{
    public class JoystickDashBoardModel : INotifyPropertyChanged
    {
        private double _rudder = 0;
        private double _throttle = 0;
        private SimulatorConnectorModel _simulatorConnectorModel;
        private FilesUpload _filesUpload;
        public event PropertyChangedEventHandler PropertyChanged;

        public JoystickDashBoardModel()
        {
            _simulatorConnectorModel = (Application.Current as App)._simultorConnectorModel;
            _filesUpload = (Application.Current as App)._filesUpload;
        }

        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void updateValues(string[] commands)
        {
            Rudder = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "rudder").Key], CultureInfo.InvariantCulture);
            Throttle = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "throttle").Key], CultureInfo.InvariantCulture);
           // Rudder = double.Parse(commands[2], CultureInfo.InvariantCulture);
           // Throttle = double.Parse(commands[6], CultureInfo.InvariantCulture);
        }

        public double Rudder
        {
            get
            {
                return _rudder;
            }
            set
            {
                _rudder = value;
                INotifyPropertyChanged("Rudder");
            }

        }
        public double Throttle
        {
            get
            {
                return _throttle;
            }
            set
            {
                _throttle = value;
                INotifyPropertyChanged("Throttle");
            }
        }
    }
}