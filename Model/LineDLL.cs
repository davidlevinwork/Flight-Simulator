using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace SimolatorDesktopApp_1.Model
{
    public class LineDll : IDLL
    {
        public LineDll() { }

        [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr getTimeSeries
            ([MarshalAs(UnmanagedType.LPStr)] StringBuilder csvFileName);

        [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void getMyCorrelatedFeature(IntPtr time_series, StringBuilder f1, StringBuilder buffer);

        [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr getHybridDetector();

        [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void callLearnNormal(IntPtr hybrid, IntPtr ts);

        [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr getLinearReg(IntPtr time_series, StringBuilder f1, StringBuilder f2);
        [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern float getYLine(Line line, float x);

        [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void updateAnomaly
        (IntPtr hybrid, IntPtr time_series,
        [MarshalAs(UnmanagedType.LPArray)] string[] descriptor);

        [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr getRupperAnomaly(IntPtr time_series, IntPtr hybrid);

        [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int getSizeRupper(IntPtr rupper);

        [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void getDescriptionRupper(IntPtr rupper, int i, StringBuilder buffer);

        [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern float getTimeStepRupper(IntPtr rupper, int i);

        private static IntPtr hybrid = IntPtr.Zero;
        private static IntPtr time_series = IntPtr.Zero;
        private static IntPtr time_series2 = IntPtr.Zero;
        /// <summary>
        private static IntPtr rupper = IntPtr.Zero;
        /// </summary>

        public void myGetTimeSeries()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string newCsvPath = projectDirectory + '\\' + "learnNormalTimeSeries.csv";
            StringBuilder path = new StringBuilder(newCsvPath);
            time_series = getTimeSeries(path);
        }

        public void myGetHybridDetector()
        {
            hybrid = getHybridDetector();
        }

        public void myCallLearnNormal()
        {
            myGetHybridDetector();
            myGetTimeSeries();
            callLearnNormal(hybrid, time_series);
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string newCsvPath = projectDirectory + '\\' + "detectTimeSeries.csv";
            StringBuilder path = new StringBuilder(newCsvPath);
            time_series2 = getTimeSeries(path);
            IntPtr rupper = getRupperAnomaly(time_series2, hybrid);
            int size = getSizeRupper(rupper);
            float x = getTimeStepRupper(rupper, 0);
/*            Console.WriteLine(x);
            Console.WriteLine("Buffer");
            StringBuilder buffer = new StringBuilder("", 100);
             getDescriptionRupper(rupper, 0, buffer);
            Console.WriteLine(buffer);*/

            // updateAnomaly(time_series2, hybrid, s);
        }

        public StringBuilder myGetMyCorrelatedFeature(StringBuilder src, StringBuilder dst)
        {
            if (time_series == IntPtr.Zero)
            {
                myGetTimeSeries();
            }
            getMyCorrelatedFeature(time_series, src, dst);
            return dst;
        }
    }
}