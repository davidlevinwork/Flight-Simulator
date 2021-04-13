﻿using SimolatorDesktopApp_1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimolatorDesktopApp_1.View.controls
{
    /*
     * Class View Joystick.
     */
    public partial class Joystick : UserControl
    {
        private VMJoystick _vmJoysick;

        /*
         * Constructor of Joystick.
         */
        public Joystick()
        {
            InitializeComponent();
            _vmJoysick = new VMJoystick((Application.Current as App)._joystickModel);
            DataContext = _vmJoysick;
        }
    }
}