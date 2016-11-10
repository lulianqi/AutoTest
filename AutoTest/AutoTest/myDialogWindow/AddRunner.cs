using AutoTest.myControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyCommonTool;

using CaseExecutiveActuator;
using CaseExecutiveActuator.Cell;

namespace AutoTest.myDialogWindow
{
    //public partial class AddRunner : Form
    public partial class AddRunner  : MyChildWindow
    {
        public AddRunner()
        {
            InitializeComponent();
        }

        CaseRunner newCaseRunner;

        AutoRunner myOwner;

        BindingSource bsPCell = new BindingSource();
        BindingSource bsCCell = new BindingSource();


        private void LoadFileData()
        {
            tb_runnerName.ReadOnly = false;
            tb_waitTime.ReadOnly = false;
            tb_cloneNum.ReadOnly = false;
            //this.pictureBox_AddRunner.Image = global::AutoTest.Properties.Resources.addUser;
            this.pictureBox_AddRunner.Image = AutoTest.Properties.Resources.addUser;

            bsPCell.DataSource = newCaseRunner.RunerActuator.RunTimeCaseDictionary;
            cb_pList.DataSource = bsPCell;
            cb_pList.DisplayMember = "Key";
            cb_pList.ValueMember = "Value";

            bsCCell.DataSource = cb_pList.SelectedValue;
            cb_cList.DataSource = bsCCell;
            cb_cList.DisplayMember = "Key";
            cb_cList.ValueMember = "Value";
        }

        private void FreezeAdd()
        {
            cb_pList.DataSource = null;
            cb_cList.DataSource = null;

            tb_runnerName.ReadOnly = true;
            tb_waitTime.ReadOnly = true;
            tb_cloneNum.ReadOnly = true;
            //this.pictureBox_AddRunner.Image = global::AutoTest.Properties.Resources.addUser2;
            this.pictureBox_AddRunner.Image = AutoTest.Properties.Resources.addUser2;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Honeydew;
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Transparent;
        }


        private void bt_openFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog_caseFile.ShowDialog() == DialogResult.OK)
            {
                tb_caseFilePath.Text = openFileDialog_caseFile.FileName;
            }
        }


        private void AddRunner_Load(object sender, EventArgs e)
        {
            FreezeAdd();
            myOwner = (AutoRunner)this.Owner;
        }

        //Load file
        private void bt_loadFile_Click(object sender, EventArgs e)
        {
            string errorMessage;
            List<string> errorList;
            newCaseRunner = new CaseRunner("NU");
            if (newCaseRunner.LoadCase(tb_caseFilePath.Text, out errorMessage, out errorList))
            {
                if(errorList!=null)
                {
                    foreach(string tempError in errorList)
                    {
                        myCommonTool.setRichTextBoxContent(ref rtb_info, tempError, Color.Black, true);
                    }
                }
                newCaseRunner.RunnerCasePath = tb_caseFilePath.Text;
                myCommonTool.setRichTextBoxContent(ref rtb_info, "加载完成请继续", Color.Blue, true);
                LoadFileData();
            }
            else
            {
                myCommonTool.setRichTextBoxContent(ref rtb_info, errorMessage, Color.Red, true);
                newCaseRunner = null;
                FreezeAdd();
                MessageBox.Show(errorMessage);
            }
        }

        //Select change
        private void cb_pList_SelectedIndexChanged(object sender, EventArgs e)
        {
            bsCCell.DataSource = cb_pList.SelectedValue;
            cb_cList.DataSource = bsCCell;
            cb_cList.DisplayMember = "Key";
            cb_cList.ValueMember = "Value";
        }

        //Num only press
        private void tb_waitTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            //'\b' is backspace
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Add User
        private void pictureBox_AddRunner_Click(object sender, EventArgs e)
        {
            if(newCaseRunner==null)
            {
                bt_loadFile_Click(null, null);
                if (newCaseRunner != null)
                {
                    pictureBox_AddRunner_Click(null, null);
                }
            }
            else
            {
                int tempIndex = 0;
                int tempCloneNum = 0;
                string tempName = tb_runnerName.Text;
                while (myOwner.IsContainRunnerName(tempName))
                {
                    tempName = tb_runnerName.Text + "_" + tempIndex;
                    tempIndex++;
                    if(tempIndex>1000000)
                    {
                        break;
                    }
                }
                try
                {
                    newCaseRunner.RunerActuator.ExecutiveThinkTime = int.Parse(tb_waitTime.Text  );
                }
                catch
                {
                    newCaseRunner.RunerActuator.ExecutiveThinkTime = 0;
                    myCommonTool.setRichTextBoxContent(ref rtb_info, "WaitTime Set Error", Color.Red, true);
                }
                try
                {
                    tempCloneNum = int.Parse(tb_cloneNum.Text);
                }
                catch
                {
                    tempCloneNum = 0;
                    myCommonTool.setRichTextBoxContent(ref rtb_info, "CloneNum Set Error", Color.Red, true);
                }
                newCaseRunner.RunnerName = tempName;
                try
                {
                    newCaseRunner.StartCell = (CaseCell)cb_cList.SelectedValue;
                }
                catch
                {
                    myCommonTool.setRichTextBoxContent(ref rtb_info, "StartCell Set Error", Color.Red, true);
                }
                myOwner.AddRunner(newCaseRunner);
                myCommonTool.setRichTextBoxContent(ref rtb_info, "新用户 " + tempName + "添加成功", Color.Red, true);
                if (tempCloneNum>0)
                {
                    if (tempCloneNum > 200)
                    {
                        if (MessageBox.Show("您创建过多的克隆用户，可能需要较长的时间，是否继续？", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            for (int i = 0; i < tempCloneNum; i++)
                            {
                                myOwner.AddRunner(newCaseRunner.Clone(tempName + "#" + i));
                                myCommonTool.setRichTextBoxContent(ref rtb_info, "新克隆用户 " + tempName + "#" + i + " 添加成功", Color.Red, true);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < tempCloneNum; i++)
                        {
                            myOwner.AddRunner(newCaseRunner.Clone(tempName + "#" + i));
                            myCommonTool.setRichTextBoxContent(ref rtb_info, "新克隆用户 " + tempName + "#" + i + " 添加成功", Color.Red, true);
                        }
                    }
                }
                tb_cloneNum.Text = "0";
                newCaseRunner = null;
            }
        }

        //Close
        private void pictureBox_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
