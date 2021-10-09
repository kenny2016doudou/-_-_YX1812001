using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TestSystem.Model.Entity.SchemeModule;

namespace TestSystem.BusinessRule.Interface.SchemeModule
{
    public interface ISchemeUCBO
    {
        /// <summary>
        /// 根据用户控件分类名称查询用户控件配置信息
        /// </summary>                
        /// <param name="Type_Name">用户控件分类名称：例,数据查询</param>
        /// <returns></returns>
        SchemeUC SelectSchemeUC(string Type_Name);
    }
}
