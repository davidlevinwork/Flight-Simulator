using OxyPlot;
using SimolatorDesktopApp_1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimolatorDesktopApp_1.ViewModel
{
    public class VMGraphs : INotifyPropertyChanged
    {
        private GraphsModel _graphsModel;
        private ObservableCollection<string> _toViewListFeatures = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public VMGraphs(GraphsModel graphsModel)
        {
            _graphsModel = graphsModel;
            _graphsModel.PropertyChanged +=
               delegate (Object sender, PropertyChangedEventArgs e)
               {
                   INotifyPropertyChanged("VM_" + e.PropertyName);
               };
        }

        public PlotModel VM_PlotFeature1
        {
            get { return _graphsModel.PlotFeature1; }
            set
            {
                _graphsModel.PlotFeature1 = value;
            }
        }

        public PlotModel VM_PlotFeature2
        {
            get { return _graphsModel.PlotFeature2; }
            set
            {
                _graphsModel.PlotFeature2 = value;
            }
        }

        public PlotModel VM_PlotCorrelatedFeatures
        {
            get { return _graphsModel.PlotCorrelatedFeatures; }
            set
            {
                _graphsModel.PlotCorrelatedFeatures = value;
            }
        }

        internal void vmSelectedFeature(string selectedItem)
        {
            _graphsModel.SelectedFeature(selectedItem);
        }

        public ObservableCollection<string> VM_AddToList
        {
            get { return _toViewListFeatures; }
            set
            {
                _toViewListFeatures = value;
                INotifyPropertyChanged("VM_AddToList");
            }
        }

        private void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
