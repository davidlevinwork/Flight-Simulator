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
     * Class Model JoystickDashBoardModel - holds the joystick and sliders of parameters
     * we send to simulator flight.
     */
    public class JoystickDashBoardModel : INotifyPropertyChanged
    {
        // parameters
        private double _rudder = 0;
        private double _throttle = 0;
        private FilesUpload _filesUpload;
        public event PropertyChangedEventHandler PropertyChanged;

        /* Constructor JoystickDashBoardModel */
        public JoystickDashBoardModel()
        {
            _filesUpload = (Application.Current as App)._filesUpload;
        }

        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /*
         * Function that update the sliders parameters.
         */
        public void updateValues(string[] commands)
        {
            Rudder = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "rudder").Key], CultureInfo.InvariantCulture);
            Throttle = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "throttle").Key], CultureInfo.InvariantCulture);
        }

        /*
         * Property of Rudder
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
         * Property of Throttle
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
    }
}