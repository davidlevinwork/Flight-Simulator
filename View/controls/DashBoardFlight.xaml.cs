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
    /*
     * Class View DashBoardFlight.
     */
    public partial class DashBoardFlight : UserControl
    {
        private VMDashBoard _vmDashBoard;
        public DashBoardFlight()
        {
            InitializeComponent();
            _vmDashBoard = new VMDashBoard((Application.Current as App)._dashBoardModel);
            DataContext = _vmDashBoard;
        }
    }
}
