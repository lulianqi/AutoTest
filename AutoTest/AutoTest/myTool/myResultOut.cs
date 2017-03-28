using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.IO;
using CaseExecutiveActuator;
using MyCommonHelper;


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
    class myResultOut
    {

        /// <summary>
        /// i will create a html file that show the test result
        /// </summary>
        /// <param name="uri">the case file path </param>
        /// <param name="reportData">reportData souse</param>
        /// <param name="reportAddress">the report path</param>
        /// <returns>is ok</returns>
        public static bool createReport(string uri, List<MyExecutionDeviceResult> reportData, ref string reportAddress)
        {
            XmlDocument myReport = new XmlDocument();
            
            try
            {
                int tempFail, tempUnknow, tempError, tempPass, tempAll, tempNull = 0;
                tempFail = tempUnknow = tempError = tempPass = tempAll = tempNull;

                myReport.Load(System.Environment.CurrentDirectory + "\\reportModel\\reportModel.html");
                XmlNode tempTabel = myReport.ChildNodes[1].ChildNodes[0].ChildNodes[14].ChildNodes[0];

                //fill report
                foreach (MyExecutionDeviceResult tempTestData in reportData)
                {
                    XmlElement newChild = myReport.CreateElement("tr");
                    if (tempTestData.result.ToString() == "Fail")
                    {
                        tempFail++;
                        newChild.SetAttribute("bgcolor", "#FA8072");
                    }
                    else if (tempTestData.result.ToString() == "Unknow")
                    {
                        tempUnknow++;
                        newChild.SetAttribute("bgcolor", "#FFFACD");
                    }
                    else if (tempTestData.result.ToString() == "Error")
                    {
                        tempError++;
                        newChild.SetAttribute("bgcolor", "#FFA500");
                    }
                    else if (tempTestData.result.ToString() == "Pass")
                    {
                        tempPass++;
                        newChild.SetAttribute("bgcolor", "#E0FFFF");
                    }
                    else
                    {
                        tempNull++;
                        newChild.SetAttribute("bgcolor", "#E6E6FA");
                    }


                    newChild.InnerXml = @"
                                    <td width=""100px"">" + @tempTestData.caseId + @"</td>
                                    <td width=""100px"">" + @tempTestData.caseProtocol.ToString() + @"</td>
                                    <td width=""100px"">" + @tempTestData.startTime + @"</td>
                                    <td width=""100px"">" + @tempTestData.spanTime + @"</td>
                                    <td width=""100px"">" + @tempTestData.result.ToString() + @"</td>
                                    <td width=""700px"">" + @tempTestData.caseTarget.ToXmlValue() + @" -> " + @tempTestData.backContent.ToXmlValue() + @"</td>
                                    <td width=""500px"">" + @tempTestData.expectMethod.ToString() + @" -> " + @tempTestData.expectContent.ToXmlValue() + @"</td>
                                    <td width=""200px"">" + @tempTestData.staticDataResultCollection.MyToFormatString().ToXmlValue() + @"</td>
                                    <td width=""200px"">" + @tempTestData.additionalRemark.ToXmlValue() + "  " + @tempTestData.additionalEroor.ToXmlValue() + @"</td>
                                    ";
                    tempTabel.AppendChild(newChild);
                }

                tempAll = tempFail + tempUnknow + tempError + tempPass + tempNull;

                //file name
                XmlElement otherChild = myReport.CreateElement("td");
                otherChild.InnerXml = @"<td width=""100px"">" + System.Web.HttpUtility.HtmlEncode(uri) + @"</td>";
                myReport.ChildNodes[1].ChildNodes[0].ChildNodes[7].ChildNodes[2].ChildNodes[1].AppendChild(otherChild);
                
                //tempAll
                XmlElement otherChild1 = myReport.CreateElement("td");
                otherChild1.InnerXml = @"<td width=""100px"">" + tempAll.ToString() + @"</td>";
                myReport.ChildNodes[1].ChildNodes[0].ChildNodes[10].ChildNodes[0].ChildNodes[1].AppendChild(otherChild1);
                //tempPass
                XmlElement otherChild2 = myReport.CreateElement("td");
                otherChild2.InnerXml = @"<td width=""100px"">" + tempPass.ToString() + @"</td>";
                myReport.ChildNodes[1].ChildNodes[0].ChildNodes[10].ChildNodes[0].ChildNodes[2].AppendChild(otherChild2);
                //tempFail
                XmlElement otherChild3 = myReport.CreateElement("td");
                otherChild3.InnerXml = @"<td width=""100px"">" + tempFail.ToString() + @"</td>";
                myReport.ChildNodes[1].ChildNodes[0].ChildNodes[10].ChildNodes[0].ChildNodes[3].AppendChild(otherChild3);
                //tempError
                XmlElement otherChild4 = myReport.CreateElement("td");
                otherChild4.InnerXml = @"<td width=""100px"">" + tempError.ToString() + @"</td>";
                myReport.ChildNodes[1].ChildNodes[0].ChildNodes[10].ChildNodes[0].ChildNodes[4].AppendChild(otherChild4);
                //tempUnknow
                XmlElement otherChild5 = myReport.CreateElement("td");
                otherChild5.InnerXml = @"<td width=""100px"">" + tempUnknow.ToString() + @"</td>";
                myReport.ChildNodes[1].ChildNodes[0].ChildNodes[10].ChildNodes[0].ChildNodes[5].AppendChild(otherChild5);
                //tempNull
                XmlElement otherChild6 = myReport.CreateElement("td");
                otherChild6.InnerXml = @"<td width=""100px"">" + tempNull.ToString() + @"</td>";
                myReport.ChildNodes[1].ChildNodes[0].ChildNodes[10].ChildNodes[0].ChildNodes[6].AppendChild(otherChild6);

                //time
                XmlElement otherChild7 = myReport.CreateElement("td");
                otherChild7.InnerXml = @"<td width=""400px"">" + System.Web.HttpUtility.HtmlEncode(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")) + @"</td>";
                myReport.ChildNodes[1].ChildNodes[0].ChildNodes[1].ChildNodes[1].ChildNodes[0].AppendChild(otherChild7);


                //save
                string tempFilePath = System.Environment.CurrentDirectory + "\\testReport\\report.html";
                if (File.Exists(tempFilePath))
                {
                    for (int i = 0; i < 500; i++)
                    {
                        if (!File.Exists(tempFilePath + ".bak" + i))
                        {
                            Directory.Move(tempFilePath, tempFilePath + ".bak" + i);
                            break;
                        }
                    }
                }
                myReport.Save(tempFilePath);
                reportAddress = tempFilePath;
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog("ID:0053  " + ex.Message);
                reportAddress = "null";
                return false;
            }
        }
    }
}
