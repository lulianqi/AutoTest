using MyCommonHelper;
using System;
using System.Collections.Generic;
using System.Text;
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
    public class CaseFileXmlAnalysis
    {
        public XmlDocument xml = new XmlDocument();
        public string myFile = "";

        //API详细信息提示
        public static XmlDocument myTip = new XmlDocument();

        public static void LoadTip(string tempFileName)
        {
            myTip.Load(tempFileName);
        }


        /// <summary>
        /// load the casefile to XmlDocument
        /// </summary>
        /// <param name="tempFileName">file path</param>
        /// <returns>is succees</returns>
        public bool LoadFile(string tempFileName)
        {
            return LoadFile(tempFileName, false);
        }


        /// <summary>
        /// load the casefile to XmlDocument
        /// </summary>
        /// <param name="tempFileName">file path</param>
        /// <param name="isWithNotes">if it is true the XmlDocument WithNotes</param>
        /// <returns>is succees</returns>
        public bool LoadFile(string tempFileName, bool isWithNotes)
        {
            try
            {
                if (isWithNotes)
                {
                     xml.Load(tempFileName);//直接使用无法过滤注释，下面使用XmlReader
                }
                else
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.IgnoreComments = true;
                    using (XmlReader reader = XmlReader.Create(tempFileName, settings))
                    {
                        xml.Load(reader);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog("ID:00017  " + ex.Message);
                xml = new XmlDocument();
                return false;
            }
            myFile = tempFileName;
            return true;
        }

        public void mySave()
        {
            xml.Save(myFile);
        }

        /// <summary>
        /// add case with all the information
        /// </summary>
        /// <param name="TargetCase"> the target cace </param>
        /// <param name="repeat"> is repeat start or end </param>
        /// <param name="times">the times case will repeat </param>
        /// <param name="Content">the case content </param>
        /// <param name="remark">the case remark </param>
        /// <param name="level">the case leve </param>
        public void AddCase(XmlNode TargetCase, string repeat, string times, string Content, string remark, string level)
        {
            //XmlDocument newChild = new XmlDocument();
            //newChild.LoadXml("<book genre='novel' ISBN='1-861001-57-5'>" + "<title>Pride And Prejudice</title>" + "</book>");
            XmlElement newChild = xml.CreateElement("case");
            newChild.SetAttribute("step", "1");
            newChild.InnerXml = @"<repeat>" + repeat + @"</repeat><times>" + times + @"</times><Content>" + Content + @"</Content><remark>" + remark + @"</remark><level>" + level + @"</level>";
            TargetCase.ParentNode.InsertAfter(newChild, TargetCase);
            //TargetCase.AppendChild(newChild);
        }

        /// <summary>
        /// add case with all null information
        /// </summary>
        /// <param name="TargetCase">the target cace </param>
        public void AddCase(XmlNode TargetCase)
        {
            XmlElement newChild = xml.CreateElement("case");
            newChild.SetAttribute("step", "x");
            newChild.InnerXml = @"<repeat></repeat><times></times><Content></Content><remark>Remark</remark><level></level>";
            TargetCase.ParentNode.InsertAfter(newChild, TargetCase);
        }

        public void AddProject(XmlNode TargetCase, string ProjectName, string ProjectRemark)
        {
            XmlElement newChild = xml.CreateElement("Project");
            newChild.SetAttribute("name", ProjectName);
            newChild.SetAttribute("remark", ProjectRemark);
            newChild.InnerXml = @"<case step=""x""><repeat></repeat><times></times><Content>B01001001#Sys_LCDOpen#1#3#E</Content><remark>Remark</remark><level>1</level></case>";
            TargetCase.ParentNode.InsertAfter(newChild, TargetCase);
        }

        public void ChangeCace(XmlNode TargetCase, string repeat, string times, string Content, string remark, string level)
        {
            TargetCase.ChildNodes[0].InnerText = repeat;
            TargetCase.ChildNodes[1].InnerText = times;
            TargetCase.ChildNodes[2].InnerText = Content;
            TargetCase.ChildNodes[3].InnerText = remark;
            TargetCase.ChildNodes[4].InnerText = level;
        }
        
    }

    class caseProject
    {
        public string name;
        public int myNum;
        public XmlNode myXmlNode;


        public caseProject(string tempName, XmlNode tempNode)
        {
            name = tempName;
            myXmlNode = tempNode;
            myNum = 0;
        }

        public string nextCaseContent()
        {
            return "";
        }
    }

}
