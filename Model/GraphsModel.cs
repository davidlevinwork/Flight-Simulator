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
        private int _numOfValues = 0;
        private FunctionsDLL _functionsDll = (Application.Current as App)._functionsDLL;
        private Dictionary<string, double[]> _allValuesMap;
        public GraphsModel()
        {
            _allValuesMap = new Dictionary<string, double[]>();
            _plot1 = new PlotModel();
            _plot2 = new PlotModel();
            _plot3 = new PlotModel();
            //_plot1.LegendTitle = "Legend";
            _plot1.LegendOrientation = LegendOrientation.Horizontal;
            _plot1.LegendPlacement = LegendPlacement.Outside;
            _plot1.LegendPosition = LegendPosition.TopLeft;
            //_plot1.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            _plot1.LegendBorder = OxyColors.Black;
           // var dateAxis = new DateTimeAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 100 };
            //_plot1.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };
            _plot1.Axes.Add(valueAxis);

            _plot2.LegendOrientation = LegendOrientation.Horizontal;
            _plot2.LegendPlacement = LegendPlacement.Outside;
            _plot2.LegendPosition = LegendPosition.TopLeft;
            //_plot1.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            _plot2.LegendBorder = OxyColors.Black;

            var valueAxis2 = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };
            _plot2.Axes.Add(valueAxis2);
        }

        public void SelectedFeature(string selectedItem)
        {
            _featureSelect = selectedItem;
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
            LineSeries lineSeries = new LineSeries();
            LineSeries lineSeries2 = new LineSeries();
            if (lineIndex < 40)
            {
                for (int i = 0; i <= lineIndex; i++)
                {
                    lineSeries.Points.Add(new DataPoint(i, valuesMap[_featureSelect][i]));
                    lineSeries2.Points.Add(new DataPoint(i, valuesMap[_featureSelect][i]));
                }
            }
            else
            {
                StringBuilder f1 = new StringBuilder(_featureSelect);
                StringBuilder buffer = new StringBuilder();
                StringBuilder f2 = _functionsDll.myGetMyCorrelatedFeature(f1, buffer);
                for (int i = lineIndex - 40; i <= lineIndex; i++)
                {
                    lineSeries.Points.Add(new DataPoint(i, valuesMap[_featureSelect][i]));
                    if(f2.ToString() != "")
                        lineSeries2.Points.Add(new DataPoint(i, valuesMap[buffer.ToString()][i]));
                }
            }
            _plot1.Series.Add(lineSeries);
            _plot2.Series.Add(lineSeries2);
            _plot1.InvalidatePlot(true);
            _plot2.InvalidatePlot(true);
            PlotFeature1 = _plot1;
            PlotFeature2 = _plot2;
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
