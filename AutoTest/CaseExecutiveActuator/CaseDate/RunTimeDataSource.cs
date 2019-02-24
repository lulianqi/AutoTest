﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyCommonHelper;

/*******************************************************************************
* Copyright (c) 2015 lijie
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   201505016           创建人: 李杰 15158155511
* 描    述: 创建
*
* 历史记录:
* 日	  期:   201708001           修改: 李杰 15158155511
* 描    述: 拆分
*******************************************************************************/

namespace CaseExecutiveActuator
{

    #region IRunTimeDataSource
    public class MyStaticDataSourceCsv : IRunTimeDataSource
    {
        private bool isNew;
        private int nowRowIndex;
        private int nowColumnIndex;
        private List<List<string>> csvData;

        public string OriginalConnectString { get; private set; }
        public string RunTimeStaticDataTypeAlias
        {
            get { return "staticDataSource_csv"; }
        }

        public MyStaticDataSourceCsv(List<List<string>> yourCsvData)
        {
            isNew = true;
            nowRowIndex = 0;
            nowColumnIndex = 0;
            if (!SetDataSource(yourCsvData))
            {
                csvData = new List<List<string>>() { new List<string>() { "NullData" } };
            }
        }

        public MyStaticDataSourceCsv(List<List<string>> yourCsvData, string originalConnectString)
            : this(yourCsvData)
        {
            OriginalConnectString = originalConnectString;
        }
        public object Clone()
        {
            return new MyStaticDataSourceCsv(csvData, OriginalConnectString);
        }
        public bool IsConnected
        {
            get { return true; }
        }

        public bool ConnectDataSource()
        {
            return true;
        }

        public bool DisConnectDataSource()
        {
            return true;
        }

        public List<List<string>> GetDataSource()
        {
            return csvData;
        }

        public bool SetDataSource(List<List<string>> yourDataSource)
        {
            if (yourDataSource.Count == 0 || yourDataSource[0] == null || yourDataSource[0].Count == 0)
            {
                return false;
            }
            for (int i = yourDataSource.Count - 1; i >= 0; i--)
            {
                if (yourDataSource[i] == null || yourDataSource[i].Count == 0)
                {
                    yourDataSource.RemoveAt(i);
                }
            }
            csvData = yourDataSource;
            if (nowRowIndex >= yourDataSource.Count || nowColumnIndex >= yourDataSource[nowRowIndex].Count)
            {
                DataReset();
            }
            return true;
        }


        public string GetDataVaule(string vauleAddress)
        {
            if (vauleAddress != null)
            {
                int[] csvPosition;
                if (vauleAddress.MySplitToIntArray('-', out csvPosition))
                {
                    if (csvPosition.Length == 2)
                    {
                        return GetDataVaule(csvPosition[1], csvPosition[0]);
                    }
                }
            }
            return null;
        }

        public string GetDataVaule(int yourRowIndex, int yourColumnIndex)
        {
            if (yourRowIndex < csvData.Count)
            {
                if (yourColumnIndex < csvData[yourRowIndex].Count)
                {
                    return csvData[yourRowIndex][yourColumnIndex] ?? "";
                }
            }
            return null;
        }

        public string DataCurrent()
        {
            //不需要检查 Index ，索引在内部操作，不可能越界  , 内部取值先定位到行再定位到列， 而外部XY坐标系是先X（列号）再Y（行号），需要注意是正好相反的（vauleAddress 使用坐标系）
            return csvData[nowRowIndex][nowColumnIndex] ?? "";
        }

        public string DataMoveNext()
        {
            if (isNew)
            {
                isNew = false;
            }
            else
            {
                //内部游标没有变化前不会越界
                if (nowColumnIndex + 1 < csvData[nowRowIndex].Count)
                {
                    nowColumnIndex++;
                }
                else if (nowRowIndex + 1 < csvData.Count)
                {
                    nowColumnIndex = 0;
                    nowRowIndex++;
                }
                else
                {
                    DataReset();
                }
            }
            return DataCurrent();
        }

        public void DataReset()
        {
            //对于csv文件解析出来的数据不可能出现空行空列的情况，所以（0,0）
            nowRowIndex = 0;
            nowColumnIndex = 0;
            isNew = true;
        }

        /// <summary>
        ///  设置源数据（使用|分割数据地址及数据值，如果以|开头则表示设置当前地址的值，不含有|的数据也表示当前值）
        /// </summary>
        /// <param name="ExpressionData">数据地址及数据内容字符串</param>
        /// <returns>是否完成</returns>
        public bool DataExpressionSet(string ExpressionData)
        {
            if (ExpressionData != null)
            {
                int splitIndex = ExpressionData.IndexOf('|');
                if (splitIndex > 0)
                {
                    return DataSet(ExpressionData.Substring(0, splitIndex), ExpressionData.Remove(0, splitIndex + 1));
                }
                else if (splitIndex == 0)
                {
                    return DataSet(ExpressionData.Remove(0, 1));
                }
                else
                {
                    return DataSet(ExpressionData);
                }
            }
            return false;
        }

        public bool DataSet(string expectData)
        {
            if (expectData != null)
            {
                csvData[nowRowIndex][nowColumnIndex] = expectData;
                return true;
            }
            return false;
        }

        public bool DataSet(int yourRowIndex, int yourColumnIndex, string expectData)
        {
            if (yourRowIndex < 0 || yourColumnIndex < 0)
            {
                return false;
            }
            if (yourColumnIndex > csvData.Count - 1)
            {
                for (int i = 0; yourColumnIndex > csvData.Count - 1; i++)
                {
                    csvData.Add(new List<string> { "" });
                }
            }
            if (yourRowIndex > csvData[yourColumnIndex].Count - 1)
            {
                for (int i = 0; yourRowIndex > csvData[yourRowIndex].Count - 1; i++)
                {
                    csvData[yourRowIndex].Add("");
                }
            }
            csvData[yourRowIndex][yourColumnIndex] = expectData;
            return true;
        }

        public bool DataSet(string vauleAddress, string expectData)
        {
            if (vauleAddress != null)
            {
                int[] csvPosition;
                if (vauleAddress.MySplitToIntArray('-', out csvPosition))
                {
                    if (csvPosition.Length == 2)
                    {
                        DataSet(csvPosition[1], csvPosition[0], expectData);
                        return true;
                    }
                }
            }
            return false;
        }
    }

    #endregion
}
