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
        private FilesUpload _filesUpload;
        public event PropertyChangedEventHandler PropertyChanged;

        public DashBoardModel( )
        {
            _filesUpload = (Application.Current as App)._filesUpload;
        }
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
