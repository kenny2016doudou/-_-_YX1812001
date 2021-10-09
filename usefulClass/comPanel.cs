using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using usefulClass;
#pragma warning disable CS0246 // 未能找到类型或命名空间名“MySql”(是否缺少 using 指令或程序集引用?)
using MySql.Data.MySqlClient;
#pragma warning restore CS0246 // 未能找到类型或命名空间名“MySql”(是否缺少 using 指令或程序集引用?)
namespace usefulClass
{
    public partial class comPanel : UserControl
    {
        // 创建一个委托，返回类型为void，两个参数
        public delegate void viewbxt(int myindex);
        // 将创建的委托和特定事件关联,在这里特定的事件为OnKeyDown
        public event viewbxt Onviewbxt;

        public string timestr = "";
        public string startindexstr = "";
        public string endindexstr = "";
        public string testindexstr = "";

        //合闸时间
        public string HZz = "0.0";


      

        public bool ifOpen = false;
        public Image fzckimg = null, hzckimg = null, bhimg = null;
        public bool ifOpen2 = true;
        private string _SaveTag = "";
        private string hzTimes = "0";
        private string hzFactStates = "合";
        private string fzTimes = "0";
        private string fzFactStates = "合";
        private string _dzValue = "0";
        private string _testValue = "正常";
        private float hgpdValue = 180;
        public comPanel()
        {
            InitializeComponent();
        }
        public comPanel(float p_hgpdValue)
        {
            InitializeComponent();
            hgpdValue = p_hgpdValue;
        }
        public bool set_States_hz
        {
            get
            {
                return ifOpen;
            }
            set
            {
                if (fzckimg == null)
                {
                    fzckimg = Image.FromFile(Application.StartupPath + "\\fzck.jpg");
                    hzckimg = Image.FromFile(Application.StartupPath + "\\hzck.jpg");
                    bhimg = Image.FromFile(Application.StartupPath + "\\bh.jpg");
                }
                ifOpen = value;

                if (int.Parse(Tag.ToString()) % 2 == 1)
                {
                    if (!ifOpen)
                        pictureBox3.Image = bhimg; //fzckimg;
                    else
                        pictureBox3.Image = hzckimg;//bhimg
                }
                else
                {
                    if (ifOpen)
                        pictureBox3.Image = fzckimg;// fzckimg;
                    else
                        pictureBox3.Image = bhimg;//bhimg

                }

            }
        }
        public bool set_States_fz
        {
            get
            {
                return ifOpen2;
            }
            set
            {
                if (fzckimg == null)
                {
                    fzckimg = Image.FromFile(Application.StartupPath + "\\fzck.jpg");
                    hzckimg = Image.FromFile(Application.StartupPath + "\\hzck.jpg");
                    bhimg = Image.FromFile(Application.StartupPath + "\\bh.jpg");
                }
                ifOpen2 = value;
                if (int.Parse(Tag.ToString()) % 2 == 1)
                {
                    if (!ifOpen2)
                        pictureBox3.Image = fzckimg; //fzckimg;
                    else
                        pictureBox3.Image = bhimg;//bhimg
                }
                else
                {
                    if (!ifOpen2)
                        pictureBox3.Image = hzckimg;// fzckimg;
                    else
                        pictureBox3.Image = bhimg;//bhimg

                }
            }
        }
        //辅助触点判断
        public bool set_States3
        {
            get
            {
                return ifOpen2;
            }
            set
            {
                if (fzckimg == null)
                {
                    fzckimg = Image.FromFile(Application.StartupPath + "\\fzck.jpg");
                    hzckimg = Image.FromFile(Application.StartupPath + "\\hzck.jpg");
                    bhimg = Image.FromFile(Application.StartupPath + "\\bh.jpg");
                }
                ifOpen2 = value;

                //    if (ifOpen2)
                //    {
                //        pictureBox3.Image = fzckimg; 
                //    }//fzckimg;
                //    else
                //        pictureBox3.Image = bhimg;//bhimg
                //}
                if (fzcdtimes_static.hzfz == "hz")
                {
                    if (int.Parse(Tag.ToString()) % 2 == 1)
                    {
                        if (ifOpen2)
                        {
                            pictureBox3.Image = fzckimg; //fzckimg;
                            //textBox9.BackColor = Color.Red;
                        }
                        else
                        {
                            pictureBox3.Image = bhimg; //bhimg
                            textBox9.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        if (!ifOpen2)
                        {
                            pictureBox3.Image = hzckimg;// hzckimg;
                            textBox9.BackColor = Color.Red;
                        }
                        else
                        {
                            pictureBox3.Image = bhimg;//bhimg
                            //textBox9.BackColor = Color.Red;
                        }

                    }
                }
                if (fzcdtimes_static.hzfz == "fz")
                {
                    if (int.Parse(Tag.ToString()) % 2 == 1)
                    {
                        if (!ifOpen2)
                        {
                            pictureBox3.Image = fzckimg; //fzckimg;
                            textBox9.BackColor = Color.Red;
                        }
                        else
                        {
                            pictureBox3.Image = bhimg; //bhimg
                            //textBox9.BackColor = Color.Lime;
                        }
                    }
                    else
                    {
                        if (ifOpen2)
                        {
                            pictureBox3.Image = hzckimg;// hzckimg;
                            //textBox9.BackColor = Color.Red;
                        }
                        else
                        {
                            pictureBox3.Image = bhimg;//bhimg
                            textBox9.BackColor = Color.Red;
                        }

                    }
                }
            }
        }

        public string set_endindexstr
        {
            get
            {
                return endindexstr;
            }
            set
            {
                if (value.CompareTo("0") == 0 || value.CompareTo("00") == 0)
                    return;
                endindexstr = value;
                label36.Text = endindexstr;
            }
        }
        public string set_testindexstr
        {
            get
            {
                return testindexstr;
            }
            set
            {
                testindexstr = value;
            }
        }
        public string SaveTag
        {
            get
            {
                return _SaveTag;
            }
        }
        public string dzValue
        {
            set
            {
                _dzValue = value;
                updateSaveTag();
            }
        }

        public string set_startindexstr
        {
            get
            {
                return startindexstr;
            }
            set
            {
                if (value.CompareTo("0") == 0 || value.CompareTo("00") == 0)
                    return;
                startindexstr = value;
                label26.Text = startindexstr;
            }
        }
        public void updateSaveTag()
        {
            _SaveTag = startindexstr + "_" + endindexstr + ":" + hzFactStates + ":" + hzTimes + ":" + fzFactStates + ":" + fzTimes + ":" + _dzValue + ":" + _testValue;
        }

        public bool set_fhFactState
        {
            set
            {
                if (fzcdtimes_static.hzfz == "hz")
                {
                    if (value)
                        hzFactStates = "合";
                    else
                        hzFactStates = "分";
                }
                else if (fzcdtimes_static.hzfz == "fz")
                {
                    if (value)
                        fzFactStates = "合";
                    else
                        fzFactStates = "分";
                }
                updateSaveTag();
            }
        }
        delegate void UpdateUIDelegate();


        public void sj(string hz)
        {
            HZz = hz;
        }
       
        public string set_time
        {
            get
            {
                return timestr;
            }
            set
            {

                timestr = value;
                //this.Invoke((UpdateUIDelegate)delegate()
                //{
                if (timestr != null)
                {

                   
                    textBox9.Text = float.Parse(timestr).ToString("0.0");
                    if (float.Parse(timestr) >= 100.0 || float.Parse(timestr) <= 0.0)
                    {
                       
                        textBox9.BackColor = Color.Red;
                    }
                    else
                    {
                        textBox9.BackColor = Color.White;
                    }
                }
                else
                    textBox9.Text = "0.0";

                if (fzcdtimes_static.hzfz == "hz")
                {
                    hzTimes = timestr;
                }
                else if (fzcdtimes_static.hzfz == "fz")
                {
                    fzTimes = timestr;
                }
                updateSaveTag();
                //});//委托处理
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (startindexstr.Length <= 0 || startindexstr == "0")
            {
                MessageBox.Show("此辅助触点联锁状态未启用检测功能!");
                return;
            }
            if (float.Parse(textBox9.Text) <= 0)
            {
                MessageBox.Show("未检测到时间值或者没有波形数据!");
                return;
            }
            Onviewbxt(int.Parse(Tag.ToString()) - 2);
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(testindexstr.ToString(), SystemFonts.DefaultFont, Brushes.Blue, new PointF(0, 0));
            //e.Graphics.DrawString(hgpdValue.ToString("0"), SystemFonts.DefaultFont, Brushes.Red, new PointF(40, 15));


        }
    }
}
