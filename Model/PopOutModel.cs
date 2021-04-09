using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace SimolatorDesktopApp_1.Model
{
    public class PopOutModel : INotifyPropertyChanged
    {
        private Dictionary<int, string> _featuresMap = new Dictionary<int, string>();
        private FilesUpload _filesUpload = (Application.Current as App)._filesUpload;
        private CircleDLL _functionsDll = (Application.Current as App)._functionsDLL;
        private ObservableCollection<string> _toViewListFeatures = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;
        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public void makeLearnNormal(string path)
        {
            Thread t = new Thread(delegate ()
            {
                _filesUpload.xmlUpload(path);
                Console.WriteLine("start!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
                string newCsvPath = projectDirectory + '\\' + "learnNormalTimeSeries.csv";
                //stringbuilder sb = new stringbuilder(newcsvpath);
                //functionsdll. = functionsdll.gettimeseries(sb);
                //console.writeline("time_series !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                //functionsdll.hybrid = functionsdll.gethybriddetector();
                //console.writeline("hybrid !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                //functionsdll.calllearnnormal(functionsdll.hybrid, functionsdll.time_series);
                //console.writeline("learnnnnnnnnn !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                _functionsDll.myGetTimeSeries();
                _functionsDll.myGetHybridDetector();
                _functionsDll.myCallLearnNormal();
                Console.WriteLine("learnnnnnnnnn !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            });
            t.Start();
        }
    }
}
