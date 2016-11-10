using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;


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
*******************************************************************************/


namespace AutoTest.myTool
{
    class myStruct
    {
    }



    public struct myDataIn
    {
        public string interfaceName, caseID, isbreak, interfaceOutData_1, interfaceOutData_2, otherOutData;
        public string ret;
        public myDataIn(string tempVal)
        {
     
            interfaceName = tempVal;
            caseID = isbreak = interfaceOutData_1 = interfaceOutData_2 = otherOutData = tempVal;
            ret = tempVal;
        }
    }

    public struct myDataOut
    {
        public string interfaceName, caseID, caseTime, interfaceOutData_1, interfaceOutData_2,
            interfaceOutData_6, interfaceOutData_3, interfaceOutData_4, interfaceOutData_5, interfaceOutData_7, interfaceOutData_8, longData ;
        public myDataOut(string tempVal)
        {
            interfaceName = caseID = caseTime = interfaceOutData_1 = interfaceOutData_2 =
            interfaceOutData_6 = interfaceOutData_3 = interfaceOutData_4 = interfaceOutData_5 = interfaceOutData_7 = interfaceOutData_8 = longData = tempVal;
        }
    }

    public struct myDataInEx
    {
        public int x, y;

        public myDataInEx(int p1, int p2)
        {
            
            x = p1;
            y = p2;
        }
    }


    /// <summary>
    /// 执行记数
    /// </summary>
    public struct myRunCount
    {
        public int allCount;
        public int passCount;
        public int failCount;
        public int otherCount;
        //public myRunCount()
        //{
        //    allCount = 0;
        //    passCount = 0;
        //    failCount = 0;
        //    otherCount = 0;
        //}
    }
}
