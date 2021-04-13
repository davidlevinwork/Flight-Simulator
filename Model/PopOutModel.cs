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
    /*
     * Class MODEL PopOutModel - handle the learn of the flight after the xml uploaded in the popOut window.
     */
    public class PopOutModel : INotifyPropertyChanged
    {
        private FilesUpload _filesUpload = (Application.Current as App)._filesUpload;
        public event PropertyChangedEventHandler PropertyChanged;
        bool _flagIfXamlUpload = false;

        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /*
         * after the xml file is uploaded - learn (from default algorithm - linearReg in our case) on the
         * timeSeries the created from the xml (and the csv we already had).
         */
        public void makeLearnNormal(string path)
        {
            _flagIfXamlUpload = true;
            _filesUpload.xmlUpload(path);
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string newCsvPath = projectDirectory + '\\' + "learnNormalTimeSeries.csv";
            List<string> dllNames = new List<string>();
            string targetDirectory = projectDirectory + '\\' + "Plugins";
            string[] dllEntries = Directory.GetFiles(targetDirectory);
            foreach (string dllName in dllEntries)
            {
                string fileName = Path.GetFileName(dllName);
                dllNames.Add(fileName);
            }
            (Application.Current as App)._algoritemDetectModel.AddAnomaliesToMyList = new ObservableCollection<string>();
            (Application.Current as App)._algorithmDll.setDllPath(targetDirectory + '\\' + dllNames[0]);
        }

        /*
         * returns true if the xml already uploaded. false otherwise.
         */
        public bool getFlagIfXamlUpload()
        {
            return _flagIfXamlUpload;
        }
    }
}
