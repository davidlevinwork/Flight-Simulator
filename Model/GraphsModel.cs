using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private double[] values = new double[2500];
        private int _numOfValues = 0;
        private Dictionary<string, double[]> _allValuesMap = new Dictionary<string, double[]>();
        public GraphsModel()
        {
            _plot1 = new PlotModel();
            _plot2 = new PlotModel();
            _plot3 = new PlotModel();
            _plot1.LegendTitle = "Legend";
            _plot1.LegendOrientation = LegendOrientation.Horizontal;
            _plot1.LegendPlacement = LegendPlacement.Outside;
            _plot1.LegendPosition = LegendPosition.TopRight;
            _plot1.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            _plot1.LegendBorder = OxyColors.Black;
            var dateAxis = new DateTimeAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            _plot1.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };
            _plot1.Axes.Add(valueAxis);

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
            
            _plot1.Series.Clear();
            LineSeries lineSeries = new LineSeries();
            if (lineIndex < 70)
            {
                for (int i = 0; i <= lineIndex; i++)
                {
                    lineSeries.Points.Add(new DataPoint(i, valuesMap[_featureSelect][i]));
                }
            }
            else
            {
                for (int i = lineIndex - 70; i <= lineIndex; i++)
                {
                    lineSeries.Points.Add(new DataPoint(i, valuesMap[_featureSelect][i]));
                }
            }
            _plot1.Series.Add(lineSeries);
            _plot1.InvalidatePlot(true);
            PlotFeature1 = _plot1;
            ////double value = double.Parse(lineValues[featuresMap.FirstOrDefault(x => x.Value == _featureSelect).Key], CultureInfo.InvariantCulture);
            //// values[_numOfValues++] = value;
            //LineSeries lineSeries = new LineSeries();

            //for (int i = 0; i < _numOfValues; i++)
            //{
            //    lineSeries.Points.Add(new DataPoint(i, values[i]));
            //    Console.WriteLine(values[i]);
            //}
            //Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            //// lineSeries.Title = Limi
            //_plot1.Series.Add(lineSeries);
            ////var series1 = new LineSeries { Title = "Series 1", MarkerType = MarkerType.Circle };
            ////series1.Points.Add(new DataPoint((double)lineIndex/10.0, value));
            //_plot1.InvalidatePlot(true);
            //Console.WriteLine((double)lineIndex / 10.0);
            //// Console.WriteLine(value);
            //PlotFeature1 = _plot1;
            ////var series2 = new LineSeries { Title = "Series 2", MarkerType = MarkerType.Square };

            //// _plot1.Series.Add(series2);
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
