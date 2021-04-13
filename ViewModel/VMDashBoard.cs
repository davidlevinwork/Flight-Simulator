using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SimolatorDesktopApp_1.Model;

namespace SimolatorDesktopApp_1.ViewModel
{
    /*
     * Class ViewModel VMDashBoard
     */
    public class VMDashBoard : INotifyPropertyChanged
    {
        private DashBoardModel _dashBoardModel;
        public event PropertyChangedEventHandler PropertyChanged;

        /*
         * Constructor of VMDashBoard.
         */
        public VMDashBoard(DashBoardModel dashBoardModel)
        {
            _dashBoardModel = dashBoardModel;
            _dashBoardModel.PropertyChanged +=
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
         * Property - VM_Altimeter.
         */
        public double VM_Altimeter
        {
            get
            {
                return _dashBoardModel.Altimeter;
            }
            set
            {
                _dashBoardModel.Altimeter = value;
            }
        }

        /*
         * Property - VM_AirSpeed.
         */
        public double VM_AirSpeed
        {
            get { return _dashBoardModel.AirSpeed; }
            set
            {
                _dashBoardModel.AirSpeed = value;
            }
        }

        /*
         * Property - VM_Roll.
         */
        public double VM_Roll
        {
            get { return _dashBoardModel.Roll; }
            set
            {
                _dashBoardModel.Roll = value;
            }
        }

        /*
         * Property - VM_Pitch.
         */
        public double VM_Pitch
        {
            get { return _dashBoardModel.Pitch; }
            set
            {
                _dashBoardModel.Pitch = value;
            }
        }

        /*
         * Property - VM_Yaw.
         */
        public double VM_Yaw
        {
            get { return _dashBoardModel.Yaw; }
            set
            {
                _dashBoardModel.Yaw = value;
            }
        }

        /*
         * Property - VM_Heading.
         */
        public double VM_Heading
        {
            get { return _dashBoardModel.Heading; }
            set
            {
                _dashBoardModel.Heading = value;
            }
        }
    }

}
