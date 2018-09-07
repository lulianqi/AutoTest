using CaseExecutiveActuator.CaseActuator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using CaseExecutiveActuator.Cell;
using CaseExecutiveActuator;

namespace AutoTest.MyTool
{
    public class CaseTreeActionControl : IDisposable
    {
        private CaseActionActuator ActionActuator { get; set; }
        public CaseTreeActionControl(CaseActionActuator yourActionActuator)
        {
            yourActionActuator.MyCaseTreeAction.OnCaseTreeChange += MyCaseTreeAction_OnCaseTreeChange;
            ActionActuator = yourActionActuator;
        }


        #region UIAction
        private delegate void delegateTreeNodeChange(TreeNode yourTreeNode);
        private delegate void delegateTreeNodeChangeColor(TreeNode yourTreeNode, Color yourColor);
        private delegate void delegateTreeNodeChangeImage(TreeNode yourTreeNode, int yourImageIndex);
        private delegate void delegateTreeNodeChangeText(TreeNode yourTreeNode, string yourText);
        private delegate void delegateTreeNodeChangeColorAndImage(TreeNode yourTreeNode, Color yourColor, int yourImageIndex);

        private static void CaseTreeNodeChangeExpand(TreeNode yourTreeNode)
        {
            yourTreeNode.TreeView.Invoke(new delegateTreeNodeChange((treeNode) => treeNode.Toggle()), new object[] { yourTreeNode });
        }

        private static void CaseTreeNodeChangeState(TreeNode yourTreeNode, Color yourColor)
        {
            yourTreeNode.TreeView.Invoke(new delegateTreeNodeChangeColor((treeNode, color) => treeNode.BackColor = color), new object[] { yourTreeNode, yourColor });
        }

        private static void CaseTreeNodeChangeState(TreeNode yourTreeNode, int yourImageIndex)
        {
            yourTreeNode.TreeView.Invoke(new delegateTreeNodeChangeImage((treeNode, imageIndex) => treeNode.SelectedImageIndex = treeNode.ImageIndex = imageIndex), new object[] { yourTreeNode, yourImageIndex });
        }

        private static void CaseTreeNodeChangeState(TreeNode yourTreeNode, Color yourColor, int yourImageIndex)
        {
            yourTreeNode.TreeView.Invoke(new delegateTreeNodeChangeColorAndImage((treeNode, color, imageIndex) => { treeNode.BackColor = color; treeNode.SelectedImageIndex = treeNode.ImageIndex = imageIndex; }), new object[] { yourTreeNode, yourColor, yourImageIndex });
        }

        private static void CaseTreeNodeChangeText(TreeNode yourTreeNode, string yourText)
        {
            yourTreeNode.TreeView.Invoke(new delegateTreeNodeChangeText((treeNode, text) => treeNode.Text = text), new object[] { yourTreeNode, yourText });
        }

        #endregion


        void MyCaseTreeAction_OnCaseTreeChange(CaseExecutiveActuator.Cell.CaseCell yourCell, CaseTreeActionEventArgs e, CaseTreeActionType actionType)
        {
            if (yourCell == null)
            {
                return;
            }
            switch(actionType)
            {
                #region CaseNodeRunning
                case CaseTreeActionType.CaseNodeRunning:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                        CaseTreeNodeChangeState(yourTreeNode, 19);
                    }
                    break; 
                #endregion
                #region CaseNodeSleeping
                case CaseTreeActionType.CaseNodeSleeping:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, 20);
                    }
                    break; 
                #endregion
                #region CaseNodePass
                case CaseTreeActionType.CaseNodePass:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, 15);
                    }
                    break; 
                #endregion
                #region CaseNodeFial
                case CaseTreeActionType.CaseNodeFial:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, 7);
                    }
                    break; 
                #endregion
                #region CaseNodeWarning
                case CaseTreeActionType.CaseNodeWarning:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, 13);
                    }
                    break; 
                #endregion
                #region CaseNodeBreak
                case CaseTreeActionType.CaseNodeBreak:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, 11);
                    }
                    break; 
                #endregion
                #region CaseNodePause
                case CaseTreeActionType.CaseNodePause:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, 18);
                    }
                    break; 
                #endregion
                #region CaseNodeStop
                case CaseTreeActionType.CaseNodeStop:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, 25);
                    }
                    break; 
                #endregion
                #region CaseNodeNukown
                case CaseTreeActionType.CaseNodeNukown:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, 12);
                    }
                    break; 
                #endregion
                #region CaseNodeAbnormal
                case CaseTreeActionType.CaseNodeAbnormal:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, 21);
                    }
                    break; 
                #endregion
                #region CaseNodeNoActuator
                case CaseTreeActionType.CaseNodeNoActuator:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, 24);
                    }
                    break; 
                #endregion
                #region CaseNodeConnectInterrupt
                case CaseTreeActionType.CaseNodeConnectInterrupt:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, 23);
                    }
                    break; 
                #endregion
                #region CaseNodeContentError
                case CaseTreeActionType.CaseNodeContentError:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, Color.Pink);
                    }
                    break; 
                #endregion
                #region CaseNodeContentWarning
                case CaseTreeActionType.CaseNodeContentWarning:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, Color.LightGoldenrodYellow);
                    }
                    break; 
                #endregion
                #region CaseNodeContentEdit
                case CaseTreeActionType.CaseNodeContentEdit:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        CaseTreeNodeChangeState(yourTreeNode, Color.PaleGoldenrod);
                    }
                    break; 
                #endregion
                #region CaseNodeLoopChange
                /// <summary>
                /// 为TreeNode添加额外运行时消息以表示loop的变化（额外消息原则上为【···】这种格式或者为空"",处理时替换原有【···】，如果没有则直接添加）
                /// </summary>
                /// <param name="yourCell">CaseCell</param>
                /// <param name="yourMessage">Message （请务必保证数据为【···】这种格式，或为空""）</param>
                case CaseTreeActionType.CaseNodeLoopChange:
                    if (yourCell.UiTag == null || e == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        if (yourTreeNode.Text.StartsWith("【"))
                        {
                            int tempEnd = yourTreeNode.Text.IndexOf('】');
                            CaseTreeNodeChangeText(yourTreeNode, e.ExtendMessage + yourTreeNode.Text.Remove(0, tempEnd + 1));
                        }
                        else
                        {
                            CaseTreeNodeChangeText(yourTreeNode, e.ExtendMessage + yourTreeNode.Text);
                        }
                    }
                    break; 
                #endregion
                #region CaseNodeLoopRefresh
                case CaseTreeActionType.CaseNodeLoopRefresh:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        yourTreeNode.TreeView.BeginUpdate();
                        if (yourTreeNode.Nodes.Count > 0)
                        {
                            foreach (TreeNode tempNode in yourTreeNode.Nodes)
                            {
                                if (((CaseCell)tempNode.Tag).CaseType != CaseType.Case)
                                {
                                    continue;
                                }
                                if (((CaseCell)(tempNode.Tag)).CaseRunData.actions != null)
                                {
                                    if (((CaseCell)(tempNode.Tag)).CaseRunData.actions.Count > 0)
                                    {
                                        CaseTreeNodeChangeState(tempNode, 16);
                                        continue;
                                    }
                                }
                                CaseTreeNodeChangeState(tempNode, 1);
                            }
                        }
                        yourTreeNode.TreeView.EndUpdate();
                    }
                    break; 
                #endregion
                #region CaseNodeExpand
                case CaseTreeActionType.CaseNodeExpand:
                    if (yourCell.UiTag == null)
                    {
                        return;
                    }
                    if (yourCell.UiTag is TreeNode)
                    {
                        TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                        if (yourTreeNode.Nodes.Count > 0)
                        {
                            //CaseType.Repeat
                            if (!yourTreeNode.IsExpanded)
                            {
                                CaseTreeNodeChangeExpand(yourTreeNode);
                            }
                        }
                        else
                        {
                            //CaseType.Case
                            TreeNode tempParentNode = yourTreeNode.Parent;
                            while (tempParentNode != null)
                            {
                                if (!tempParentNode.IsExpanded)
                                {
                                    CaseTreeNodeChangeExpand(tempParentNode);
                                }
                                tempParentNode = tempParentNode.Parent;
                            }
                        }
                    }
                    break; 
                #endregion
                default:
                    throw new Exception("MyCaseTreeAction_OnCaseTreeChange unknow type");
                    
            }
        }

        /// <summary>
        /// 请在UI资源被释放前，释放CaseTreeActionControl
        /// </summary>
        public void Dispose()
        {
            if (ActionActuator != null)
            {
                if (ActionActuator.MyCaseTreeAction != null)
                {
                    ActionActuator.MyCaseTreeAction.OnCaseTreeChange -= MyCaseTreeAction_OnCaseTreeChange;
                }
                ActionActuator = null;
            }
        }
    }
}
