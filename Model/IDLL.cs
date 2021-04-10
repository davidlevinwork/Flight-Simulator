using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimolatorDesktopApp_1.Model
{
    public interface IDLL
    {
        void myGetTimeSeries();
        void myGetHybridDetector();
        void myCallLearnNormal();
        StringBuilder myGetMyCorrelatedFeature(StringBuilder src, StringBuilder dst);
    }
}
