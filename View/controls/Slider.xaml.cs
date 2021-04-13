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

    /*
     * Class View Slider.
     */
    public partial class Slider : UserControl
    {
        private VMSlider _VMSlider;
        private string pathCsv = "";
        private bool ifValidCsvFile = false;

        /*
         * Constructor of Slider
         */
        public Slider()
        {
            InitializeComponent();
            _VMSlider = new VMSlider((Application.Current as App)._sliderModel);
            DataContext = _VMSlider;
        }

        /*
         * when the user drags and drops the csv file to it's place.
         */
        private void CsvDropStackPanel_Drop(object sender, DragEventArgs e)
        {
            if ((Application.Current as App)._popOutModel.getFlagIfXamlUpload()) // if the user already uploaded the xml
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
                        _csvFileNameLabel.FontSize = 14;
                        _csvFileNameLabel.Content = fileName + "\n you can play the flight";
                        _csvFileNameLabel.Foreground = Brushes.LawnGreen;
                        Console.WriteLine(pathCsv);
                        _VMSlider.VMUploadCsvFile(pathCsv);
                        ifValidCsvFile = true;
                        _PauseButton.IsEnabled = true;
                        _PlayButton.IsEnabled = true;
                        _rightJumpButton.IsEnabled = true;
                        _leftJumpButton.IsEnabled = true;
                        _StopButton.IsEnabled = true;
                        _SliderButton.IsEnabled = true;
                    }
                    else
                    {
                        _csvFileNameLabel.FontSize = 13;
                        _csvFileNameLabel.Content = "Invalid file type\n upload csv file";
                        _csvFileNameLabel.Foreground = Brushes.Red;
                        ifValidCsvFile = false;
                    }
                }
            }
            else
            {
                _csvFileNameLabel.FontSize = 13;
                _csvFileNameLabel.Content = "Firstly press Connect\n and than drag Csv file";
            }

        }

        /*
         * if the user pressed on play button
         */
        private void PlayButton(object sender, RoutedEventArgs e)
        {
            if(ifValidCsvFile)
                _VMSlider.VMplay(pathCsv);
        }

        /*
         * if the user pressed on stop button
         */
        private void StopButton(object sender, RoutedEventArgs e)
        {
            _VMSlider.VMstop();
        }

        /*
         * if the user pressed on pause button
         */
        private void PauseButton(object sender, RoutedEventArgs e)
        {
            _VMSlider.VMpause();
        }

        /*
         * when the user selected a speed.
         */
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
            }
            
        }

        /*
         * if the user pressed on left jump button.
         */
        private void LeftJumpButton(object sender, RoutedEventArgs e)
        {
            _VMSlider.VMupdateIndexSlider(_VMSlider.VM_IndexLine - 50);
        }

        /*
         * if the user pressed on right jump button.
         */
        private void RightJumpButton(object sender, RoutedEventArgs e)
        {
            _VMSlider.VMupdateIndexSlider(_VMSlider.VM_IndexLine + 50);
        }

        /*
         * when the user moves the slider button.
         */
        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _VMSlider.VMupdateIndexSlider((int)_SliderButton.Value);
        }
    }
}
