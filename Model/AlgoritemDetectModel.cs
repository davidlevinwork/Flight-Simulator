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
    public class AlgoritemDetectModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<string> _toViewListFeatures = new ObservableCollection<string>();
        private ObservableCollection<string> _anomaliesViewList = new ObservableCollection<string>();
        private string _algorithmSelect;

        public AlgoritemDetectModel()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string targetDirectory = projectDirectory + '\\' + "Plugins";
            string[] dllEntries = Directory.GetFiles(targetDirectory);
            foreach (string dllName in dllEntries)
            {
                string fileName = Path.GetFileName(dllName);
                _toViewListFeatures.Add(fileName);
            }
            AddToMyList = _toViewListFeatures;
            (Application.Current as App)._lineDLL.setDllPath(targetDirectory + '\\' + _toViewListFeatures[1]);
        }

        public ObservableCollection<string> AddToMyList
        {
            get
            {
                return _toViewListFeatures;
            }
            set
            {
                _toViewListFeatures = value;
                INotifyPropertyChanged("AddToMyList");
            }
        }

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

        public void SelectedAlgorithm(string selectedItem)
        {
            AddAnomaliesToMyList = new ObservableCollection<string>();
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string targetDirectory = projectDirectory + '\\' + "Plugins" + '\\' + selectedItem;
            (Application.Current as App)._lineDLL.setDllPath(targetDirectory);
/*            _algorithmSelect = selectedItem;
            if (selectedItem.Equals("shared_DLL.dll"))
            {
                (Application.Current as App)._graphModel.SetDllType((Application.Current as App)._lineDLL);
            } 
            else if (selectedItem.Equals("DllCircle.dll"))
            {
                (Application.Current as App)._graphModel.SetDllType((Application.Current as App)._circleDLL);
            }*/
        }

    }
}
