using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimolatorDesktopApp_1.Model
{
    /*
    * Class MODEL AlgoritemDetectModel - load the available dll's Algoritems in Plugin libary,
    * which the user want to detect with them, and set the Algoritem.
    */
    public class AlgoritemDetectModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<string> _toViewListAlgorithms = new ObservableCollection<string>();
        private ObservableCollection<string> _anomaliesViewList = new ObservableCollection<string>();
        private DllAlgorithms _dll = (Application.Current as App)._algorithmDll;

        /*
         * Constructor that add to _toViewListAlgorithms the Algorithms in the Plugin libary.
         */
        public AlgoritemDetectModel()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string targetDirectory = projectDirectory + '\\' + "Plugins";
            string[] dllEntries = Directory.GetFiles(targetDirectory);
            foreach (string dllName in dllEntries)
            {
                string fileName = Path.GetFileName(dllName);
                _toViewListAlgorithms.Add(fileName);
            }
            AddToMyList = _toViewListAlgorithms;
        }

        /*
         * Property AddToMyList
         */
        public ObservableCollection<string> AddToMyList
        {
            get
            {
                return _toViewListAlgorithms;
            }
            set
            {
                _toViewListAlgorithms = value;
                INotifyPropertyChanged("AddToMyList");
            }
        }

        /*
        * Property AddAnomaliesToMyList - anomalies the algoritem detected
        */
        public ObservableCollection<string> AddAnomaliesToMyList
        {
            get
            {
                return _anomaliesViewList;
            }
            set
            {
                _anomaliesViewList = value;
                INotifyPropertyChanged("AddAnomaliesToMyList");
            }
        }

        /*
        * When user select on anomaly, we move to the current time of the anomaly
        */
        public void selectedAnomaly(string anomaly)
        {
            int x = anomaly.LastIndexOf(" ");
            string sub = anomaly.Substring(x + 1);
            (Application.Current as App)._sliderModel.IndexLine = Int32.Parse(sub);
        }

        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /*
        * When user select the algorithm he want to load we set the DLL path and load it generic,
        * and detect.
        */
        public void SelectedAlgorithm(string selectedItem)
        {
            AddAnomaliesToMyList = new ObservableCollection<string>();
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string targetDirectory = projectDirectory + '\\' + "Plugins" + '\\' + selectedItem;
            _dll.setDllPath(targetDirectory);
            _dll.playDetect();
        }

    }
}
