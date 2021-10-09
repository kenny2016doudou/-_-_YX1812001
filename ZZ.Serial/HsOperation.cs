using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ZZ.Serial
{
    
    public class HsOperation
    {
        //  Return codes = bit values. Non-zero is an error:
        public const int E_NO_ERRORS = 0x0;
        public const int ltmStream = 0;//数据采集模式 0为默认数据 1 为有新数据时
        public static bool Paint = false;
        public static double Amplitude;
        public static double DcLevel;
        public static byte DcOrAc;

       

        //打开\关闭仪器
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort InitInstrument(ushort wAddress);
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort ExitInstrument();
        //获取仪器信息
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort GetSerialNumber(ref int serialnummer);
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern double GetMaxSampleFrequencyF();
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort GetNrChannels(ushort channels);
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern int GetMaxRecordLength();

        //测量控制
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort ADC_Start();
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort ADC_Abort();

        //检索测量数据
        //测量数据回送函数
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort SetDataReadyEvent(IntPtr hEvent);

        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort SetDataReadyCallback(DataReadyCallback DataReady);
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort SetTransferMode(short ltmStream);
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern int ADC_GetDataCh(ushort channel, ushort[] data);
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern int GetRecordLength();
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern int SetRecordLength(UInt32 recordLength);
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort SetSampleFrequencyF(ref double freq);
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern double GetSampleFrequencyF();
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort SetPostSamples(uint post);
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern uint GetPostSamples();
        //设置灵敏度
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern uint SetSensitivity(byte ch, ref double setSensitivity);
        //读取灵敏度
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern uint GetSensitivity(byte ch, ref double getSensitivity);
        //设置触发模式
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern uint SetCoupling(byte ch, byte acOrDc);
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern uint GetCoupling(byte ch, ref byte acOrDc);

        //取得振幅
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort GetFuncGenAmplitude(ref double amplitude);
        //取得DC Level
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort GetDcLevel(byte ch, ref double dcLevel);


        //匝间触发
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern int ADC_Ready(); 


        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort GetTriggerHys(byte byCh, ref double dHysteresis);

        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort SetTriggerHys(byte byCh, double dHysteresis);

        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort GetTriggerLevel(byte byCh, ref double dLevel);

        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort SetTriggerLevel(byte byCh, double dLevel );

        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort GetTriggerSource(ref byte bySource);

        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort SetTriggerSource(byte bySource);


        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort GetTriggerMode(ref byte byMode);
        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort SetTriggerMode(byte byMode);

        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort SetTriggerModeCh(byte ch,byte byMode);

        [DllImport("C:\\WINDOWS\\system32\\HS4.dll")]
        public static extern ushort ADC_GetDataVoltCh(ushort wCh, double[] dData);
        

        //数据处理
        public delegate void DataReadyCallback();

        public static DataReadyCallback callBack = new DataReadyCallback(ReceiveData);
        public static ushort[] Data = new ushort[131092];
        public static void ReceiveData()
        {
            ADC_GetDataCh(1, Data);
            Paint = true;
            GetFuncGenAmplitude(ref Amplitude);
            GetDcLevel(1, ref DcLevel);
            GetCoupling(1, ref DcOrAc);
        }


        public static DataReadyCallback callBack2 = new DataReadyCallback(ReceiveData2);

        public static ushort[] Data1 = new ushort[131092];
        public static ushort[] Data2 = new ushort[131092];
        public static ushort[] Data3 = new ushort[131092];
        public static ushort[] Data4 = new ushort[131092];
        public static ushort tongDaoHao;

        public static void ReceiveData2()
        {
            if (tongDaoHao < 1)
            {
                tongDaoHao = 1;
            }
            for (int i = 0; i < tongDaoHao; i++)
            {

                int tdh = i + 1;
                if (tdh == 1)
                {
                    ADC_GetDataCh((ushort)tdh, Data1);
                }
                else if (tdh == 2)
                {
                    ADC_GetDataCh((ushort)tdh, Data2);
                }
                else if (tdh == 3)
                {
                    ADC_GetDataCh((ushort)tdh, Data3);
                }
                else if (tdh == 4)
                {
                    ADC_GetDataCh((ushort)tdh, Data4);
                }

                Paint = true;
                GetFuncGenAmplitude(ref Amplitude);
                GetDcLevel((byte)(tdh), ref DcLevel);
                GetCoupling((byte)(tdh), ref DcOrAc);
            }
        }

        /*Public Function re_CHValue(ByVal V As Double) As Double
    If V > 3.8 And Round(V, 2) < 4.5 Then
        Dim Str As String
        V = V + (4.6 - Round(V, 2))
        If V >= 4 Then
            Str = CStr(V)
            Mid(Str, InStr(Str, ".", -1, 1) + 1, 1) = 5
            V = Val(Str)
        Else
            V = V + 1
        End If
    End If
    re_CHValue = V
End Function*/




        public static double re_CHValue(double V)
        {
            if (V > 3.8 && Math.Round(V, 2) < 4.5)
            {
                //string Str;
                V = V + (4.6 - Math.Round(V, 2));
                if (V >= 4)
                {
                    //Str = V.ToString();
                }
                else
                {
                    V = V + 1;
                }
            }
            return V;
        }

















    }
}
