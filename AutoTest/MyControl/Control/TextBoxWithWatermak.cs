﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MyCommonControl.Control
{
    public class TextBoxWithWatermak : System.Windows.Forms.TextBox
    {
        private string watermarkText;
        private const uint ECM_FIRST = 0x1500;
        private const uint EM_SETCUEBANNER = ECM_FIRST + 1;


        [Category("扩展属性"), Description("显示的水印提示信息")]
        public string WatermarkText
        {
            get { return watermarkText; }
            set
            {
                watermarkText = value;
                SetWatermark(watermarkText);
            }
        }

        private void SetWatermark(string watermarkText)
        {
            UnsafeNativeMethods.SendMessage(this.Handle, EM_SETCUEBANNER, 0, watermarkText);
        }

    }
}
