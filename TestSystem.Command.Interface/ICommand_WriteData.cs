using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.Interface
{
    public interface ICommand_WriteData : ICommand
    {
        string StrData
        {
            set;
        }
    }
}
