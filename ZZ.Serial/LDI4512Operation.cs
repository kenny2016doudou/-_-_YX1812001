using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ZZ.Serial
{
    public class LDI4512Operation
    {
        //加载LDI4512.DLL动态连接库
        //卡初始化函数
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int System_Init(ref int CardNumbers, ref uint CardAddr);

        //获取LDI4512采集卡的配置参数
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int LDI4512_PackInfo(uint CardAddr, ref TSysInfo pSysInfo);

        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int LDI4512_VBPackInfo(uint CardAddress, ref int baseline, ref double GainTable);

        //设置采集参数函数
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int LDI4512_VBSetHardWare(uint CardAddr, uint TrigMode, uint TrigEdge, uint TrigPreIdx, uint TrigSource, uint TrigLevel, uint SampleIdx, uint SampleLengthIdx, uint RangeIdxch0, uint RangeIdxch1, uint RangeIdxch2, uint RangeIdxch3);


        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int LDI4512_SetHardWare(int CardAddr, ref  TTrgInfo pTrgInfo, ref TCtrlInfo pCtrlInfo);

        //启动采集函数
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int LDI4512_Acq(int CardAddr);

        //取数据函数
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int LDI4512_PackData(int CardAddr, int Dots, ref double WaveData1, ref double WaveData2, ref double WaveData3, ref double WaveData4);


        //写DO端口函数  
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern void LDI4512_WriteDO(int CardAddress, int data);

        //读DI端口函数
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int LDI4512_ReadDI(int CardAddress);

        //获取波形特征参数函数        
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int PackWavePara(int Dots, ref double Data, ref double Dots2, ref double vmax, ref double vmin, ref double vrms, ref double vmean, ref double vduty);


        //设置基线零点         
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int LDI4512_SetZero(ref int Zero);


        ////写卡配置参数  

        /*-----------以下两个函数为厂家调试使用-----*/
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int Write_24c02(int CardAddress, int chan, int length, ref char buffin);


        //统计清空
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int LDI4512_FreqClr(int CardAddr);
        /*------------------------------------------*/

        //取CHA，CHB相位差        
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int PackDeltaX(int Dots, ref double chadata, ref double chbdata, ref double deltax);
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int PackDeltaX_Corr(int dots, ref double chadata, ref double chbdata, ref double deltax);

        //统计
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int LDI4512_PackFreqVal(int CardAddr, ref uint Val);

        //求频率
        [DllImport("C:\\WINDOWS\\system32\\LDI4512DLL.dll")]
        public static extern int FFTAssay(int Dots, int FFTWindowIdx, ref double InputData, double SampleFrequency, ref double MainFrequency, ref double OutputData);


    }

    public struct TSysInfo
    {
        //LDI4512卡的配置参数
        public char[] idNumber; //卡ID号

        //基线补偿
        public int[] baseLine;

        //采样时钟列表
        public double[] clk_tab;

        //量程列表
        public double[] range_tab;

        //增益列表
        public double[,] adjgain;


        public long maxsmplength; //最大采样长度


        public int resolution; //AD精度



    }


    public struct TCtrlInfo
    {
        //数据采集控制
        public uint smapleIdx; //采样率序号


        public uint sampleLengthIdx; //采集长度序号


        public uint[] rangeIdx;//各通道量程


    }

    public struct TTrgInfo
    {
        //触发控制
        public uint trigMode; //触发模式


        public uint trigEdge;//触发边沿

        public uint trigSource; //触发源


        public uint sampleLength; //采样长度


        public uint trigPreIdx; //预触发


        public uint trigLevel; //触发电平


    }


}
