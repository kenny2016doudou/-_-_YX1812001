using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace frequentlyCtrlClass
{
    public partial class yxLedRound : UserControl
    {
        private Size ttsize = new Size();
        private bool myState = false; 
        private byte fillIndex = 0;
        private float _fontsize = 9;

        private string xxtypes = "信息提示"; //信息提示，危险警示
        public string str_提示类型
        {
            get
            {
                return xxtypes;
            }
            set
            {
                xxtypes = value.Trim();
                Active = myState;
            }
        }
        private void toUpdateUI()
        {
            if (label1.Text.Length <= 0)
            {
                panel1.Dock = DockStyle.Fill;
                label1.Height = 0;
            }
            else
            {
                ttsize = TextRenderer.MeasureText(Graphics.FromHwnd(this.Handle), label1.Text, label1.Font);
                label1.Width = ttsize.Width;
                label1.Height = ttsize.Height;
            }
        }

        public float fontsize
        {
            get
            {
                return _fontsize;
            }
            set
            {
                _fontsize = value;
                label1.Font = new Font(label1.Font.FontFamily, _fontsize);
                toUpdateUI();
            }
        }

        public yxLedRound()
        {
            InitializeComponent();
        }
        public byte byte_对齐方式
        {
            get
            {
                return fillIndex;
            }
            set
            {
                fillIndex = value;
                toUpdateUI();
                if (fillIndex == 0)
                {
                    label1.Dock = DockStyle.Bottom;
                }
                else if (fillIndex == 2)
                {
                    label1.Dock = DockStyle.Top;
                }
                else if (fillIndex == 1)
                {
                    label1.Dock = DockStyle.Left;
                }
                else if (fillIndex == 3)
                {
                    label1.Dock = DockStyle.Right;
                }
                this.Refresh();
            }
        }

        public string str_注释说明
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value.Trim();
                toUpdateUI();
                this.Refresh();
            }
        }

        public bool Active
        {
            get
            {
                return myState;
            }
            set
            {
                myState = value;
                if (xxtypes.CompareTo("危险警示") == 0)
                {
                    if (myState)
                    {
                        panel1.BackColor = Color.Red;
                    }
                    else
                    {
                        panel1.BackColor = Color.Maroon;
                    }
                }
                else
                {
                    if (myState)
                    {
                        panel1.BackColor = Color.Lime;
                    }
                    else
                    {
                        panel1.BackColor = Color.DarkGreen;
                    }
                }
            }
        }
        private String whatTrue = "";
        public void SetActiveDesc(bool iftrue,string _whatTrue)
        {
            myState = iftrue;
            if (iftrue)
            {
                whatTrue = _whatTrue;
            }
            else
            {
                whatTrue = "";
            }
        }

        private void yxLedRound_Paint(object sender, PaintEventArgs e)
        {
            //ttsize = TextRenderer.MeasureText(e.Graphics, smStr, SystemFonts.CaptionFont);
            //if (fillIndex == 1)
            //{
            //    e.Graphics.DrawString(smStr, SystemFonts.CaptionFont, Brushes.Black, (this.Width - ttsize.Width - panel1.Width) / 2 + panel1.Width, (this.Height - ttsize.Height) / 2);
            //}
            //else if (fillIndex == 3)
            //{
            //    e.Graphics.DrawString(smStr, SystemFonts.CaptionFont, Brushes.Black, (this.Width - ttsize.Width - panel1.Width) / 2, (this.Height - ttsize.Height) / 2);
            //}
            //else if (fillIndex == 2)
            //{
            //    e.Graphics.DrawString(smStr, SystemFonts.CaptionFont, Brushes.Black, (this.Width - ttsize.Width) / 2, (this.Height - panel1.Height - ttsize.Height) / 2 + panel1.Height);
            //}
            //else if (fillIndex == 4)
            //{
            //    e.Graphics.DrawString(smStr, SystemFonts.CaptionFont, Brushes.Black, (this.Width - ttsize.Width) / 2, (this.Height - panel1.Height - ttsize.Height) / 2);
            //}
            //else
            //{
            //    e.Graphics.DrawString(smStr, SystemFonts.CaptionFont, Brushes.Black, (this.Width - ttsize.Width) / 2 + panel1.Width, (this.Height / 2 - ttsize.Height) / 2);
            //}
            //panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, panel1.Width, panel1.Height);
            panel1.Region = new Region(path);
            if (whatTrue.Length > 0)
            {
              //  e.Graphics.DrawString(whatTrue, SystemFonts.DefaultFont, Brushes.DeepSkyBlue,9);
            }
        }

        private void yxLedRound_SizeChanged(object sender, EventArgs e)
        {
            byte_对齐方式 = byte_对齐方式;
        }
    }


}
