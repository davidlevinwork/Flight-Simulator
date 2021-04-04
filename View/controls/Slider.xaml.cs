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
    using SimolatorDesktopApp_1.Model;
    using System.Globalization;
    using System.IO;
    using DragEventArgs = System.Windows.DragEventArgs;
    /// <summary>
    /// Interaction logic for Slider.xaml
    /// </summary>
    public partial class Slider : UserControl
    {
        private VMSlider _VMSlider;
        private string pathCsv = "";
        private bool ifValidCsvFile = false;

        public Slider()
        {
            InitializeComponent();
            _VMSlider = new VMSlider(new SliderModel((Application.Current as App)._simultorConnectorModel));
            DataContext = _VMSlider;
        }

        private void CsvDropStackPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {

                string[] filePath = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                pathCsv = filePath[0];
                string fileName = Path.GetFileName(filePath[0]);
                string tempFileName = fileName;
                int i = tempFileName.LastIndexOf('.');
                string fileType = i < 0 ? "" : tempFileName.Substring(i + 1);
                if (String.Equals("csv", fileType))
                {
                    CsvFileNameLabel.Content = fileName;
                    _VMSlider.VMUploadCsvFile(pathCsv);
                    ifValidCsvFile = true;
                }
                else
                {
                    CsvFileNameLabel.Content = "Invalid file type";
                    ifValidCsvFile = false;
                }
            }
        }

        private void PlayButton(object sender, RoutedEventArgs e)
        {
            if(ifValidCsvFile)
                _VMSlider.VMplay(pathCsv);
        }
        

        private void StopButton(object sender, RoutedEventArgs e)
        {
            _VMSlider.VMstop();
        }

        private void PauseButton(object sender, RoutedEventArgs e)
        {
            _VMSlider.VMpause();
        }

        private void ComboBoxSelectedSpeed(object sender, RoutedEventArgs e)
        {
            string x = _SpeedComboBox.SelectedItem.ToString();
            int l = x.IndexOf(": ");
            string newX = x.Substring(l + 2);
            try
            {
                double speed = double.Parse(newX, CultureInfo.InvariantCulture);
                _VMSlider.VMsetSpeed(speed);
            }
            catch
            {
                Console.WriteLine("Error in combo box speed");
            }
            
        }

        private void LeftJumpButton(object sender, RoutedEventArgs e)
        {
            _VMSlider.VMupdateIndexSlider(_VMSlider.VM_IndexLine - 50);
        }

        private void RightJumpButton(object sender, RoutedEventArgs e)
        {
            _VMSlider.VMupdateIndexSlider(_VMSlider.VM_IndexLine + 50);
        }

        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _VMSlider.VMupdateIndexSlider((int)_SliderButton.Value);
        }
        
    }
}
