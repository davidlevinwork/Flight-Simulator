﻿using SimolatorDesktopApp_1.Model;
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
    /// Interaction logic for GraphsUserControl.xaml
    /// </summary>
    public partial class GraphsUserControl : UserControl
    {

        private VMGraphs _vmGraphs;
        private FilesUpload _filesUpload;
        public GraphsUserControl()
        {
            _filesUpload = (Application.Current as App)._filesUpload;
            _vmGraphs = new VMGraphs((Application.Current as App)._graphModel);
            DataContext = _vmGraphs;
            InitializeComponent();

            this.ListViewFeatures.View = GridViewFeatures;

            for (int i = 0; i < _filesUpload.FeaturesMap.Count; i++)
            {
                this.ListViewFeatures.Items.Add(_filesUpload.FeaturesMap[i]);
            }

        }
    }
}