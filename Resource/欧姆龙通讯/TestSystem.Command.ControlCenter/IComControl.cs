using System;
namespace TestSystem.Command.ControlCenter
{
    public interface IComControl
    {
        /// <summary>
        /// 读取命令对象单个值(线程)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        object GetReader(string position);
        /// <summary>
        /// 读取命令对象多个值(线程)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        object[] GetReaderIData(string position);
        /// <summary>
        /// 读取命令对象单个值(非线程)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        object GetStepReader(string position);
        /// <summary>
        /// 读取命令对象多个值(非线程)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        object[] GetStepReaderIData(string position);


        /// <summary>
        /// 设置置位、复位、写入等命令对象
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="OnCommand"></param>
        void SetCommand(string slot, TestSystem.Command.Interface.ICommand OnCommand);

        /// <summary>
        /// 设置读取命令对象(多线程方式)
        /// </summary>
        /// <param name="position">命令对象自定义名称</param>
        /// <param name="read">命令对象</param>
        void SetReader(string position, TestSystem.Command.Interface.IRead read);


        /// <summary>
        /// 设置读取命令对象(非线程方式)
        /// </summary>
        /// <param name="position">命令对象自定义名称</param>
        /// <param name="read">命令对象</param>
        void SetStepReader(string position, TestSystem.Command.Interface.IRead read);
        /// <summary>
        /// 停止处理命令
        /// </summary>
        void Stop();
        /// <summary>
        /// 开始处理命令
        /// </summary>
        void Start();

        /// <summary>
        /// 执行置位、复位、写入等 命令对象
        /// </summary>
        /// <param name="slot"></param>
        void ExcuteCommand(string slot);

        /// <summary>
        /// 根据描述 获取命令对象
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        TestSystem.Command.Interface.ICommand GetCommand(string position);

        /// <summary>
        /// 根据描述 获得写入命令对象
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        TestSystem.Command.Interface.ICommand_WriteData GetWriteDataCommand(string position);
    }
}
