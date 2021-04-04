﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimolatorDesktopApp_1.Model
{
    public class FilesUpload
    {
        private Dictionary<int, string> _featuresMap = new Dictionary<int, string>();
        private string[] _userCsvFile;
        bool isCsvUploaded = false, isXmlUploaded = false;
        public FilesUpload()
        {
            //_featuresMap = new Dictionary<int, string>();
        }

        public Dictionary<int, string> FeaturesMap
        {
            get
            {
                return _featuresMap;
            }
            
            set
            { 
            }
        }

        public void xmlUpload()
        {
            //Console.WriteLine("entered xmlUpload!!!!!!!!!!!!!!!!!!!!!1");
            try 
            {
                XDocument doc = XDocument.Load("C:/Program Files/FlightGear 2020.3.6/data/Protocol/playback_small.xml");
                string xmlFile = doc.ToString();
                string[] words = xmlFile.Split(' ');
                int location = 0;
                string sub;
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Contains("<output>"))
                    {
                       // Console.WriteLine("entered xmlUpload!!!!!!!!!!!!!!!!!!!!!2");
                        while (!words[i].Contains("</output>"))
                        {
                            //Console.WriteLine("entered xmlUpload!!!!!!!!!!!!!!!!!!!!!3");

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
                }
                isXmlUploaded = true;
                if(isCsvUploaded)
                {
                    writeNewCSVFile();
                    isXmlUploaded = false;
                }
            }
            catch
            {
                Console.WriteLine("error xml");
            }
        }


        public string[] csvUpload(string csvPath)
        {
            try 
            {
                _userCsvFile = File.ReadAllLines(csvPath);
                isCsvUploaded = true;
                if (isXmlUploaded)
                {
                    writeNewCSVFile();
                    isCsvUploaded = false;
                }
                return _userCsvFile;
            }
           catch(Exception e)
            {
                Console.WriteLine("error csvvvvvvv");
                return null;
            }
        }

        public void writeNewCSVFile()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string path = projectDirectory + '\\' + "timeseries.csv";
            char delimiter = ',';
            string line = "";
            int i;
            for(i = 0; i < _featuresMap.Count - 1; i++)
            {
                line += _featuresMap[i] + delimiter;
            }
            line += _featuresMap[i] + Environment.NewLine;
            for(i = 0; i < _userCsvFile.Length; i++)
            {
                line += _userCsvFile[i] + Environment.NewLine;
            }
            File.WriteAllText(path, line);
        }
    }
}