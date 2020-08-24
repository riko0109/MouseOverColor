using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Threading;

namespace MouseOverColor
{
    public partial class NotifyIconWrapper : Component
    {
        private Icon CurIcon { get; set; }//現在のアイコンのインスタンス
        private Bitmap Bitmap { get; set; }//CurIconのBitmap情報
        private List<Point> ColorChangePoint { get; set; } = new List<Point>();//Bitmapの色変更座標のリスト


        public static MainWindow Window { get; set; } = new MainWindow();//MainWindow操作用インスタンス
        
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public NotifyIconWrapper()
        {
            InitializeComponent();

            //Iconに設定する画像の指定
            using (System.IO.Stream IconStream =
                System.Windows.Application.GetResourceStream(new Uri("Pack://application:,,,/MouseOverColor;component/ico/multimeda.ico")).Stream)
            {
                notifyIcon1.Icon = new System.Drawing.Icon(IconStream);
            }
            CurIcon = notifyIcon1.Icon;
            Bitmap = notifyIcon1.Icon.ToBitmap();

            //Iconの左半分を色変更アドレスに設定
            for (int x = 0; x <= 63; x++)
            {
                for (int y = 0; y <= Bitmap.Height - 1; y++)
                {
                    ColorChangePoint.Add(new Point(x, y));

                }
            }
            
            //Iconの右半分を白色に変更
            for (int x = 64; x <= 127; x++)
            {
                for (int y = 0; y <= 127; y++)
                {
                    Bitmap.SetPixel(x, y, System.Drawing.Color.White);
                }
            }
            
            //timerの設定
            var timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);//10ミリ秒毎（0.01秒毎）
            timer.Start();
                
            //イベントハンドラの登録
            ShowWindow.Click += ToolStripMenuItemShow_Click;
            Exit.Click += ToolStripMenuItemExit_Click;
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// 引数ありコンストラクタ
        /// </summary>
        /// <param name="container"></param>
        public NotifyIconWrapper(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        
        
        //アイコン破棄用dll
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);
        
        /// <summary>
        /// アイコン画像変更メソッド
        /// </summary>
        private void ChangeIcon()
        {
            var G = new MouseOverColorGetter();
            var c = G.GetedDrawingColor;

            for (int x = 0; x<ColorChangePoint.Count;x++)
            {
                    Bitmap.SetPixel(ColorChangePoint[x].X,ColorChangePoint[x].Y, c); 
            }

            notifyIcon1.Icon = Icon.FromHandle(Bitmap.GetHicon());
            DestroyIcon(notifyIcon1.Icon.Handle);
            DestroyIcon(CurIcon.Handle);
        }

        /// <summary>
        /// コンテキストメニューのExitが押下された時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// コンテキストメニューのShowWindowが押下された時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemShow_Click(object sender, EventArgs e)
        {
            Window.WindowState = System.Windows.WindowState.Normal;
            Window.ShowActivated = true;
            Window.ShowInTaskbar = true;
            Window.Show();
        }
        
        /// <summary>
        /// アイコンがダブルクリックされた時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Window.WindowState = System.Windows.WindowState.Normal;
            Window.ShowActivated = true;
            Window.ShowInTaskbar = true;
            Window.Show();
        }

        /// <summary>
        /// タイマーの作動時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender,EventArgs e)
        {
            ChangeIcon();
        }
    }
}
