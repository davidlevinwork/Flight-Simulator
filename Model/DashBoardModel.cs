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
    public class DashBoardModel : INotifyPropertyChanged 
    {
        private double _altimeter = 0;
        private double _airSpeed = 0;
        private double _roll = 0;
        private double _pitch = 0;
        private double _yaw = 0;
        private double _hedaing = 0;
        private SimulatorConnectorModel _simulatorConnectorModel;

        public event PropertyChangedEventHandler PropertyChanged;


        /* Methods: */
        /* Constructors: */
        public DashBoardModel( )
        {
            _simulatorConnectorModel = (Application.Current as App)._simultorConnectorModel;
        }
        public void updateValues(string[] commands)
        {
            Altimeter = double.Parse(commands[25], CultureInfo.InvariantCulture);
            AirSpeed = double.Parse(commands[24], CultureInfo.InvariantCulture);
            Roll = double.Parse(commands[17], CultureInfo.InvariantCulture);
            Pitch = double.Parse(commands[18], CultureInfo.InvariantCulture);
            Yaw = double.Parse(commands[20], CultureInfo.InvariantCulture);
            Heading = double.Parse(commands[19], CultureInfo.InvariantCulture);
        }
        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        /* Features: */
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
