using SimolatorDesktopApp_1.Model;
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
    /*
     * Class View GraphsUserControl.
     */
    public partial class GraphsUserControl : UserControl
    {
        private VMGraphs _vmGraphs;
        private FilesUpload _filesUpload;

        /*
         * Constructor of GraphsUserControl.
         */
        public GraphsUserControl()
        {
            _filesUpload = (Application.Current as App)._filesUpload;
            _vmGraphs = (Application.Current as App)._vmGraphs;
            DataContext = _vmGraphs;
            InitializeComponent();
        }

        /*
         * Function that command to ViewModel _vmGraphs of selected feature.
         */
        private void ListViewFeatures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vmGraphs.vmSelectedFeature(ListViewFeatures.SelectedItem.ToString());
        }
    }
}
