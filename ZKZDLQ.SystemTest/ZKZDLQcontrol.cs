using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.ControlCenter;
using System.IO.Ports;
using TestSystem.Command.PLC.Omron.HostLink.Fins;
using TestSystem.Command.YB.TongHui;
using TestSystem.Command.Interface;
using System.Threading;
using ZZ.Serial;
using modbusClass;
using ManagementSpecificTools.PlcConnectivity;

namespace ZKZDLQ.SystemTest
{
    public class ZDLQcontrol
    {

        private static ZDLQcontrol uniqueInstance;
        /// <summary>
        /// 欧姆龙通讯
        /// </summary>
        public IComControl IComOmron = null;

        public IComControl IComTongHui = null;
        public IComControl Com_TianChen = null;
        public SerialPort sp_tianchen = new SerialPort();
        
        public SiemensCommManage Com_Siemens = null;
        
        private SerialPort spOmron;
        private SerialPort spTongHui;
        modbus runmodbus = new modbus();


        /// <summary>
        /// 欧姆龙通讯命令集合
        /// </summary>
        private string[] OmronCommand = new string[150];
        /// <summary>s
        /// 同惠仪表通讯命令集合
        /// </summary>
        private TongHuiCommand[] TonghuiCommand = new TongHuiCommand[20];

        public static ZDLQcontrol getInstance()
        {

            if (uniqueInstance == null)
            {
                uniqueInstance = new ZDLQcontrol();
            }


            return uniqueInstance;
        }

        public string get_testdy()
        {
            string tempstr = "0";
            if (runmodbus.Open("COM1"))
            {
                string laststr = "";
                ushort[] values = new ushort[8];
                byte[] response = null;
                try
                {
                    if (runmodbus.SendFc3((byte)1, (byte)3, (byte)0, (byte)1, ref values, ref response))
                    {
                        for (int i = 0; i < response.Length; i++)
                        {
                            tempstr = Convert.ToString(response[i], 16);
                            if (tempstr.Length == 1)
                                tempstr = "0" + tempstr;
                            laststr += tempstr.ToUpper();
                        }
                        tempstr = laststr.Substring(6, 4);
                        float ttfloat = Convert.ToInt32(tempstr, 16);
                        ttfloat = ttfloat / 100;
                        tempstr = ttfloat.ToString("0.0");
                    }
                    else
                    {

                    }
                }
                catch
                {
                }
            }
            return tempstr;
        }

        public string get_testdl()
        {
            string tempstr = "0";
            return tempstr;
        }

        private ZDLQcontrol()
        {
            Com_Siemens = SiemensCommManage.getInstance();
            Com_Siemens.Start();

            LoadTongHuiData();
            LoadTianChenData();
        }
 

        //判断分闸是否置位
        public string 读取合闸状态指示()
        {

            return "";

        }
        public string 读取分闸状态指示()
        {

            return "";

        }

        public void 保压置位()
        {


           // IComOmron.ExcuteCommand("保压置位");
            Com_Siemens.ExcuteCommand_Set("保压置位");

        }

        public void 保压复位()
        {


            //IComOmron.ExcuteCommand("保压复位");
            Com_Siemens.ExcuteCommand_Set("保压复位");

        }
        //public string 分合闸状态指示1()
        //{
        //    if (IComOmron.GetStepReader("合闸状态").ToString() != null)
        //    {
        //        return IComOmron.GetStepReader("合闸状态").ToString();
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
        //public string 分合闸状态指示()
        //{
        //    if (IComOmron.GetReader("读取分合闸状态指示").ToString() != null)
        //    {
        //       return  IComOmron.GetReader("读取分合闸状态指示").ToString();
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
        /// <summary>
        /// 读取电阻值
        /// </summary>
        /// <returns></returns>
        public object[] 读取电阻值()
        {
            return null;

        }
        /// <summary>
        /// 读取充气状态
        /// </summary>
        /// <returns></returns>
        public string 充气状态()
        {
            if (IComOmron != null)
            {
                if (IComOmron.GetReader("读取充气状态").ToString() != null)
                {
                   // return IComOmron.GetReader("读取充气状态").ToString();
                   return IComOmron.GetReader("读取充气状态").ToString();
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 读取单双电状态
        /// </summary>
        /// <returns></returns>
        public string 读取单双电状态()
        {
            if (Com_Siemens != null)
            {
                if (Com_Siemens.GetReader("单电双电控制").ToString() != null)
                {
                    return Com_Siemens.GetReaderB("单电双电控制").ToString();
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        ///  读取长电状态
        /// </summary>
        /// <returns></returns>
        public string 读取长电状态()
        {
            if (IComOmron != null)
            {
                if (IComOmron.GetReader("长电控制状态").ToString() != null)
                {
                    return IComOmron.GetReader("长电控制状态").ToString();
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 读取24给电状态
        /// </summary>
        /// <returns></returns>
        public string 读取24给电状态()
        {
            if (IComOmron != null)
            {
                if (IComOmron.GetReader("读取24给电状态").ToString() != null)
                {
                    return IComOmron.GetReader("读取24给电状态").ToString();
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 读取整流电流
        /// </summary>
        /// <returns></returns>
        public string 读取整流电流()
        {
            if (IComOmron != null)
            {
                if (IComOmron.GetReader("读取整流电流值").ToString() != null)
                {
                    return IComOmron.GetReader("读取整流电流值").ToString();

                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

            // return get_testdy();
        }

        public string 读取整流电压()
        {
            if (IComOmron != null)
            {
                if (IComOmron.GetReader("读取电压2").ToString() != null)
                {
                    return IComOmron.GetReader("读取电压2").ToString();

                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

            // return get_testdy();
        }
        public string 读取电阻()
        {
            if (IComOmron != null)
            {
                if (IComOmron.GetReader("读取电阻").ToString() != null)
                {
                    return IComOmron.GetReader("读取电阻").ToString();

                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

            // return get_testdy();
        }
        /// <summary>
        /// 读取显示电压
        /// </summary>
        /// <returns></returns>
        public string 读取显示电压()
        {
            if (Com_TianChen != null)
            {
                if (Com_TianChen.GetReader("试验电压").ToString() != null)
                {
                    return Com_TianChen.GetReader("试验电压").ToString();

                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

            // return get_testdy();
        }
        /// <summary>
        /// 读取显示气压
        /// </summary>
        /// <returns></returns>
        public string 读取显示气压()
        {
            Console.WriteLine(Com_TianChen);
            if (Com_TianChen != null)
            {
                if (Com_TianChen.GetReader("试验气压").ToString() != null)
                {
                    return Com_TianChen.GetReader("试验气压").ToString();
                }
                else
                {
                    return "weiqi";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 读取设置气压
        /// </summary>
        /// <param name="value"></param>
        public void 设置气压(string value)
        {
            if (Com_Siemens != null)
            {
                if (Com_Siemens.GetReader("气压设置值").ToString() != null)
                {
                    // Com_Siemens.ExcuteCommand_Write("气压设置值", value);    
                    Com_Siemens.ExcuteCommand_Write("气压设置值", 750);
                }
            }
        }
        public void 设置电压(string value)
        {
            if (Com_Siemens != null)
            {
                if (Com_Siemens.GetReader("电压设置值").ToString() != null)
                {
                   
                    Com_Siemens.ExcuteCommand_Write("电压设置值", value);
                }
            }
        }

        public void 设置整流电流(string value)
        {
            if (IComOmron != null)
            {
                if (IComOmron.GetWriteDataCommand("设置电流").ToString() != null)
                {
                    IComOmron.GetWriteDataCommand("设置电流").StrData = value;
                    IComOmron.ExcuteCommand("设置电流");                   
                }
            }
        }


        public void 设置电磁阀电压(string value)
        {
            if (IComOmron != null)
            {
                if (IComOmron.GetWriteDataCommand("设置电磁阀电压").ToString() != null)
                {
                    IComOmron.GetWriteDataCommand("设置电磁阀电压").StrData = value;
                    IComOmron.ExcuteCommand("设置电磁阀电压");                   
                }
            }
        }



        /*电阻测试--------------------------------------------------------------------------------------------------*/
        public void 设置电流(string value)
        {
            if (IComOmron != null)
            {
                if (IComOmron.GetWriteDataCommand("设置电流值").ToString() != null)
                {
                    IComOmron.GetWriteDataCommand("设置电流值").StrData = value;
                    IComOmron.ExcuteCommand("设置电流值");                    
                }
            }
        }

        public string 读取设置电流值()
        {
            if (IComOmron != null)
            {
                if (IComOmron.GetReader("读取设置电流值").ToString() != null)
                {
                    return IComOmron.GetReader("读取设置电流值").ToString();

                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }


        public string 读取输出电压()
        {
            if (IComOmron != null)
            {
                if (IComOmron.GetReader("读取输出电压值").ToString() != null)
                {
                    float a = int.Parse(IComOmron.GetReader("读取输出电压值").ToString()) / 1000f;
                    return a.ToString("0.000");
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public string 读取输出电流()
        {
            if (IComOmron != null)
            {
                if (IComOmron.GetReader("读取输出电流值").ToString() != null)
                {
                    float a = int.Parse(IComOmron.GetReader("读取输出电流值").ToString()) / 10f;
                    return a.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public void 自动测试()
        {
            Com_Siemens.ExcuteCommand_Set("自动测试置位");
            Com_Siemens.ExcuteCommand_Set("自动测试复位");
        //    Com_Siemens.ExcuteCommand_Set("自动测试复位");
        }


        public void 电阻测试启动()
        {

            Com_Siemens.ExcuteCommand_Set("电阻测试启动置位");
            Thread.Sleep(500);
            Com_Siemens.ExcuteCommand_Set("电阻测试启动复位");
            Thread.Sleep(500);
        }

        public void 电阻测试停止()
        {

            Com_Siemens.ExcuteCommand_Set("电阻测试停止置位");
            Thread.Sleep(500);
            Com_Siemens.ExcuteCommand_Set("电阻测试停止复位");
            Thread.Sleep(500);
        }
        /*电阻测试--------------------------------------------------------------------------------------------------*/



        /// <summary>
        /// 
        /// </summary>
        public void 合闸置位()
        {
            //分闸复位();
            //IComOmron.ExcuteCommand("合闸置位");
            Com_Siemens.ExcuteCommand_PulseZW("软件合闸");
        }
        public void 合闸复位()
        {
          //IComOmron.ExcuteCommand("合闸复位");
            Com_Siemens.ExcuteCommand_PulseFW("软件合闸");

        }
        public void 分闸置位()
        {
            //合闸复位();
           //IComOmron.ExcuteCommand("分闸置位");
            Com_Siemens.ExcuteCommand_PulseZW("软件分闸");
        }
        public void 分闸复位()
        {
            //IComOmron.ExcuteCommand("分闸复位");
            Com_Siemens.ExcuteCommand_PulseFW("软件分闸");


        }
        /// <summary>
        /// 打开整流电源
        /// </summary>
        public void 打开整流电源()
        {
            //IComOmron.ExcuteCommand("断开气源复位");
            Com_Siemens.ExcuteCommand_Set("整流电源启动");
        }
        /// <summary>
        /// 打开整流电源复位
        /// </summary>
        public void 整流电源复位()
        {
            //IComOmron.ExcuteCommand("断开气源复位");
            Com_Siemens.ExcuteCommand_Set("整流电源启动复位");
        }

        /// <summary>
        /// 打开气源
        /// </summary>
        public void 打开气源()
        {
            //IComOmron.ExcuteCommand("断开气源复位");
          //IComOmron.ExcuteCommand("打开气源置位");
          //IComOmron.ExcuteCommand("打开气源复位");
           Com_Siemens.ExcuteCommand_Set("打开气源置位");
           Com_Siemens.ExcuteCommand_Set("打开气源复位");

        }
        /// <summary>
        /// 断开气源
        /// </summary>
        public void 断开气源()
        {
            // IComOmron.ExcuteCommand("打开气源复位");
            //IComOmron.ExcuteCommand("断开气源置位");

        }

        /// <summary>
        /// 断开气源复位
        /// </summary>
        public void 断开气源复位()
        {
            //IComOmron.ExcuteCommand("断开气源复位");

        }

        /// <summary>
        ///打开第二个电磁阀气源复位
        /// </summary>
        public void 打开第二个电磁阀气源()
        {
            //IComOmron.ExcuteCommand("打开第二个电磁阀气源置位");
        }

        /// <summary>
        /// 开始测试
        /// </summary>
        public void 开始测试()
        {
            //IComOmron.ExcuteCommand("开始测试置位");

        }
        /// 停止测试
        /// </summary>
        public void 停止测试()
        {
           // IComOmron.ExcuteCommand("开始测试复位");

        }

        public void 开始充气()
        {
            Com_Siemens.ExcuteCommand_Pulse("软件充气");
        }

        //20131112
        public void 停止充气()
        {
            //IComOmron.ExcuteCommand("停止充气置位");
            //IComOmron.ExcuteCommand("停止充气复位");
            //IComOmron.ExcuteCommand("开始充气复位");
            //IComOmron.ExcuteCommand("开始充气复位");
        }

        public void 停止充气置位()
        {

            // Com_Siemens.ExcuteCommand_Set("停止充气置位");
            // Com_Siemens.ExcuteCommand_Set("停止充气复位");
        }
        //

        public void 开始排气()
        {

            Com_Siemens.ExcuteCommand_Pulse("软件排气");
            //IComOmron.ExcuteCommand("开始排气置位");
            //IComOmron.ExcuteCommand("开始排气复位");
        }
        /// <summary>
        /// 关闭通讯
        /// </summary>
        public void closePort()
        { 
            Com_TianChen.Stop();
            if (sp_tianchen.IsOpen)
            {
                sp_tianchen.Close();
            }
            uniqueInstance = null;
            GC.Collect();
        }

        /// <summary>
        /// 打开电阻测试的串口
        /// </summary>
        private void OpenTianChenPort()
        {
            if (!sp_tianchen.IsOpen)
            {
                try
                {
                    sp_tianchen.PortName = "COM2";
                    sp_tianchen.StopBits = StopBits.One;
                    sp_tianchen.Parity = Parity.None;
                    sp_tianchen.BaudRate = 115200;
                    sp_tianchen.DataBits = 8;
                    sp_tianchen.Open();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        /// <summary>
        /// 加载天辰仪表的数据
        /// </summary>
        private void LoadTianChenData()
        {
            OpenTianChenPort();

            Com_TianChen = ComControl_Factory.getInstance().CreateComControl(ref sp_tianchen);

            //dc110电压
            TestSystem.Command.YB.TianChen.ReadValue_YBTianChen read_试验电压 = new TestSystem.Command.YB.TianChen.ReadValue_YBTianChen() { AddressStr = "01", Sleep = 20, MeterTypeEnum = TestSystem.Command.YB.TianChen.MeterType.XST };
            //dc110电流
            TestSystem.Command.YB.TianChen.ReadValue_YBTianChen read_试验气压 = new TestSystem.Command.YB.TianChen.ReadValue_YBTianChen() { AddressStr = "02", Sleep = 20, MeterTypeEnum = TestSystem.Command.YB.TianChen.MeterType.XST };


            Com_TianChen.SetReader("试验电压", read_试验电压);
            Com_TianChen.SetReader("试验气压", read_试验气压);

            Com_TianChen.Start();
        }
        /// <summary>
        /// 加载同惠仪表的数据
        /// </summary>
        private void LoadTongHuiData()
        {
            OpenTianChenPort();

            spTongHui = new SerialPort();
            if (!this.spTongHui.IsOpen)
            {
                try
                {
                    string COM = "COM2";
                    TestSystem.Command.YB.TongHui.TongHuiPort.getInstance().OpenPort(spTongHui, COM, Parity.None, 115200, 8, StopBits.One);
                    IComTongHui = ComControl_Factory.getInstance().CreateComControl(ref spTongHui);
                    TongHuiRead RDZ = new TongHuiRead(TongHuiCommand.V);
                    IComTongHui.SetStepReader("电阻", RDZ);
                }
                catch
                {
                }
            }
            TonghuiCommand[0] = TongHuiCommand.V;
            IComTongHui.Start();
        }
    }
    }
