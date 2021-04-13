using SimolatorDesktopApp_1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimolatorDesktopApp_1.ViewModel
{
    /*
     * Class ViewModel VMJoystick.
     */
    public class VMJoystick : INotifyPropertyChanged
    {
        private JoystickModel _joystickModel;
        public event PropertyChangedEventHandler PropertyChanged;

        /*
         * Constructor of VMJoystick
         */
        public VMJoystick(JoystickModel joystickModel)
        {
            _joystickModel = joystickModel;
            _joystickModel.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    INotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        private void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /*
         * Propperty - VM_Location parameter.
         */
        public Thickness VM_Location
        {
            get
            {
                return _joystickModel.Location;
            }
            set
            {
                _joystickModel.Location = value;
            }
        }

        /*
         * Propperty - VM_Aileron parameter.
         */
        public double VM_Aileron
        {
            get
            {
                return _joystickModel.Aileron;
            }
            set
            {
                _joystickModel.Aileron = value;
            }
        }

        /*
         * Propperty - VM_Elevator parameter.
         */
        public double VM_Elevator
        {
            get { return _joystickModel.Elevator; }
            set
            {
                _joystickModel.Elevator = value;
            }
        }

        /*
         * Propperty - VM_Rudder parameter.
         */
        public double VM_Rudder
        {
            get { return _joystickModel.Rudder; }
            set
            {
                _joystickModel.Rudder = value;
            }
        }

        /*
         * Propperty - VM_Throttle parameter.
         */
        public double VM_Throttle
        {
            get { return _joystickModel.Throttle; }
            set
            {
                _joystickModel.Throttle = value;
            }
        }
    }
}