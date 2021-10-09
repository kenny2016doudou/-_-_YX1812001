using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.BusinessRule.Interface.SchemeModule
{
    public interface ISchemeInfoBO
    {
        //删除解决方案，删除表，删除列配置，删除create表

        bool DeleteSchemeInfo(string schemeID);
    }
}
