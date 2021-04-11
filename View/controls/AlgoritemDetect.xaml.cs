using SimolatorDesktopApp_1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimolatorDesktopApp_1.View.controls
{
    /// <summary>
    /// Interaction logic for AlgoritemDetect.xaml
    /// </summary>
    public partial class AlgoritemDetect : UserControl
    {
        private VMAlgoritemDetect _vMAlgoritemDetect;
        public AlgoritemDetect()
        {
            _vMAlgoritemDetect = new VMAlgoritemDetect((Application.Current as App)._algoritemDetectModel);
            DataContext = _vMAlgoritemDetect;
            InitializeComponent();
        }

        private void ListViewAlgoritems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vMAlgoritemDetect.vmSelectedAlgorithm(ListViewAlgorithms.SelectedItem.ToString());
        }

        private void ListViewAnomalies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListViewAnomalies.SelectedItem != null)
                _vMAlgoritemDetect.vmSelectedAnomaly(ListViewAnomalies.SelectedItem.ToString());
        }
}
}
