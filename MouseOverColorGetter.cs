using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseOverColor
{
    class MouseOverColorGetter
    {
        public System.Drawing.Color GetedDrawingColor { get; set; }//取得したDrawingColor
        public System.Windows.Media.Color GetedMediaColor//GetedDrawingColorをMediaColorに変換するGetter
        {
            get
            {
                return System.Windows.Media.Color.FromArgb(
                                                            GetedDrawingColor.A, 
                                                            GetedDrawingColor.R,
                                                            GetedDrawingColor.G, 
                                                            GetedDrawingColor.B
                                                            );
            }
        }

        public MouseOverColorGetter()
        {
            //マウスカーソル位置取得
            System.Drawing.Point p = System.Windows.Forms.Cursor.Position;

            //1x1サイズのBitmap作成
            using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(
                1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (var bmpGraphics = System.Drawing.Graphics.FromImage(bitmap))
                {
                    //画面全体をキャプチャして指定座標の1ピクセルだけBitmapにコピー
                    bmpGraphics.CopyFromScreen(p.X, p.Y, 0, 0, new System.Drawing.Size(1, 1));
                    //ピクセルの色取得
                    GetedDrawingColor = bitmap.GetPixel(0, 0);

                }
            }
        }

    }
}
