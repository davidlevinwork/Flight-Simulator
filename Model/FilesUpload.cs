using SimolatorDesktopApp_1.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace SimolatorDesktopApp_1.Model
{
    /*
     * Class FilesUpload - upload the XML, and CSV files.  
     */
    public class FilesUpload : INotifyPropertyChanged
    {
        private Dictionary<int, string> _featuresMap = new Dictionary<int, string>();
        private VMGraphs _vmGraphs = (Application.Current as App)._vmGraphs;
        private GraphsModel _graphsModel = (Application.Current as App)._graphModel;
        private string[] _myCsvFile, _userCsvFile;
        private ObservableCollection<string> _toViewListFeatures = new ObservableCollection<string>();
        Dictionary<string, double[]> _allValues = new Dictionary<string, double[]>();
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructor FilesUpload
        public FilesUpload() { }

        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /*
         * Property of GetAllValues.
         */
        public Dictionary<string, double[]> GetAllValues
        {
            get
            {
                return _allValues;
            }
            set
            {
                _allValues = value;
            }
        }

        /*
         * Property of FeaturesMap.
         */
        public Dictionary<int, string> FeaturesMap
        {
            get
            {
                return _featuresMap;
            }
            set { }
        }

        /*
         * Function that load the xml file with the path we get, and parser the xml file
         * to features we need for creating csv file with columns of features.
         */
        public void xmlUpload(string path)
        {
            try 
            {
                XDocument doc = XDocument.Load(path);
                string xmlFile = doc.ToString();
                string[] words = xmlFile.Split(' ');
                int location = 0;
                string sub;
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Contains("<output>"))
                    {
                        while (!words[i].Contains("</output>"))
                        {
                            if (words[i].StartsWith("<name>"))
                            {
                                sub = words[i].Split('>')[1];
                                sub = sub.Split('<')[0];
                                _featuresMap.Add(location++, sub);
                            }
                            i++;
                        }
                        break;
                    }
                } // push the featurs to _featuresMap.
                for (int i = 0; i < _featuresMap.Count; i++)
                {
                    if (i > 0 && _featuresMap[i] == _featuresMap[i - 1])
                        _featuresMap[i] = _featuresMap[i] + "2";
                    _toViewListFeatures.Add(_featuresMap[i]);
                }
                _vmGraphs.VM_AddToList = _toViewListFeatures;
                string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
                string csvPath = projectDirectory + '\\' + "reg_flight.csv";
                _myCsvFile = File.ReadAllLines(csvPath);
                writeLearnCSVFile();
            }
            catch
            {
                Console.WriteLine("error xml");
            }
        }

        /*
         * Function that load the csv file and returns array of lines in the csv.
         */
        public string[] csvUpload(string csvPath)
        {
            try 
            {
                _userCsvFile = File.ReadAllLines(csvPath);
                writeDetectCSVFile();
                return _userCsvFile;
            }
            catch(Exception e)
            {
                Console.WriteLine("error in csvUpload");
                return null;
            }
        }

        /*
         * Function that write new csv file with the features of the xml for learn.
         */
        public void writeLearnCSVFile()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string path = projectDirectory + '\\' + "learnNormalTimeSeries.csv";
            char delimiter = ',';
            string line = "";
            int i;
            for(i = 0; i < _featuresMap.Count - 1; i++)
            {
                line += _featuresMap[i] + delimiter;
            }
            line += _featuresMap[i] + Environment.NewLine;
            for(i = 0; i < _myCsvFile.Length; i++)
            {
                line += _myCsvFile[i] + Environment.NewLine;
            }
            File.WriteAllText(path, line);
        }

        /*
         * Function that write new csv file with the features of the xml for detect.
         */
        public void writeDetectCSVFile()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string csvPath = projectDirectory + '\\' + "detectTimeSeries.csv";
            char delimiter = ',';
            string line = "";
            int i;
            for (i = 0; i < _featuresMap.Count - 1; i++)
            {
                line += _featuresMap[i] + delimiter;
            }
            line += _featuresMap[i] + Environment.NewLine;
            for (i = 0; i < _userCsvFile.Length; i++)
            {
                line += _userCsvFile[i] + Environment.NewLine;
            }
            File.WriteAllText(csvPath, line);
            updateDictionary();
            (Application.Current as App)._algorithmDll.playDetect();
        }

        /*
         * Function that update the dictionary.
         */
        public void updateDictionary()
        {
            Dictionary<string, double[]> allValues = new Dictionary<string, double[]>();
            for (int i = 0; i < _featuresMap.Count; i++)
            {
                allValues.Add(_featuresMap[i], new double[_userCsvFile.Length]);
            }
            for (int i = 0; i < _userCsvFile.Length; i++)
            {
                string[] line = _userCsvFile[i].Split(',');
                for (int j = 0; j < allValues.Count; j++)
                {
                    allValues[_featuresMap[j]][i] = double.Parse(line[j], CultureInfo.InvariantCulture);
                }
            }
            GetAllValues = allValues;
        }
    }
}
