using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.BusinessRule.Interface.SchemeModule
{
    public interface ISchemeTableB0
    {
        //删除表 ， 删除列配置，删除create表

        bool DeleteTableInfo(string tableID, string tableName);
    }
}
