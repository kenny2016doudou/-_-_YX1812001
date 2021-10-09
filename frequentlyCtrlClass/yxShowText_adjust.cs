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
    public partial class yxShowText_adjust : UserControl
    {
        private Double _yzValue = 1.0;
        private string smStr = "注释说明";
        private Double myValue = 0;
        private byte xsCount = 1;
        private string dwStr = "单位";
        private Size ttsize = new Size();
        private byte fillIndex = 1;

        public yxShowText_adjust()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            updateShow();
        }
        public void SetValue(string valueStr)
        {
            Value = double.Parse(valueStr);
        }
        public override String Text
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                try
                {
                    double ttDouble = double.Parse(value);
                    myValue = ttDouble;
                }
                catch
                { }
            }
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
                if (fillIndex == 1)
                {
                    textBox1.Dock = DockStyle.Bottom;
                }
                else if (fillIndex == 3)
                {
                    textBox1.Dock = DockStyle.Top;
                }
                else
                {
                    textBox1.Dock = DockStyle.Bottom;
                }
                this.Refresh();
            }
        }

        public Font show_字体
        {
            get
            {
                return textBox1.Font;
            }
            set
            {
                textBox1.Font = value; 
            }
        }

        public string str_注释说明
        {
            get
            {
                return smStr;
            }
            set
            {
                smStr = value;
                this.Refresh();
            }
        }
        public Double yzValue
        {
            get
            {
                return _yzValue;
            }
            set
            {
                _yzValue = value; 
            }
        }
        public Double Value
        {
            get
            {
                return myValue;
            }
            set
            {
                myValue = value * _yzValue;
                updateShow();
            }
        }

        public byte byte_小数位
        {
            get
            {
                return xsCount;
            }
            set
            {
                xsCount = value;
                updateShow();
            }
        }

        public string str_单位
        {
            get
            {
                return dwStr;
            }
            set
            {
                dwStr = value;
                updateShow();
            }
        }

        private void updateShow()
        {
            string tempstr = "0";
            if (xsCount >= 1)
            {
                tempstr += ".";
                for (int i = 0; i < xsCount; i++)
                    tempstr += "0";
            }
            textBox1.Text = myValue.ToString(tempstr) + dwStr;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text.Length <= 0)
            {
                this.Value = 0;
            }
        }

        private void yxShowText_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            //this.
            //yxShowText_Click(this, e);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            //yxShowText_Click(this, e);
        }

        private void yxShowText_Paint(object sender, PaintEventArgs e)
        {
            ttsize = TextRenderer.MeasureText(e.Graphics, smStr, SystemFonts.CaptionFont);
            if (fillIndex == 1)
            {
                e.Graphics.DrawString(smStr, SystemFonts.CaptionFont, Brushes.Black, (this.Width - ttsize.Width) / 2, (this.Height - ttsize.Height - textBox1.Height) / 2);
            }
            else if (fillIndex == 3)
            {
                e.Graphics.DrawString(smStr, SystemFonts.CaptionFont, Brushes.Black, (this.Width - ttsize.Width) / 2, (this.Height - ttsize.Height + textBox1.Height) / 2);
            }
            else
            {
                e.Graphics.DrawString(smStr, SystemFonts.CaptionFont, Brushes.Black, (this.Width - ttsize.Width) / 2, (this.Height - ttsize.Height - textBox1.Height) / 2);
            }
           
            
        }

    }


}
