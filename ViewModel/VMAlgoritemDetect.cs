using SimolatorDesktopApp_1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimolatorDesktopApp_1.ViewModel
{
    public class VMAlgoritemDetect : INotifyPropertyChanged
    {
        private AlgoritemDetectModel _algoritemDetectModel;
        //private ObservableCollection<string> _toViewListFeatures = new ObservableCollection<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public VMAlgoritemDetect(AlgoritemDetectModel algoritemDetectModel)
        {
            _algoritemDetectModel = algoritemDetectModel;
            _algoritemDetectModel.PropertyChanged +=
               delegate (Object sender, PropertyChangedEventArgs e)
               {
                   INotifyPropertyChanged("VM_" + e.PropertyName);
               };
        }

        public ObservableCollection<string> VM_AddToMyList
        {
            get { return _algoritemDetectModel.AddToMyList; }
            set
            {
                _algoritemDetectModel.AddToMyList = value;
                INotifyPropertyChanged("VM_AddToMyList");
            }
        }

        public ObservableCollection<string> VM_AddAnomaliesToMyList
        {
            get { return _algoritemDetectModel.AddAnomaliesToMyList; }
            set
            {
                _algoritemDetectModel.AddAnomaliesToMyList = value;
                INotifyPropertyChanged("VM_AddAnomaliesToMyList");
            }
        }

        public void vmSelectedAnomaly(string anomaly)
        {
            _algoritemDetectModel.selectedAnomaly(anomaly);
        }

        private void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        internal void vmSelectedAlgorithm(string selectedItem)
        {
            _algoritemDetectModel.SelectedAlgorithm(selectedItem);
        }

    }
}
