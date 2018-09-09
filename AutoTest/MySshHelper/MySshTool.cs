using MyCommonHelper;
using MyCommonHelper.FileHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Tamir.SharpSsh;

/*******************************************************************************
* Copyright (c) 2016 lulianqi 
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   201700120          创建人: lulianqi mycllq@hotmail.com
* 描    述: 创建
*******************************************************************************/

//namespace Tamir.SharpSsh
//{

//}

/// <summary>
/// 对于Tamir.SharpSsh的引用（来自于Tamir.SharpSSH.dll），由于Tamir.SharpSSH.dll自身引用了DiffieHellman.dll,Org.Mentalis.Security.dll，所以需要将DiffieHellman.dll,Org.Mentalis.Security.dll放在执行文件同目录（不一定是Tamir.SharpSSH.dll的同级目录），或者直接将DiffieHellman也引用入工程
/// </summary>
namespace MySshHelper
{

    //using MySshHelper = Tamir.SharpSsh;//可让2类/命名空间完全等价 (内部使用)
    
    public class MySshTool
    {
        /// <summary>
        /// make folder in full Path (创建完整路径的文件夹，对不存在的根进行循环创建 文件夹路径使用/分割)
        /// </summary>
        /// <param name="sshCp">ssh</param>
        /// <param name="FilePath">File Path</param>
        /// <returns>is success</returns>
        public static bool SshFileMkFullDir(SshTransferProtocolBase sshCp, string FilePath)
        {
            string[] files = FilePath.Split('/');
            List<string> nowFileList = files.ToList<string>();
            List<string> nextFileList = new List<string>();
            string outErrMes = null;
            if (files.Length < 2)
            {
                return false;
            }

            while (!SshFileMkDir(sshCp, StringHelper.StrListAdd(nowFileList, @"/"), out outErrMes))
            {
                if (outErrMes.Contains("No such file or directory"))
                {
                    nextFileList.Add(nowFileList[nowFileList.Count - 1]);
                    nowFileList.RemoveAt(nowFileList.Count - 1);
                }
                else
                {
                    return false;
                }
            }
            for (int i = nextFileList.Count - 1; i >= 0; i--)
            {
                nowFileList.Add(nextFileList[i]);
                if (!SshFileMkDir(sshCp, StringHelper.StrListAdd(nowFileList, @"/"), out outErrMes))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// make folder
        /// </summary>
        /// <param name="sshCp">ssh</param>
        /// <param name="FilePath">File Path</param>
        /// <param name="errMes"></param>
        /// <returns>is success</returns>
        public static bool SshFileMkDir(SshTransferProtocolBase sshCp, string FilePath, out string errMes)
        {
            errMes = null;
            try
            {
                sshCp.Mkdir(FilePath);
                return true;
            }
            catch (Exception ex)
            {
                errMes = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// move all file to  remote Asyn （使用异步的方式将指定文件夹内的所有文件包括之目录的所有文件在保留目录结果的提前向下复制到shh服务器的指定目录）
        /// </summary>
        /// <param name="sshCp">ssh</param>
        /// <param name="LocalFilePath">Local File Path</param>
        /// <param name="remoteFilePath">remote File Path</param>
        /// <param name="reportProcess">report Process </param>
        /// <param name="reportError">report Error</param>
        /// <returns>is creat task success</returns>
        public static bool SshMvAllFileAsyn(SshTransferProtocolBase sshCp, string LocalFilePath, string remoteFilePath, Action<string> reportProcess, Action<string> reportError)
        {
            string outError = null;
            if (!sshCp.Connected)
            {
                return false;
            }
            ParameterizedThreadStart fileMvTask = new ParameterizedThreadStart((args) => { SshMvAllFileSync((SshTransferProtocolBase)((object[])args)[0], (string)((object[])args)[1], (string)((object[])args)[2], out outError, (Action<string>)((object[])args)[3], (Action<string>)((object[])args)[4]); });
            Thread mySshFileMvThread = new Thread(fileMvTask);
            mySshFileMvThread.Name = "SshMvAllFileAsynWithSshTransferProtocolBase";
            mySshFileMvThread.Priority = ThreadPriority.Normal;
            mySshFileMvThread.IsBackground = true;
            mySshFileMvThread.Start(new object[] { sshCp, LocalFilePath, remoteFilePath, reportProcess, reportError });
            return true;
        }

        public static bool SshMvAllFileAsyn(string host, string user, string password, string LocalFilePath, string remoteFilePath, Action<string> reportProcess, Action<string> reportError)
        {
            string outError = null;
            ParameterizedThreadStart fileMvTask = new ParameterizedThreadStart((args) => { SshMvAllFileSync((string)((object[])args)[0], (string)((object[])args)[1], (string)((object[])args)[2], (string)((object[])args)[3], (string)((object[])args)[4], out outError, (Action<string>)((object[])args)[5], (Action<string>)((object[])args)[6]); });
            Thread mySshFileMvThread = new Thread(fileMvTask);
            mySshFileMvThread.Name = "SshMvAllFileAsynWithUserName";
            mySshFileMvThread.Priority = ThreadPriority.Normal;
            mySshFileMvThread.IsBackground = true;
            mySshFileMvThread.Start(new object[] { host, user, password, LocalFilePath, remoteFilePath, reportProcess, reportError });
            return true;
        }

        /// <summary>
        /// move all file to remote Sync （使用同步的方式将指定文件夹内的所有文件包括之目录的所有文件在保留目录结果的提前向下复制到shh服务器的指定目录）
        /// </summary>
        /// <param name="sshCp"></param>
        /// <param name="LocalFilePath">Local File Path</param>
        /// <param name="remoteFilePath">remote File Path</param>
        /// <param name="errMes"></param>
        /// <param name="reportProcess">report Process </param>
        /// <param name="reportError">report Error</param>
        /// <returns></returns>
        public static bool SshMvAllFileSync(SshTransferProtocolBase sshCp, string LocalFilePath, string remoteFilePath, out string errMes, Action<string> reportProcess, Action<string> reportError)
        {
            errMes = null;
            bool outResult = true;
            var PutOutReport = new Action<string>((str) => { if (reportProcess != null) { reportProcess(str); } });
            var PutOutError = new Action<string>((str) => { if (reportProcess != null) { reportError(str); } });
            FileTransferEvent fileTransferStart = new FileTransferEvent((src, dst, transferredBytes, totalBytes, message) => PutOutReport(string.Format("Put file {0} to {1}   state：{2}", src, dst, message)));
            FileTransferEvent fileTransferEnd = new FileTransferEvent((src, dst, transferredBytes, totalBytes, message) => PutOutReport(string.Format("Put file {0} to {1}   state：{2}", src, dst, message)));

            if(!sshCp.Connected)
            {
                errMes = "SshTransferProtocolBase not Connected";
                PutOutError(errMes);
                return false;
            }
            FileInfo[] distFIles = FileService.GetAllFiles(LocalFilePath);
            if (distFIles == null)
            {
                errMes = "no file in LocalFilePath";
                PutOutError(errMes);
                return false;
            }

            sshCp.OnTransferStart += fileTransferStart;
            sshCp.OnTransferEnd += fileTransferEnd;

            PutOutReport("start Mv");
            foreach (FileInfo tempFileInfo in distFIles)
            {
                string tempNowPath = remoteFilePath + tempFileInfo.DirectoryName.MyTrimStr(LocalFilePath, null).Replace(@"\", @"/") + @"/" + tempFileInfo.Name;
                try
                {
                    sshCp.Put(tempFileInfo.DirectoryName + @"\" + tempFileInfo.Name, tempNowPath);
                }
                catch (Exception ex)
                {
                    PutOutReport(ex.Message);
                    if (ex.Message.Contains("No such file or directory"))
                    {
                        string tempPath = ex.Message;
                        tempPath = tempPath.Remove(0, tempPath.IndexOf(@"/"));
                        tempPath = tempPath.Remove(tempPath.LastIndexOf(@"/"));
                        PutOutReport("Create folder" + tempPath);
                        if (MySshTool.SshFileMkFullDir(sshCp, tempPath))
                        {
                            try
                            {
                                sshCp.Put(tempFileInfo.DirectoryName + @"\" + tempFileInfo.Name, tempNowPath);
                            }
                            catch (Exception innerEx)
                            {
                                PutOutError(innerEx.Message);
                                PutOutError(string.Format("transfer fail ，skip this file [from {0} to {1}]",tempFileInfo.DirectoryName + @"\" + tempFileInfo.Name,tempNowPath));
                                outResult = false;
                            }
                        }
                        else
                        {
                            PutOutError(string.Format("create folder Failed，skip this folder [{0}]", tempPath));
                            outResult = false;
                        }
                    }
                }
            }
            PutOutReport("Mv Complete");
            sshCp.OnTransferStart -= fileTransferStart;
            sshCp.OnTransferEnd -= fileTransferEnd;
            return outResult;
        }

        public static bool SshMvAllFileSync(string host, string user, string password, string LocalFilePath, string remoteFilePath, out string errMes, Action<string> reportProcess,Action<string> reportError)
        {
            SshTransferProtocolBase sshCp = new Scp(host, user, password);
            sshCp.Connect();
            bool outResult = SshMvAllFileSync(sshCp, LocalFilePath, remoteFilePath, out errMes, reportProcess, reportError);
            sshCp.Close();
            return outResult;
        }
    }
}
