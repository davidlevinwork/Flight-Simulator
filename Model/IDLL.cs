using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimolatorDesktopApp_1.Model
{
    interface IDLL
    {

        void myGetHybridDetector();
        void myCallLearnNormal();
        StringBuilder myGetMyCorrelatedFeature(StringBuilder src, StringBuilder dst);
    }
}
