using CaseExecutiveActuator.Cell;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

namespace CaseExecutiveActuator.CaseActuator
{
    /// <summary>
    /// 在这里绑定UI组件，如果想让运行过程事实反馈到UI界面上，请到此订阅（如果使用了自定义的UI组件，请在相应的位置按原有规则添加反馈）
    /// </summary>
    class MyActionActuator
    {
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




        public static void SetCaseNodeRunning(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 19);
            }
        }

        public static void SetCaseNodeSleeping(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 20);
            }
        }

        public static void SetCaseNodePass(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 15);
            }
        }

        public static void SetCaseNodeFial(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 7);
            }
        }

        public static void SetCaseNodeWarning(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 13);
            }
        }

        public static void SetCaseNodeBreak(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 11);
            }
        }

        public static void SetCaseNodePause(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 18);
            }
        }

        public static void SetCaseNodeStop(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 25);
            }
        }

        public static void SetCaseNodeNukown(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 12);
            }
        }

        public static void SetCaseNodeAbnormal(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 21);
            }
        }

        public static void SetCaseNodeNoActuator(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 24);
            }
        }

        public static void SetCaseNodeConnectInterrupt(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 23);
            }
        }

        public static void SetCaseNodeContentError(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, Color.Pink);
            }
        }

        public static void SetCaseNodeContentWarning(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, Color.LightGoldenrodYellow);
            }
        }

        public static void SetCaseNodeContentEdit(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                CaseTreeNodeChangeState(yourTreeNode, Color.PaleGoldenrod);
            }
        }



        /// <summary>
        /// 为TreeNode添加额外运行时消息以表示loop的变化（额外消息原则上为【···】这种格式或者为空"",处理时替换原有【···】，如果没有则直接添加）
        /// </summary>
        /// <param name="yourCell">CaseCell</param>
        /// <param name="yourMessage">Message （请务必保证数据为【···】这种格式，或为空""）</param>
        public static void SetCaseNodeLoopChange(CaseCell yourCell, string yourMessage)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                if (yourTreeNode.Text.StartsWith("【"))
                {
                    int tempEnd = yourTreeNode.Text.IndexOf('】');
                    CaseTreeNodeChangeText(yourTreeNode, yourMessage + yourTreeNode.Text.Remove(0, tempEnd + 1));
                }
                else
                {
                    CaseTreeNodeChangeText(yourTreeNode, yourMessage + yourTreeNode.Text);
                }
            }
        }

        /// <summary>
        /// 进行下一个loop刷新/清除当前loop的节点执行结果（最后的loop请不要调用该方法）
        /// </summary>
        /// <param name="yourCell">CaseCell</param>
        public static void SetCaseNodeLoopRefresh(CaseCell yourCell)
        {
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
        }

        /// <summary>
        /// 展开当前执行节点脚本
        /// </summary>
        /// <param name="yourCell">your CaseCell</param>
        public static void SetCaseNodeExpand(CaseCell yourCell)
        {
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
            //若要支持更多的ui请在后面添加
        }

    }
}
