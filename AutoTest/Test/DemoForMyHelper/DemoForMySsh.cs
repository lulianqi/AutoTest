using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySshHelper;
using Tamir.SharpSsh;
using System.IO;
using MyCommonHelper;


namespace DemoForMyHelper
{
    /// <summary>
    /// 对于Tamir.SharpSsh的引用（来自于Tamir.SharpSSH.dll），由于Tamir.SharpSSH.dll自身引用了DiffieHellman.dll,Org.Mentalis.Security.dll，所以需要将DiffieHellman.dll,Org.Mentalis.Security.dll放在执行文件同目录（不一定是Tamir.SharpSSH.dll的同级目录），或者直接将DiffieHellman也引用入工程
    /// </summary>
    class DemoForMySsh
    {
        SshTransferProtocolBase sshCp = new Scp("192.168.200.153", "root", "B2CCentOs7!");
        SshShell shell = new SshShell("192.168.200.153", "root", "B2CCentOs7!");
        public DemoForMySsh ()
        {
            sshCp.OnTransferStart += new FileTransferEvent((src, dst, transferredBytes, totalBytes, message) => { Console.WriteLine(string.Format("Put file {0} to {1}   state：{2}", src, dst, message)); });
            sshCp.OnTransferEnd += new FileTransferEvent((src, dst, transferredBytes, totalBytes, message) => { Console.WriteLine(string.Format("Put file {0} to {1}   state：{2}", src, dst, message)); });
        }

        public void RunTest()
        {
            Console.WriteLine("文件传输");
            sshCp.Connect();
            string localPath = @"F:\file\Code\SharpSSH-1.1.1.13.src";
            string serverPath=@"/usr/lijie";
            FileInfo[] distFIles = FileService.GetAllFiles(localPath);
            if (distFIles == null)
            {
                Console.WriteLine("没有发现更新文件");
                //exec.Close();
                sshCp.Close();
            }
            Console.WriteLine("开始更新");
            foreach (FileInfo tempFileInfo in distFIles)
            {
                string tempNowPath = serverPath + tempFileInfo.DirectoryName.MyTrimStr(localPath, null).Replace(@"\", @"/") + @"/" + tempFileInfo.Name;
                try
                {
                    sshCp.Put(tempFileInfo.DirectoryName + @"\" + tempFileInfo.Name, tempNowPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //scp: /tmp/dist/123: No such file or directory
                    if (ex.Message.Contains("No such file or directory"))
                    {
                        string tempPath = ex.Message;
                        tempPath = tempPath.Remove(0, tempPath.IndexOf(@"/"));
                        tempPath = tempPath.Remove(tempPath.LastIndexOf(@"/"));
                        Console.WriteLine("创建文件夹" + tempPath);
                        if (MySsh.SshFileMkFullDir(sshCp, tempPath))
                        {
                            try
                            {
                                sshCp.Put(tempFileInfo.DirectoryName + @"\" + tempFileInfo.Name, tempNowPath);
                            }
                            catch (Exception innerEx)
                            {
                                Console.WriteLine(innerEx.Message);
                                Console.WriteLine("传输失败，跳过该文件");
                            }
                        }
                        else
                        {
                            Console.WriteLine("创建文件夹失败，跳过该文件");
                        }
                    }
                }
            }
            Console.WriteLine("更新完成");
            sshCp.Close();
        }

        public void RunFileMv()
        {
            if (MySsh.SshMvAllFileAsyn("192.168.200.153", "root", "B2CCentOs7!", @"F:\file\Code\SharpSSH-1.1.1.13.src", @"/usr/lijie", new Action<string>((str) => Console.WriteLine(str)), new Action<string>((str) => Console.WriteLine("********************************"+str))))
            {
                Console.WriteLine("______________________________________");
                Console.WriteLine("SshMvAllFileAsyn TRUE");
                Console.WriteLine("______________________________________");
            }
        }

        public void RunShellTest()
        {
            shell.Connect();
            shell.ExpectPattern = "#";
            Console.WriteLine(shell.Expect());
            shell.WriteLine(@"ls");
            Console.WriteLine(shell.Expect());
            shell.WriteLine(@"ll");
            Console.WriteLine(shell.Expect());
            shell.WriteLine(@"cd /usr/local");
            Console.WriteLine(shell.Expect());
            shell.WriteLine(@"ll");
            Console.WriteLine(shell.Expect());
            shell.Close();
        }
    }
}
