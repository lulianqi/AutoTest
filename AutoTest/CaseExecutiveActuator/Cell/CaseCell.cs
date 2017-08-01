using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CaseExecutiveActuator.Cell
{
    //using CaseCell = TreeNode;//可让2类完全等价
    public class CaseCell
    {
        List<CaseCell> childCellList;

        private CaseType caseType;
        private XmlNode caseXmlNode;
        private MyRunCaseData<ICaseExecutionContent> caseRunData;
        private object uiTag;

        private CaseCell nextCell;
        private CaseCell parentCell;


        public CaseCell()
        {

        }

        /// <summary>
        /// CaseCell构造函数
        /// </summary>
        /// <param name="yourCaseType">CaseType</param>
        /// <param name="yourXmlNode">CaseCell脚本原始信息</param>
        /// <param name="yourCaseRunData">CaseCell脚本解析后的信息</param>
        public CaseCell(CaseType yourCaseType, XmlNode yourXmlNode, MyRunCaseData<ICaseExecutionContent> yourCaseRunData)
        {
            caseType = yourCaseType;
            caseXmlNode = yourXmlNode;
            caseRunData = yourCaseRunData;
        }

        /// <summary>
        /// 获取或设置CaseCell脚本解析后的信息
        /// </summary>
        public MyRunCaseData<ICaseExecutionContent> CaseRunData
        {
            get { return caseRunData; }
            set { caseRunData = value; }
        }

        /// <summary>
        /// 获取或设置CaseCell脚本原始信息
        /// </summary>
        public XmlNode CaseXmlNode
        {
            get { return caseXmlNode; }
            set { caseXmlNode = value; }
        }

        /// <summary>
        /// 获取或设置UiTag,可以用于UI控件与cell的绑定
        /// </summary>
        public object UiTag
        {
            get { return uiTag; }
            set { uiTag = value; }
        }

        /// <summary>
        /// 获取当前Cell类型
        /// </summary>
        public CaseType CaseType
        {
            get { return caseType; }
        }

        /// <summary>
        /// 获取下一个Cell，如果没有返回null
        /// </summary>
        public CaseCell NextCell
        {
            get { return nextCell; }
        }

        /// <summary>
        /// 获取当前Cell的父Cell，如果没有返回null
        /// </summary>
        public CaseCell ParentCell
        {
            get { return parentCell; }
        }

        /// <summary>
        /// 获取当前Cell的ChildCells列表
        /// </summary>
        public List<CaseCell> ChildCells
        {
            get { return childCellList; }
        }

        /// <summary>
        /// 获取一个值标识当前Cell是否有NextCell
        /// </summary>
        public bool IsHasNextCell
        {
            get { return nextCell != null; }
        }

        /// <summary>
        /// 获取一个值标识当前Cell是否有parentCell
        /// </summary>
        public bool IsHasParent
        {
            get
            {
                if (parentCell != null)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 获取一个值标识当前Cell是否有ChildCell
        /// </summary>
        public bool IsHasChild
        {
            get
            {
                if (childCellList != null)
                {
                    if (childCellList.Count != 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 设置下一个Cell
        /// </summary>
        /// <param name="yourCaseCell">下一个Cell</param>
        public void SetNextCell(CaseCell yourCaseCell)
        {
            nextCell = yourCaseCell;
        }

        /// <summary>
        /// 设置ParentCell
        /// </summary>
        /// <param name="yourCaseCell">ParentCell</param>
        public void SetParentCell(CaseCell yourCaseCell)
        {
            parentCell = yourCaseCell;
        }

        /// <summary>
        /// 向当前Cell中插入子Cell
        /// </summary>
        /// <param name="yourCaseCell">子Cell</param>
        public void Add(CaseCell yourCaseCell)
        {
            if (childCellList == null)
            {
                childCellList = new List<CaseCell>();
            }
            yourCaseCell.SetParentCell(this);
            childCellList.Add(yourCaseCell);
            if (childCellList.Count > 1)
            {
                childCellList[childCellList.Count - 2].SetNextCell(yourCaseCell);
            }
        }

        //一个tag存放ui指针/引用
        //实现一个Nodes.Count计算每层数目，或返回是否有子结构
        //Nodes[0]索引或实现NodeStart,返回层中第一个CaseCell
        //实现一个NextNode返回层中的下一个CaseCell
    }

}
