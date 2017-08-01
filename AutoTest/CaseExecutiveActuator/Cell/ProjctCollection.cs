using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

namespace CaseExecutiveActuator.Cell
{
    public class ProjctCollection
    {
        List<CaseCell> myProjectChilds;

        public List<CaseCell> ProjectCells
        {
            get { return myProjectChilds; }
        }

        public void Add(CaseCell yourCaseCell)
        {
            if (myProjectChilds == null)
            {
                myProjectChilds = new List<CaseCell>();
            }
            myProjectChilds.Add(yourCaseCell);
        }

        public CaseCell this[int indexP, int indexC]
        {
            get
            {
                if (myProjectChilds.Count > indexP)
                {
                    if (myProjectChilds[indexP].IsHasChild)
                    {
                        if (myProjectChilds[indexP].ChildCells.Count > indexC)
                        {
                            return myProjectChilds[indexP].ChildCells[indexC];
                        }
                    }
                }
                return null;
            }
        }


    }
}
