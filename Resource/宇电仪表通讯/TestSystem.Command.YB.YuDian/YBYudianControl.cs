using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace TestSystem.Command.YB.YuDian
{
    public class YBYudianControl
    {
       

        private static YBYudianControl uniqueInstance;

       
        private YBYudianControl()
        {
           
        }

        public static YBYudianControl getInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new YBYudianControl();
            }
            return uniqueInstance;
        }

        /// <summary>
        /// 读取操作
        /// </summary>
        /// <param name="addr">操作地址</param>
        /// <param name="parm_addr">参数代号</param>
        /// <param name="sp">串口</param>
        /// <returns></returns>
        public bool ExcuteRead(string addr, string parm_addr,int sleep ,SerialPort sp,out object[] Response)
        {
            bool result=false;

            Response=new string[10];
            try
            {
                //构建发送命令
                string cmd = YuDianYB.SendStr(addr, parm_addr);

                byte[] SendData=YuDianYB.ReadCommand(cmd);

                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();


                sp.Write(SendData, 0, SendData.Length);

                System.Threading.Thread.Sleep(sleep);

                byte[] RecvData = new byte[10];

                sp.Read(RecvData, 0, RecvData.Length);

               
                Response= YuDianYB.ResponseConvertToHex16(RecvData);
                if(YuDianYB.CheckFCS(Response,int.Parse(addr)))
                {
                    result=true;
                }
            }
            catch

            {
 
            }

            return result;
            
        }

        /// <summary>
        /// 写入操作
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="parm_addr"></param>
        /// <param name="parm_value"></param>
        /// <param name="sleep"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public bool ExcuteWrite(string addr, string parm_addr, string parm_value, int sleep, SerialPort sp,out object[] Response)
        {
            bool result = false;
            Response = new string[10];
            try
            {
                 string cmd = YuDianYB.WriteStr(addr, parm_addr, parm_value);

                byte[] SendCmd= YuDianYB.WriteCommand(cmd);

                sp.DiscardOutBuffer();

                sp.DiscardInBuffer();

                sp.Write(SendCmd, 0, SendCmd.Length);

                System.Threading.Thread.Sleep(sleep);

                byte[] RecvData=new byte[10];
                sp.Read(RecvData, 0, RecvData.Length);

                Response = YuDianYB.ResponseConvertToHex16(RecvData);
                if (YuDianYB.CheckFCS(Response, int.Parse(addr)))
                {
                    result = true;
                }

            }
            catch
            {
 
            }

            return result;
 
        }
        /// <summary>
        /// 读取PV值
        /// </summary>
        /// <param name="response">一串返回的响应代码</param>
        /// <param name="addr"></param>
        /// <returns></returns>
        public string ReadPV(object[] response, string addr)
        {
            try
            {
                return YuDianYB.ReadPV(response, int.Parse(addr));
            }
            catch
            {
                return "0";
            }
        }

        /// <summary>
        /// 读取PV值
        /// </summary>
        /// <param name="response">一串返回的响应代码</param>
        /// <param name="addr"></param>
        /// <returns></returns>
        public string ReadSV(object[] response, string addr)
        {
            try
            {
                return YuDianYB.ReadSV(response, int.Parse(addr));
            }
            catch
            {
                return "0";
            }
        }
       
    }
}
