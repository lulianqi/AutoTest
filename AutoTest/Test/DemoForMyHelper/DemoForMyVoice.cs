using MyVoiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoForMyHelper
{
    class DemoForMyVoice
    {
        public void Run()
        {
            VoiceService.Beep();
            VoiceService.Speak("我爱你  i love you 1234");
        }
    }
}
