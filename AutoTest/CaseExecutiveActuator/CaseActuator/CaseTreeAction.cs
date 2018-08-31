using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaseExecutiveActuator.Cell;

namespace CaseExecutiveActuator.CaseActuator
{
    public enum CaseTreeActionType
    {
        CaseNodeRunning,
        CaseNodeSleeping,
        CaseNodePass,
        CaseNodeFial,
        CaseNodeWarning,
        CaseNodeBreak,
        CaseNodePause,
        CaseNodeStop,
        CaseNodeNukown,
        CaseNodeAbnormal,
        CaseNodeNoActuator,
        CaseNodeConnectInterrupt,
        CaseNodeContentError,
        CaseNodeContentWarning,
        CaseNodeContentEdit,
        CaseNodeLoopChange,
        CaseNodeLoopRefresh,
        CaseNodeExpand
    }

    public class CaseTreeActionEventArgs : EventArgs
    {
        public string ExtendMessage{ get; set; }
        public CaseTreeActionEventArgs(string yourExtendMessage)
        {
            ExtendMessage = yourExtendMessage;
        }
    }

    public class CaseTreeAction
    {
        public delegate void delegateCaseTreeChange(CaseCell yourTreeNode, CaseTreeActionEventArgs e, CaseTreeActionType actionType);
        public event delegateCaseTreeChange OnCaseTreeChange;
        internal void SetCaseNodeRunning(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange!=null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeRunning);
            }  
        }

        internal void SetCaseNodeSleeping(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeSleeping);
            }
        }

        internal void SetCaseNodePass(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodePass);
            }
        }

        internal void SetCaseNodeFial(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeFial);
            }
        }

        internal void SetCaseNodeWarning(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeWarning);
            }
        }

        internal void SetCaseNodeBreak(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeBreak);
            }
        }

        internal void SetCaseNodePause(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodePause);
            }
        }

        internal void SetCaseNodeStop(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeStop);
            }
        }

        internal void SetCaseNodeNukown(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeNukown);
            }
        }

        internal void SetCaseNodeAbnormal(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeAbnormal);
            }
        }

        internal void SetCaseNodeNoActuator(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeNoActuator);
            }
        }

        internal void SetCaseNodeConnectInterrupt(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeConnectInterrupt);
            }
        }

        internal void SetCaseNodeContentError(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeContentError);
            }
        }

        internal void SetCaseNodeContentWarning(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeContentWarning);
            }
        }

        internal void SetCaseNodeContentEdit(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeContentEdit);
            }
        }



        /// <summary>
        /// 为TreeNode添加额外运行时消息以表示loop的变化（额外消息原则上为【···】这种格式或者为空"",处理时替换原有【···】，如果没有则直接添加）
        /// </summary>
        /// <param name="yourCell">CaseCell</param>
        /// <param name="yourMessage">Message （请务必保证数据为【···】这种格式，或为空""）</param>
        internal void SetCaseNodeLoopChange(CaseCell yourCell, string yourMessage)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell,new CaseTreeActionEventArgs(yourMessage) , CaseTreeActionType.CaseNodeLoopChange);
            }
        }

        /// <summary>
        /// 进行下一个loop刷新/清除当前loop的节点执行结果（最后的loop请不要调用该方法）
        /// </summary>
        /// <param name="yourCell">CaseCell</param>
        internal void SetCaseNodeLoopRefresh(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeLoopRefresh);
            }
        }

        /// <summary>
        /// 展开当前执行节点脚本
        /// </summary>
        /// <param name="yourCell">your CaseCell</param>
        internal void SetCaseNodeExpand(CaseCell yourCell)
        {
            if (yourCell != null && OnCaseTreeChange != null)
            {
                this.OnCaseTreeChange(yourCell, null, CaseTreeActionType.CaseNodeExpand);
            }
        }


    }
}
