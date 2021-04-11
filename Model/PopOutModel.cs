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
        private IDLL _dllType = (Application.Current as App)._lineDLL;
        private ObservableCollection<string> _toViewListFeatures = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;
        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void SetDllType(IDLL dllType)
        {
            _dllType = dllType;
        }

        public void makeLearnNormal(string path)
        {
            //Thread t = new Thread(delegate ()
           // {
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

               // _dllType.myGetTimeSeries();
               // _dllType.myGetHybridDetector();
                // _dllType.myCallLearnNormal();
                Console.WriteLine("learnnnnnnnnn !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                List<string> dllNames = new List<string>();
                string targetDirectory = projectDirectory + '\\' + "Plugins";
                string[] dllEntries = Directory.GetFiles(targetDirectory);
                foreach (string dllName in dllEntries)
                {
                    string fileName = Path.GetFileName(dllName);
                    dllNames.Add(fileName);
                }
             (Application.Current as App)._algoritemDetectModel.AddAnomaliesToMyList = new ObservableCollection<string>();
             (Application.Current as App)._lineDLL.setDllPath(targetDirectory + '\\' + dllNames[1]);

            //(Application.Current as App)._algoritemDetectModel.SelectedAlgorithm(dllNames[1], 0);
                //(Application.Current as App)._graphModel.initizlizeDefaultDLL(targetDirectory + '\\' + dllNames[1]);
            //});
            //t.Start();
        }
    }
}
