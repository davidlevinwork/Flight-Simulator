using SimolatorDesktopApp_1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimolatorDesktopApp_1.ViewModel
{
    /*
     * Class ViewModel VMJoystickDashBoard.
     */
    public class VMJoystickDashBoard : INotifyPropertyChanged
    {
        private JoystickDashBoardModel _joystickDashBoardModel;
        public event PropertyChangedEventHandler PropertyChanged;

        /*
         * Constructor of VMJoystickDashBoard.
         */
        public VMJoystickDashBoard(JoystickDashBoardModel joystickDashBoardModel)
        {
            _joystickDashBoardModel = joystickDashBoardModel;
            _joystickDashBoardModel.PropertyChanged +=
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
         * Property - VM_Rudder parameter.
         */
        public double VM_Rudder
        {
            get { return _joystickDashBoardModel.Rudder; }
            set
            {
                _joystickDashBoardModel.Rudder = value;
            }
        }

        /*
         * Property - VM_Throttle parameter.
         */
        public double VM_Throttle
        {
            get { return _joystickDashBoardModel.Throttle; }
            set
            {
                _joystickDashBoardModel.Throttle = value;
            }
        }
    }
}