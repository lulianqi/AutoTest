using MyCommonTool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Tamir.SharpSsh;



//namespace Tamir.SharpSsh
//{

//}

namespace MySshHelper
{

    //using MySshHelper = Tamir.SharpSsh;//可让2类/命名空间完全等价 (内部使用)
    
    public class MySsh
    {
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
                string tempNowPath = remoteFilePath + tempFileInfo.DirectoryName.myTrimStr(LocalFilePath, null).Replace(@"\", @"/") + @"/" + tempFileInfo.Name;
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
                        if (MySsh.SshFileMkFullDir(sshCp, tempPath))
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
