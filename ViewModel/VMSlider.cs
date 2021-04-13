using SimolatorDesktopApp_1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimolatorDesktopApp_1.ViewModel
{
    /*
     * Class ViewModel VMSlider.
     */
    public class VMSlider : INotifyPropertyChanged
    {
        private SliderModel _SliderModel;
        public event PropertyChangedEventHandler PropertyChanged;

        /*
         * Constructor VMSlider
         */
        public VMSlider(SliderModel sliderModel)
        {
            _SliderModel = sliderModel;
            _SliderModel.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    INotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        /*
         * Property VM_IndexLine
         */
        public int VM_IndexLine
        {
            get { return _SliderModel.IndexLine; }
            set { _SliderModel.IndexLine = value; }
        }

        /*
         * Property VM_MaxLine
         */
        public int VM_MaxLine
        {
            get { return _SliderModel.MaxLine; }
        }

        /*
         * Property VM_Speed
         */
        public double VM_Speed
        {
            get { return _SliderModel.Speed; }
            set { _SliderModel.Speed = value; }
        }

        /*
         * Property VM_TimerString
         */
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

        /*
         * when the user pressed on play button
         */
        public void VMplay(string pathCsv)
        {
            _SliderModel.play(pathCsv);
        }

        /*
         * when the user uploaded his csv file.
         */
        internal void VMUploadCsvFile(string pathCsv)
        {
            _SliderModel.uploadCsvFile(pathCsv);
        }

        /*
         * when the user pressed on stop button.
         */
        public void VMstop()
        {
            _SliderModel.stop();
        }

        /*
         * when the user moves the slider button or pressed on the left or right jump.
         */
        public void VMupdateIndexSlider(int index)
        {
            _SliderModel.updateIndexSlider(index);
        }

        /*
         * when the user pressed on pause button.
         */
        internal void VMpause()
        {
            _SliderModel.pause();

        }

        /*
         * when the user changed the speed.
         */
        internal void VMsetSpeed(double speedRate)
        {
            _SliderModel.Speed = speedRate;
        }
    }
}
