using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.ControlCenter;
using System.IO.Ports;
using TestSystem.Command.PLC.Omron.HostLink.Fins;

namespace OmronPLCTest
{
    public class OmronStaticControl
    {
        public IComControl Com = null;//操作对象
        private SerialPort sp;//串口对象
        private string[] Command = new string[100];//地址集合
        public OmronStaticControl(ref SerialPort sp)
        {
            this.sp = sp;
            if (!this.sp.IsOpen)
            {
                try
                {
                   string COM=  Omron_SerialPort.GetOmronCOM(9600, Parity.Even, 7, StopBits.Two);//自动搜寻可用的欧姆龙通讯COM口
                   Omron_Port.getInstance().OpenPort(sp, COM,  StopBits.Two,Parity.Even, 9600,7);//打开欧姆龙通讯口
                }
                catch
                {
 
                }
            }

            //定义操作地址
            Command[0] = "D0";
            Command[1] = "D1";
            Command[2] = "D2";
            Command[3] = "D3";
            Command[4] = "D4";
            Command[5] = "D5";
            Command[6] = "W0.01";

            Command[7] = "I100";
        }

        public void LoadData()
        {
            Com = ComControl_Factory.getInstance().CreateComControl(ref sp);

            //读取单点值
            Omron_Reader readerD0 = new Omron_Reader(Command[0], DataType.Word, 1);
            Com.SetReader("读取D0位置", readerD0);

            //读取连续多通道值
            Omron_Reader readerD1_D5 = new Omron_Reader(Command[1], DataType.Word, 5);
            Com.SetReader("读取D1-D5位置", readerD1_D5);

            //写入单通道
            Omron_WriteData writeD0 = new Omron_WriteData(Command[0], DataType.Word);
            Com.SetCommand("写入D0位置", writeD0);

            //写入多通道
            Omron_WriteData writeD1_D5 = new Omron_WriteData(Command[1], DataType.Word);
            Com.SetCommand("写入D1-D5位置", writeD1_D5);

            //将W0.01点置1
            Omron_ClickDown downW0_01 = new Omron_ClickDown(Command[6], DataType.Bit, true);
            Com.SetCommand("W0_01摁住", downW0_01);

            //读取W0.01状态
            Omron_Reader readerW0_01 = new Omron_Reader(Command[6], DataType.Bit, 1);
            Com.SetReader("W0_01状态", readerW0_01);

            //将W0.01点置0
            Omron_ClickUp upW0_01 = new Omron_ClickUp(Command[6], DataType.Bit, false);
            Com.SetCommand("W0_01复位", upW0_01);

            Dictionary<string, string> dicStatus = new Dictionary<string, string>();
            dicStatus.Add("I100.00状态", "I100.00");

            dicStatus.Add("I100.01状态", "I100.01");

            dicStatus.Add("I100.02状态", "I100.02");

            dicStatus.Add("I100.03状态", "I100.03");

            dicStatus.Add("I100.04状态", "I100.04");


            Omron_BatchReadStatus RI100 = new Omron_BatchReadStatus(Command[7], DataType.Bit, 5, dicStatus);
            Com.SetReader("100状态", RI100);

            Com.Start();

            
        }
    }
}
