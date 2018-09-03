using CaseExecutiveActuator;
using CaseExecutiveActuator.CaseActuator;
using CaseExecutiveActuator.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Windows.Forms;
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


namespace CaseExecutiveActuator.CaseActuator
{
    public delegate void delegateQueueChangeEventHandler(CaseCell yourTarget, string yourMessage);
    public delegate void delegateLoopChangeEventHandler(CaseCell yourTarget, string yourMessage);

    class RunCaseCount
    {
        /// <summary>
        /// for count strut
        /// </summary>
        struct CaseLoopCountInfo
        {
            CaseCell loopNode;
            int caseRate;

            /// <summary>
            /// Initialization the CaseLoopCountInfo
            /// </summary>
            /// <param name="yourLoopNode">your LoopNode</param>
            /// <param name="yourCaseRate">your CaseRate</param>
            public CaseLoopCountInfo(CaseCell yourLoopNode, int yourCaseRate)
            {
                loopNode = yourLoopNode;
                caseRate = yourCaseRate;
            }

            /// <summary>
            /// get the LoopNode
            /// </summary>
            public CaseCell LoopNode
            {
                get { return loopNode; }
            }

            /// <summary>
            /// get the CaseRate
            /// </summary>
            public int CaseRate
            {
                get { return caseRate; }
            }
        }


        /// <summary>
        /// get main task case count(just main but not Include the goto case)
        /// </summary>
        /// <param name="startNode">start Node</param>
        /// <returns>count</returns>
        public static int GetCount(CaseCell startNode)
        {
            int nowCount = 0;
            List<CaseLoopCountInfo> nowLoops = new List<CaseLoopCountInfo>();
            while (startNode!=null)
            {
                if (startNode.CaseType == CaseType.Case)
                {
                    nowCount++;
                }
                else if (startNode.CaseType == CaseType.Repeat)
                {
                    if (startNode.IsHasChild)
                    {
                        myCaseLaodInfo tempProjectLoadInfo = MyCaseScriptAnalysisEngine.getCaseLoadInfo(startNode.CaseXmlNode);
                        nowLoops.Add(new CaseLoopCountInfo(startNode.ChildCells[0], tempProjectLoadInfo.times));
                    }
                }
                else if (startNode.CaseType == CaseType.Project)
                {
                    if(startNode.IsHasChild)
                    {
                        startNode = startNode.ChildCells[0];
                    }
                    continue;
                }
                startNode = startNode.NextCell;
            }
            while (nowLoops.Count!=0)
            {
                startNode = nowLoops[nowLoops.Count - 1].LoopNode;
                int tempRate = nowLoops[nowLoops.Count - 1].CaseRate;
                nowLoops.Remove(nowLoops[nowLoops.Count - 1]);
                while (startNode != null)
                {
                    if (startNode.CaseType == CaseType.Case)
                    {
                        nowCount += tempRate;
                    }
                    else if (startNode.CaseType == CaseType.Repeat)
                    {
                        if (startNode.IsHasChild)
                        {
                            myCaseLaodInfo tempProjectLoadInfo = MyCaseScriptAnalysisEngine.getCaseLoadInfo(startNode.CaseXmlNode);
                            nowLoops.Add(new CaseLoopCountInfo(startNode.ChildCells[0], tempProjectLoadInfo.times * tempRate));
                        }
                    }
                    startNode = startNode.NextCell;
                }
            }
            return nowCount;
        }


    }


    /// <summary>
    /// CsaeQueue it will only used in myCaseRunTime
    /// </summary>
    class MyCsaeQueue
    {
        private CaseCell startCaseNode;
        private CaseCell nowCaseNode;
        List<MyCaseLoop> myCaseLoopList;

        private int queueTotalCount;
        private int queueNowCount;

        public event delegateLoopChangeEventHandler OnLoopChangeEvent;

        /// <summary>
        /// myCsaeQueue initialize
        /// </summary>
        /// <param name="yourStartCase">your StartCase and make sure it is not null</param>
        public MyCsaeQueue(CaseCell yourStartCase)
        {
            queueTotalCount = RunCaseCount.GetCount(yourStartCase);
            startCaseNode = yourStartCase;
            nowCaseNode = null;
            myCaseLoopList = new List<MyCaseLoop>();
        }

        /// <summary>
        /// get now CaseCell
        /// </summary>
        public CaseCell NowCaseNode
        {
            get
            {
                if (nowCaseNode != null)
                {
                    if (myCaseLoopList.Count > 0)
                    {
                        return myCaseLoopList[myCaseLoopList.Count - 1].NowCaseNode;
                    }
                    else
                    {
                        return nowCaseNode;
                    }
                }
                else
                {
                    return startCaseNode;
                }
            }
        }

        /// <summary>
        /// get the Queue Count Progress(queueTotalCount and queueNowCount)
        /// </summary>
        public KeyValuePair<int,int> GetCountProgress
        {
            get
            {
                return new KeyValuePair<int, int>(queueTotalCount, queueNowCount);
            }
        }

        
        /// <summary>
        /// i will add new CaseLoop and Subscribe 【OnLoopChangeEvent】
        /// </summary>
        /// <param name="yourStartCase">your StartCase</param>
        /// <param name="yourTimes">your Times</param>
        private void AddCaseLoop(CaseCell yourStartCase, int yourTimes)
        {
            myCaseLoopList.Add(new MyCaseLoop(yourStartCase, yourTimes));
            myCaseLoopList[myCaseLoopList.Count - 1].OnLoopChangeEvent += OnLoopChangeEvent;
        }

        /// <summary>
        /// i will remove your CaseLoop and unSubscribe 【OnLoopChangeEvent】
        /// </summary>
        /// <param name="yourCaseLoop">yourCaseLoop</param>
        private void DelCaseLoop(MyCaseLoop yourCaseLoop)
        {
            yourCaseLoop.OnLoopChangeEvent -= OnLoopChangeEvent;
            myCaseLoopList.Remove(yourCaseLoop);
        }


        /// <summary>
        /// i will get the next myTreeTagInfo in my queue
        /// </summary>
        /// <returns>the CaseCell you want</returns>
        public CaseCell nextCase()
        {
           
            if(nowCaseNode==null) //起始节点
            {
                nowCaseNode = startCaseNode;
                if (nowCaseNode.CaseType == CaseType.Repeat)
                {
                    if (nowCaseNode.IsHasChild)
                    {
                        myCaseLaodInfo tempProjectLoadInfo = MyCaseScriptAnalysisEngine.getCaseLoadInfo(nowCaseNode.CaseXmlNode);
                        AddCaseLoop(nowCaseNode.ChildCells[0], tempProjectLoadInfo.times);
                    }
                    return nextCase();
                }
                else if (nowCaseNode.CaseType == CaseType.Case)
                {
                    queueNowCount++;
                    return nowCaseNode;
                }
                else if (nowCaseNode.CaseType == CaseType.Project)
                {
                    if (nowCaseNode.IsHasChild)
                    {
                        startCaseNode = nowCaseNode.ChildCells[0];
                        nowCaseNode = null;
                        return nextCase();
                    }
                    return null; //空Project
                }
                else
                {
                    return null; //当前设计不会有这种情况
                }
            }
            else
            {
                if (myCaseLoopList.Count > 0)
                {
                    int tempNowListIndex = myCaseLoopList.Count - 1;
                    CaseCell tempNextLoopTreeNode = myCaseLoopList[tempNowListIndex].nextCase();
                    if (tempNextLoopTreeNode == null)
                    {
                        DelCaseLoop(myCaseLoopList[tempNowListIndex]);
                        return nextCase();
                    }
                    else
                    {
                        if (tempNextLoopTreeNode.CaseType == CaseType.Repeat)
                        {
                            if (tempNextLoopTreeNode.IsHasChild)
                            {
                                myCaseLaodInfo tempProjectLoadInfo = MyCaseScriptAnalysisEngine.getCaseLoadInfo(tempNextLoopTreeNode.CaseXmlNode);
                                AddCaseLoop(tempNextLoopTreeNode.ChildCells[0], tempProjectLoadInfo.times);
                            }

                            return nextCase();
                        }
                        else if (tempNextLoopTreeNode.CaseType == CaseType.Case)
                        {
                            queueNowCount++;
                            return tempNextLoopTreeNode;
                        }
                        else
                        {
                            return null; //当前设计不会有这种情况
                        }
                    }
                }
                else
                {
                    if(nowCaseNode.NextCell == null)
                    {
                        return null; //当前 【Queue】 结束
                    }
                    else
                    {
                        nowCaseNode = nowCaseNode.NextCell;
                        if (nowCaseNode.CaseType == CaseType.Repeat)
                        {
                            if (nowCaseNode.IsHasChild)
                            {
                                myCaseLaodInfo tempProjectLoadInfo = MyCaseScriptAnalysisEngine.getCaseLoadInfo(nowCaseNode.CaseXmlNode);
                                AddCaseLoop(nowCaseNode.ChildCells[0], tempProjectLoadInfo.times);
                            }

                            return nextCase();
                        }
                        else if (nowCaseNode.CaseType == CaseType.Case)
                        {
                            queueNowCount++;
                            return nowCaseNode;
                        }
                        else
                        {
                            return null; //当前设计不会有这种情况
                        }
                    }
                }
            }
        }

    }

    /// <summary>
    /// CaseLoop it will only used in myCsaeQueue
    /// </summary>
    class MyCaseLoop
    {
        private CaseCell startCaseNode;
        private CaseCell nowCaseNode;
        private int totalTimes;
        private int myTimes;

        public event delegateLoopChangeEventHandler OnLoopChangeEvent;

        /// <summary>
        /// myCaseLoop initialize
        /// </summary>
        /// <param name="yourStartCase">your StartCase and make sure it is not null</param>
        /// <param name="yourTimes">your Times </param>
        public MyCaseLoop(CaseCell yourStartCase, int yourTimes)
        {
            totalTimes = myTimes = yourTimes;
            startCaseNode = yourStartCase;
            nowCaseNode = null;
        }

        /// <summary>
        /// get now CaseCell
        /// </summary>
        public CaseCell NowCaseNode
        {
            get
            {
                if (nowCaseNode != null)
                {
                    return nowCaseNode;
                }
                else
                {
                    return startCaseNode;
                }
            }
        }

        /// <summary>
        /// i will trigger 【OnLoopChangeEvent】
        /// </summary>
        /// <param name="yourTarget"></param>
        private void ReportLoopProgress(CaseCell yourTarget)
        {
            if (OnLoopChangeEvent != null)
            {
                OnLoopChangeEvent(yourTarget.ParentCell, string.Format("{0}/{1}", totalTimes, totalTimes - myTimes + 1));
            }
        }

        /// <summary>
        /// i will trigger 【OnLoopChangeEvent】 and this lood is end
        /// </summary>
        /// <param name="yourTarget"></param>
        private void ReportLoopEnd(CaseCell yourTarget)
        {
            if (OnLoopChangeEvent != null)
            {
                this.OnLoopChangeEvent(yourTarget.ParentCell, "");
            }
        }
        
        /// <summary>
        /// i will get the next myTreeTagInfo in my loop
        /// </summary>
        /// <returns>the CaseCell you want</returns>
        public CaseCell nextCase()
        {
            if (myTimes > 0)
            {
                if (nowCaseNode == null) //起始节点
                {
                    nowCaseNode = startCaseNode;
                    //report position
                    ReportLoopProgress(nowCaseNode);
                    return nowCaseNode;
                }
                else
                {
                    if (nowCaseNode.NextCell == null)
                    {
                        myTimes--;
                        if (myTimes > 0)
                        {
                            nowCaseNode = startCaseNode;
                            ReportLoopProgress(nowCaseNode);
                            return nowCaseNode;
                        }
                        else
                        {
                            ReportLoopEnd(nowCaseNode);
                            return null;   //此处为null，指示当前【Loop】结束
                        }

                    }
                    else
                    {
                        nowCaseNode = nowCaseNode.NextCell;
                        return nowCaseNode;    //此处caseType可能为case或repeat，该类的拥有者将会分别处理
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
    
    /// <summary>
    /// myCaseRunTime - you can get next case here
    /// </summary>
    public sealed class MyCaseRunTime
    {
        
        private List<MyCsaeQueue> myCsaeQueueList;
        private bool isThroughAllCase;

        /// <summary>
        /// show loop track 
        /// </summary>
        public event delegateLoopChangeEventHandler OnLoopChangeEvent;
        /// <summary>
        /// show Queue track （the frist and last Queue will nor trigger）
        /// </summary>
        public event delegateQueueChangeEventHandler OnQueueChangeEvent;

        /// <summary>
        /// myCaseRunTime initialize
        /// </summary>
        public MyCaseRunTime()
        {
            myCsaeQueueList = new List<MyCsaeQueue>();
        }

        /// <summary>
        /// get now CaseRunTime all Progress
        /// </summary>
        public List<KeyValuePair<int ,int >> GetNowCountProgress
        {
            get
            {
                List<KeyValuePair<int, int>> nowCountProgress = new List<KeyValuePair<int, int>>();
                foreach (var tempCsaeQueue in myCsaeQueueList)
                {
                    nowCountProgress.Add(tempCsaeQueue.GetCountProgress);
                }
                return nowCountProgress;
            }
        }

        /// <summary>
        /// i will add new CsaeQueue and Subscribe 【OnLoopChangeEvent】
        /// </summary>
        /// <param name="yourCsaeQueue">your CsaeQueue that will add</param>
        private void AddCsaeQueue(MyCsaeQueue yourCsaeQueue)
        {
            myCsaeQueueList.Add(yourCsaeQueue);
            yourCsaeQueue.OnLoopChangeEvent += OnLoopChangeEvent;
        }

        //// <summary>
        /// i will add new CsaeQueue and Subscribe 【OnLoopChangeEvent】(and will trigger【OnQueueChangeEvent】)
        /// </summary>
        /// <param name="yourCsaeQueue">your CsaeQueue that will add</param>
        /// <param name="yourProjectId">Project Id to OnQueueChangeEvent</param>
        /// <param name="yourCaseId">Case Id to OnQueueChangeEvent</param>
        private void AddCsaeQueue(MyCsaeQueue yourCsaeQueue, int yourProjectId, int yourCaseId)
        {
            ReportQueueAction(myCsaeQueueList[myCsaeQueueList.Count - 1].NowCaseNode, string.Format(MyConfiguration.CaseShowJumpGotoNode+"GoTo Project：{0} Case：{1}", yourProjectId, yourCaseId));
            AddCsaeQueue(yourCsaeQueue);
            ReportQueueAction(myCsaeQueueList[myCsaeQueueList.Count - 1].NowCaseNode, MyConfiguration.CaseShowGotoNodeStart);
        }

        /// <summary>
        /// i will remove the CaseQueue and unSubscribe 【OnLoopChangeEvent】
        /// </summary>
        /// <param name="yourCsaeQueue">your CsaeQueue that will rwmove</param>
        private void DelCsaeQueue(MyCsaeQueue yourCsaeQueue)
        {
            if (myCsaeQueueList.Count>1)
            {
                ReportQueueAction(yourCsaeQueue.NowCaseNode, MyConfiguration.CaseShowJumpGotoNode);
                ReportQueueAction(myCsaeQueueList[myCsaeQueueList.Count - 2].NowCaseNode, MyConfiguration.CaseShowJumpGotoNode + MyConfiguration.CaseShowGotoNodeStart);
            }
            yourCsaeQueue.OnLoopChangeEvent -= OnLoopChangeEvent;
            myCsaeQueueList.Remove(yourCsaeQueue);
        }


        /// <summary>
        /// i will report the QueueAction to his user 
        /// </summary>
        /// <param name="yourTarget">your CaseCell Target</param>
        /// <param name="yourMessage">your Message</param>
        private void ReportQueueAction(CaseCell yourTarget, string yourMessage)
        {
            if (OnQueueChangeEvent != null)
            {
                OnQueueChangeEvent(yourTarget, yourMessage);
            }
        }


        /// <summary>
        /// you must readyStart before get nextCase (and here also can reset the StartCase)
        /// </summary>
        /// <param name="yourStartCase">your StartCase</param>
        public void readyStart(CaseCell yourStartCase)
        {
            myCsaeQueueList.Clear();
            AddCsaeQueue(new MyCsaeQueue(yourStartCase));
            ReportQueueAction(yourStartCase, MyConfiguration.CaseShowCaseNodeStart);
        }


        /// <summary>
        /// you must readyStart before get nextCase (and here also can reset the StartCase)
        /// </summary>
        /// <param name="yourStartCase">your StartCase</param>
        /// <param name="yourIsThrough">it will change the behaviour that is it will go through all case(now it is replaced by [goto])</param>
        public void readyStart(CaseCell yourStartCase, bool yourIsThrough)
        {
            readyStart(yourStartCase);
            isThroughAllCase = yourIsThrough;
        }

        /// <summary>
        /// i will get the next myTreeTagInfo in myCaseRunTime
        /// </summary>
        /// <returns>the CaseCell you want</returns>
        public CaseCell nextCase()
        {
            if (myCsaeQueueList.Count > 0)
            {
                CaseCell tempTreeNodeCase = myCsaeQueueList[myCsaeQueueList.Count - 1].nextCase();
                if(tempTreeNodeCase==null)
                {
                    DelCsaeQueue(myCsaeQueueList[myCsaeQueueList.Count - 1]);
                    return nextCase();
                }
                else
                {
                    return tempTreeNodeCase;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// here i will jump into other case in myCaseRunTime
        /// </summary>
        /// <param name="yourProjectId">your Project Id</param>
        /// <param name="yourCaseId">your Case Id</param>
        /// <returns>is success</returns>
        public bool gotoMyCase(int yourProjectId, int yourCaseId, Dictionary<int, Dictionary<int, CaseCell>> myRunTimeCaseDictionary)
        {
            if (myRunTimeCaseDictionary.ContainsKey(yourProjectId))
            {
                if (myRunTimeCaseDictionary[yourProjectId].ContainsKey(yourCaseId))
                {
                    AddCsaeQueue(new MyCsaeQueue(myRunTimeCaseDictionary[yourProjectId][yourCaseId]), yourProjectId, yourCaseId);
                    return true;
                }
                else
                {
                    ReportQueueAction(myCsaeQueueList[myCsaeQueueList.Count - 1].NowCaseNode, MyConfiguration.CaseShowJumpGotoNode + "GoTo error");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    /*
    * this class reference System.Windows.Forms so remove 
    /// <summary>
    /// myCelerityCaseRunTime same to myCaseRunTime ,but it more higher performance
    /// </summary>
    public class MyCelerityCaseRunTime
    {
        /// <summary>
        /// CsaeQueue it will only used in myCaseRunTime
        /// </summary>
        private class myCsaeQueue
        {
            private TreeNode startCaseNode;
            private TreeNode nowCaseNode;
            List<myCaseLoop> myCaseLoopList;

            /// <summary>
            /// myCsaeQueue initialize
            /// </summary>
            /// <param name="yourStartCase">your StartCase and make sure it is not null</param>
            public myCsaeQueue(TreeNode yourStartCase)
            {
                startCaseNode = yourStartCase;
                nowCaseNode = null;
                myCaseLoopList = new List<myCaseLoop>();
            }



            /// <summary>
            /// i will get the next myTreeTagInfo in my queue
            /// </summary>
            /// <returns>the TreeNode you want</returns>
            public TreeNode nextCase()
            {
                if (nowCaseNode == null) //起始节点
                {
                    nowCaseNode = startCaseNode;
                    if (((myTreeTagInfo)(nowCaseNode.Tag)).tagCaseType == CaseType.Repeat)
                    {
                        if (nowCaseNode.Nodes.Count > 0)
                        {
                            myCaseLaodInfo tempProjectLoadInfo = MyCaseScriptAnalysisEngine.getCaseLoadInfo(((myTreeTagInfo)(nowCaseNode.Tag)).tagCaseXmlNode);
                            myCaseLoopList.Add(new myCaseLoop(nowCaseNode.Nodes[0], tempProjectLoadInfo.times));
                        }
                        return nextCase();
                    }
                    else if (((myTreeTagInfo)(nowCaseNode.Tag)).tagCaseType == CaseType.Case)
                    {
                        return nowCaseNode;
                    }
                    else if (((myTreeTagInfo)(nowCaseNode.Tag)).tagCaseType == CaseType.Project)
                    {
                        if (nowCaseNode.Nodes.Count > 0)
                        {
                            startCaseNode = nowCaseNode.Nodes[0];
                            nowCaseNode = null;
                            return nextCase();
                        }
                        return null; //空Project
                    }
                    else
                    {
                        return null; //当前设计不会有这种情况
                    }
                }
                else
                {
                    if (myCaseLoopList.Count > 0)
                    {
                        int tempNowListIndex = myCaseLoopList.Count - 1;
                        TreeNode tempNextLoopTreeNode = myCaseLoopList[tempNowListIndex].nextCase();
                        if (tempNextLoopTreeNode == null)
                        {
                            myCaseLoopList.Remove(myCaseLoopList[tempNowListIndex]);
                            return nextCase();
                        }
                        else
                        {
                            if (((myTreeTagInfo)(tempNextLoopTreeNode.Tag)).tagCaseType == CaseType.Repeat)
                            {
                                if (tempNextLoopTreeNode.Nodes.Count > 0)
                                {
                                    myCaseLaodInfo tempProjectLoadInfo = MyCaseScriptAnalysisEngine.getCaseLoadInfo(((myTreeTagInfo)(tempNextLoopTreeNode.Tag)).tagCaseXmlNode);
                                    myCaseLoopList.Add(new myCaseLoop(tempNextLoopTreeNode.Nodes[0], tempProjectLoadInfo.times));
                                }
                                return nextCase();
                            }
                            else if (((myTreeTagInfo)(tempNextLoopTreeNode.Tag)).tagCaseType == CaseType.Case)
                            {
                                return tempNextLoopTreeNode;
                            }
                            else
                            {
                                return null; //当前设计不会有这种情况
                            }
                        }
                    }
                    else
                    {
                        if (nowCaseNode.NextNode == null)
                        {
                            return null; //当前 【Queue】 结束
                        }
                        else
                        {
                            nowCaseNode = nowCaseNode.NextNode;
                            if (((myTreeTagInfo)(nowCaseNode.Tag)).tagCaseType == CaseType.Repeat)
                            {
                                if (nowCaseNode.Nodes.Count > 0)
                                {
                                    myCaseLaodInfo tempProjectLoadInfo = MyCaseScriptAnalysisEngine.getCaseLoadInfo(((myTreeTagInfo)(nowCaseNode.Tag)).tagCaseXmlNode);
                                    myCaseLoopList.Add(new myCaseLoop(nowCaseNode.Nodes[0], tempProjectLoadInfo.times));
                                }
                                return nextCase();
                            }
                            else if (((myTreeTagInfo)(nowCaseNode.Tag)).tagCaseType == CaseType.Case)
                            {
                                return nowCaseNode;
                            }
                            else
                            {
                                return null; //当前设计不会有这种情况
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// CaseLoop it will only used in myCsaeQueue
        /// </summary>
        private class myCaseLoop
        {
            private TreeNode startCaseNode;
            private TreeNode nowCaseNode;
            private int myTimes;

            /// <summary>
            /// myCaseLoop initialize
            /// </summary>
            /// <param name="yourStartCase">your StartCase and make sure it is not null</param>
            /// <param name="yourTimes">your Times </param>
            public myCaseLoop(TreeNode yourStartCase, int yourTimes)
            {
                myTimes = yourTimes;
                startCaseNode = yourStartCase;
                nowCaseNode = null;
            }

            /// <summary>
            /// i will get the next myTreeTagInfo in my loop
            /// </summary>
            /// <returns>the TreeNode you want</returns>
            public TreeNode nextCase()
            {
                if (myTimes > 0)
                {
                    if (nowCaseNode == null) //起始节点
                    {
                        nowCaseNode = startCaseNode;
                        return nowCaseNode;
                    }
                    else
                    {
                        if (nowCaseNode.NextNode == null)
                        {
                            myTimes--;
                            if (myTimes > 0)
                            {
                                nowCaseNode = startCaseNode;
                                return nowCaseNode;
                            }
                            else
                            {
                                return null;   //此处为null，指示当前【Loop】结束
                            }

                        }
                        else
                        {
                            nowCaseNode = nowCaseNode.NextNode;
                            return nowCaseNode;    //此处caseType可能为case或repeat，该类的拥有者将会分别处理
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        private Dictionary<int, Dictionary<int, TreeNode>> myRunTimeCaseDictionary;
        private List<myCsaeQueue> myCsaeQueueList;
        private bool isThroughAllCase;

        /// <summary>
        /// myCelerityCaseRunTime initialize
        /// </summary>
        /// <param name="yourCaseDictionary">the Dictionary with all case and his id</param>
        public MyCelerityCaseRunTime(Dictionary<int, Dictionary<int, TreeNode>> yourCaseDictionary)
        {
            myCsaeQueueList = new List<myCsaeQueue>();
            myRunTimeCaseDictionary = yourCaseDictionary;
        }

        /// <summary>
        /// i can updata you myRunTimeCaseDictionary()
        /// </summary>
        /// <param name="yourCaseDictionary">you myRunTimeCaseDictionary</param>
        public void updataRunTimeCaseDictionary(Dictionary<int, Dictionary<int, TreeNode>> yourCaseDictionary)
        {
            myRunTimeCaseDictionary = yourCaseDictionary;
        }

        /// <summary>
        /// you must readyStart before get nextCase (and here also can reset the StartCase)
        /// </summary>
        /// <param name="yourStartCase">your StartCase</param>
        public void readyStart(TreeNode yourStartCase)
        {
            myCsaeQueueList.Clear();
            myCsaeQueueList.Add(new myCsaeQueue(yourStartCase));
        }

        /// <summary>
        /// you must readyStart before get nextCase (and here also can reset the StartCase)
        /// </summary>
        /// <param name="yourStartCase">your StartCase</param>
        /// <param name="yourIsThrough">it will change the behaviour that is it will go through all case(now it is replaced by [goto])</param>
        public void readyStart(TreeNode yourStartCase, bool yourIsThrough)
        {
            readyStart(yourStartCase);
            isThroughAllCase = yourIsThrough;
        }

        /// <summary>
        /// i will get the next myTreeTagInfo in myCaseRunTime
        /// </summary>
        /// <returns>the TreeNode you want</returns>
        public TreeNode nextCase()
        {
            if (myCsaeQueueList.Count > 0)
            {
                TreeNode tempTreeNodeCase = myCsaeQueueList[myCsaeQueueList.Count - 1].nextCase();
                if (tempTreeNodeCase == null)
                {
                    myCsaeQueueList.RemoveAt(myCsaeQueueList.Count - 1);
                    return nextCase();
                }
                else
                {
                    return tempTreeNodeCase;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// here i will jump into other case in myCaseRunTime
        /// </summary>
        /// <param name="yourProjectId">your Project Id</param>
        /// <param name="yourCaseId">your Case Id</param>
        /// <returns>is success</returns>
        public bool gotoMyCase(int yourProjectId, int yourCaseId)
        {
            if (myRunTimeCaseDictionary.ContainsKey(yourProjectId))
            {
                if (myRunTimeCaseDictionary[yourProjectId].ContainsKey(yourCaseId))
                {
                    myCsaeQueueList.Add(new myCsaeQueue(myRunTimeCaseDictionary[yourProjectId][yourCaseId]));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
     * */
}
