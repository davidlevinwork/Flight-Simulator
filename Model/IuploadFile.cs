using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.IO;

namespace SimolatorDesktopApp_1.Model
{
    public class IuploadFile
    {
        public string[] ReadFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            return lines;
        }

    }
}
