using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimolatorDesktopApp_1
{
    using SimolatorDesktopApp_1.Model;
    using System;
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public JoystickDashBoardModel _joystickDashBoardModel { get; private set; }
        public FilesUpload _filesUpload { get; private set; }
        public SimulatorConnectorModel _simultorConnectorModel { get; private set; }
        public DashBoardModel _dashBoardModel { get; private set; }
        public JoystickModel _joystickModel { get; private set; }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _filesUpload = new FilesUpload();
            _simultorConnectorModel = new SimulatorConnectorModel();
            _dashBoardModel = new DashBoardModel();
            _joystickModel = new JoystickModel();
            _joystickDashBoardModel = new JoystickDashBoardModel();

            // Create main application window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}