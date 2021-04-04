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
    public class JoystickModel : INotifyPropertyChanged
    {
        private double _aileron = 0;
        private double _elevator = 0;
        private double _rudder = 0;
        private double _throttle = 0;
        private Thickness _location;
        private SimulatorConnectorModel _simulatorConnectorModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public JoystickModel()
        {
            _simulatorConnectorModel = (Application.Current as App)._simultorConnectorModel;
            _location = new Thickness(0, 0, 0, 0);
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
            Aileron = double.Parse(commands[0], CultureInfo.InvariantCulture);
            Elevator = double.Parse(commands[1], CultureInfo.InvariantCulture);
            Rudder = double.Parse(commands[2], CultureInfo.InvariantCulture);
            Throttle = double.Parse(commands[6], CultureInfo.InvariantCulture);

            Location = new Thickness(_aileron * 200, 0, 0, _elevator * 200);

        }

        /* Features: */
        public double Aileron
        {
            get
            {
                return _aileron;
            }
            set
            {
                _aileron = value;
                INotifyPropertyChanged("Aileron");
            }
        }
        public double Elevator
        {
            get
            {
                return _elevator;
            }
            set
            {
                _elevator = value;
                INotifyPropertyChanged("Elevator");
            }

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

        public Thickness Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
                INotifyPropertyChanged("Location");
            }
        }
    }
}