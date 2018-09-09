using MyCommonHelper.FileHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ShanxiHuala_Interface
{
    public class TestApiDataAnalysis
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
            try
            {
                xml.Load(tempFileName);
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog("ID:00017  " + ex.Message);
                return false;
            }
            myFile = tempFileName;
            return true;
        }
        public void mySave()
        {
            xml.Save(myFile);
        }
    }
}
