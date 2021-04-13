using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SimolatorDesktopApp_1.Model
{
    /*
     * Class MODEL SliderModel - controls the movement of the flight simulator.
     */
    public class SliderModel : INotifyPropertyChanged
    {
        public SimulatorConnectorModel _simulatorConnectorModel;
        private int _maxLine = 1;
        private int _indexLine = 0;
        private string[] _linesArray;
        private bool stopFlag = false;
        private bool puaseFlag = false;
        private double _timer = 0;
        private TimeSpan time;
        private double _speed = 1.0;
        private bool _flagIscsvUpload = false;
        private Thread t;
        private ManualResetEvent _manualResetEvent = new ManualResetEvent(true);
        private DashBoardModel _dashBoardModel;
        private JoystickModel _joystickModel;
        private JoystickDashBoardModel _joystickDashBoardModel;
        private FilesUpload _filesUpload;
        private GraphsModel _graphsModel;
        public event PropertyChangedEventHandler PropertyChanged;

        /*
         * Constructor.
         */
        public SliderModel(SimulatorConnectorModel simulatorConnectorModel)
        {
            _filesUpload = (Application.Current as App)._filesUpload;
            _simulatorConnectorModel = simulatorConnectorModel;
            _dashBoardModel = (Application.Current as App)._dashBoardModel;
            _joystickModel = (Application.Current as App)._joystickModel;
            _joystickDashBoardModel = (Application.Current as App)._joystickDashBoardModel;
            _graphsModel = (Application.Current as App)._graphModel;
            time = TimeSpan.FromSeconds(_timer);
        }

        /*
         * Property IndexLine
         */
        public int IndexLine
        {
            get
            {
                return _indexLine;
            }
            set // from here we control all the flight data that represented on the screen to be according to the _indexLine.
            {
                if (value >= _maxLine)
                    _indexLine = _maxLine - 1;
                else if (value < 0)
                    _indexLine = 0;
                else
                    _indexLine = value;
                string[] commands = _linesArray[_indexLine].Split(',');
                _dashBoardModel.updateValues(commands);
                _joystickModel.updateValues(commands);
                _joystickDashBoardModel.updateValues(commands);
                _graphsModel.updateGraph(_filesUpload.GetAllValues, _indexLine);
                TimerString = _indexLine.ToString();
                INotifyPropertyChanged("IndexLine");

            }
        }

        /*
         * Property MaxLine - will be the number of lines in the csv file
         */
        public int MaxLine
        {
            get
            {
                return _maxLine;
            }
            set
            {
                _maxLine = value;
                INotifyPropertyChanged("MaxLine");
            }
        }

        /*
         * Property Speed
         */
        public double Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
                INotifyPropertyChanged("Speed");
            }
        }


        /*
         * Property TimerString
         */
        public string TimerString
        {
            get
            {
                return time.ToString(@"hh\:mm\:ss");
            }
            set
            {
                double d_time = double.Parse(value, CultureInfo.InvariantCulture);
                _timer = (d_time / 10); // number of line diving with 10
                time = TimeSpan.FromSeconds(_timer);
                INotifyPropertyChanged("TimerString");
            }
        }

        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /*
         * if the user pressed on stop button
         */
        public void stop()
        {
            stopFlag = true;
            puaseFlag = false;
            _manualResetEvent.Set();
        }

        /*
         * if the user pressed on pause button
         */
        public void pause()
        {
            _manualResetEvent.Reset();
            puaseFlag = true;
        }

        /*
         * if the user pressed on play button
         */
        public void play(string pathCsv)
        {
            if (pathCsv.Equals("")) // if path csv not recived  
                return;
            if (puaseFlag)
            {
                _manualResetEvent.Set();
                puaseFlag = false;
                return;
            }
            t = new Thread(delegate () // new thread for runnung the simulator.
                {
                    stopFlag = false;
                    while (_simulatorConnectorModel.IsConnected && (IndexLine < _maxLine) && !stopFlag)
                    {
                        _simulatorConnectorModel.Write(_linesArray[IndexLine]);
                        int speedRate =(int)(100 / _speed); // sleep time - according to the speed that the user chose.
                        Thread.Sleep(speedRate);
                        IndexLine++; // update the line (and by that - all the data on the screen will be updated).
                        _manualResetEvent.WaitOne(Timeout.Infinite);
                    }
                    IndexLine = 0;
                });
            t.Start();
        }

        /*
         * when the user upload his csv file.
         */
        public void uploadCsvFile(string pathCsv)
        {
            try
            { 
                _linesArray = _filesUpload.csvUpload(pathCsv);
                MaxLine = _linesArray.Length; // set number of lines
                _flagIscsvUpload = true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in uploadCsvFile Modal");
            }
        }

        /*
         * Function that returns if csv is upload
         */
         public bool get_flagIscsvUpload()
        {
            return _flagIscsvUpload;
        }

        /*
         * when the user moves the slider button or pressed on the left or right jump.
         */
        public void updateIndexSlider(int index)
        {
            IndexLine = index;
        }
    }
}
