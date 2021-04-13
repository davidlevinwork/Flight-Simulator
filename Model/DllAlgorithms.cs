using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace SimolatorDesktopApp_1.Model
{
    /*
     * Class DllAlgorithms - load dll algorithms for detect in generic way, each algorithm that 
     * we want to load have implement the delgete functions.
     */
    public class DllAlgorithms
    {
        private IntPtr _handle = IntPtr.Zero;
        private static IntPtr hybrid = IntPtr.Zero;
        private static IntPtr time_series = IntPtr.Zero;
        private static IntPtr time_series2 = IntPtr.Zero;
        private static IntPtr wrapper = IntPtr.Zero;
        private static IntPtr DrawingWrapper = IntPtr.Zero;
        private static List<string> descriptionsList = new List<string>();
        private static List<int> timeStepsList = new List<int>();
        
        /*
         * Static class for using kernel libaries which help in loading dll files.
         */
        static class NativeMethods
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr LoadLibrary(string libname);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern bool FreeLibrary(IntPtr hModule);

            [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetLastError();
        }
        
        public DllAlgorithms() { }

        /*
         * Functions that each dll algorithm for detect have to implement.
         */

        /*
         *  Function that Return timesSeries from csvFile of flight.
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr getTimeSeries([MarshalAs(UnmanagedType.LPStr)] StringBuilder csvFileName);

        /*
         * Function that get timeseries and stringbuilders f1,f2 and find the correlative feature to
         * f1 and copy it to buffer f2.
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void getMyCorrelatedFeature(IntPtr time_series, StringBuilder f1, StringBuilder buffer);

        /*
         * Function that return IntPtr of HybridDetector.
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr getHybridDetector();

        /*
        * Function that make LearnNormal with hybried and timeseries.
        */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void callLearnNormal(IntPtr hybrid, IntPtr ts);

        /*
         * Function that returns liner regression.
         *//*
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr getLinearReg(IntPtr time_series, StringBuilder f1, StringBuilder f2);*/

        /*
         * Function that return y value from line and parameter x.
         *//*
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate float getYLine(Line line, float x);*/
/*
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void updateAnomaly(IntPtr hybrid, IntPtr time_series, [MarshalAs(UnmanagedType.LPArray)] string[] descriptor);*/

        /*
         * Function that returns wrapper of anomalies vector.
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr getRupperAnomaly(IntPtr time_series, IntPtr hybrid);

        /*
         * Function that returns size of wrapper of anomalies vector.
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int getSizeRupper(IntPtr rupper);

        /*
         * Function that copy description of index i in wrapper to buffer.
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void getDescriptionRupper(IntPtr rupper, int i, StringBuilder buffer);

        /*
         * Function that returns timeStep of anomaly in wrapper of anomalies vector.
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int getTimeStepRupper(IntPtr rupper, int i);

        /*
         * Function that returns intPtr wrapper of points the draw the algorithm structure.
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr getDrawingRupper(IntPtr time_series, StringBuilder f1, StringBuilder f2, IntPtr hybrid);

        /*
         * Function that returns x value of of index in drawingWrapper points.
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate float getXValueByIndexDrawingWrapper(IntPtr drawingWrapper, int i);

        /*
         * Function that returns y value of of index in drawingWrapper points.
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate float getYValueByIndexDrawingWrapper(IntPtr drawingWrapper, int i);

        /*
         * Function that returns size if drawingWrapper vector points.
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int getSizeDrawingWrapper(IntPtr drawingWrapper);

        /*
         * Function that Wraps the delegate function that I stated above
         */
        public IntPtr setDllgetDrawingRupper(IntPtr time_series, StringBuilder f1, StringBuilder f2, IntPtr hybrid)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getDrawingRupper");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getDrawingRupper");
                return funcaddr;
            }
            getDrawingRupper function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getDrawingRupper)) as getDrawingRupper;

            return function.Invoke(time_series, f1, f2, hybrid);
        }

        /*
         * Function that Wraps the delegate function that I stated above
         */
        public float setDllgetXValueByIndexDrawingWrapper(IntPtr drawingWrapper, int i)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getXValueByIndexDrawingWrapper");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getXValueByIndexDrawingWrapper");
                return 0;
            }
            getXValueByIndexDrawingWrapper function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getXValueByIndexDrawingWrapper)) as getXValueByIndexDrawingWrapper;

            return function.Invoke(drawingWrapper, i);
        }

        /*
         * Function that Wraps the delegate function that I stated above
         */
        public float setDllgetYValueByIndexDrawingWrapper(IntPtr drawingWrapper, int i)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getYValueByIndexDrawingWrapper");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getYValueByIndexDrawingWrapper");
                return 0;
            }
            getYValueByIndexDrawingWrapper function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getYValueByIndexDrawingWrapper)) as getYValueByIndexDrawingWrapper;

            return function.Invoke(drawingWrapper, i);
        }

        /*
         * Function that Wraps the delegate function that I stated above
         */
        public int setDllgetSizeDrawingWrapper(IntPtr drawingWrapper)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getSizeDrawingWrapper");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getSizeDrawingWrapper");
                return 0;
            }
            getSizeDrawingWrapper function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getSizeDrawingWrapper)) as getSizeDrawingWrapper;

            return function.Invoke(drawingWrapper);
        }

        /*
         * Function that Wraps the delegate function that I stated above
         */
        public IntPtr setDllgetTimeSeries([MarshalAs(UnmanagedType.LPStr)] StringBuilder csvFileName)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getTimeSeries");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getTimeSeries");
                return funcaddr;
            }
            getTimeSeries function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getTimeSeries)) as getTimeSeries;

            return function.Invoke(csvFileName);
        }

        /*
         * Function that Wraps the delegate function that I stated above
         */
        public void setDllgetMyCorrelatedFeature(IntPtr time_series, StringBuilder f1, StringBuilder buffer)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getMyCorrelatedFeature");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getMyCorrelatedFeature");
                return;
            }
            getMyCorrelatedFeature function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getMyCorrelatedFeature)) as getMyCorrelatedFeature;

            function.Invoke(time_series, f1, buffer);
        }

        /*
         * Function that Wraps the delegate function that I stated above
         */
        public IntPtr setDllgetHybridDetector()
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getHybridDetector");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getHybridDetector");
                return funcaddr;
            }
            getHybridDetector function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getHybridDetector)) as getHybridDetector;

            return function.Invoke();
        }

        /*
         * Function that Wraps the delegate function that I stated above
         */
        public void setDllcallLearnNormal(IntPtr hybrid, IntPtr ts)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "callLearnNormal");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function callLearnNormal");
                return;
            }
            callLearnNormal function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(callLearnNormal)) as callLearnNormal;

            function.Invoke(hybrid, ts);
        }

        /*
         * Function that Wraps the delegate function that I stated above
         */
       /* public IntPtr setDllgetLinearReg(IntPtr time_series, StringBuilder f1, StringBuilder f2)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getLinearReg");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getLinearReg");
                return funcaddr;
            }
            getLinearReg function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getLinearReg)) as getLinearReg;

            return function.Invoke(time_series, f1, f2);
        }*/

        /*
         * Function that Wraps the delegate function that I stated above
         */
       /* public float setDllgetYLine(Line line, float x)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getYLine");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getYLine");
                return 0;
            }
            getYLine function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getYLine)) as getYLine;

            return function.Invoke(line, x);
        }*/

        /*
         * Function that Wraps the delegate function that I stated above
         */
       /* public void setDllupdateAnomaly(IntPtr hybrid, IntPtr time_series, [MarshalAs(UnmanagedType.LPArray)] string[] descriptor)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "updateAnomaly");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function updateAnomaly");
                return;
            }
            updateAnomaly function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(updateAnomaly)) as updateAnomaly;

            function.Invoke(hybrid, time_series, descriptor);
        }*/

        /*
         * Function that Wraps the delegate function that I stated above
         */
        public IntPtr setDllgetRupperAnomaly(IntPtr time_series, IntPtr hybrid)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getRupperAnomaly");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getRupperAnomaly");
                return funcaddr;
            }
            getRupperAnomaly function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getRupperAnomaly)) as getRupperAnomaly;

            return function.Invoke(time_series, hybrid);
        }

        /*
         * Function that Wraps the delegate function that I stated above
         */
        public int setDllgetSizeRupper(IntPtr rupper)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getSizeRupper");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getSizeRupper");
                return 0;
            }
            getSizeRupper function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getSizeRupper)) as getSizeRupper;

            return function.Invoke(rupper);
        }

        /*
         * Function that Wraps the delegate function that I stated above
         */
        public void setDllgetDescriptionRupper(IntPtr rupper, int i, StringBuilder buffer)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getDescriptionRupper");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getDescriptionRupper");
                return;
            }
            getDescriptionRupper function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getDescriptionRupper)) as getDescriptionRupper;

            function.Invoke(rupper, i, buffer);
        }

        /*
         * Function that Wraps the delegate function that I stated above
         */
        public int setDllgetTimeStepRupper(IntPtr rupper, int i)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getTimeStepRupper");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getTimeStepRupper");
                return 0;
            }
            getTimeStepRupper function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getTimeStepRupper)) as getTimeStepRupper;

            return function.Invoke(rupper, i);
        }

        /*
         * Function that set the dll algorithm from the path we get.
         */
        public void setDllPath(string dllPath)
        {
            if(_handle != IntPtr.Zero)
            {
                freeDllPath();
            }
            _handle = NativeMethods.LoadLibrary(dllPath);
            if (_handle == IntPtr.Zero)
            {
                Console.WriteLine("fail loading dll");
                return;
            }
            myCallLearnNormal();
        }

        /*
         * Function that play the detect algorithm and get the anomalies we get to list.
         */
        public void playDetect()
        {
            myDetectAnomalies();
            int size = setDllgetSizeRupper(wrapper);
            for (int i = 0; i < size; i++)
            {
                timeStepsList.Add(setDllgetTimeStepRupper(wrapper, i));
                StringBuilder buffer = new StringBuilder("", 100);
                setDllgetDescriptionRupper(wrapper, i, buffer);
                descriptionsList.Add(buffer.ToString());
            }
        }

        /*
         * Function that free the dll algorithm resources.
         */
        public void freeDllPath()
        {
            descriptionsList.Clear();
            timeStepsList.Clear();
            bool res = NativeMethods.FreeLibrary(_handle);
            if (!res)
            {
                Console.WriteLine("fail free dll");
                return;
            }
        }

        public List<string> getDescriptionsList()
        {
            return descriptionsList;
        }

        public List<int> getTimeStepList()
        {
            return timeStepsList;
        }

        /*
         * Function that check if there is anomaly that suitable to description and line.
         */
        public bool isAnomalyPoint(string description, int line)
        {
            int size = timeStepsList.Count;
            for (int i = 0; i < size; i++)
            {
                if (timeStepsList[i] == line && descriptionsList[i].Equals(description))
                   return true;
            }
            return false;
        }

        /*
         * Function that making learnNormalTimeSeries and set him.
         */
        public void myGetTimeSeries()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string newCsvPath = projectDirectory + '\\' + "learnNormalTimeSeries.csv";
            StringBuilder path = new StringBuilder(newCsvPath);
            time_series = setDllgetTimeSeries(path);
        }

        public void myGetHybridDetector()
        {
            hybrid = setDllgetHybridDetector();
        }

        /*
         * Function that calls to learnNormal function.
         */
        public void myCallLearnNormal()
        {
            myGetHybridDetector();
            if(time_series == IntPtr.Zero)
            {
                myGetTimeSeries();
            }
            setDllcallLearnNormal(hybrid, time_series);
        }

        /*
         * Fucntion that set the wrapper which is the anomalies list the algorithm detect.
         */
        public void myDetectAnomalies()
        {
            if (time_series2 == IntPtr.Zero)
            {
                string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
                string newCsvPath = projectDirectory + '\\' + "detectTimeSeries.csv";
                StringBuilder path = new StringBuilder(newCsvPath);
                time_series2 =  setDllgetTimeSeries(path);
            }
            wrapper = setDllgetRupperAnomaly(time_series2, hybrid);
        }

        /*
         * Function that call to setDllgetMyCorrelatedFeature.
         */
        public StringBuilder myGetMyCorrelatedFeature(StringBuilder src, StringBuilder dst)
        {
            if (time_series == IntPtr.Zero)
            {
                myGetTimeSeries();
            }
            setDllgetMyCorrelatedFeature(time_series, src, dst);
            return dst;
        }

        /*
         * Function that returns the DrawingWrapper of the algorithm.
         */
        public IntPtr myGetDrawingRupper(StringBuilder f1, StringBuilder f2)
        {
            return setDllgetDrawingRupper(time_series, f1, f2, hybrid);
        }
    }
}

