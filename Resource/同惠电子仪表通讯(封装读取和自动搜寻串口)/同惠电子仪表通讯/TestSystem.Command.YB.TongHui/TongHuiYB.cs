using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.YB.TongHui
{
    public class TongHuiYB
    {
        /// <summary>
        /// 解析仪表返回的结果(返回三个长度的集合 0表示正负 1 表示值   2表示单位 
        /// </summary>
        /// <param name="ReadExists">返回+或者- 的值  以及对应的单位 0 表示欧姆 K0 表示 千欧  M0 表示兆欧  m0 表示毫欧</param>
        /// <returns></returns>
        public static string[] GetResponse(string ReadExists)
        {
            string[] response = new string[3];
            try
            {
                //返回结果应该是类似R8=+000.00  0   
                if (ReadExists.Trim().Length == 13)
                {
                    string[] str = ReadExists.Split('=');

                    string value = str[1];

                    string  t= value.Substring(0, 1);//获得+或者-
                    response[0] = t;
                    string testVal = value.Substring(1, 6);//获得值
                    response[1] =float.Parse(testVal).ToString();
                    string danwei = value.Substring(7, 3).Trim();//获得单位
                    response[2] = danwei;
                    switch (danwei)
                    {
                        case "mO":
                            response[2]="mΩ";
                            break;
                        case "O":
                            response[2] = "Ω";
                            break;
                        case "kO":
                            response[2] = "kΩ";
                            break;
                        case "MO":
                            response[2] = "MΩ";
                            break;
                    }


                }

            }
            catch
            {
 
            }

            return response;
 
        }

        /// <summary>
        /// 执行仪表通讯(包括设置和读取都可以通用)
        /// </summary>
        /// <param name="Sleep"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static  string ExecuteCommandRead(int Sleep, TongHuiCommand command, System.IO.Ports.SerialPort sp)
        {
            string response = "";
            try 
            {
                if (sp != null)
                {
                    string cmdStr = "";
                    if (command == TongHuiCommand.V)
                    {
                        cmdStr = "?";
                    }
                    else
                    {
                        cmdStr = command.ToString();
                    }
                    cmdStr = cmdStr + "\n";

                    sp.Write(cmdStr);

                    System.Threading.Thread.Sleep(Sleep);

                    response = sp.ReadExisting();


                }

            }
            catch
            {

            }

            return response;
 
        }
    }
}
