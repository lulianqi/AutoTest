using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCommonHelper.EncryptionHelper;
using MyCommonHelper;

namespace DemoForMyHelper
{
    class DemoForMyCommonHelper
    {
        public static void Dotest()
        {
            Console.WriteLine(MyBytes.StringToHexString("test data for test !@#$%^&*()ZXCVBNM<QWERTYUIOASDFGHJK",Encoding.UTF8 ,HexaDecimal.hex16, ShowHexMode.space));
            Console.WriteLine("RC4 Encrypt");
            byte[] data1 = MyRC4.Encrypt("test data for test !@#$%^&*()ZXCVBNM<QWERTYUIOASDFGHJK", "123", Encoding.UTF8);
            Console.WriteLine(MyBytes.ByteToHexString(data1, HexaDecimal.hex16, ShowHexMode.space));
            data1 = MyRC4.Encrypt("test data for test !@#$%^&*()ZXCVBNM<QWERTYUIOASDFGHJK", "123", Encoding.UTF8);
            Console.WriteLine(MyBytes.ByteToHexString(data1, HexaDecimal.hex16, ShowHexMode.space));
            Console.WriteLine(Convert.ToBase64String(data1));
            Console.WriteLine("RC4 Decrypt");
            byte[] data2 = MyRC4.Decrypt(data1, "123", Encoding.UTF8);
            Console.WriteLine(MyBytes.ByteToHexString(data2, HexaDecimal.hex16, ShowHexMode.space));
            Console.WriteLine(Encoding.UTF8.GetString(data2));
        }
    }
}
