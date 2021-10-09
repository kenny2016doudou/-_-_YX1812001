using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;


namespace TestSystem.Command.Interface
{
    public interface ICommand
    {
        bool Excute(ref SerialPort sp);
    }
}
