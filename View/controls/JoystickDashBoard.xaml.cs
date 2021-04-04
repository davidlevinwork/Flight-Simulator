using SimolatorDesktopApp_1.ViewModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimolatorDesktopApp_1.View.controls
{

    public partial class JoystickDashBoard : UserControl
    {
        private VMJoystickDashBoard _vmJoystickDashBoard;

        public JoystickDashBoard()
        {
            InitializeComponent();

            _vmJoystickDashBoard = new VMJoystickDashBoard((Application.Current as App)._joystickDashBoardModel);
            DataContext = _vmJoystickDashBoard;
        }
    }
}