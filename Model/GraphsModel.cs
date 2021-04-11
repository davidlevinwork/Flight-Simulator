using System;
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
    public class GraphsModel : INotifyPropertyChanged
    {
        private PlotModel _plot1;
        private PlotModel _plot2;
        private PlotModel _plot3;
        private string _featureSelect = "";
        private string _correlativeFeature = "";
        private int _numOfValues = 0;
        private Dictionary<string, double[]> _allValuesMap;
        private LineDll _dll;
        public GraphsModel()
        {
            _dll = (Application.Current as App)._lineDLL;
            _allValuesMap = new Dictionary<string, double[]>();
            _plot1 = new PlotModel();
            _plot2 = new PlotModel();
            _plot3 = new PlotModel();
            _plot1.LegendOrientation = LegendOrientation.Horizontal;
            _plot1.LegendPlacement = LegendPlacement.Outside;
            _plot1.LegendPosition = LegendPosition.TopLeft;
            _plot1.LegendBackground = OxyColor.FromAColor(200, OxyColors.Blue);
            _plot1.LegendBorder = OxyColors.Black;
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };
            _plot1.Axes.Add(valueAxis);

            _plot2.LegendOrientation = LegendOrientation.Horizontal;
            _plot2.LegendPlacement = LegendPlacement.Outside;
            _plot2.LegendPosition = LegendPosition.TopLeft;
            _plot2.LegendBorder = OxyColors.Black;

            var valueAxis2 = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };
            _plot2.Axes.Add(valueAxis2);

            //var valueAxis3 = new LinearAxis() { MajorGridlineStyle = LineStyle.None, MinorGridlineStyle = LineStyle.Dot };
            //_plot3.Axes.Add(valueAxis3);

            _plot3.LegendOrientation = LegendOrientation.Horizontal;
            _plot3.LegendPlacement = LegendPlacement.Outside;
            _plot3.LegendPosition = LegendPosition.TopLeft;
            _plot3.LegendBorder = OxyColors.Black;
        }

/*        public void SetDllType(IDLL dllType)
        {
            _dllType = dllType;
            _dllType.myCallLearnNormal();
        }*/

        public void SelectedFeature(string selectedItem)
        {
            _featureSelect = selectedItem;
            StringBuilder f1 = new StringBuilder(_featureSelect);
            StringBuilder buffer = new StringBuilder();
            _correlativeFeature = _dll.myGetMyCorrelatedFeature(f1, buffer).ToString();
            ObservableCollection<string> anomaliesViewList = new ObservableCollection<string>();
            int size = _dll.getTimeStepList().Count;
            for (int i = 0; i < size; i++)
            {
                if(_dll.getDescriptionsList()[i].Equals(_featureSelect + '-' + _correlativeFeature))
                {
                    anomaliesViewList.Add("Anomaly at " + _dll.getTimeStepList()[i].ToString());
                }
            }
            (Application.Current as App)._algoritemDetectModel.AddAnomaliesToMyList = anomaliesViewList;
        }

        public void updateAllValues(Dictionary<string, double[]> allValues)
        {
            _allValuesMap = allValues;
        }

        public void updateGraph(Dictionary<string, double[]> valuesMap, int lineIndex)
        {
            if (_featureSelect == "")
                return;
            _plot1.Series.Clear();
            _plot2.Series.Clear();
            _plot3.Series.Clear();
            LineSeries lineSeries = new LineSeries();
            LineSeries lineSeries2 = new LineSeries();
            LineSeries lineSeries3 = new LineSeries { LineStyle = LineStyle.Dot, MarkerType = MarkerType.Circle, MarkerSize = 2, MarkerFill = OxyColors.Gray };
            LineSeries lineSeries3H = new LineSeries { LineStyle = LineStyle.Dot, MarkerType = MarkerType.Circle, MarkerSize = 2, MarkerFill = OxyColors.Red };
            LineSeries lineSeries4 = new LineSeries();
            //StringBuilder f1 = new StringBuilder(_featureSelect);
            //_plot3.Series.Add(new FunctionSeries((x) => Math.Sqrt(Math.Max(16 - Math.Pow(x, 2), 0)), -4, 4, 0.1, "x^2 + y^2 = 16") { Color = OxyColors.Red });
            //_plot3.Series.Add(new FunctionSeries((x) => - Math.Sqrt(Math.Max(16 - Math.Pow(x, 2), 0)), -4, 4, 0.1) { Color = OxyColors.Red });
            //StringBuilder f1Saver = new StringBuilder(_featureSelect);
            //StringBuilder buffer = new StringBuilder();
            //StringBuilder f2 = _dll.myGetMyCorrelatedFeature(f1, buffer);
            _plot1.LegendTitle = _featureSelect;
            _plot2.LegendTitle = _correlativeFeature.ToString();
            _plot3.LegendTitle = "correlattive features";
            if (_correlativeFeature.ToString() != "")
            {
                Line l = AnomalyDetectionUtil.LinearReg(valuesMap[_featureSelect], valuesMap[_correlativeFeature.ToString()], valuesMap[_featureSelect].Length - 1);
                double min = Math.Min(valuesMap[_featureSelect].Min(), valuesMap[_correlativeFeature.ToString()].Min());
                double max = Math.Max(valuesMap[_featureSelect].Max(), valuesMap[_correlativeFeature.ToString()].Max());
                double y1 = l.f(min);
                double y2 = l.f(max);
                lineSeries4.Points.Add(new DataPoint(min, y1));
                lineSeries4.Points.Add(new DataPoint(max, y2));
            }
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
