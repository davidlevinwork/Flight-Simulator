using System;
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
//using DragEventArgs = System.Windows.DragEventArgs;



namespace SimolatorDesktopApp_1
{
    using DragEventArgs = System.Windows.DragEventArgs;
    using SimolatorDesktopApp_1.Model;
    using System.IO;


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool readGuide = true, readyToConnect = true; // both need to be false. need to add guide. 
        /// </summary>
        public enum Status { active, connect, disconnect, inActive }
        private const string disconnected = "Simulator Disconnected";
        private const string connected = "Simulator Connected";
        public MainWindow()
        {
            InitializeComponent();
            // Media.Source = new Uri(Environment.CurrentDirectory+ @"\load1.jpg");
        }

        /*
        private void CsvDropStackPanel_Drop(object sender, DragEventArgs e)
        {
            if (readGuide && e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] filePath = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                Console.WriteLine(filePath[0]);
                string fileName = Path.GetFileName(filePath[0]);
                string tempFileName = fileName;
                int i = tempFileName.LastIndexOf('.');
                string fileType = i < 0 ? "" : tempFileName.Substring(i + 1);
                if (String.Equals("csv", fileType))
                {
                    CsvFileNameLabel.Content = fileName;
                    readyToConnect = true;
                }
                else
                {
                    CsvFileNameLabel.Content = "Invalid file type";
                }
            }
        }
        */

        /*
        private void button1_Click(object sender, EventArgs e)
        {
            firstButton.Background = Brushes.LimeGreen;
            firstButton.Content = "Do it now. before loading the csv file";
            firstButton.FontSize = 7;
            //Console.WriteLine("clicked");
            readGuide = true;
            Form form = new Form();
            form.Show();
            PictureBox pb = new PictureBox();
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string path = projectDirectory + "/images/check.jpg";
            //Console.WriteLine(path);
            pb.ImageLocation = path;
            form.Location = new System.Drawing.Point(0, 0);
            form.Size = new System.Drawing.Size(900, 960);
            pb.Size = new System.Drawing.Size(900, 900);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.BorderStyle = BorderStyle.Fixed3D;
            form.Controls.Add(pb);
        }

        */

        private void ConnectControl_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("asdasd");

        }

        private void ConnectControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("asdasd");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }



        private void ConnectControl_Loaded_2(object sender, RoutedEventArgs e)
        {

        }

        private void DashBoardFlight_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void JoystickFlight_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {

        }

        private void button2_ClickToConnect(object sender, EventArgs e)
        {
            if (readyToConnect)
            {
                SimulatorConnectorModel simConnector = new SimulatorConnectorModel();
                SimulatorModel simulatorModel = new SimulatorModel(simConnector);
                simConnector.Connect("127.0.0.1", 5400);
               // LabelConnectStatus.Content = "Status: Connected";
                // LabelConnectStatus.Background = Brushes.LimeGreen;
                simulatorModel.startSimulator();
            }
        }

        private void XmlDropStackPanel_Drop(object sender, DragEventArgs e) ///////////////////////
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {

                string[] filePath = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                string pathXml = filePath[0];
                string fileName = Path.GetFileName(filePath[0]);
                string tempFileName = fileName;
                int i = tempFileName.LastIndexOf('.');
                string fileType = i < 0 ? "" : tempFileName.Substring(i + 1);
                if (String.Equals("xml", fileType) || String.Equals("xaml", fileType))
                {
                    _xmlFileNameLabel.Content = fileName;
                    (System.Windows.Application.Current as App)._popOutModel.makeLearnNormal(pathXml);
                    //ifValidXmlFile = true;
                }
                else
                {
                    _xmlFileNameLabel.Content = "Invalid file type";
                    //ifValidXmlFile = false;
                }
            }
        }
    }
}
