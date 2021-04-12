using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace SimolatorDesktopApp_1.Model
{
    public class DllAlgorithms
    {
        private IntPtr _handle = IntPtr.Zero;

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


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr getTimeSeries([MarshalAs(UnmanagedType.LPStr)] StringBuilder csvFileName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void getMyCorrelatedFeature(IntPtr time_series, StringBuilder f1, StringBuilder buffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr getHybridDetector();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void callLearnNormal(IntPtr hybrid, IntPtr ts);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr getLinearReg(IntPtr time_series, StringBuilder f1, StringBuilder f2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate float getYLine(Line line, float x);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void updateAnomaly(IntPtr hybrid, IntPtr time_series, [MarshalAs(UnmanagedType.LPArray)] string[] descriptor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr getRupperAnomaly(IntPtr time_series, IntPtr hybrid);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int getSizeRupper(IntPtr rupper);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void getDescriptionRupper(IntPtr rupper, int i, StringBuilder buffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int getTimeStepRupper(IntPtr rupper, int i);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr getDrawingRupper(IntPtr time_series, StringBuilder f1, StringBuilder f2, IntPtr hybrid);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate float getXValueByIndexDrawingWrapper(IntPtr drawingWrapper, int i);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate float getYValueByIndexDrawingWrapper(IntPtr drawingWrapper, int i);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int getSizeDrawingWrapper(IntPtr drawingWrapper);




        private static IntPtr hybrid = IntPtr.Zero;
        private static IntPtr time_series = IntPtr.Zero;
        private static IntPtr time_series2 = IntPtr.Zero;
        private static IntPtr wrapper = IntPtr.Zero;
        private static IntPtr DrawingWrapper = IntPtr.Zero;
        private static List<string> descriptionsList = new List<string>();
        private static List<int> timeStepsList = new List<int>();




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


        public IntPtr setDllgetLinearReg(IntPtr time_series, StringBuilder f1, StringBuilder f2)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getLinearReg");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getLinearReg");
                return funcaddr;
            }
            getLinearReg function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getLinearReg)) as getLinearReg;

            return function.Invoke(time_series, f1, f2);
        }


        public float setDllgetYLine(Line line, float x)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "getYLine");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function getYLine");
                return 0;
            }
            getYLine function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(getYLine)) as getYLine;

            return function.Invoke(line, x);
        }

        public void setDllupdateAnomaly(IntPtr hybrid, IntPtr time_series, [MarshalAs(UnmanagedType.LPArray)] string[] descriptor)
        {
            IntPtr funcaddr = NativeMethods.GetProcAddress(_handle, "updateAnomaly");
            if (funcaddr == IntPtr.Zero)
            {
                Console.WriteLine("fail loading function updateAnomaly");
                return;
            }
            updateAnomaly function = Marshal.GetDelegateForFunctionPointer(funcaddr, typeof(updateAnomaly)) as updateAnomaly;

            function.Invoke(hybrid, time_series, descriptor);
        }


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

        public void myCallLearnNormal()
        {
            myGetHybridDetector();
            if(time_series == IntPtr.Zero)
            {
                myGetTimeSeries();
            }
            setDllcallLearnNormal(hybrid, time_series);
        }

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
            Console.WriteLine(setDllgetSizeRupper(wrapper));
        }

        public StringBuilder myGetMyCorrelatedFeature(StringBuilder src, StringBuilder dst)
        {
            if (time_series == IntPtr.Zero)
            {
                myGetTimeSeries();
            }
            setDllgetMyCorrelatedFeature(time_series, src, dst);
            return dst;
        }

        public IntPtr myGetDrawingRupper(StringBuilder f1, StringBuilder f2)
        {
            return setDllgetDrawingRupper(time_series, f1, f2, hybrid);
        }


    }
}



















/*[DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
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
       private static extern void updateAnomaly(IntPtr hybrid, IntPtr time_series, [MarshalAs(UnmanagedType.LPArray)] string[] descriptor);

       [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
       private static extern IntPtr getRupperAnomaly(IntPtr time_series, IntPtr hybrid);

       [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
       private static extern int getSizeRupper(IntPtr rupper);

       [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
       private static extern void getDescriptionRupper(IntPtr rupper, int i, StringBuilder buffer);

       [DllImport("shared_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
       private static extern float getTimeStepRupper(IntPtr rupper, int i);*/