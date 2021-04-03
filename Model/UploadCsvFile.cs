using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimolatorDesktopApp_1.Model
{
    class UploadCsvFile
    {
        public string[] ReadFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            return lines;
        }
    }
}
