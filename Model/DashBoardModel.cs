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
     * Class Model DashBoardModel - takes the paramaters we send to flight simulator and update them
     * in order they will display on view.
     */
    public class DashBoardModel : INotifyPropertyChanged
    {
        // paramaters we send to simulator flight.
        private double _altimeter = 0;
        private double _airSpeed = 0;
        private double _roll = 0;
        private double _pitch = 0;
        private double _yaw = 0;
        private double _hedaing = 0;
        private FilesUpload _filesUpload;
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructor DashBoardModel
        public DashBoardModel()
        {
            _filesUpload = (Application.Current as App)._filesUpload;
        }

        /*
         * Function that update the propertyies parameters each line that we send.
         */
        public void updateValues(string[] commands)
        {
            Altimeter = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "altimeter_indicated-altitude-ft").Key], CultureInfo.InvariantCulture);
            AirSpeed = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "airspeed-kt").Key], CultureInfo.InvariantCulture);
            Roll = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "roll-deg").Key], CultureInfo.InvariantCulture);
            Pitch = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "pitch-deg").Key], CultureInfo.InvariantCulture);
            Yaw = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "side-slip-deg").Key], CultureInfo.InvariantCulture);
            Heading = double.Parse(commands[_filesUpload.FeaturesMap.FirstOrDefault(x => x.Value == "heading-deg").Key], CultureInfo.InvariantCulture);
        }

        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /*
         * Property of Altimeter parameter
         */
        public double Altimeter
        {
            get
            {
                return _altimeter;
            }
            set
            {
                _altimeter = value;
                INotifyPropertyChanged("Altimeter");
            }
        }

        /*
        * Property of AirSpeed parameter
        */
        public double AirSpeed
        {
            get
            {
                return _airSpeed;
            }
            set
            {
                _airSpeed = value;
                INotifyPropertyChanged("AirSpeed");
            }

        }

        /*
        * Property of Roll parameter
        */
        public double Roll
        {
            get
            {
                return _roll;
            }
            set
            {
                _roll = value;
                INotifyPropertyChanged("Roll");
            }

        }

        /*
        * Property of Pitch parameter
        */
        public double Pitch
        {
            get
            {
                return _pitch;
            }
            set
            {
                _pitch = value;
                INotifyPropertyChanged("Pitch");
            }
        }

        /*
        * Property of Yaw parameter
        */
        public double Yaw
        {
            get
            {
                return _yaw;
            }
            set
            {
                _yaw = value;
                INotifyPropertyChanged("Yaw");
            }
        }

        /*
        * Property of Heading parameter
        */
        public double Heading
        {
            get
            {
                return _hedaing;
            }
            set
            {
                _hedaing = value;
                INotifyPropertyChanged("Heading");
            }
        }
    }
}
