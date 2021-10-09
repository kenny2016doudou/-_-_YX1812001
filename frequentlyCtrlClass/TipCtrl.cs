using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace frequentlyCtrlClass
{
    public partial class TipCtrl : UserControl
    {
        public TipCtrl()
        {
            InitializeComponent();
        }

        public string tipDesc
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }


        public String _TYPES = "运行";
        public string types
        {
            get
            {
                return _TYPES;
            }
            set
            {
                _TYPES = value;
                if (_TYPES.Contains("运行"))
                {
                    ovalShape7.FillColor = Color.Lime;
                }
                else
                {
                    ovalShape7.FillColor = Color.Red;
                }
            }
        }

        public bool ActiveValue()
        {
            if (ovalShape7.FillColor == Color.Lime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ActiveTip(String p_Value)
        {
            if (_TYPES.Contains("运行"))
            {
                if (p_Value.Contains("1") || p_Value.ToUpper().Contains("TRUE"))
                {
                    ovalShape7.FillColor = Color.Lime;
                }
                else
                {
                    ovalShape7.FillColor = Color.Red;
                }
            }
            else
            {
                if (p_Value.Contains("1") || p_Value.ToUpper().Contains("TRUE"))
                {
                    ovalShape7.FillColor = Color.Red;
                }
                else
                {
                    ovalShape7.FillColor = Color.Lime;
                }
            }
        }
        public void ActiveTip(bool p_Value)
        {
            if (_TYPES.Contains("运行"))
            {
                if (p_Value)
                {
                    ovalShape7.FillColor = Color.Lime;
                }
                else
                {
                    ovalShape7.FillColor = Color.Red;
                }
            }
            else
            {
                if (p_Value)
                {
                    ovalShape7.FillColor = Color.Red;
                }
                else
                {
                    ovalShape7.FillColor = Color.Lime;
                }
            }
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            int minValue = panel1.Width > panel1.Height ? panel1.Height : panel1.Width;
            ovalShape7.SetBounds((panel1.Width - minValue) / 2, (panel1.Height - minValue) / 2, minValue - 2, minValue - 2);
        }

        private void TipCtrl_SizeChanged(object sender, EventArgs e)
        {
            panel1_SizeChanged(sender, e);
        }

    }
}
