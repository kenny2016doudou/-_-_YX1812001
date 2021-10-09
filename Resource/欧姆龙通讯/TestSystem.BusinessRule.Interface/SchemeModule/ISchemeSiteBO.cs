using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Model.Entity.SchemeModule;
using System.Data;

namespace TestSystem.BusinessRule.Interface.SchemeModule
{
    public interface ISchemeSiteBO
    {

        /// <summary>
        /// 配置试验台服务点
        /// </summary>
        /// <param name="schemeInfo">解决方法对象</param>
        /// <param name="num">试验台个数</param>
        /// <returns></returns>
        bool SaveData(SchemeInfo schemeInfo, int num);

        /// <summary>
        /// 查询实验台服务点
        /// </summary>
        /// <param name="sTestBed_ID">服务点ID, 为空：""</param>
        /// <param name="sScheme_ID">解决方案ID, 为空：""</param>
        /// <param name="sTestBed_Name">试验台名称，为空：""</param>
        /// <param name="iTestBed_Sequence">排序号，为空：-1</param>
        /// <returns></returns>
        DataSet SelectSchemeSite(string sTestBed_ID, string sScheme_ID, string sTestBed_Name, int iTestBed_Sequence);

    }
}
