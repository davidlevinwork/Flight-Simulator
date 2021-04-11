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
    using SimolatorDesktopApp_1.ViewModel;
    using System;
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public JoystickDashBoardModel _joystickDashBoardModel { get; private set; }
        public GraphsModel _graphModel { get; private set; }
        public VMGraphs _vmGraphs { get; private set; }
        public FilesUpload _filesUpload { get; private set; }
        public SimulatorConnectorModel _simultorConnectorModel { get; private set; }
        public DashBoardModel _dashBoardModel { get; private set; }
        public JoystickModel _joystickModel { get; private set; }
        public PopOutModel _popOutModel { get; private set; }
        public CircleDLL _circleDLL { get; private set; }
        public AlgoritemDetectModel _algoritemDetectModel { get; private set; }
        public SliderModel _sliderModel { get; private set; }

        public LineDll _lineDLL { get; private set; }




        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _circleDLL = new CircleDLL();
            _lineDLL = new LineDll();
            _graphModel = new GraphsModel();
            _vmGraphs = new VMGraphs(_graphModel);
            _filesUpload = new FilesUpload();
            _popOutModel = new PopOutModel();
            _simultorConnectorModel = new SimulatorConnectorModel();
            _dashBoardModel = new DashBoardModel();
            _joystickModel = new JoystickModel();
            _joystickDashBoardModel = new JoystickDashBoardModel();
            _sliderModel = new SliderModel(_simultorConnectorModel);
            _algoritemDetectModel = new AlgoritemDetectModel();
            // Create main application window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}