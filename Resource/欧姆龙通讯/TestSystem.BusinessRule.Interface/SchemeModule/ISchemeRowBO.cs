using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TestSystem.BusinessRule.Interface.SchemeModule
{
    public interface ISchemeRowBO
    {
        /// <summary>
        /// 表数据保存，默认最后一条baseinfo表记录ID作为外键
        /// </summary>        
        /// <param name="table_Code">表名：例：BaseInfo，TestData</param>
        /// <param name="argColName">列中文名</param>
        /// <param name="argColContent">列内容</param>
        /// <param name="hasCofig">true:与Config表有外键关系</param>
        bool SaveData(string table_Code, string[] argColName, object[] argContent, bool hasConfig);


        /// <summary>
        /// 表数据保存，以自定义列与值对应的baseinfo表ID作为外键
        /// </summary>
        /// <param name="table_Code">表名,例：BaseInfo，TestData</param>
        /// <param name="argColName">列中文名</param>
        /// <param name="argContent">列内容</param>
        /// <param name="hasConfig">true：与Config表有外键关系</param>
        /// <param name="refKeyColName">自定义外键列中文名</param>
        /// <param name="refKeyColContent">自定义外键列值</param>
        /// <returns></returns>
        bool SaveData(string table_Code, string[] argColName, object[] argContent, bool hasConfig, string refKeyColName, string refKeyColContent);
        

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="Table_Name">表名：例，BaseInfo，TestData</param>
        /// <param name="queryType">列名</param>
        /// <param name="queryStr">查询条件</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        DataTable SelectData(string Table_Name, string queryType, string queryStr, string startTime, string endTime);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="Table_Name">表名：例：BaseInfo，TestData</param>
        /// <returns></returns>
        DataTable SelectData(string Table_Name);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="table_Code">表名：例：BaseInfo，TestData</param>
        /// <param name="keyColumnName">主键列或不变数据列名</param>
        /// <param name="argColName">列名数组</param>
        /// <param name="argContent">除主键列外其他列内容可变更内容的数组</param>
        /// <returns></returns>
        bool ModifyData(string table_Code, string keyColumnName, string[] argColName, string[] argContent);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="table_Code">表名：例：BaseInfo，TestData</param>        
        /// <param name="argColName">删除查询条件列名</param>
        /// <param name="argContent">删除查询条件列对应的列内容</param>
        /// <returns></returns>
        bool DeleteData(string table_Code, string[] argColName, string[] argContent);
    }
}
