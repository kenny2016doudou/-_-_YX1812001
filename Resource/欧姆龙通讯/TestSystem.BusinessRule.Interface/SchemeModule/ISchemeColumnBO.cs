using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Model.Entity.SchemeModule;


namespace TestSystem.BusinessRule.Interface.SchemeModule
{
    public interface ISchemeColumnBO
    {
        //保存列配置，新增列，修改列，删除列,,（列配置处理，表处理）
        /// <summary>
        /// 重新生成表
        /// </summary>
        bool SaveColumns(List<SchemeColumn> listColumns, string table_Name, List<SchemeColumn> listColumnsToDel);

     

        /// <summary>
        /// 保存列配置，并修改表结构
        /// </summary>
        /// <param name="table_Name"></param>
        /// <param name="_ColumnToAdd"></param>
        /// <returns></returns>
        bool SaveColumn(string table_Name, SchemeColumn _ColumnToAdd);

        /// <summary>
        /// 删除表配置，并修改表结构
        /// </summary>
        /// <param name="table_Name"></param>
        /// <param name="_ColumnToDel"></param>
        /// <returns></returns>
        bool DeleteColumn(string table_Name, SchemeColumn _ColumnToDel);


        /// <summary>
        /// 操作新表的结构 OK
        /// </summary>
        /// <param name="listColumns"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool CreateNewTableColumns(List<SchemeColumn> listColumns, string tableName);


        /// <summary>
        /// 操作新表的列结果 OK
        /// </summary>
        /// <param name="colobj">列对象</param>
        /// <param name="type">1.新增， 2.修改，3，无操作，4，删除，</param>
        /// <returns></returns>
        bool EditNewTableColumns(SchemeColumn colobj, string tableName,string old_Col_Name, int type);

        
    }
}
