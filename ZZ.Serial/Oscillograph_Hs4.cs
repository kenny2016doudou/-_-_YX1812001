using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace ZZ.Serial
{
    public class Oscillograph_Hs4
    {


        private double _Frequency;
        private ushort _PostSamples;
        private uint _RecordLength;
        private double _Sensitivity;

        /// <summary>
        /// 初始化波形
        /// </summary>
        public void Initialize()
        {
            if (ZZ.Serial.HsOperation.InitInstrument(0x308) == ZZ.Serial.HsOperation.E_NO_ERRORS)
            {
                _RecordLength = 2000;
                _PostSamples =  1000;
                _Frequency = 250000;
                _Sensitivity =2;
                int serialNumber = 0;

                ZZ.Serial.HsOperation.SetRecordLength(_RecordLength);
                ZZ.Serial.HsOperation.SetPostSamples(_PostSamples);
                ZZ.Serial.HsOperation.SetSampleFrequencyF(ref _Frequency);
                ZZ.Serial.HsOperation.SetSensitivity(1, ref _Sensitivity);

                ZZ.Serial.HsOperation.GetSerialNumber(ref serialNumber);
                ZZ.Serial.HsOperation.SetDataReadyCallback(ZZ.Serial.HsOperation.callBack);
                ZZ.Serial.HsOperation.SetTransferMode(ZZ.Serial.HsOperation.ltmStream);
                ZZ.Serial.HsOperation.SetCoupling(1, 1);
                _RecordLength =Convert.ToUInt16( ZZ.Serial.HsOperation.GetRecordLength());
                ZZ.Serial.HsOperation.GetSensitivity(1, ref _Sensitivity);

            }
            else
            {
                //txbInitialize.Text = "初始化失败！";
            }
        }


        /// <summary>
        /// 画图并且采集数据
        /// </summary>
        /// <param name="pb">显示窗体</param>
        /// <param name="c">组控件</param>
        /// <param name="lst"></param>
        /// <param name="data"></param>
        public void DisplayPaint(ref PictureBox pb, ref Control c, ref ArrayList lst, ushort[] data,int RecordLength)
        {
            Bitmap btm = new Bitmap(pb.Width, pb.Height);
            Graphics gph = Graphics.FromImage(btm);
            gph.Clear(Color.Black);
            ushort[] tempdata = new ushort[131092];
            for (int i = 0; i <= RecordLength; i++)
            {
                tempdata[i] = (ushort)((double)(65536.0 - (float)data[i]) * pb.Height / 65536);
            }

            lst.Add(new BoXing(data, RecordLength));   //采集标准波形


            KeDu(ref pb,ref gph,ref c);


            pb.Image = btm;
        }


        /// <summary>
        /// 显示图形
        /// </summary>      
        public void DisplayPaint(BoXing bx, ref PictureBox pb,ref Control c,Pen pen_color)
        {

            ushort[] args = new ushort[131092];

            for (int i = 0; i < bx.Data.Length; i++)
            {
                args[i] = bx.Data[i];
            }

            ushort[] temp_boxing = new ushort[131092];

            Bitmap btm = new Bitmap(pb.Width, pb.Height);
            Graphics gph = Graphics.FromImage(btm);
            gph.Clear(Color.Black);
            for (int i = 0; i <= bx.RecordLength; i++)
            {
                temp_boxing[i] = (ushort)((double)(65536.0 - (float)args[i]) * pb.Height / 65536);

                //temp_boxing[i] = Data_BiaoZhunBoXing[i];
            }
            for (int i = 1; i <= bx.RecordLength - 1; i++)
            {
                PointF startPoint = new PointF((i - 1) * pb.Width / (bx.RecordLength - 1), temp_boxing[i - 1]);
                PointF endPoint = new PointF(i * pb.Width / (bx.RecordLength - 1), temp_boxing[i]);
                gph.DrawLine(pen_color, startPoint, endPoint);
            }
            KeDu(ref pb, ref gph, ref c);

            pb.Image = btm;
            
        }


        /// <summary>
        /// 显示图形
        /// </summary>      
        public string DisplayPaint(ushort[] biaozhun, ushort[] ceshi, ref PictureBox pb, ref Control c, int RecordLength,double wucha)
        {

            Bitmap btm = new Bitmap(pb.Width, pb.Height);
            Graphics gph = Graphics.FromImage(btm);
            gph.Clear(Color.Black);
            //测试波形

            ZZ.Serial.HsOperation.ADC_GetDataCh(1, ceshi);

            ushort[] temp_ceshi_boxing = new ushort[131092];
            double cMaxValue = 0;
            double cMinValue = 1000000;
            for (int i = 0; i <= RecordLength; i++)
            {
                if (cMaxValue < temp_ceshi_boxing[i])
                {
                    cMaxValue = temp_ceshi_boxing[i];
                }
                if (cMinValue > temp_ceshi_boxing[i])
                {
                    cMinValue = temp_ceshi_boxing[i];
                }
                temp_ceshi_boxing[i] = (ushort)((double)(65536.0 - (float)ceshi[i]) * pb.Height / 65536);
            }


            for (int i = 1; i <= RecordLength - 1; i++)
            {
                PointF startPoint = new PointF((i - 1) * pb.Width / (RecordLength - 1), temp_ceshi_boxing[i - 1]);
                PointF endPoint = new PointF(i * pb.Width / (RecordLength - 1), temp_ceshi_boxing[i]);
                gph.DrawLine(Pens.Red, startPoint, endPoint);
            }


            //标准波形
            ushort[] temp_biaozhun_boxing = new ushort[131092];
            double bMaxValue = 0;
            double bMinValue = 1000000;
            for (int i = 0; i <= RecordLength; i++)
            {
                if (bMaxValue < temp_biaozhun_boxing[i])
                {
                    bMaxValue = temp_biaozhun_boxing[i];
                }
                if (bMinValue > temp_biaozhun_boxing[i])
                {
                    bMinValue = temp_biaozhun_boxing[i];
                }
                temp_biaozhun_boxing[i] = (ushort)((double)(65536.0 - (float)biaozhun[i]) * pb.Height / 65536);
            }


            for (int i = 1; i <= RecordLength - 1; i++)
            {
                PointF startPoint = new PointF((i - 1) * pb.Width / (RecordLength - 1), temp_biaozhun_boxing[i - 1]);
                PointF endPoint = new PointF(i * pb.Width / (RecordLength - 1), temp_biaozhun_boxing[i]);
                gph.DrawLine(Pens.White, startPoint, endPoint);

            }

            KeDu( ref pb,ref gph,ref c);

            pb.Image = btm;
        
            //double wuchazhi = (double)numericUpDown1.Value / 100;
            if (cMinValue > bMinValue * wucha || cMaxValue < bMaxValue * wucha)
            {
                return "不合格";
            }
            else
            {
                return "合格";
            }



        }








        /// <summary>
        /// 画刻度
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="gph"></param>
        /// <param name="c"></param>
        private void KeDu(ref PictureBox pb,ref Graphics gph,ref Control c)
        {
            //画刻度-------------------------------------
            ZZ.Serial.HsOperation.GetSensitivity(1, ref _Sensitivity);
            double bH = _Sensitivity * 10000;
            double minH = bH / 4;
            double beishu = bH / 20000;

            //虚线
            Pen dotPen = new Pen(Color.Green);
            dotPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            double picMH = (double)pb.Height / 2;//中间线的高度位置

            double lineH1 = ((double)(65536.0 - minH) * pb.Height / 65536);
            double lineH2 = ((double)(65536.0 - minH * 2) * pb.Height / 65536);
            double pH = (lineH1 - lineH2) / beishu;//线的高度

            Label lab = new Label();
            lab.Text = "0";
            lab.Top = pb.Top + (int)picMH - 3;
            lab.Left = 8;
            lab.BackColor = Color.Transparent;
            lab.BringToFront();
            lab.Name = "labMid";
            if (!c.Controls.Contains(lab))
                c.Controls.Add(lab);

            gph.DrawLine(Pens.LightGreen, 0, (int)picMH, pb.Width, (int)picMH);
            for (int i = 1; i <= 4; i++)
            {
                PointF startPoint = new PointF(0, (float)(picMH + pH * i));
                PointF endPoint = new PointF(pb.Width, (float)(picMH + pH * i));
                gph.DrawLine(dotPen, startPoint, endPoint);
                Label lb = new Label();
                lb.Text = "-" + (minH * i).ToString();
                lb.Top = pb.Top + (int)(picMH + pH * i) - 3;
                lb.Left = 3;
                lb.BackColor = Color.Transparent;
                lb.BringToFront();
                lb.Name = "labt" + i.ToString();
                if (!c.Controls.Contains(lb))
                    c.Controls.Add(lb);
            }
            for (int i = 1; i <= 4; i++)
            {
                PointF startPoint = new PointF(0, (float)(picMH + (-pH * i)));
                PointF endPoint = new PointF(pb.Width, (float)(picMH + (-pH * i)));
                gph.DrawLine(dotPen, startPoint, endPoint);
                Label lb = new Label();
                lb.Text = (minH * i).ToString();
                lb.Top = pb.Top + (int)(picMH + (-pH * i)) - 3;
                lb.Left = 3;
                lb.BackColor = Color.Transparent;
                lb.BringToFront();
                lb.Name = "labb" + i.ToString();
                if (!c.Controls.Contains(lb))
                    c.Controls.Add(lb);
            }
            //Y-------------------------------------------------Y--------------------------------
            double Hz = ZZ.Serial.HsOperation.GetSampleFrequencyF();
            int Len = ZZ.Serial.HsOperation.GetRecordLength();
            double pW = (Hz / 1000) * (Len / 1000);

            for (int i = 1; i <= 1000; i++)
            {
                if (i % 100 == 0)
                {
                    PointF startPoint = new PointF((i - 1) * pb.Width / (1000 - 1), 0);
                    PointF endPoint = new PointF(i * pb.Width / (1000 - 1), pb.Height);
                    gph.DrawLine(dotPen, startPoint, endPoint);
                    gph.DrawString((i / 100 * pW).ToString() + "ms", new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134))), Brushes.White, (i - 1) * pb.Width / (1000 - 1), pb.Height - 18);
                 
                }
            }
        }




        public class BoXing
        {
            private int recordLength;
            private ushort[] data = new ushort[131092];

            public BoXing()
            {
            }

            public BoXing(ushort[] data1, int length1)
            {
                recordLength = length1;
                data = data1;

            }

            public int RecordLength
            {
                get
                {
                    return recordLength;
                }
                set
                {
                    recordLength = value;
                }

            }

            public ushort[] Data
            {
                get
                {
                    return data;
                }
                set
                {
                    data = value;
                }
            }

        }









    }
}
