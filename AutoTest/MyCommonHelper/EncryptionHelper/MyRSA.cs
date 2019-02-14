using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MyCommonHelper.EncryptionHelper
{
    public class MyRSA
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlainText"></param>
        /// <param name="modulus">n (p*q) [Big Endian]</param>
        /// <param name="publicExponent">e，公共指数 [Big Endian]</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] PlainText, byte[] modulus,byte[] publicExponent)
        {
            if(PlainText==null || modulus==null || publicExponent==null)
            {
                return null;
            }
            if(modulus.Length>0 && modulus[0]==0)
            {
                byte[] newModulus=new byte[modulus.Length-1];
                Array.Copy(modulus, 1, newModulus, 0, modulus.Length - 1);
                modulus = newModulus;
            }
            if (publicExponent.Length > 0 && publicExponent[0] == 0)
            {
                byte[] newPublicExponent = new byte[publicExponent.Length - 1];
                Array.Copy(publicExponent, 1, newPublicExponent, 0, publicExponent.Length - 1);
                publicExponent = newPublicExponent;
            }
            RSAParameters tempRSAKeyInfo = new RSAParameters();
            tempRSAKeyInfo.Modulus = modulus;
            tempRSAKeyInfo.Exponent = publicExponent;
            return RSAEncrypt(PlainText, tempRSAKeyInfo, false);
        }

        public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {

                //Create a new instance of RSACryptoServiceProvider.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                //Import the RSA Key information. This only needs
                //toinclude the public key information.
                RSA.ImportParameters(RSAKeyInfo);

                //Encrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                return RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (Exception)
            {
                return null;
            }

        }
    }
}
