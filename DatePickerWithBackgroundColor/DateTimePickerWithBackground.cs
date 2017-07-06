using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatePickerWithBackgroundColor
{
    public class DateTimePickerWithBackground : DateTimePicker
    {
        private Bitmap _bmp;

        enum BorderSize
        {
            One = 1,
            Two = 2
        };

        public DateTimePickerWithBackground()
        {
            _bmp = new Bitmap(ClientRectangle.Height, ClientRectangle.Width);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0xf) //WM_PAINT message
            {   
                Graphics g = Graphics.FromHwnd(m.HWnd);
                g.DrawImage(_bmp, ClientRectangle.Width - Convert.ToInt32(9 * ClientRectangle.Width / 100), 2);
                g.Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(new SolidBrush(Color.LawnGreen), ClientRectangle);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                   Color.LightGray, (int)BorderSize.One, ButtonBorderStyle.Solid,
                                   Color.LightGray, (int)BorderSize.One, ButtonBorderStyle.Solid,
                                   Color.LightGray, (int)BorderSize.One, ButtonBorderStyle.Solid,
                                   Color.LightGray, (int)BorderSize.One, ButtonBorderStyle.Solid);
            TextRenderer.DrawText(e.Graphics, Text, Font, Rectangle.FromLTRB(0, 0, Width - Height, Height), 
                SystemColors.ControlText);
            Image img = Properties.Resources.calendar_picker;
            _bmp = new Bitmap(img, new Size(img.Width, img.Height));
        }
    }
}
