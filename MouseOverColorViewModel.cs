using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace MouseOverColor
{
    public class MouseOverColorViewModel:INotifyPropertyChanged
    {
        private MouseOverColorModel MOCM;

        public string MouseOverColorText
        {
            get
            {
                return MOCM.MouseOverColor.ToString();
            }
        }
        public string MouseOverFontColor
        {
            get
            {
                return MOCM.MouseOverFontColor.ToString();
            }
        }
        public string LatestClickedPointColorText
        {
            get
            {
                return MOCM.LatestClickedPointColor.ToString();
            }
        }
        public string LatestClickedPointFontColor
        {
            get
            {
                return MOCM.LatestClickedPointFontColor.ToString();
            }
        }
        public string MousePosition
        {
            get
            {
                return MOCM.MousePosition;
            }
            set
            {
                MOCM.MousePosition = value;
                RaisePropertyChanged(nameof(MousePosition));
            }
        }

        public MouseOverColorViewModel()
        {
            MOCM = new MouseOverColorModel();
            MOCM.PropertyChanged += PropertyUpdate;
        }

        private void PropertyUpdate(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "MouseOverColor":
                    RaisePropertyChanged(nameof(MouseOverColorText));
                    break;

                case "MouseOverFontColor":
                    RaisePropertyChanged(nameof(MouseOverFontColor));
                    break;

                case "LatestClickedPointColor":
                    RaisePropertyChanged(nameof(LatestClickedPointColorText));
                    break;

                case "LatestClickedPointFontColor":
                    RaisePropertyChanged(nameof(LatestClickedPointFontColor));
                    break;

                case "MousePosition":
                    RaisePropertyChanged(nameof(MousePosition));
                    break;
            }

            RaisePropertyChanged(e.PropertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    }
}
