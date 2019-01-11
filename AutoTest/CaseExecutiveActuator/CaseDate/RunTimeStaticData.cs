using System;
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

    
    #region IRunTimeStaticData  

    //这里的IRunTimeStaticData全部是struct，如果要修改为class 请重新检查深度克隆的逻辑   (已经切换为class)

    /// <summary>
    /// 为StaticData提供类似索引递增的动态数据【IRunTimeStaticData】
    /// </summary>
    public class MyStaticDataIndex : IRunTimeStaticData
    {
        private bool isNew;
        private int dataIndex;
        private int defaultStart;
        private int defaultEnd;
        private int defaultStep;

        public string OriginalConnectString { get; private set; }
        public string RunTimeStaticDataTypeAlias
        {
            get { return "staticData_index"; }
        }

        public MyStaticDataIndex(int yourStart, int yourEnd, int yourStep)
        {
            isNew = true;
            dataIndex = defaultStart = yourStart;
            defaultEnd = yourEnd;
            defaultStep = yourStep;
        }

        public MyStaticDataIndex(int yourStart, int yourEnd, int yourStep, string originalConnectString)
            : this(yourStart, yourEnd, yourStep)
        {
            OriginalConnectString = originalConnectString;
        }

        public object Clone()
        {
            return new MyStaticDataIndex(defaultStart, defaultEnd, defaultStep, OriginalConnectString);
        }


        public string DataCurrent()
        {
            return dataIndex.ToString();
        }

        public string DataMoveNext()
        {
            if (isNew)
            {
                isNew = false;
                return dataIndex.ToString();
            }
            if (dataIndex >= defaultEnd)
            {
                DataReset();
                return DataMoveNext();
            }
            else
            {
                dataIndex += defaultStep;
            }
            return dataIndex.ToString();
        }


        public void DataReset()
        {
            isNew = true;
            dataIndex = defaultStart;
        }


        public bool DataSet(string expectData)
        {
            int tempData;
            if (int.TryParse(expectData, out tempData))
            {
                if (tempData >= defaultStart && tempData <= defaultEnd)
                {
                    dataIndex = tempData;
                    return true;
                }
            }
            return false;
        }

    }


    /// <summary>
    /// 为StaticData提定长字符串型数字索引支持【IRunTimeStaticData】
    /// </summary>
    public class MyStaticDataStrIndex : IRunTimeStaticData
    {
        private bool isNew;
        private long dataIndex;
        private long defaultStart;
        private long defaultEnd;
        private long defaultStep;
        private int strLen;

        public string OriginalConnectString { get; private set; }
        public string RunTimeStaticDataTypeAlias
        {
            get { return "staticData_strIndex"; }
        }
        public MyStaticDataStrIndex(long yourStart, long yourEnd, long yourStep, int yourStrLen)
        {
            isNew = true;
            dataIndex = defaultStart = yourStart;
            defaultEnd = yourEnd;
            defaultStep = yourStep;
            strLen = yourStrLen;
        }

        public MyStaticDataStrIndex(long yourStart, long yourEnd, long yourStep, int yourStrLen, string originalConnectString)
            : this(yourStart, yourEnd, yourStep, yourStrLen)
        {
            OriginalConnectString = originalConnectString;
        }

        public object Clone()
        {
            return new MyStaticDataStrIndex(defaultStart, defaultEnd, defaultStep, strLen, OriginalConnectString);
        }

        private string GetLenStr(long yourLeng)
        {
            string outStr = yourLeng.ToString();
            int distinction = strLen - outStr.Length;
            if (distinction > 0)
            {
                for (int i = 0; i < distinction; i++)
                {
                    outStr = "0" + outStr;
                }
            }
            return outStr;
        }

        public string DataCurrent()
        {
            return GetLenStr(dataIndex);
        }

        public string DataMoveNext()
        {
            if (isNew)
            {
                isNew = false;
                return GetLenStr(dataIndex);
            }
            if (dataIndex >= defaultEnd)
            {
                DataReset();
                return DataMoveNext();
            }
            else
            {
                dataIndex += defaultStep;
            }
            return GetLenStr(dataIndex);
        }


        public void DataReset()
        {
            isNew = true;
            dataIndex = defaultStart;
        }


        public bool DataSet(string expectData)
        {
            long tempData;
            if (long.TryParse(expectData, out tempData))
            {
                if (tempData >= defaultStart && tempData <= defaultEnd)
                {
                    dataIndex = tempData;
                    return true;
                }
            }
            return false;
        }

    }

    /// <summary>
    /// 为StaticData提供长数字索引支持【IRunTimeStaticData】
    /// </summary>
    public class MyStaticDataLong : IRunTimeStaticData
    {
        private bool isNew;
        private long dataIndex;
        private long defaultStart;
        private long defaultEnd;
        private long defaultStep;

        public string OriginalConnectString { get; private set; }
        public string RunTimeStaticDataTypeAlias
        {
            get { return "staticData_long"; }
        }
        public MyStaticDataLong(long yourStart, long yourEnd, long yourStep)
        {
            isNew = true;
            dataIndex = defaultStart = yourStart;
            defaultEnd = yourEnd;
            defaultStep = yourStep;
        }

        public MyStaticDataLong(long yourStart, long yourEnd, long yourStep, string originalConnectString)
            : this(yourStart, yourEnd, yourStep)
        {
            OriginalConnectString = originalConnectString;
        }

        public object Clone()
        {
            return new MyStaticDataLong(defaultStart, defaultEnd, defaultStep, OriginalConnectString);
        }


        public string DataCurrent()
        {
            return dataIndex.ToString();
        }

        public string DataMoveNext()
        {
            if (isNew)
            {
                isNew = false;
                return dataIndex.ToString();
            }
            if (dataIndex >= defaultEnd)
            {
                DataReset();
                return DataMoveNext();
            }
            else
            {
                dataIndex += defaultStep;
            }
            return dataIndex.ToString();
        }


        public void DataReset()
        {
            isNew = true;
            dataIndex = defaultStart;
        }


        public bool DataSet(string expectData)
        {
            long tempData;
            if (long.TryParse(expectData, out tempData))
            {
                if (tempData >= defaultStart && tempData <= defaultEnd)
                {
                    dataIndex = tempData;
                    return true;
                }
            }
            return false;
        }

    }

    /// <summary>
    /// 为StaticData提供随机字符串动态数据【IRunTimeStaticData】
    /// </summary>
    public class MyStaticDataRandomStr : IRunTimeStaticData
    {
        string myNowStr;
        int myStrNum;
        int myStrType;

        public string OriginalConnectString { get; private set; }
        public string RunTimeStaticDataTypeAlias
        {
            get { return "staticData_random"; }
        }

        public MyStaticDataRandomStr(int yourStrNum, int yourStrType)
        {
            myNowStr = "";
            myStrNum = yourStrNum;
            myStrType = yourStrType;
        }

        public MyStaticDataRandomStr(int yourStrNum, int yourStrType, string originalConnectString)
            : this(yourStrNum, yourStrType)
        {
            OriginalConnectString = originalConnectString;
        }

        public object Clone()
        {
            return new MyStaticDataRandomStr(myStrNum, myStrType, OriginalConnectString);
        }

        public string DataCurrent()
        {
            return myNowStr;
        }

        public string DataMoveNext()
        {
            myNowStr = MyCommonTool.GenerateRandomStr(myStrNum, myStrType);
            return myNowStr;
        }

        public void DataReset()
        {
            myNowStr = "";
        }


        public bool DataSet(string expectData)
        {
            if (expectData != null)
            {
                myNowStr = expectData;
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// 为StaticData提供当前时间的动态数据【IRunTimeStaticData】
    /// </summary>
    public class MyStaticDataNowTime : IRunTimeStaticData
    {
        string myNowStr;
        string myDataFormatInfo;

        public string OriginalConnectString { get; private set; }
        public string RunTimeStaticDataTypeAlias
        {
            get { return "staticData_time"; }
        }

        public MyStaticDataNowTime(string yourRormatInfo)
        {
            myNowStr = "";
            myDataFormatInfo = yourRormatInfo;
        }

        public MyStaticDataNowTime(string yourRormatInfo, string originalConnectString)
            : this(yourRormatInfo)
        {
            OriginalConnectString = originalConnectString;
        }

        public object Clone()
        {
            return new MyStaticDataNowTime(myDataFormatInfo, OriginalConnectString);
        }

        public string DataCurrent()
        {
            return myNowStr;
        }

        public string DataMoveNext()
        {
            myNowStr = System.DateTime.Now.ToString(myDataFormatInfo);
            return myNowStr;
        }

        public void DataReset()
        {
            myNowStr = "";
        }


        public bool DataSet(string expectData)
        {
            if (expectData != null)
            {
                myNowStr = expectData;
                return true;
            }
            return false;
        }
    }

    /// <summary>
    ///  为StaticData提供当基于List的列表数据支持据【IRunTimeStaticData】
    /// </summary>
    public class MyStaticDataList : IRunTimeStaticData
    {
        private bool isNew;
        private string souseData;
        private List<string> souseListData;
        private int nowIndex;
        private bool isRandom;
        private Random ran;

        public string OriginalConnectString { get;private set; }
        public string RunTimeStaticDataTypeAlias
        {
            get { return "staticData_list"; }
        }

        public MyStaticDataList(string yourSourceData, bool isRandomNext)
        {
            isNew = true;
            souseData = yourSourceData;
            souseListData = yourSourceData.Split(',').ToList();
            nowIndex = 0;
            isRandom = isRandomNext;
            if (isRandom)
            {
                ran = new Random();
            }
            else
            {
                ran = null;
            }
        }

        public MyStaticDataList(string yourSourceData, bool isRandomNext, string originalConnectString)
            : this(yourSourceData, isRandomNext)
        {
            OriginalConnectString = originalConnectString;
        }

        public object Clone()
        {
            return new MyStaticDataList(souseData, isRandom, OriginalConnectString);
        }

        public string DataCurrent()
        {
            return souseListData[nowIndex];
        }

        public string DataMoveNext()
        {
            if (isRandom)
            {
                nowIndex = ran.Next(0, souseListData.Count - 1);
                return souseListData[nowIndex];
            }
            else
            {
                if (isNew)
                {
                    isNew = false;
                }
                else
                {
                    nowIndex++;
                    if (nowIndex > (souseListData.Count - 1))
                    {
                        nowIndex = 0;
                    }
                }
                return souseListData[nowIndex];
            }
        }

        public void DataReset()
        {
            isNew = true;
            nowIndex = 0;
        }

        public bool DataSet(string expectData)
        {
            if (souseListData.Contains(expectData))
            {
                nowIndex = souseListData.IndexOf(expectData);
            }
            return false;
        }
    }

    #endregion
}
