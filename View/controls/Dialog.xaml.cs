using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System;

namespace SimolatorDesktopApp_1.View
{
    public partial class Dialog : Window
    {
        public bool flag = false;
        public Dialog()
        {
            InitializeComponent();
        }

        public bool getFlag()
        {
            return flag;
        }
    private void StackPanel_Drop_1(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] filePath = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                string pathXml = filePath[0];
                string fileName = System.IO.Path.GetFileName(filePath[0]);
                string tempFileName = fileName;
                int i = tempFileName.LastIndexOf('.');
                string fileType = i < 0 ? "" : tempFileName.Substring(i + 1);
                if (String.Equals("xml", fileType) || String.Equals("xaml", fileType))
                {
                    flag = true;
                    _xmlFile.Content = fileName;
                    (System.Windows.Application.Current as App)._popOutModel.makeLearnNormal(pathXml);
                }
                else
                {
                    _xmlFile.Content = "Invalid file type";
                }
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {

            if(!flag)
            {
                MessageBoxResult r = System.Windows.MessageBox.Show("Please upload the XML file.", "Flight-Simulator", MessageBoxButton.OK);
                this.Show();
            }
            else
            {
                this.Close();
                MessageBoxResult result = System.Windows.MessageBox.Show("Now you are ready to use the app. Enjoy! ", "Flight-Simulator", MessageBoxButton.OK);
            }
        }
    }
}
