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
    public class SliderModel : INotifyPropertyChanged
    {
        public SimulatorConnectorModel _simulatorConnectorModel;
        private int _minLine = 0;
        private int _maxLine = 1;
        private int _indexLine = 0;
        private string[] _linesArray;
        private bool stopFlag = false;
        private bool puaseFlag = false;
        private double _timer = 0;
        private TimeSpan time;
        private double _speed = 1.0;
        private Thread t;
        private ManualResetEvent _manualResetEvent = new ManualResetEvent(true);
        private DashBoardModel _dashBoardModel;
        private JoystickModel _joystickModel;
        private JoystickDashBoardModel _joystickDashBoardModel;
        private FilesUpload _filesUpload;
        public event PropertyChangedEventHandler PropertyChanged;

        /* Constructors: */
        public SliderModel(SimulatorConnectorModel simulatorConnectorModel)
        {
            _filesUpload = (Application.Current as App)._filesUpload;
            _simulatorConnectorModel = simulatorConnectorModel;
            _dashBoardModel = (Application.Current as App)._dashBoardModel;
            _joystickModel = (Application.Current as App)._joystickModel;
            _joystickDashBoardModel = (Application.Current as App)._joystickDashBoardModel;
            time = TimeSpan.FromSeconds(_timer);
        }
        public int IndexLine
        {
            get
            {
                return _indexLine;
            }
            set
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
                TimerString = _indexLine.ToString();
                INotifyPropertyChanged("IndexLine");

            }
        }

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

        public string TimerString
        {
            get
            {
                return time.ToString(@"hh\:mm\:ss");
            }
            set
            {
                double d_time = double.Parse(value, CultureInfo.InvariantCulture);
                _timer = (d_time / 10);
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

        public void stop()
        {
            stopFlag = true;
            puaseFlag = false;
            _manualResetEvent.Set();
        }

        public void pause()
        {
            _manualResetEvent.Reset();
            puaseFlag = true;
        }

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
            t = new Thread(delegate ()
                {
                    stopFlag = false;
                    while (_simulatorConnectorModel.IsConnected && (IndexLine < _maxLine) && !stopFlag)
                    {
                        //Console.WriteLine(_linesArray[IndexLine]);
                        _simulatorConnectorModel.Write(_linesArray[IndexLine]);
                        int speedRate =(int)(100 / _speed);
                        //Console.WriteLine(_speed);
                        Thread.Sleep(speedRate);
                        IndexLine++;
                        _manualResetEvent.WaitOne(Timeout.Infinite);
                    }
                    IndexLine = 0;
                });
            t.Start();
        }

        public void uploadCsvFile(string pathCsv)
        {
            try
            {
                _linesArray = _filesUpload.csvUpload(pathCsv);
                MaxLine = _linesArray.Length; // set number of lines
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in uploadCsvFile Modal");
            }
        }

        public void updateIndexSlider(int index)
        {
            IndexLine = index;
        }
    }
}
