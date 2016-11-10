using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CaseExecutiveActuator;
using MyCommonControl;
using System.IO;
using System.Collections;
using System.Xml;
using CaseExecutiveActuator.Cell;
using MyCommonTool;
using System.Windows.Forms;
using System.Drawing;
using RemoteService.MyWindow;

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

namespace RemoteService
{
    public class CaseRunner
    {

        private bool isUpdata;

        public bool IsNeedUpdata
        {
            get { return isUpdata; }
            set { isUpdata = value; }
        }

        private string runnerName;

        private string runnerCasePath;

        /// <summary>
        /// Runer CaseActionActuator
        /// </summary>
        private CaseActionActuator runerActuator;

        /// <summary>
        /// start Cell
        /// </summary>
        private CaseCell startCell;

        public ListViewItem tagItem;

        public ProgressBarList runerProgressBar;

        public PlayButton runnerButton;

        /// <summary>
        /// CaseRunner构造函数
        /// </summary>
        /// <param name="yourNmae">用户标识名</param>
        public CaseRunner(string yourNmae)
        {
            runnerName = yourNmae;
            runnerCasePath = "";
            runerActuator = new CaseActionActuator(yourNmae);
            runerActuator.OnActuatorStateChanged += runerActuator_OnActuatorStateChanged;
            runerActuator.OnExecutiveResult += runerActuator_OnExecutiveResult;  
            runerProgressBar = new ProgressBarList();
            runnerButton = new PlayButton();
            runnerButton.ButtonSetClickEvent += runnerButton_ButtonSetClickEvent;
            runnerButton.ButtonOutClickEvent += runnerButton_ButtonOutClickEvent;
            runnerButton.ButtonDelClickEvent += runnerButton_ButtonDelClickEvent;
            runnerButton.ButtonStateChangedEvent += runnerButton_ButtonStateChangedEvent;
        }



        /// <summary>
        /// CaseRunner构造函数（用于克隆当前对象）
        /// </summary>
        /// <param name="yourNmae">用户标识名</param>
        /// <param name="yourCaseActuator">克隆需要的深度复制的新执行器</param>
        private CaseRunner(string yourNmae, CaseActionActuator yourCaseActuator)
        {
            runnerName = yourNmae;
            yourCaseActuator.MyName = yourNmae;
            runerActuator = yourCaseActuator;
            runerActuator.OnActuatorStateChanged += runerActuator_OnActuatorStateChanged;
            runerActuator.OnExecutiveResult += runerActuator_OnExecutiveResult;
            runerProgressBar = new ProgressBarList();
            runnerButton = new PlayButton();
            runnerButton.ButtonSetClickEvent += runnerButton_ButtonSetClickEvent;
            runnerButton.ButtonOutClickEvent += runnerButton_ButtonOutClickEvent;
            runnerButton.ButtonDelClickEvent += runnerButton_ButtonDelClickEvent;
            runnerButton.ButtonStateChangedEvent += runnerButton_ButtonStateChangedEvent;
        }

        /// <summary>
        /// Clone当前用户
        /// </summary>
        /// <param name="yourNmae">克隆出的用户的标识名</param>
        /// <returns>克隆体</returns>
        public CaseRunner Clone(string yourNmae)
        {
            if (runerActuator.IsActuatorDataFill)
            {
                CaseRunner cloneRunner=new CaseRunner(yourNmae, (CaseActionActuator)runerActuator.Clone());
                cloneRunner.runnerCasePath = this.runnerCasePath;
                cloneRunner.startCell = this.startCell;                //由于克隆用户的Cell数据是公用根用户的，所以起始点的指向也是可以共用的
                return cloneRunner;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取或设置当前用户的标识名
        /// </summary>
        public string RunnerName
        {
            get { return runnerName; }
            set 
            { 
                runnerName = value;
                runerActuator.MyName = runnerName;
            }
        }

        /// <summary>
        /// 获取或设置当前脚本路径
        /// </summary>
        public string RunnerCasePath
        {
            get { return runnerCasePath; }
            set { runnerCasePath = value; }
        }

        /// <summary>
        /// 获取当前用户的执行状态
        /// </summary>
        public CaseActuatorState RunnerState
        {
            get
            {
                if(runerActuator==null)
                {
                    return CaseActuatorState.Stop;
                }
                else
                {
                    if(runerActuator.IsActuatorDataFill)
                    {
                        return runerActuator.Runstate;
                    }
                    else
                    {
                        return CaseActuatorState.Stop;
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置当前用户的起始Cell
        /// </summary>
        public CaseCell StartCell
        {
            get { return startCell; }
            set { startCell = value; }
        }

        /// <summary>
        /// 获取当前用户的起始Cell的名称
        /// </summary>
        public string StartCellName
        {
            get
            {
                if(startCell==null)
                {
                    return "NOT SET";
                }
                else
                {
                    switch (startCell.CaseType)
                    {
                        case CaseType.Case:
                            return "ID:" + startCell.CaseRunData.id;
                        case CaseType.Repeat:
                            return "Repeat Cell";
                        default:
                            return "Error Cell";
                    }
                }
            }
        }

        /// <summary>
        /// 获取当前用户的执行器器
        /// </summary>
        public CaseActionActuator RunerActuator
        {
            get { return runerActuator;}
        }

        private bool AnalysisXmlCase(string myCasePath, CaseActionActuator caseActuator, Hashtable checkDataHt, bool isWithSouseXml, out string errorMessage, out List<string> loadErrorList)
        {
            bool tempIsScriptRunTimeDeal=false;
            errorMessage = null;
            loadErrorList = new List<string>();
            CaseFileXmlAnalysis xmlAnalysis = new CaseFileXmlAnalysis();
            if (caseActuator==null)
            {
                return false;
            }
            if (caseActuator.Runstate != CaseActuatorState.Stop)
            {
                errorMessage= "The TestCase not stop";
                return false;
            }
            if (!File.Exists(myCasePath))
            {
                errorMessage = "用例文件不存在，请重新选择";
                return false;
            }
            if (!xmlAnalysis.LoadFile(myCasePath))
            {
                errorMessage= "该脚本数据格式错误，请修正错误。详情请查看错误日志";
                return false;
            }

            XmlNodeList myCaseProject = xmlAnalysis.xml.ChildNodes[1].ChildNodes;

            #region check case data
            if (checkDataHt != null)
            {
                foreach (DictionaryEntry de in checkDataHt)
                {
                    string tempStr = CaseTool.CheckCase(xmlAnalysis.xml.ChildNodes[1], (string)de.Key, (string[])de.Value);
                    if (tempStr != "")
                    {
                        errorMessage = tempStr;
                        return false;
                    }
                }
            }
            #endregion

            Dictionary<int, Dictionary<int, CaseCell>> myProjectCaseDictionary = new Dictionary<int, Dictionary<int, CaseCell>>();

            caseActuator.DisconnectExecutionDevice();
            caseActuator.Dispose();

            ProjctCollection myProjctCollection = new ProjctCollection();
            try
            {

                #region Project analyze

                foreach (XmlNode tempNode in myCaseProject)
                {
                    myCaseLaodInfo tempProjectLoadInfo = myCaseScriptAnalysisEngine.getCaseLoadInfo(tempNode);
                    string thisErrorTitle = "Project ID:" + tempProjectLoadInfo.id;
                    if (tempProjectLoadInfo.ErrorMessage != "")
                    {
                        loadErrorList.Add(string.Format("【{1}】:{0}",tempProjectLoadInfo.ErrorMessage, thisErrorTitle));
                    }
                    if (tempProjectLoadInfo.caseType == CaseType.ScriptRunTime)
                    {
                        //deal with ScriptRunTime
                        if (!tempIsScriptRunTimeDeal)
                        {
                            caseActuator.LoadScriptRunTime(tempNode);
                            tempIsScriptRunTimeDeal = true;
                        }
                        else
                        {
                            thisErrorTitle = "ScriptRunTime";
                            loadErrorList.Add(string.Format("【{1}】:{0}","find another ScriptRunTime ,ScriptRunTime is unique so it will be skip", thisErrorTitle));
                        }
                        continue;
                    }
                    if (tempProjectLoadInfo.caseType != CaseType.Project)
                    {
                        loadErrorList.Add(string.Format("【{1}】:{0}", "not legal Project ,it will be skip", "legal"));
                        continue;
                    }

                    #region deal this Project

                    CaseCell tempProjctCell = new CaseCell(tempProjectLoadInfo.caseType, tempNode, null);
                    myProjctCollection.Add(tempProjctCell);

                    Dictionary<int, CaseCell> tempCaseDictionary = new Dictionary<int, CaseCell>();
                    if (myProjectCaseDictionary.ContainsKey(tempProjectLoadInfo.id))
                    {
                        loadErrorList.Add(string.Format("【{1}】:{0}","find the same project id in this file ,it will make [Goto] abnormal", thisErrorTitle));
                    }
                    else
                    {
                        myProjectCaseDictionary.Add(tempProjectLoadInfo.id, tempCaseDictionary);
                    }

                    //myTargetCaseList 将包含当前project或repeat集合元素
                    List<KeyValuePair<CaseCell, XmlNode>> myTargetCaseList = new List<KeyValuePair<CaseCell, XmlNode>>();
                    myTargetCaseList.Add(new KeyValuePair<CaseCell, XmlNode>(tempProjctCell, tempNode));
                    while (myTargetCaseList.Count > 0)
                    {
                        //Case analyze
                        foreach (XmlNode tempChildNode in myTargetCaseList[0].Value)
                        {
                            //load Show Info
                            myCaseLaodInfo tempCaseLoadInfo = myCaseScriptAnalysisEngine.getCaseLoadInfo(tempChildNode);
                            thisErrorTitle = "Case ID:" + tempCaseLoadInfo.id;
                            if (tempCaseLoadInfo.ErrorMessage != "")
                            {
                                loadErrorList.Add(string.Format("【{1}】:{0}", tempCaseLoadInfo.ErrorMessage, thisErrorTitle));
                                loadErrorList.Add(string.Format("【{1}】:{0}", "this error can not be repair so drop it", thisErrorTitle));
                            }
                            else
                            {
                                if (tempCaseLoadInfo.caseType == CaseType.Case)
                                {
                                    //load Run Data
                                    var tempCaseRunData = myCaseScriptAnalysisEngine.getCaseRunData(tempChildNode);
                                    if (tempCaseRunData.errorMessages != null)
                                    {
                                        foreach (string tempErrorMes in tempCaseRunData.errorMessages)
                                        {
                                            loadErrorList.Add(string.Format("【{1}】:{0}", tempErrorMes, thisErrorTitle));
                                        }
                                    }
                                    CaseCell tempChildCell = new CaseCell(tempCaseLoadInfo.caseType, tempChildNode, tempCaseRunData);
                                    if (tempCaseDictionary.ContainsKey(tempCaseLoadInfo.id))
                                    {
                                        loadErrorList.Add(string.Format("【{1}】:{0}", "find the same case id in this project ,it will make [Goto] abnormal", thisErrorTitle));
                                    }
                                    else
                                    {
                                        tempCaseDictionary.Add(tempCaseLoadInfo.id, tempChildCell);
                                    }
                                    myTargetCaseList[0].Key.Add(tempChildCell);
                                }
                                else if (tempCaseLoadInfo.caseType == CaseType.Repeat)
                                {
                                    CaseCell tempChildCell = new CaseCell(tempCaseLoadInfo.caseType, tempChildNode, null);

                                    myTargetCaseList[0].Key.Add(tempChildCell);
                                    myTargetCaseList.Add(new KeyValuePair<CaseCell, XmlNode>(tempChildCell, tempChildNode));
                                }
                                else
                                {
                                    //it will cant be project and if it is unknow i will not show it
                                    loadErrorList.Add(string.Format("【{1}】:{0}", "find unkown case so drop it", thisErrorTitle));
                                }
                            }
                        }
                        myTargetCaseList.Remove(myTargetCaseList[0]);
                    }
                    #endregion

                }
                #endregion

                if (!tempIsScriptRunTimeDeal)
                {
                    loadErrorList.Add(string.Format("【{1}】:{0}","ScriptRunTime is not find ","ScriptRunTime"));
                    errorMessage = "ScriptRunTime is not find ,the case will cannot run";
                    return false;
                }
                else
                {
                    caseActuator.SetCaseRunTime(myProjectCaseDictionary, myProjctCollection);
                    //启动数据呈现
                    return true;

                }
            }
            //严重错误
            catch (Exception ex)
            {
                ErrorLog.PutInLogEx(ex);
                errorMessage = ex.Message;
                caseActuator.DisconnectExecutionDevice();
                caseActuator.Dispose();
                return false;
            }
        }


        void runerActuator_OnExecutiveResult(string sender, myExecutionDeviceResult yourResult)
        {
            if (tagItem != null)
            {
                tagItem.ListView.BeginInvoke(new Action<myExecutionDeviceResult>(UpdateDateShow), yourResult);
            }
            //UpdateDateShow(yourResult);
        }

        void runerActuator_OnActuatorStateChanged(string sender, CaseActuatorState yourState)
        {
            isUpdata = true;
            if(tagItem!=null)
            {
                if(tagItem.ListView.InvokeRequired)
                {
                    tagItem.ListView.BeginInvoke(new Action<CaseActuatorState>(UpdatePlayButtonState), yourState);
                }
                else
                {
                    UpdatePlayButtonState(yourState);
                }
            }
        }

        //PlayButton 运行状态通知
        void runnerButton_ButtonStateChangedEvent(object sender, PlayButton.PlayButtonEventArgs e)
        {
            switch (e.PlayState)
            {
                case PlayButton.PlayButtonState.Run:
                    if(!runerActuator.RunCaseScript(startCell))
                    {
                        MessageBox.Show(runerActuator.ErrorInfo);
                    }
                    break;
                case PlayButton.PlayButtonState.Stop:
                    if (!runerActuator.StopCaseScript())
                    {
                        MessageBox.Show(runerActuator.ErrorInfo);
                    }
                    break;
                case PlayButton.PlayButtonState.Pause:
                    if (!runerActuator.PauseCaseScript())
                    {
                        MessageBox.Show(runerActuator.ErrorInfo);
                    }
                    break;
                default:
                    //do nothing
                    break;
            }
        }

        //PlayButton 设置请求通知
        void runnerButton_ButtonSetClickEvent(object sender, EventArgs e)
        {
            if (this.RunnerState == CaseActuatorState.Stop)
            {
                RunnerSet newSet = new RunnerSet(this);
                newSet.StartPosition = FormStartPosition.CenterParent;
                newSet.ShowDialog();
            }
            else
            {
                MessageBox.Show("指定用户正在运行，请先停止后再进行设置操作","STOP");
            }
        }

        //PlayButton 删除请求通知
        void runnerButton_ButtonDelClickEvent(object sender, EventArgs e)
        {
            if (this.RunnerState == CaseActuatorState.Stop)
            {
                MessageBox.Show("请使用复选删除功能对用户进行移除", "STOP");
            }
            else
            {
                MessageBox.Show("指定用户正在运行，请先停止后再进行移除操作", "STOP");
            }
        }

        //PlayButton 输出报告请求通知
        void runnerButton_ButtonOutClickEvent(object sender, EventArgs e)
        {
            //if (this.RunnerState == CaseActuatorState.Stop)
            //{
            //    if (runerActuator == null)
            //    {
            //        MessageBox.Show("未发现可用执行器", "stop");
            //        return;
            //    }
            //    else if (runerActuator.NowExecutionResultList.Count < 1)
            //    {
            //        MessageBox.Show("执行器中未发现任何可用数据", "stop");
            //        return;
            //    }
            //    string myReportPath="";
            //    if (!myTool.myResultOut.createReport(runnerCasePath, runerActuator.NowExecutionResultList, ref myReportPath))
            //    {
            //        MessageBox.Show("报告生成失败，祥见错误日志", "stop");
            //    }
            //    else
            //    {
            //        if (MessageBox.Show("报告生成成功是否查看", "ok", MessageBoxButtons.OKCancel) == DialogResult.OK)
            //        {
            //            myCommonTool.OpenPress(myReportPath, "");
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("指定用户正在运行，请先停止后再进行报告输出操作", "STOP");
            //}
        }

        public bool LoadCase(string casePath, out string errorMessage, out List<string> errorList)
        {
            if(AnalysisXmlCase(casePath, runerActuator, null, true, out errorMessage, out errorList))
            {
                runnerCasePath = casePath;
                startCell = runerActuator.RunCellProjctCollection[0, 0];
                return true;
            }
            else
            {
                runnerCasePath = "";
                startCell = null ;
                return false;
            }
        }

        /// <summary>
        /// 更新当前显示信息
        /// </summary>
        /// <param name="yourResult"></param>
        private void  UpdateDateShow(myExecutionDeviceResult yourResult)
        {
            if (tagItem != null && yourResult!=null)
            {
                isUpdata = true;
                tagItem.SubItems[2].Text = yourResult.caseId.ToString();
                tagItem.SubItems[3].Text = yourResult.backContent;
                tagItem.SubItems[4].Text = yourResult.result.ToString();
                tagItem.SubItems[5].Text = yourResult.spanTime;
            }
        }

        /// <summary>
        /// 更新进度条
        /// </summary>
        public void UpdateProgressBar()
        {
            if (runerActuator != null)
            {
                if (runerProgressBar.InvokeRequired)
                {
                    runerProgressBar.Invoke(new Action<List<KeyValuePair<int, int>>>(runerProgressBar.UpdateListMinimal), runerActuator.RunProgress);
                }
                else
                {
                    runerProgressBar.UpdateListMinimal(runerActuator.RunProgress);
                }
            }
        }

        /// <summary>
        /// 更新PlayButton状态及运行状态
        /// </summary>
        /// <param name="yourState">运行状态</param>
        private void UpdatePlayButtonState(CaseActuatorState yourState)
        {
            if (tagItem != null)
            {
                tagItem.SubItems[7].Text = yourState.ToString();
                UpdateProgressBar();
                switch (yourState)
                {
                    case CaseActuatorState.Stop:
                        tagItem.SubItems[7].ForeColor = Color.Black;
                        //runnerButton.OnChangeState(PlayButton.PlayButtonState.Stop);
                        runnerButton.BeginInvoke(new Action<PlayButton.PlayButtonState>(runnerButton.OnChangeState), PlayButton.PlayButtonState.Stop); 
                        break;
                    case CaseActuatorState.Pause:
                        tagItem.SubItems[7].ForeColor = Color.Red;
                        //runnerButton.OnChangeState(PlayButton.PlayButtonState.Pause);
                        runnerButton.BeginInvoke(new Action<PlayButton.PlayButtonState>(runnerButton.OnChangeState), PlayButton.PlayButtonState.Pause); 
                        break;
                    case CaseActuatorState.Running:
                        tagItem.SubItems[7].ForeColor = Color.Orange;
                        //runnerButton.OnChangeState(PlayButton.PlayButtonState.Run);
                        runnerButton.BeginInvoke(new Action<PlayButton.PlayButtonState>(runnerButton.OnChangeState), PlayButton.PlayButtonState.Run); 
                        break;
                    default:
                        break;
                }
            }
        }

        public void RunQuiet()
        {
            runerActuator.RunCaseScript(startCell);
        }

        public void PauseQuiet()
        {
            runerActuator.PauseCaseScript();
        }

        public void StopQuiet()
        {
            runerActuator.StopCaseScript();
        }
    }
}
