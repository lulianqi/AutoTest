using SpeechLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MyVoiceHelper
{
    /// <summary>
    /// 语音服务
    /// </summary>
    public class VoiceService
    {
        private static SpVoiceClass voic = new SpVoiceClass();

        private static void SpVoiceInitialization()
        {
            voic.Voice = voic.GetVoices(null, null).Item(0);
            voic.Volume = 100;
        }

        /// <summary>
        /// Speak your data
        /// </summary>
        /// <param name="yourData">your Data to Speak</param>
        public static void Speak(string yourData)
        {
            try
            {
                voic.Speak(yourData, SpeechVoiceSpeakFlags.SVSFDefault);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <param name="iFrequency">声音频率（从37Hz到32767Hz）。在windows95中忽略</param>   
        /// <param name="iDuration">声音的持续时间，以毫秒为单位。</param>   
        [DllImport("Kernel32.dll")] //引入命名空间 using System.Runtime.InteropServices;   
        public static extern bool Beep(int frequency, int duration);

        /// <summary>
        /// Warning tone
        /// </summary>
        /// <returns>is ok</returns>
        public static bool Beep()
        {
            return Beep(1600, 800);
        }
    }  
}
