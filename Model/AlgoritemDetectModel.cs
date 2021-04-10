using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimolatorDesktopApp_1.Model
{
    public class AlgoritemDetectModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<string> _toViewListFeatures = new ObservableCollection<string>();

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

        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }
}
