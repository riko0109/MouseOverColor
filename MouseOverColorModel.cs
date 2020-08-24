using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MouseOverColor
{
    class MouseOverColorModel:INotifyPropertyChanged
    {

        private Color _MouseOverColor;
        private Color _MouseOverColorFontColor;
        private Color _LatestClickedPointColor;
        private Color _LatestClickedPointFontColor;
        private string _MousePosition;

        public Color MouseOverColor
        {
            get { return _MouseOverColor; }
            set
            {
                _MouseOverColor = value;
                MouseOverFontColor = GetAntiColor(_MouseOverColor);
                RaisePropertyChanged(nameof(MouseOverColor));
            }
        }
        public Color MouseOverFontColor
        {
            get { return GetAntiColor(MouseOverColor); }
            set
            {
                _MouseOverColorFontColor = value;
                RaisePropertyChanged(nameof(MouseOverFontColor));
            }
        }
        public Color LatestClickedPointColor
        {
            get { return _LatestClickedPointColor; }
            set
            {
                _LatestClickedPointColor = value;
                LatestClickedPointFontColor = GetAntiColor(_LatestClickedPointColor);
                RaisePropertyChanged(nameof(LatestClickedPointColor));
            }
        }
        public Color LatestClickedPointFontColor
        {
            get { return GetAntiColor(_LatestClickedPointColor); }
            set
            {
                _LatestClickedPointFontColor = value;
                RaisePropertyChanged(nameof(LatestClickedPointFontColor));
            }
        }
        public string MousePosition
        {
            get { return _MousePosition; }
            set
            {
                _MousePosition = value;
                RaisePropertyChanged(nameof(MousePosition));
            }
        }
          
        public MouseOverColorModel()
        {
            //タイマーの設定、一定時間ごとにマウスカーソルの状態を見る
            var timer = new DispatcherTimer(DispatcherPriority.Normal);
            //timer.Interval = new TimeSpan(100000);//ナノ秒
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);//10ミリ秒毎（0.01秒毎）
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private Color GetAntiColor(Color c)
        {
            var ReturnColor = new Color();
            ReturnColor.A = 255;
            if (MouseOverColor.R + MouseOverColor.G + MouseOverColor.B <= 382)
            {
                ReturnColor.R = 255;
                ReturnColor.G = 255;
                ReturnColor.B = 255;

            }
            else if (MouseOverColor.R + MouseOverColor.G + MouseOverColor.B >= 383)
            {
                ReturnColor.R = 0;
                ReturnColor.G = 0;
                ReturnColor.B = 0;
            }

            return ReturnColor;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            System.Drawing.Point p = System.Windows.Forms.Cursor.Position;//マウスカーソル位置取得
            var G = new MouseOverColorGetter();
            System.Windows.Media.Color c = G.GetedMediaColor;//マウスカーソル位置の色取得
            var b = new SolidColorBrush(c);
            MouseOverColor = c;
            MousePosition = $"マウスの位置 = {System.Windows.Forms.Cursor.Position}";

            if (IsClickDown())
            {
                LatestClickedPointColor = c;
            }
        }

        //クリックされているか判定用
        [System.Runtime.InteropServices.DllImport("user32.dll")] private static extern short GetKeyState(int nVirtkey);
        //クリック判定
        private bool IsClickDown()
        {
            //マウス左ボタン(0x01)の状態、押されていたらマイナス値(-127)、なかったら0
            return GetKeyState(0x01) < 0;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
