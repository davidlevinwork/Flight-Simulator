using SimolatorDesktopApp_1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimolatorDesktopApp_1.ViewModel
{
    public class VMSlider : INotifyPropertyChanged
    {
        private SliderModel _SliderModel;
        public event PropertyChangedEventHandler PropertyChanged;

        public VMSlider(SliderModel sliderModel)
        {
            _SliderModel = sliderModel;
            sliderModel.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    INotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public int VM_IndexLine
        {
            get { return _SliderModel.IndexLine; }
            set { _SliderModel.IndexLine = value; }
        }

        public int VM_MaxLine
        {
            get { return _SliderModel.MaxLine; }
        }

        public double VM_Speed
        {
            get { return _SliderModel.Speed; }
            set { _SliderModel.Speed = value; }
        }

        public string VM_TimerString
        {
            get {
                return _SliderModel.TimerString; 
            }
            set {
                _SliderModel.TimerString = value; 
            }
        }

        private void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void VMplay(string pathCsv)
        {
            _SliderModel.play(pathCsv);
        }

        internal void VMUploadCsvFile(string pathCsv)
        {
            _SliderModel.uploadCsvFile(pathCsv);
        }

        public void VMstop()
        {
            _SliderModel.stop();
        }

        public void VMupdateIndexSlider(int index)
        {
            _SliderModel.updateIndexSlider(index);
        }

        internal void VMpause()
        {
            _SliderModel.pause();

        }

        internal void VMsetSpeed(double speedRate)
        {
            _SliderModel.Speed = speedRate;
        }


    }
}
