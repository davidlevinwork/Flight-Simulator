﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace SimolatorDesktopApp_1.Model
{
    /*
     * Class Model GraphsModel - controls on the Graphs of the Simulator flight.
     */
    public class GraphsModel : INotifyPropertyChanged
    {
        // Data members.
        private PlotModel _plot1; // first graph display values of selected feature
        private PlotModel _plot2; // second graph display values of corelative feature to selected.
        private PlotModel _plot3; // third graph display drawing structure of the algorithm.
        private string _featureSelect = "";
        private string _correlativeFeature = "";
        private int _numOfValues = 0;
        private Dictionary<string, double[]> _allValuesMap;
        private DllAlgorithms _dll;
        private List<double> _XValuesDrawingWrapper = new List<double>();
        private List<double> _YValuesDrawingWrapper = new List<double>();

        /* Constructor that set the graphs */
        public GraphsModel()
        {
            _dll = (Application.Current as App)._algorithmDll;
            _allValuesMap = new Dictionary<string, double[]>();
            _plot1 = new PlotModel();
            _plot2 = new PlotModel();
            _plot3 = new PlotModel();
            _plot1.LegendOrientation = LegendOrientation.Horizontal;
            _plot1.LegendPlacement = LegendPlacement.Outside;
            _plot1.LegendPosition = LegendPosition.TopLeft;
            _plot1.LegendBackground = OxyColor.FromAColor(200, OxyColors.Transparent);
            _plot1.LegendBorder = OxyColors.Black;
            _plot1.LegendTitleFontSize = 9;

            var valueAxis = new LinearAxis() {};
            _plot1.Axes.Add(valueAxis);
            _plot2.LegendOrientation = LegendOrientation.Horizontal;
            _plot2.LegendPlacement = LegendPlacement.Outside;
            _plot2.LegendPosition = LegendPosition.TopLeft;
            _plot2.LegendBorder = OxyColors.Black;
            _plot2.LegendBackground = OxyColor.FromAColor(200, OxyColors.Transparent);
            _plot2.LegendTitleFontSize = 9;
            var valueAxis2 = new LinearAxis() {};
            _plot2.Axes.Add(valueAxis2);
            _plot3.LegendOrientation = LegendOrientation.Horizontal;
            _plot3.LegendPlacement = LegendPlacement.Outside;
            _plot3.LegendPosition = LegendPosition.TopLeft;
            _plot3.LegendBorder = OxyColors.Black;
            _plot3.LegendBackground = OxyColor.FromAColor(200, OxyColors.Transparent);
            var valueAxis3 = new LinearAxis() {};
            _plot3.Axes.Add(valueAxis3);
        }

        /*
         * Function that play when user select on feature and check for the corelative feature
         * to him.
         */
        public void SelectedFeature(string selectedItem)
        {
            _featureSelect = selectedItem;
            StringBuilder f1 = new StringBuilder(_featureSelect);
            StringBuilder f1Saver = new StringBuilder(_featureSelect);
            StringBuilder buffer = new StringBuilder();
            _correlativeFeature = _dll.myGetMyCorrelatedFeature(f1Saver, buffer).ToString();
            StringBuilder f2 = new StringBuilder(_correlativeFeature);
            ObservableCollection<string> anomaliesViewList = new ObservableCollection<string>();
            int size = _dll.getTimeStepList().Count;
            for (int i = 0; i < size; i++) // add to view list of anomalies.
            {
                if(_dll.getDescriptionsList()[i].Equals(_featureSelect + '-' + _correlativeFeature))
                {
                    anomaliesViewList.Add("Anomaly at " + _dll.getTimeStepList()[i].ToString());
                }
            }
            (Application.Current as App)._algoritemDetectModel.AddAnomaliesToMyList = anomaliesViewList;
            _XValuesDrawingWrapper.Clear();
            _YValuesDrawingWrapper.Clear();
            if (!f2.ToString().Equals(""))
            { // display the structure of the algorithm detect.
                IntPtr vecForDrawing = _dll.myGetDrawingRupper(f1, f2);
                int sizeVecDrawingWrapper = _dll.setDllgetSizeDrawingWrapper(vecForDrawing);
                for (int i = 0; i < sizeVecDrawingWrapper; i++)
                {
                    _XValuesDrawingWrapper.Add(_dll.setDllgetXValueByIndexDrawingWrapper(vecForDrawing, i));
                    _YValuesDrawingWrapper.Add(_dll.setDllgetYValueByIndexDrawingWrapper(vecForDrawing, i));
                }
            }

        }

        /*
         * Function that update all values map.
         */
        public void updateAllValues(Dictionary<string, double[]> allValues)
        {
            _allValuesMap = allValues;
        }

        /*
         * Function that update the Graphs each line we send to simulator.
         */
        public void updateGraph(Dictionary<string, double[]> valuesMap, int lineIndex)
        {
            if (_featureSelect == "") // if feature is not selected 
                return;
            // clear the plots graph
            _plot1.Series.Clear();
            _plot2.Series.Clear();
            _plot3.Series.Clear();
            LineSeries lineSeries = new LineSeries() { LineStyle = LineStyle.Automatic,  Color = OxyColors.LawnGreen };
            LineSeries lineSeries2 = new LineSeries() { Color = OxyColors.LawnGreen };
            LineSeries lineSeries3 = new LineSeries { LineStyle = LineStyle.None, MarkerType = MarkerType.Circle, MarkerSize = 2, MarkerFill = OxyColors.Gray };
            LineSeries lineSeries3H = new LineSeries { LineStyle = LineStyle.None, MarkerType = MarkerType.Circle, MarkerSize = 2, MarkerFill = OxyColors.Red };
            LineSeries lineSeries4 = new LineSeries();
            _plot1.LegendTitle = _featureSelect; // title
            _plot2.LegendTitle = _correlativeFeature.ToString();
            _plot3.LegendTitle = "correlattive features";

            if (_correlativeFeature.ToString() != "") // if there is correlative feature
            { // drawing the structure of the algorithm.
                for (int i = 0; i < _XValuesDrawingWrapper.Count; i++)
                {
                    lineSeries4.Points.Add(new DataPoint(_XValuesDrawingWrapper[i], _YValuesDrawingWrapper[i]));
                }
            } // display the 30 last minutes values of the selected feature.
            if (lineIndex < 300)
            {
                for (int i = 0; i <= lineIndex; i++)
                {
                    lineSeries.Points.Add(new DataPoint(i, valuesMap[_featureSelect][i]));
                    if(_correlativeFeature.ToString() != "")
                    {
                        lineSeries2.Points.Add(new DataPoint(i, valuesMap[_correlativeFeature.ToString()][i]));
                        if (_dll.isAnomalyPoint(_featureSelect + '-' + _correlativeFeature.ToString(), i))
                        {
                            lineSeries3H.Points.Add(new DataPoint(valuesMap[_featureSelect][i], valuesMap[_correlativeFeature.ToString()][i]));
                        }
                        else if (_correlativeFeature.ToString() != "")
                        {
                            lineSeries3.Points.Add(new DataPoint(valuesMap[_featureSelect][i], valuesMap[_correlativeFeature.ToString()][i]));
                        }
                    }
                    else
                    {
                        _plot2.LegendTitle = "no correlative feature!";
                        _plot3.LegendTitle = "no correlative feature!";
                    }
                }
            }
            else
            {
                for (int i = lineIndex - 300; i <= lineIndex; i++)
                {
                    lineSeries.Points.Add(new DataPoint(i, valuesMap[_featureSelect][i]));
                    if (_correlativeFeature.ToString() != "")
                    {
                        lineSeries2.Points.Add(new DataPoint(i, valuesMap[_correlativeFeature.ToString()][i]));
                        if (_dll.isAnomalyPoint(_featureSelect + '-' + _correlativeFeature.ToString(), i))
                        {
                            lineSeries3H.Points.Add(new DataPoint(valuesMap[_featureSelect][i], valuesMap[_correlativeFeature.ToString()][i]));
                        }
                        else if (_correlativeFeature.ToString() != "")
                        {
                            lineSeries3.Points.Add(new DataPoint(valuesMap[_featureSelect][i], valuesMap[_correlativeFeature.ToString()][i]));
                        }
                    }
                    else
                    {
                        _plot2.LegendTitle = "no correlative feature!";
                        _plot3.LegendTitle = "no correlative feature!";
                    }
                }
            }
            _plot1.Series.Add(lineSeries);
            _plot2.Series.Add(lineSeries2);
            _plot3.Series.Add(lineSeries3);
            _plot3.Series.Add(lineSeries3H);
            _plot3.Series.Add(lineSeries4);
            _plot1.InvalidatePlot(true);
            _plot2.InvalidatePlot(true);
            _plot3.InvalidatePlot(true);
            PlotFeature1 = _plot1;
            PlotFeature2 = _plot2;
            PlotCorrelatedFeatures = _plot3;
        }

        /*
         * Propety of PlotFeature1.
         */
        public PlotModel PlotFeature1
        {
            get
            {
                return _plot1;
            }
            set
            {
                _plot1 = value;
                INotifyPropertyChanged("PlotFeature1");
            }
        }

        /*
         * Propety of PlotFeature2.
         */
        public PlotModel PlotFeature2
        {
            get
            {
                return _plot2;
            }
            set
            {
                _plot2 = value;
                INotifyPropertyChanged("PlotFeature2");
            }
        }

        /*
         * Propety of PlotCorrelatedFeatures.
         */
        public PlotModel PlotCorrelatedFeatures
        {
            get
            {
                return _plot3;
            }
            set
            {
                _plot3 = value;
                INotifyPropertyChanged("PlotCorrelatedFeatures");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
