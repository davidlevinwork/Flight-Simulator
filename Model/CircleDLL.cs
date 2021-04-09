﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace SimolatorDesktopApp_1.Model
{
    public class CircleDLL : IDLL
    {
        public CircleDLL() {}

        [DllImport("DllCircle.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr getTimeSeries
            ([MarshalAs(UnmanagedType.LPStr)] StringBuilder csvFileName);

        [DllImport("DllCircle.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void getMyCorrelatedFeature(IntPtr time_series, StringBuilder f1, StringBuilder buffer);

        [DllImport("DllCircle.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr getHybridDetector();

        [DllImport("DllCircle.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void callLearnNormal(IntPtr hybrid, IntPtr ts);

        [DllImport("DllCircle.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr getLinearReg(IntPtr time_series, StringBuilder f1, StringBuilder f2);
        [DllImport("DllCircle.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern float getYLine(Line line, float x);

        private static IntPtr hybrid = IntPtr.Zero;
        private static IntPtr time_series = IntPtr.Zero;

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
            callLearnNormal(hybrid, time_series);
        }

        public StringBuilder myGetMyCorrelatedFeature(StringBuilder src, StringBuilder dst)
        {
            getMyCorrelatedFeature(time_series, src, dst);
            return dst;
        }
    }
}