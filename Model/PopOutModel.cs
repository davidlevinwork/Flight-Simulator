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
        private FilesUpload _filesUpload = (Application.Current as App)._filesUpload;
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
    }
}
