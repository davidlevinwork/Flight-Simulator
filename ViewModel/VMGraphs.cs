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
        public PlotModel VM_Plot
        {
            get { return _graphsModel.Plot; }
            set
            {
                _graphsModel.Plot = value;
            }
        }

        public ObservableCollection<string> VM_AddToList
        {
            get { return _toViewListFeatures; }
            set
            {
                _toViewListFeatures = value;
                Console.WriteLine(_toViewListFeatures.Count);
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
