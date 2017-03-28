using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AutoTest.myTool;
using System.Xml;
using System.Collections;
using MyCommonHelper;
using CaseExecutiveActuator;
using CaseExecutiveActuator.Cell;

namespace AutoTest.myControl
{
    public partial class MyCBalloon : CBalloon.CBalloonBase
    {
        public MyCBalloon(TreeNode yourNode )
        {
            InitializeComponent();
            myTargetNode = yourNode;
        }

        //RichTextBox rtb_errorList = new RichTextBox();
        //rtb_errorList.AppendText("test");
        //rtb_errorList.Dock = DockStyle.Fill;
        //this.Controls.Add(rtb_errorList);

        TreeNode myTargetNode;

        private void analyzeCaseData()
        {
            myRunCaseData<ICaseExecutionContent> yourCaseRunData = ((CaseCell)myTargetNode.Tag).CaseRunData;
            if(yourCaseRunData.errorMessages==null)
            {
                llb_errorInfo.Visible = false;
            }
            lb_caseId.Text = "ID:" + yourCaseRunData.id;
            lb_caseTarget.Text = "->" + yourCaseRunData.testContent.myExecutionTarget;
            lb_protocol.Text = "Protocol:" + yourCaseRunData.contentProtocol.ToString();
            lb_delay.Text = "Delay:" + yourCaseRunData.caseAttribute.attributeDelay + "ms";
            lb_level.Text = "CaseLevel:" + yourCaseRunData.caseAttribute.attributeLevel;
            if(yourCaseRunData.caseAttribute.myParameterSaves!=null)
            {
                foreach(ParameterSave tempValue in yourCaseRunData.caseAttribute.myParameterSaves)
                {
                    listView_parameterSave.Items.Add(new ListViewItem(new string[] { tempValue.parameterName, tempValue.parameterFindVaule }));
                }
            }

            lb_expectType.Text = "AssertType:" + yourCaseRunData.caseExpectInfo.myExpectType.ToString();
            if(yourCaseRunData.caseExpectInfo.myExpectContent.IsFilled())
            {
                tb_expectContent.Text = yourCaseRunData.caseExpectInfo.myExpectContent.contentData;
            }
            if(yourCaseRunData.actions!=null)
            {
                foreach(KeyValuePair<CaseResult, CaseActionDescription> tempAction in yourCaseRunData.actions)
                {
                    listView_action.Items.Add(new ListViewItem(new string[] { tempAction.Key.ToString(), tempAction.Value.caseAction.ToString() + tempAction.Value.addInfo.MyValue()}));
                }
            }
            if(yourCaseRunData.testContent!=null)
            {
                MyCommonTool.myAddRtbStr(ref rtb_Content, "【Actuator】:" + yourCaseRunData.testContent.myCaseActuator, Color.DarkOrchid, true);
                MyCommonTool.myAddRtbStr(ref rtb_Content, yourCaseRunData.testContent.myExecutionContent, Color.Maroon, true);
                rtb_Content.AppendText((((CaseCell)myTargetNode.Tag).CaseXmlNode)["Content"].InnerXml);
                rtb_Content.Select(0, 0);
                rtb_Content.ScrollToCaret();
            }
            else
            {
                rtb_Content.Text = "NULL";
            }
        }


        private void MyCBalloon_Load(object sender, EventArgs e)
        {
            analyzeCaseData();
        }

        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            this.Owner.Activate();
            this.Close();
        }

        private void llb_errorInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (string tempError in ((CaseCell)myTargetNode.Tag).CaseRunData.errorMessages)
            {
                MessageBox.Show(tempError);
            }
        }


    }
}
