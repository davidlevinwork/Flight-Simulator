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
    /*
     * Class Model JoystickModel - the joystick movement.
     */
    public class JoystickModel : INotifyPropertyChanged
    {
        private double _aileron = 0;
        private double _elevator = 0;
        private double _rudder = 0;
        private double _throttle = 0;
        private Thickness _location;
        private SimulatorConnectorModel _simulatorConnectorModel;
        private FilesUpload _filesUpload;

        public event PropertyChangedEventHandler PropertyChanged;

        /*
         * Constructor.
         */
        public JoystickModel()
        {
            _simulatorConnectorModel = (Application.Current as App)._simultorConnectorModel;
            _filesUpload = (Application.Current as App)._filesUpload;
            _location = new Thickness(0, 0, 0, 0);
        }

        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /*
         * updates the values (that the joystick moves by them) to be the matching values to the current line.
         */
        public void updateValues(string[] commands)
        {
            Aileron = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "aileron").Key], CultureInfo.InvariantCulture);
            Elevator = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "elevator").Key], CultureInfo.InvariantCulture);
            Rudder = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "rudder").Key], CultureInfo.InvariantCulture);
            Throttle = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "throttle").Key], CultureInfo.InvariantCulture);
            Location = new Thickness(_aileron * 250, 0, 0, _elevator * 250);

        }

        /*
         * Property Aileron
         */
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

        /*
         * Property Elevator
         */
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

        /*
         * Property Rudder
         */
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

        /*
         * Property Throttle
         */
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

        /*
         * Property Location
         */
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