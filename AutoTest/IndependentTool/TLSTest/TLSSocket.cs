#define TESTMODE
#define THREADPOOLMODE

using MyCommonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace TLSTest
{

    //CipherSuite TLS_NULL_WITH_NULL_NULL = { 0x00,0x00 };
    //CipherSuite TLS_RSA_WITH_NULL_MD5 = { 0x00,0x01 };
    //CipherSuite TLS_RSA_WITH_NULL_SHA = { 0x00,0x02 };
    //CipherSuite TLS_RSA_WITH_NULL_SHA256 = { 0x00,0x3B };
    //CipherSuite TLS_RSA_WITH_RC4_128_MD5 = { 0x00,0x04 };
    //CipherSuite TLS_RSA_WITH_RC4_128_SHA = { 0x00,0x05 };
    //CipherSuite TLS_RSA_WITH_3DES_EDE_CBC_SHA = { 0x00,0x0A };
    //CipherSuite TLS_RSA_WITH_AES_128_CBC_SHA = { 0x00,0x2F };
    //CipherSuite TLS_RSA_WITH_AES_256_CBC_SHA = { 0x00,0x35 };
    //CipherSuite TLS_RSA_WITH_AES_128_CBC_SHA256 = { 0x00,0x3C };
    //CipherSuite TLS_RSA_WITH_AES_256_CBC_SHA256 = { 0x00,0x3D };
    //CipherSuite TLS_DH_DSS_WITH_3DES_EDE_CBC_SHA = { 0x00,0x0D };
    //CipherSuite TLS_DH_RSA_WITH_3DES_EDE_CBC_SHA = { 0x00,0x10 };
    //CipherSuite TLS_DHE_DSS_WITH_3DES_EDE_CBC_SHA = { 0x00,0x13 };
    //CipherSuite TLS_DHE_RSA_WITH_3DES_EDE_CBC_SHA = { 0x00,0x16 };
    //CipherSuite TLS_DH_DSS_WITH_AES_128_CBC_SHA = { 0x00,0x30 };
    //CipherSuite TLS_DH_RSA_WITH_AES_128_CBC_SHA = { 0x00,0x31 };
    //CipherSuite TLS_DHE_DSS_WITH_AES_128_CBC_SHA = { 0x00,0x32 };
    //CipherSuite TLS_DHE_RSA_WITH_AES_128_CBC_SHA = { 0x00,0x33 };
    //CipherSuite TLS_DH_DSS_WITH_AES_256_CBC_SHA = { 0x00,0x36 };
    //CipherSuite TLS_DH_RSA_WITH_AES_256_CBC_SHA = { 0x00,0x37 };
    //CipherSuite TLS_DHE_DSS_WITH_AES_256_CBC_SHA = { 0x00,0x38 };
    //CipherSuite TLS_DHE_RSA_WITH_AES_256_CBC_SHA = { 0x00,0x39 };
    //CipherSuite TLS_DH_DSS_WITH_AES_128_CBC_SHA256 = { 0x00,0x3E };
    //CipherSuite TLS_DH_RSA_WITH_AES_128_CBC_SHA256 = { 0x00,0x3F };
    //CipherSuite TLS_DHE_DSS_WITH_AES_128_CBC_SHA256 = { 0x00,0x40 };
    //CipherSuite TLS_DHE_RSA_WITH_AES_128_CBC_SHA256 = { 0x00,0x67 };
    //CipherSuite TLS_DH_DSS_WITH_AES_256_CBC_SHA256 = { 0x00,0x68 };
    //CipherSuite TLS_DH_RSA_WITH_AES_256_CBC_SHA256 = { 0x00,0x69 };
    //CipherSuite TLS_DHE_DSS_WITH_AES_256_CBC_SHA256 = { 0x00,0x6A };
    //CipherSuite TLS_DHE_RSA_WITH_AES_256_CBC_SHA256 = { 0x00,0x6B };
    //CipherSuite TLS_DH_anon_WITH_RC4_128_MD5 = { 0x00,0x18 };
    //CipherSuite TLS_DH_anon_WITH_3DES_EDE_CBC_SHA = { 0x00,0x1B };
    //CipherSuite TLS_DH_anon_WITH_AES_128_CBC_SHA = { 0x00,0x34 };
    //CipherSuite TLS_DH_anon_WITH_AES_256_CBC_SHA = { 0x00,0x3A };
    //CipherSuite TLS_DH_anon_WITH_AES_128_CBC_SHA256 = { 0x00,0x6C };
    //CipherSuite TLS_DH_anon_WITH_AES_256_CBC_SHA256 = { 0x00,0x6D };

    public interface IRecordProtocol
    {
        byte[] GetProtocolRawData();
    }

    public class TLSSocket 
    {
        
    }

    public class TLSPacket
    {

        public enum AlertLevel
        {
            warning=1,
            fatal=2
        }

        public enum AlertDescription
        {
            close_notify=0,
            unexpected_message=10,
            bad_record_mac=20,
            decryption_failed_RESERVED=21,
            record_overflow=22,
            decompression_failure=30,
            handshake_failure=40,
            no_certificate_RESERVED=41,
            bad_certificate=42,
            unsupported_certificate=43,
            certificate_revoked=44,
            certificate_expired=45,
            certificate_unknown=46,
            illegal_parameter=47,
            unknown_ca=48,
            access_denied=49,
            decode_error=50,
            decrypt_error=51,
            export_restriction_RESERVED=60,
            protocol_version=70,
            insufficient_security=71,
            internal_error=80,
            user_canceled=90,
            no_renegotiation=100,
            unsupported_extension=110
        }


        public enum HandshakeType
        {
            hello_request=0, 
            client_hello=1, 
            server_hello=2,
            certificate=11, 
            server_key_exchange=12,
            certificate_request=13, 
            server_hello_done=14,
            certificate_verify=15, 
            client_key_exchange=16,
            finished=20
        }


        /// <summary>
        /// 核心子协议
        /// </summary>
        public enum TLSContentType
        {
            ChangeCipherSpec=20,     //密钥规格交换协议 0x14
            Alert=21,                //报警协议 0x15
            Handshake=22,            //握手协议 0x16
            ApplicationData = 23     //应用数据需要 0x17
        }

        /// <summary>
        /// 警告协议
        /// </summary>
        public struct Alert
        {
            AlertLevel level;
            AlertDescription description;
        }

        public class Random
        {
            public byte[] RandomData { get; set; }

            public static byte[] CreatRandom()
            {
                byte[] GMTUnixTime = BitConverter.GetBytes(GetTimeStamp());
                Array.Reverse(GMTUnixTime);
                byte[] RandomBytes = MyCommonHelper.MyBytes.CreatRandomBytes(28);
                return MyCommonHelper.MyBytes.GroupByteList(new List<byte[]>() { GMTUnixTime, RandomBytes });
            }

            /// <summary>  
            /// 获取时间戳  
            /// </summary>  
            /// <returns></returns>  
            public static Int32 GetTimeStamp()
            {
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return Convert.ToInt32(ts.TotalSeconds);
            }
        }

        public class SessionID
        {
            byte sessionIDLengh;
            byte[] sessionID;

            public byte[] SessionIDData { get; set; }
            public static byte[] CreatSessionID(int SessionIDLengh)
            {
                if (SessionIDLengh==0)
                {
                    return new byte[] { 0x00 };
                }
                else if(SessionIDLengh<255)
                {
                    byte[] tempSession = MyCommonHelper.MyBytes.CreatRandomBytes(SessionIDLengh+1);
                    tempSession[0] = BitConverter.GetBytes(SessionIDLengh)[0];
                    return tempSession;
                }
                else
                {
                    return new byte[] { 0x00 };
                }
            }
        }

        public class CipherSuites
        {
            public byte[] CipherSuitesData { get; set; }

            public static byte[] CreatCipherSuites(List<byte[]> cipherSuiteList)
            {
                if (cipherSuiteList==null)
                {
                    return new byte[]{0x00,0x00};
                }
                byte[] tempCipherSuites = new byte[cipherSuiteList.Count*2+2];
                tempCipherSuites[1] = BitConverter.GetBytes(cipherSuiteList.Count*2)[0];
                tempCipherSuites[0] = BitConverter.GetBytes(cipherSuiteList.Count*2)[1];
                int tempIndex = 2;
                foreach(var cipherSuite in cipherSuiteList)
                {
                    if(cipherSuite.Length==2)
                    {
                        tempCipherSuites[tempIndex] = cipherSuite[0];
                        tempCipherSuites[tempIndex+1] = cipherSuite[0+1];
                        tempIndex = tempIndex + 2;
                    }
                    else
                    {
                        return null;
                    }
                }
                return tempCipherSuites;
            }

            public static byte[] CreatCipherSuites()
            {
                return new byte[] { 0x00, 0x1c, 0x5a, 0x5a, 0xc0, 0x2b, 0xc0, 0x2f, 0xc0, 0x2c, 0xc0, 0x30, 0xcc, 0xa9, 0xcc, 0xa8, 0xc0, 0x13, 0xc0, 0x14, 0x00, 0x9c, 0x00, 0x9d, 0x00, 0x2f, 0x00, 0x35, 0x00, 0x0a };
            }
        }

        public class CompressionMethod
        {
            public byte[] CompressionMethodData { get; set; }
            public static byte[] CreatCompressionMethod()
            {
                return new byte[] { 0x01, 0x00 };
            }
        }

        public class Extensions
        {
            List<Extension> extensionList;

            public byte[] ExtensionsData { get; set; }

            public static byte[] CreatExtensions(List<Extension> yourExtensionList)
            {
                if (yourExtensionList == null)
                {
                    return new byte[] { 0x00, 0x00 }; 
                }
                else
                {
                    int extentionLengh = 0 ;
                    foreach(var tempExtention in yourExtensionList)
                    {
                        extentionLengh += tempExtention.ExtensionData.Length + 4;
                    }
                    byte[] tempRawExtentions = new byte[extentionLengh+2];
                    tempRawExtentions[1] = BitConverter.GetBytes(extentionLengh)[0];
                    tempRawExtentions[0] = BitConverter.GetBytes(extentionLengh)[1];
                    int nowCopyIndex = 2;
                    foreach (var tempExtention in yourExtensionList)
                    {
                        Array.Copy(tempExtention.ExtensionType, 0, tempRawExtentions, nowCopyIndex, 2);
                        nowCopyIndex = nowCopyIndex + 2;
                        tempRawExtentions[nowCopyIndex+1] = BitConverter.GetBytes(tempExtention.ExtensionData.Length)[0];
                        tempRawExtentions[nowCopyIndex] = BitConverter.GetBytes(tempExtention.ExtensionData.Length)[1];
                        nowCopyIndex = nowCopyIndex + 2;
                        Array.Copy(tempExtention.ExtensionData, 0, tempRawExtentions, nowCopyIndex, tempExtention.ExtensionData.Length);
                        nowCopyIndex = tempExtention.ExtensionData.Length;
                    }
                    return tempRawExtentions;
                }
            }

            public static byte[] CreatExtensions(string yourHostName)
            {
                #region server_name
                //host name
                byte sererNameType = 0x00;
                byte[] hostName = Encoding.UTF8.GetBytes(yourHostName);

                int serverNameListLen = 5 + hostName.Length;
                byte[] serverName = new byte[serverNameListLen];
                serverName[1] = BitConverter.GetBytes(serverNameListLen-2)[0];
                serverName[0] = BitConverter.GetBytes(serverNameListLen-2)[1];
                serverName[2] = sererNameType;
                serverName[4] = BitConverter.GetBytes(hostName.Length)[0];
                serverName[3] = BitConverter.GetBytes(hostName.Length)[1];
                Array.Copy(hostName, 0, serverName, 5, hostName.Length);
                #endregion

                return CreatExtensions(new List<Extension>() { new Extension(new byte[] { 0x00, 0x00 }, serverName) });
            }
        }

        public class Extension
        {
            byte[] extensionType;
            byte[] extensionData;

            public Extension(byte[] yourExtentionType,byte[] yourExtentionData)
            {
                ExtensionType = yourExtentionType;
                ExtensionData = yourExtentionData;
            }
            public byte[] ExtensionType
            {
                get { return extensionType; }
                set { if (value.Length == 2) { extensionType = value; } else { extensionType = new byte[]{ 0x00, 0x00 }; } }
            }

            public byte[] ExtensionData
            {
                get { return extensionData; }
                set { extensionData = value; }
            }
        }

        /// <summary>
        /// 版本     (0303 is LTS1.2  0302 is LTS1.2 0301 is LTS1.1  0300 is SSLv3)
        /// </summary>
        public struct ProtocolVersion
        {
            public byte Major;
            public byte Minor;
            public ProtocolVersion(byte major,byte minor)
            {
                Major = major;
                Minor = minor;
            }
        }

        /// <summary>
        /// 记录协议表头
        /// </summary>
        public class TLSPlaintext
        {
            TLSContentType contentType;
            ProtocolVersion version;
            //UInt16 length;      //此 API 不兼容 CLS。 兼容 CLS 的替代 API 为 Int32。
            int length;      //此 API 不兼容 CLS。 兼容 CLS 的替代 API 为 Int32。
            byte[] rawData;
            
            public TLSPlaintext(TLSContentType yourType, ProtocolVersion yourVersion)
            {
                contentType = yourType;
                version = yourVersion;
                rawData = new byte[5] { (byte)yourType, version.Major, version.Minor, 0x00, 0x00, };

            }

            public byte[] GetRawData(int yourLength)
            {
                length = yourLength;
                byte[] myTempLen = BitConverter.GetBytes(length);
                Array.Reverse(myTempLen);
                Array.Copy(myTempLen, 2, rawData, 3, 2);
                return rawData;
            }

            public byte[] CreateRawData(byte[] yourChildProtocolData)
            {
                length = yourChildProtocolData.Length;
                byte[] myTempLen = BitConverter.GetBytes(length);
                Array.Reverse(myTempLen);
                Array.Copy(myTempLen, 2, rawData, 3, 2);
                return MyCommonHelper.MyBytes.GroupByteList(new List<byte[]> { rawData, yourChildProtocolData });
            }

        }
    
        /// <summary>
        /// 握手协议抽象类
        /// </summary>
        public abstract class Handshake
        {
            public HandshakeType handshakeType;
            public int length;

             //select (HandshakeType) {
             //case hello_request: HelloRequest;
             //case client_hello: ClientHello;
             //case server_hello: ServerHello;
             //case certificate: Certificate;
             //case server_key_exchange: ServerKeyExchange;
             //case certificate_request: CertificateRequest;
             //case server_hello_done: ServerHelloDone;
             //case certificate_verify: CertificateVerify;
             //case client_key_exchange: ClientKeyExchange;
             //case finished: Finished;
             //} body;

            //IRecordProtocol body;
        }


        public class ClientHello : Handshake ,IRecordProtocol
        {
            ProtocolVersion client_version;
            Random random;
            SessionID session_id;
            CipherSuites cipherSuites;
            CompressionMethod compressionMethod;
            Extensions extensions;

            string hostName;

            public ClientHello(string yourHost)
            {
                hostName = yourHost;
                base.handshakeType = HandshakeType.client_hello;
            }

            public byte[] GetProtocolRawData()
            {
                byte[] nowVersion = new byte[] { 0x03, 0x03 };
                byte[] nowRandom = Random.CreatRandom();
                byte[] nowSessionID = SessionID.CreatSessionID(28);
                byte[] nowCipherSuites = CipherSuites.CreatCipherSuites();
                byte[] nowCompressionMethod = CompressionMethod.CreatCompressionMethod();
                byte[] nowExtention = Extensions.CreatExtensions(hostName);
                //nowExtention = MyBytes.HexStringToByte("014c0000001c001a00001764617461756e696f6e2e62616977616e6469616e2e636e000500050100000000000a00080006001d00170018000b00020100000d00140012060106030401050102010403050302030202002300d0826acd16d68307143347c574ad11aabec8fb5237460655dfcbb28505c7941a0b80ee75db6b339ab2033e991c5cdb8a39748f313c0dfb08467646007170577fd9f395ff621fbf59cb1aea78081121e0f626d625aab620a760a44b18a14e6eb91dcea5356fdbf2d2f9afa0984ed00e356761ac5693b127b7e0824380eb1968d1a0c6a85ce8d4f532c895e0456a9d56820e57abdd86925d629ef86b75e5f9cea098750d789f5e47e7ec5dcd8bafca47b7709b47fd7ca1e0f258f9d16394923def423aa57cd72149edd10406d1cecc2b31ce0010000e000c02683208687474702f312e310017000000180006000a03020100ff01000100",
                //HexaDecimal.hex16, ShowHexMode.@null);

                byte[] nowClientHello = MyCommonHelper.MyBytes.GroupByteList(new List<byte[]> { nowVersion, nowRandom, nowSessionID, nowCipherSuites, nowCompressionMethod, nowExtention });
                base.length = nowClientHello.Length;

                byte[] nowRawData = new byte[nowClientHello.Length + 4];
                nowRawData[0] = (byte)base.handshakeType;
                nowRawData[3] = BitConverter.GetBytes(base.length)[0];
                nowRawData[2] = BitConverter.GetBytes(base.length)[1];
                nowRawData[1] = BitConverter.GetBytes(base.length)[2];
                Array.Copy(nowClientHello, 0, nowRawData, 4, base.length);

                return nowRawData;
            }
        }
    
    }
}
